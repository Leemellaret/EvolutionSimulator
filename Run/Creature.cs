namespace EvolutionSimulator.Run
{
    class Creature
    {
        public string Id { get; }
        public int CountOfReproductions { get; private set; }
        public bool IsAlive { get; private set; }
        public Brain Brain { get; }
        private double energy;
        private double health;
        public double Alpha { get; }
        public double HitForce { get; private set; }
        public double AbsorbAble { get; private set; }
        public double MaxHealth { get; }
        public double RegenerationValue { get; }

        public Creature(
            string id,
            Brain brain,
            double alpha,
            double initHealth = -1,
            double initEnergy = -1)
        {
            Id = id;
            CountOfReproductions = 0;
            IsAlive = true;
            this.Brain = brain;
            Alpha = alpha;
            HitForce = CreatureParameters.HitForce(alpha);
            AbsorbAble = CreatureParameters.AbsorbAble(alpha);
            MaxHealth = CreatureParameters.MaxHealth(alpha);
            health = (initHealth == -1 ? CreatureParameters.MaxHealth(alpha) : initHealth);
            Energy = (initEnergy == -1 ? CreatureParameters.InitEnergy(alpha) : initEnergy);
            RegenerationValue = CreatureParameters.RegenerationValue(alpha);
        }

        public double Health
        {
            get => health;
            set
            {
                if (value > MaxHealth)
                {
                    health = MaxHealth;
                }
                else if (0 <= value && value <= MaxHealth)
                {
                    health = value;
                }
                else
                {
                    health = -1;
                    IsAlive = false;
                    World.Deaths[0]++;
                }
            }
        }

        public double Energy
        {
            get => energy;
            set
            {
                if (value > 0)
                {
                    energy = value;
                }
                else
                {
                    energy = 0;
                    IsAlive = false;
                    World.Deaths[1]++;
                }
            }
        }

        public double EnergyLostPerStep
        {
            get => (HitForce * CreatureParameters.OneHitForceCost) + 
                   (MaxHealth * CreatureParameters.OneMaxShieldCost) + 
                   (AbsorbAble * CreatureParameters.OneAbsorbAbleCost) + 
                   (RegenerationValue * CreatureParameters.OneRegenerationValueCost);
        }

        public double EnergyLostInLastStep { get; private set; }

        public Interaction Interact(WorldData worldData)
        {
            Energy -= EnergyLostPerStep;
            EnergyLostInLastStep = EnergyLostPerStep;
            if (!IsAlive)
                return null;

            Interaction interaction;


            BrainCommand command = Brain.Process(new BrainInput(Health, Energy, worldData.Cells)) as BrainCommand;
            // [0] - что сделать. 0-шаг, 1-съесть, 2-ударить
            // [1] - в каком направлении [0, 7]
            // В Data записывается текущее здоровье, текущая энергия и что находится под телом и по сторонам от него

            double energyLostForAction = 0;
            if (command.Action == CreatureAction.Go)
            {
                //orientation = w.Move(this, (int)commands[1]);
                interaction = new Interaction(CreatureAction.Go, command.Direction, 1);
                //Energy -= CreatureParameters.MoveCostPerStep;
                energyLostForAction += CreatureParameters.MoveCostPerStep;
            }
            else if (command.Action == CreatureAction.Eat)
            {
                //Energy += w.AbsorbFood(this, (int)commands[1], AbsorbAble);
                interaction = new Interaction(CreatureAction.Eat, command.Direction, AbsorbAble);
                //Energy -= CreatureParameters.FoodAbsorbCostPerUnit * AbsorbAble;
                energyLostForAction += CreatureParameters.FoodAbsorbCostPerUnit * AbsorbAble;
            }
            else if (command.Action == CreatureAction.Hit)
            {
                //w.DoHit(orientation as Orientation2DCell, (int)commands[1], HitForce);
                interaction = new Interaction(CreatureAction.Hit, command.Direction, HitForce);
                //Energy -= CreatureParameters.HitCostPerUnit * HitForce;
                energyLostForAction += CreatureParameters.HitCostPerUnit * HitForce;
            }
            else //if (command.Action == CreatureAction.Reproduce)
            {
                interaction = new Interaction(CreatureAction.Reproduce, command.Direction, 1);
                CountOfReproductions++;
            }
            Energy -= energyLostForAction;
            EnergyLostInLastStep += energyLostForAction;

            if (Health < MaxHealth/* && energy > RegenerationValue * CreatureParameters.RegenerationCostPerUnit*/)
            {
                double dh = MaxHealth - Health;
                if (dh > RegenerationValue)
                    dh = RegenerationValue;
                if (Energy > dh * CreatureParameters.RegenerationCostPerUnit)
                {
                    Energy -= dh * CreatureParameters.RegenerationCostPerUnit;
                    EnergyLostInLastStep += dh * CreatureParameters.RegenerationCostPerUnit;
                    Health += dh;
                }
            }

            return interaction;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (!obj.GetType().Equals(typeof(Creature)))
                return false;

            return Id == (obj as Creature).Id;
        }

        public override string ToString()
        {
            return $"[Id={Id} a={Alpha} H={Health} E={Energy} ELpS={EnergyLostPerStep} S={Brain.State}]";
        }
    }
}

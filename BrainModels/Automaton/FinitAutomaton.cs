using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.Automaton
{
	public class FinitAutomaton : IAutomaton
	{
		private SymbolType[] inputSymbolTypes;
		private SymbolType[] outputSymbolTypes;

		private AutomatonTableKV defaultOutput;

		private SymbolType states;
		private int currentState;

		private Dictionary<AutomatonTableKV, AutomatonTableKV> automatonTable;

		public FinitAutomaton(SymbolType[] inputSymbolTypes, 
                         SymbolType[] outputSymbolTypes,
						 AutomatonTableKV defaultOutput,
						 SymbolType states,
						 int initialState, 
						 Dictionary<AutomatonTableKV, AutomatonTableKV> automatonTable)
		{

			if (!SymbolTypeUtils.IsComplyWith(defaultOutput.IOSymbols.Symbols, outputSymbolTypes) && states.Contains(defaultOutput.State))
				throw new AutomatonException("Default output does not comply with inputSymbolTypes or/and states");

			if (!states.Contains(initialState))
				throw new AutomatonException("initial state is not correct");

			foreach (var key in automatonTable.Keys)
			{
				if (!SymbolTypeUtils.IsComplyWith(key.IOSymbols.Symbols, inputSymbolTypes) || !SymbolTypeUtils.IsComplyWith(automatonTable[key].IOSymbols.Symbols, outputSymbolTypes))
					throw new AutomatonException("Automaton table is not correct");
			}



			this.inputSymbolTypes = inputSymbolTypes;
			this.outputSymbolTypes = outputSymbolTypes;
			this.defaultOutput = defaultOutput;
			this.states = states;
			this.currentState = initialState;
			this.automatonTable = new Dictionary<AutomatonTableKV, AutomatonTableKV>(automatonTable);
		}


		public AutomatonIO Process(AutomatonIO input)
		{
			/*if (!SymbolTypeUtils.IsComplyWith(input.Symbols, inputSymbolTypes))
				throw new AutomatonException("Input is not correct");*/

			AutomatonTableKV key = new AutomatonTableKV(input, currentState);
			if (automatonTable.ContainsKey(key))
			{
				AutomatonTableKV output = automatonTable[key];
				this.currentState = output.State;
				return output.IOSymbols;
			}
			else
			{
				this.currentState = defaultOutput.State;
				return defaultOutput.IOSymbols;
			}

		}


		public SymbolType[] InputSymbolTypes
		{
			get => inputSymbolTypes;
		}
		public int InputLength
		{
			get => inputSymbolTypes.Length;
		}
		public SymbolType[] OutputSymbolTypes
		{
			get => outputSymbolTypes;
		}
		public int OutputLength
		{
			get => outputSymbolTypes.Length;
		}
		public AutomatonTableKV DefaultOutput
		{
			get => defaultOutput;
			set => defaultOutput = value;
		}
		public SymbolType States
		{
			get => states;
		}
		public int CurrentState
		{
			get => currentState;
		}


		/*public void AddState(int state)
		{
			if (state != states.IntervalEnd + 1 || state != states.IntervalBegin - 1)
				throw new AutomatonException("New state is not near interval");

			if (state == states.IntervalEnd + 1) states = new SymbolType(states.IntervalBegin, states.IntervalEnd + 1);
			else states = new SymbolType(states.IntervalBegin - 1, states.IntervalEnd);
		}
		public void RemoveState(int state)
		{
			if (state == states.IntervalEnd) 
				states = new SymbolType(states.IntervalBegin, states.IntervalEnd - 1);
			else if (state == states.IntervalBegin) 
				states = new SymbolType(states.IntervalBegin + 1, states.IntervalEnd);
			else 
				throw new AutomatonException("Removing state is not on border of interval");
		}
		public void AddTableRow(AutomatonTableKV input, AutomatonTableKV output)
		{
			if (!SymbolTypeUtils.IsComplyWith(input.IOSymbols.Symbols, inputSymbolTypes) || !SymbolTypeUtils.IsComplyWith(output.IOSymbols.Symbols, outputSymbolTypes))
				throw new AutomatonException("New row is not correct");

			if (!states.Contains(input.State) || !states.Contains(output.State))
				throw new AutomatonException("Automaton does not have this state");

			if (automatonTable.ContainsKey(input))
				throw new AutomatonException("Automaton contains this key");

			automatonTable.Add(input, output);
		}
		public void RemoveTableRow(AutomatonTableKV input)
		{
			if (!automatonTable.ContainsKey(input))
				throw new AutomatonException("This input is not in table");

			automatonTable.Remove(input);
		}
		public void ChangeOutputOf(AutomatonTableKV input, AutomatonTableKV newOutput)
		{
			if (!automatonTable.ContainsKey(input))
				throw new AutomatonException("This input is not in table");

			if (!SymbolTypeUtils.IsComplyWith(newOutput.IOSymbols.Symbols, outputSymbolTypes) || !states.Contains(newOutput.State))
				throw new AutomatonException("newOutput is not correct");

			automatonTable[input] = newOutput;
		}*/
	}
}

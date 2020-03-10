using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;
using EvolutionSimulator.BrainModels.Automaton;

namespace EvolutionSimulator.BrainModels.SequentialModel.AutomatonsLayer
{
    class AutomatonInLayer
	{
		private IAutomaton automaton;
		private DataRedirection[] toInputRedirections;
		private DataConverter<double, int>[] dataToAInput;
		private DataConverter<int, double>[] aOutputToData;

		public AutomatonInLayer(IAutomaton automaton,
								DataRedirection[] toInputRedirections, 
								DataConverter<double, int>[] convertersDataToAInput, 
								DataConverter<int, double>[] convertersAOutputToData)
		{
			if (automaton.InputLength != toInputRedirections.Length)
				throw new AutomatonException("Length of toInputRedirection does not equal to length of automaton input");
			if (automaton.InputLength != convertersDataToAInput.Length)
				throw new AutomatonException("Length of convertersDataToAInput does not equal to length of automaton input");
			if (automaton.OutputLength != convertersAOutputToData.Length)
				throw new AutomatonException("Length of convertersAOutputToData does not equal to length of automaton output");

			this.automaton = automaton;
			this.toInputRedirections = toInputRedirections;
			this.dataToAInput = convertersDataToAInput;
			this.aOutputToData = convertersAOutputToData;
		}

		public double[] Process()
		{
			int[] aInput = new int[automaton.InputLength];
			DataRedirection inputRedirection;
			for (int i = 0; i < aInput.Length; i++)
			{
				inputRedirection = toInputRedirections[i];
				aInput[i] = dataToAInput[i].Convert(inputRedirection.Layer.Data[inputRedirection.IndexOfData]);
			}

			AutomatonIO aOutput = automaton.Process(new AutomatonIO(aInput));
			double[] res = new double[automaton.OutputLength];
			for (int i = 0; i < res.Length; i++)
			{
				res[i] = aOutputToData[i].Convert(aOutput[i]);
			}

			return res;
		}

		public IAutomaton Automaton
		{
			get => automaton;
		}
		public DataRedirection[] ToInputRedirections
		{
			get => toInputRedirections;
		}
		public DataConverter<double, int>[] ConvertersDataToAInput
		{
			get => dataToAInput;
		}
		public DataConverter<int, double>[] ConvertersAOutputToData
		{
			get => aOutputToData;
		}
	}
}

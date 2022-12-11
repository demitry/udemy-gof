using System.Collections.ObjectModel;
using static NeuralNetworks.Program;

namespace NeuralNetworks
{
    internal class Program
    {
        public class Neuron
        {
            public float value;
            public List<Neuron> In, Out;

            public void ConnectTo(Neuron other)
            {
                Out.Add(other);
                other.In.Add(this);
            }
        }

        public class NeuronLayer : Collection<Neuron> {}

        //public class NouronRing : List<Neuron> { }// for example

        static void Main(string[] args)
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            neuron1.ConnectTo(neuron2); //1 method

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            //Connectivity is still required
            // Need 4 methods? 
            // neuron to neuron 
            // neuron to layer
            // layer to layer
            // layer to neuron

        }
    }
}
using System.Collections;
using System.Collections.ObjectModel;
using static NeuralNetworks.Program;

namespace NeuralNetworks
{
    // Solution: treat Neuron and NeuronLayer as a collection of Neuron

    public static class ExtentiosionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach (var from in self)
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
        }
    }

    public class Program
    {
        public class Neuron : IEnumerable<Neuron>
        {
            public float value;
            public List<Neuron> In => (new Lazy<List<Neuron>>()).Value;
            public List<Neuron> Out => (new Lazy<List<Neuron>>()).Value;
            
            public IEnumerator<Neuron> GetEnumerator()
            {
                yield return this; // I am an only element
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
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

            neuron1.ConnectTo(neuron2);
            layer1.ConnectTo(layer2);
            neuron1.ConnectTo(layer2);
        }
    }
}
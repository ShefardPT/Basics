using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Basics
{
    public class PedigreePath
    {
        public PedigreePath()
        {
            Nodes = new List<PedigreePathNode>();
        }

        public ICollection<PedigreePathNode> Nodes { get; private set; }

        public void AddNode(PedigreePathNode node)
        {
            Nodes.Add(node);
        }

        public int NumberOfNodes()
        {
            return Nodes.Count;
        }

        public PedigreePath Copy()
        {
            var copy = new PedigreePathNode[Nodes.Count];
            this.Nodes.CopyTo(copy, 0);

            return new PedigreePath()
            {
                Nodes = copy.ToList()
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this.Nodes.Select(x => x.Name));
        }
    }

    public class PedigreePathNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

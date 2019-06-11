using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basics
{
    public class InbreedingCalculator
    {
        public double CalculateInbreedingCoefficient(ICollection<PedigreeUnit> pedigree, Guid fatherId, Guid motherId)
        {
            var father = pedigree.FirstOrDefault(x => x.ItemId.Equals(fatherId));
            var mother = pedigree.FirstOrDefault(x => x.ItemId.Equals(motherId));

            if (father == null || mother == null)
            {
                return 0;
            }

            var paths = new List<PedigreePath>();

            var path = new PedigreePath();
            path.AddNode(new PedigreePathNode(){Id = mother.ItemId, Name = mother.Name});

            ProceedParents(mother, pedigree, path, paths);

            paths = paths.Where(x => x.Nodes.Last().Id.Equals(father.ItemId)).ToList();

            return paths.Select(x => Math.Pow(0.5, x.NumberOfNodes())).Sum();
        }

        private void ProceedParents
            (PedigreeUnit root, 
            ICollection<PedigreeUnit> pedigree, 
            PedigreePath path,
            ICollection<PedigreePath> paths)
        {
            var parents = pedigree.Where(x => x.Children.Any(y => y.ItemId.Equals(root.ItemId))).ToList();

            if (!parents.Any())
            {
                paths.Add(path);
            }

            foreach (var parent in parents)
            {
                var currentPath = path.Copy();

                currentPath.AddNode(new PedigreePathNode() { Id = parent.ItemId, Name = parent.Name });

                ProceedChildren(root, parent, currentPath, paths);

                ProceedParents(parent, pedigree, currentPath, paths);
            }
        }

        private void ProceedChildren(PedigreeUnit chosenChild, PedigreeUnit parent, PedigreePath path, ICollection<PedigreePath> paths)
        {
            if (!parent.Children.Any())
            {
                paths.Add(path);
            }

            foreach (var child in parent.Children)
            {
                if (child.ItemId.Equals(chosenChild.ItemId))
                {
                    continue;
                }

                var currentPath = path.Copy();

                currentPath.AddNode(new PedigreePathNode() { Id = child.ItemId, Name = child.Name });

                ProceedChildren(parent, child, currentPath, paths);
            }
        }
    }
}

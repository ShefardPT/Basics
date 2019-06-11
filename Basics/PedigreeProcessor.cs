using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basics
{
    public class PedigreeProcessor
    {
        public IEnumerable<PedigreeUnit> ProcessPedigrees
            (ICollection<HorsePedigreeDTO> pedigreeA, ICollection<HorsePedigreeDTO> pedigreeB)
        {
            var result = new List<PedigreeUnit>();

            var rootA = pedigreeA.FirstOrDefault(x => x.PedigreePosition.Equals("X"));
            if (rootA == null)
            {
                throw new Exception("There is no root in pedigree A.");
            }
            rootA.PedigreePosition = string.Empty;

            var rootB = pedigreeB.FirstOrDefault(x => x.PedigreePosition.Equals("X"));
            if (rootB == null)
            {
                throw new Exception("There is no root in pedigree A.");
            }
            rootB.PedigreePosition = string.Empty;

            ProceedParents(rootA, pedigreeA, result, null);
            ProceedParents(rootB, pedigreeB, result, null);

            return result;
        }

        private void ProceedParents
            (HorsePedigreeDTO root, 
            ICollection<HorsePedigreeDTO> pedigree, 
            ICollection<PedigreeUnit> target,
            HorsePedigreeDTO child)
        {
            AddPedigreeUnit(child, root, target);

            var father = pedigree.FirstOrDefault(x => x.PedigreePosition.Equals(root.PedigreePosition + "F"));
            if (father != null)
            {
                ProceedParents(father, pedigree, target, root);
            }

            var mother = pedigree.FirstOrDefault(x => x.PedigreePosition.Equals(root.PedigreePosition + "M"));
            if (mother != null)
            {
                ProceedParents(mother, pedigree, target, root);
            }
        }

        private void AddPedigreeUnit
            (HorsePedigreeDTO child, 
            HorsePedigreeDTO item, 
            ICollection<PedigreeUnit> newPedigree)
        {
            var itemNewPedigree = newPedigree.FirstOrDefault(x => x.IdNumber.Equals(item.Item.IdNumber));

            var isNew = itemNewPedigree == null;

            if (isNew)
            {
                itemNewPedigree = new PedigreeUnit()
                {
                    ItemId = Guid.NewGuid(),
                    IdNumber = item.Item.IdNumber,
                    Name = item.Item.Name
                };
            }

            if (child != null)
            {
                var childNewPedigree = newPedigree.FirstOrDefault(x => x.IdNumber.Equals(child.Item.IdNumber));

                if (childNewPedigree != null && !itemNewPedigree.Children.Any(x => x.ItemId.Equals(childNewPedigree.ItemId)))
                {
                    itemNewPedigree.Children.Add(childNewPedigree);
                } 
            }

            if (isNew)
            {
                newPedigree.Add(itemNewPedigree);
            }
        }
    }
}

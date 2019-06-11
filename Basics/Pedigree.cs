using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basics
{
    public class HorsePedigreeItemDTO
    {
        public HorsePedigreeItemDTO()
        {
            Name = string.Empty;
            IdNumber = string.Empty;
            ProfileId = string.Empty;
            SemanticUrl = string.Empty;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public Sex Sex { get; set; }
        public string ProfileId { get; set; }
        public string SemanticUrl { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }

    public class HorsePedigreeDTO
    {
        public string PedigreePosition { get; set; }
        public HorsePedigreeItemDTO Item { get; set; }
    }

    public class PedigreeUnit
    {
        public PedigreeUnit()
        {
            Name = string.Empty;
            IdNumber = string.Empty;
            ItemId = Guid.Empty;
            Children = new List<PedigreeUnit>();
        }

        public string Name { get; set; }
        public string IdNumber { get; set; }
        public Guid ItemId { get; set; }
        public ICollection<PedigreeUnit> Children { get; set; }

        public override string ToString()
        {
            return $"{this.Name}: {this.IdNumber}";
        }
    }

    public class PedigreeUnitShort
    {
        public PedigreeUnitShort()
        {
            Name = string.Empty;
            IdNumber = string.Empty;
            ItemId = Guid.Empty;
            ChildrenId = new List<string>();
        }

        public string Name { get; set; }
        public string IdNumber { get; set; }
        public Guid ItemId { get; set; }
        public IEnumerable<string> ChildrenId { get; set; }

        public static PedigreeUnitShort Parse(PedigreeUnit item)
        {
            return new PedigreeUnitShort()
            {
                Name = item.Name,
                IdNumber = item.IdNumber,
                ItemId = item.ItemId,
                ChildrenId = item.Children.Select(x => x.IdNumber)
            };
        }
    }
}

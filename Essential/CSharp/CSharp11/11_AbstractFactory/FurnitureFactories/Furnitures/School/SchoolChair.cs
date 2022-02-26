using FurnitureFactories.Furnitures.Common;

namespace FurnitureFactories.Furnitures.School
{
    public class SchoolChair : IChair, IFurniture<SchoolFurniture>
    {
        public bool HasLegs { get; set; }
        public bool SitOn { get; set; }
        public override string ToString()
        {
            return nameof(SchoolChair);
        }
    }
}

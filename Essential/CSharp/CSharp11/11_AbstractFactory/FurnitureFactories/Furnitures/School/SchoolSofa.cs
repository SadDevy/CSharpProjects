using FurnitureFactories.Furnitures.Common;

namespace FurnitureFactories.Furnitures.School
{
    public class SchoolSofa : ISofa, IFurniture<SchoolFurniture>
    {
        public bool HasLegs { get; set; }
        public bool HasSidePanels { get; set; }
        public override string ToString()
        {
            return nameof(SchoolSofa);
        }
    }
}

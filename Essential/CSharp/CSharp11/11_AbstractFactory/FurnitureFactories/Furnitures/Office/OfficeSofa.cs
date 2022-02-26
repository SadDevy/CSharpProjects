using FurnitureFactories.Furnitures.Common;

namespace FurnitureFactories.Furnitures.Office
{
    public class OfficeSofa : ISofa, IFurniture<OfficeFurniture>
    {
        public bool HasLegs { get; set; }
        public bool HasSidePanels { get; set; }
        public override string ToString()
        {
            return nameof(OfficeSofa);
        }
    }
}

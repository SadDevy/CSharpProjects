using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactory.Furnitures.Victorian
{
    public class VictorianSofa : ISofa
    {
        public bool HasLegs { get; set; }
        public bool HasSidePanels { get; set; }
        public override string ToString()
        {
            return nameof(VictorianSofa);
        }
    }
}

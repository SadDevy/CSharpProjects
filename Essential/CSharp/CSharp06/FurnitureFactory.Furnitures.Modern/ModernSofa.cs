using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactory.Furnitures.Modern
{
    public class ModernSofa : ISofa
    {
        public bool HasLegs { get; set; }
        public bool HasSidePanels { get; set; }
        public override string ToString()
        {
            return nameof(ModernSofa);
        }
    }
}

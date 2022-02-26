using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactory.Furnitures.Modern
{
    public class ModernFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair(bool hasLegs, bool sitOn)
        {
            return new ModernChair() { HasLegs = hasLegs, SitOn = sitOn };
        }

        public ISofa CreateSofa(bool hasLegs, bool hasSidePanels)
        {
            return new ModernSofa() { HasLegs = hasLegs, HasSidePanels = hasSidePanels };
        }
    }
}

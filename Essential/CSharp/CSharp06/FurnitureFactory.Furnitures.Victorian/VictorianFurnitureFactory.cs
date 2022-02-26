using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactory.Furnitures.Victorian
{
    public class VictorianFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair(bool hasLegs, bool sitOn)
        {
            return new VictorianChair() { HasLegs = hasLegs, SitOn = sitOn };
        }

        public ISofa CreateSofa(bool hasLegs, bool hasSidePanels)
        {
            return new VictorianSofa() { HasLegs = hasLegs, HasSidePanels = hasSidePanels };
        }
    }
}

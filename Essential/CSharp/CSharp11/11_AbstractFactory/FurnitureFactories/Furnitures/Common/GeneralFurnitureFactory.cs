namespace FurnitureFactories.Furnitures.Common
{
    public class GeneralFurnitureFactory<TFurniture, TChair, TSofa> : IFurnitureFactory
        where TChair : IChair, IFurniture<TFurniture>, new()
        where TSofa : ISofa, IFurniture<TFurniture>, new()
        where TFurniture : class, IFurniture<TFurniture>
    {
        public IChair CreateChair(bool hasLegs, bool sitOn)
        {
            return new TChair() { HasLegs = hasLegs, SitOn = sitOn };
        }
        public ISofa CreateSofa(bool hasLegs, bool hasSidePanels)
        {
            return new TSofa() { HasLegs = hasLegs, HasSidePanels = hasSidePanels };
        }
    }
}

namespace FurnitureFactory.Furnitures.Common
{
    public interface IFurnitureFactory
    {
        IChair CreateChair(bool hasLegs, bool sitOn);
        ISofa CreateSofa(bool hasLegs, bool hasSidePanels);
    }
}

using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactoryUI
{
    public class Client
    {
        private IChair _chair;
        private ISofa _sofa;

        public Client(IFurnitureFactory factory)
        {
            _chair = factory.CreateChair(true, true);
            _sofa = factory.CreateSofa(true, true);
        }

        public override string ToString()
        {
            return string.Format("Кровать: {0}, стул: {1}", _sofa, _chair);
        }
    }
}

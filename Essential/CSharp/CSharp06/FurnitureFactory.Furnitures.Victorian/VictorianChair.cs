using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactory.Furnitures.Victorian
{
    public class VictorianChair : IChair
    {
        public bool HasLegs { get; set; }
        public bool SitOn { get; set; }
        public override string ToString()
        {
            return nameof(VictorianChair);
        }
    }
}

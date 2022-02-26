using FurnitureFactory.Furnitures.Common;

namespace FurnitureFactory.Furnitures.Modern
{
    public class ModernChair : IChair
    {
        public bool HasLegs { get; set; }
        public bool SitOn { get; set; }
        public override string ToString()
        {
            return nameof(ModernChair);
        }
    }
}

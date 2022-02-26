using FurnitureFactories.Furnitures.Common;

namespace FurnitureFactories.Furnitures.Office
{
    public class OfficeChair : IChair, IFurniture<OfficeFurniture>
    {
        public bool HasLegs { get; set; }
        public bool SitOn { get; set; }
        public override string ToString()
        {
            return nameof(OfficeChair);
        }
    }
}

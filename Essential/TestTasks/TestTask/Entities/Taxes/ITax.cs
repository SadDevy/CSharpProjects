namespace Entities.Taxes
{
    public interface ITax
    {
        int GetPercent(string goodsName);
    }
}

using Entities;

namespace Utilities.Formatters
{
    public interface IFormatter
    {
        string Format(Basket basket, int outputNumber);
    }
}

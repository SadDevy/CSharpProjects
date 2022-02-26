using System;

namespace CurrencyConverter.Models
{
    public interface IView
    {
        void SetDollar(decimal value);
        void SetRuble(decimal value);

        decimal InputValue { get; }
        decimal InputCourse { get; }

        event EventHandler<EventArgs> DollarSetted;
        event EventHandler<EventArgs> RubleSetted;
        event EventHandler<EventArgs> CourseSetted;
    }
}

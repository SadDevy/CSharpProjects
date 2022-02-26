using System;

namespace CurrencyConverter.Models
{
    public class Presenter
    {
        private Converter converter = new Converter();
        private IView view;

        public Presenter(IView view)
        {
            this.view = view;
            this.view.DollarSetted += OnSetDollar;
            this.view.RubleSetted += OnSetRuble;
            this.view.CourseSetted += OnSetCourse;

            RefreshView();
        }

        private void OnSetDollar(object sender, EventArgs e)
        {
            converter.ValueDollar = view.InputValue;
            RefreshView();
        }

        private void OnSetRuble(object sender, EventArgs e)
        {
            converter.ValueRuble = view.InputValue;
            RefreshView();
        }

        private void OnSetCourse(object sender, EventArgs e)
        {
            converter.DollarCource = view.InputCourse;
        }

        private void RefreshView()
        {
            view.SetDollar(converter.ValueDollar);
            view.SetRuble(converter.ValueRuble);
        }
    }
}

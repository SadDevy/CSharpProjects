using CurrencyConverter.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CurrencyConverter.WinView
{
    public partial class FormView : Form, IView
    {
        public FormView()
        {
            InitializeComponent();
        }

        public void SetDollar(decimal value)
        {
            tbDollars.Text = value.ToString("C4", new CultureInfo("en"));
        }

        public void SetRuble(decimal value)
        {
            tbRubles.Text = value.ToString("C4");
        }

        public decimal InputValue
        {
            get => decimal.Parse(tbValue.Text);
        }

        public decimal InputCourse
        {
            get => decimal.Parse(tbCourse.Text);
        }

        public event EventHandler<EventArgs> DollarSetted;
        public event EventHandler<EventArgs> RubleSetted;
        public event EventHandler<EventArgs> CourseSetted;

        private void btnInDollars_Click(object sender, EventArgs e)
        {
            AddZeroToTextBox();
            DollarSetted?.Invoke(this, EventArgs.Empty);
        }

        private void btnInRubles_Click(object sender, EventArgs e)
        {
            AddZeroToTextBox();
            RubleSetted?.Invoke(this, EventArgs.Empty);
        }

        private void AddZeroToTextBox()
        {
            const char dot = ',';
            const char zero = '0';

            AddToTextBox(tbCourse, dot, zero);
            AddToTextBox(tbValue, dot, zero);
        }

        private void AddToTextBox(TextBox textbox, char dot, char zero)
        {
            if (textbox.Text.Last() == dot)
                textbox.Text += zero;
        }

        private void tbCourse_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(tbCourse.Text, out decimal d))
                CourseSetted?.Invoke(this, EventArgs.Empty);
        }

        private void tbCourse_KeyPress(object sender, KeyPressEventArgs e) => CheckEnter(e, tbCourse);

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e) => CheckEnter(e, tbValue);

        private void CheckEnter(KeyPressEventArgs e, TextBox textbox)
        {
            if ((e.KeyChar == ','))
            {
                e.Handled = string.IsNullOrEmpty(textbox.Text) || textbox.Text.Contains(",");
                return;
            }

            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
    }
}

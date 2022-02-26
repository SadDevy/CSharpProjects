using System;
using System.Collections.Generic;

namespace ArgumentParsing.Marshalers
{
    public class DoubleArgumentMarshaler : IArgumentMarshaler
    {
        private double doubleValue = 0;
        public object Get() => doubleValue;

        public void Set(IEnumerator<string> currentArgument)
        {
            string parameter = null;
            try
            {
                parameter = currentArgument.Current;
                doubleValue = double.Parse(parameter);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgsException(ErrorCode.MissingDouble, parameter);
            }
            catch (FormatException ex)
            {
                throw new ArgsException(ErrorCode.InvalidDouble, parameter);
            }
        }
    }
}

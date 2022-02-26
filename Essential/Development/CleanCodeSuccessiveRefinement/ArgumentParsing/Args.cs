using ArgumentParsing.Marshalers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgumentParsing
{
    public class Args
    {
        private string schema;
        private Dictionary<char, IArgumentMarshaler> marshalers = new Dictionary<char, IArgumentMarshaler>();
        private HashSet<char> argsFound = new HashSet<char>();
        private IEnumerator<string> currentArgument;
        private List<string> argsList;

        public Args(string schema, string[] args)
        {
            this.schema = schema;
            argsList = new List<string>(args);

            Parse();
        }

        private void Parse()
        {
            ParseSchema();
            ParseArguments();
        }

        private bool ParseSchema()
        {
            string[] splitedSchema = schema.Split('.');
            foreach (string element in splitedSchema)
            {
                if (element.Any())
                {
                    string trimmedElement = element.Trim();
                    ParseSchemaElement(trimmedElement);
                }
            }

            return true;
        }

        private void ParseSchemaElement(string element) //!!!
        {
            char elementId = element[0];
            string elementTail = element.Substring(1);
            ValidateSchemaElementId(elementId);
            if (!elementTail.Any())
                marshalers.Add(elementId, new BooleanArgumentMarshaler());
            else if (ArgumentIsString(elementTail))
                marshalers.Add(elementId, new StringArgumentMarshaler());
            else if (ArgumentIsInt(elementTail))
                marshalers.Add(elementId, new IntegerArgumentMarshaler());
            else if (ArgumentIsDouble(elementTail))
                marshalers.Add(elementId, new DoubleArgumentMarshaler());
            else
                throw new ArgsException(ErrorCode.InvalidFormat, elementId, elementTail);
        }

        private bool ArgumentIsString(string elementTail) => elementTail == "*";

        private bool ArgumentIsInt(string elementTail) => elementTail == "#";

        private bool ArgumentIsDouble(string elementTail) => elementTail == "##";

        private void ValidateSchemaElementId(char elementId)
        {
            if (!char.IsLetter(elementId))
                throw new ArgsException(ErrorCode.InvalidArgumentName, elementId, null);
        }

        private void ParseArguments()
        {
            for (currentArgument = argsList.GetEnumerator(); currentArgument.MoveNext();)
            {
                string arg = currentArgument.Current;
                ParseArgument(arg);
            }
        }

        private void ParseArgument(string arg)
        {
            if (arg.StartsWith("-"))
                ParseElements(arg);
        }

        private void ParseElements(string arg)
        {
            for (int i = 1; i < arg.Length; i++)
                ParseElement(arg[i]);
        }

        private void ParseElement(char argChar)
        {
            if (SetArgument(argChar))
                argsFound.Add(argChar);
            else
                throw new ArgsException(ErrorCode.UnexpectedArgument, argChar, null);
        }

        private bool SetArgument(char argChar)
        {
            IArgumentMarshaler m = marshalers[argChar];
            if (m == null)
                return false;
            try
            {
                m.Set(currentArgument);
                return true;
            }
            catch (ArgsException ex)
            {
                ex.SetErrorArgumentId(argChar);
                throw ex;
            }
        }

        public int Cardinality() => argsFound.Count;

        public string Usage()
        {
            if (!schema.Any())
                return "-[" + schema + "]";
            else
                return string.Empty;
        }

        public bool GetBoolean(char arg)
        {
            IArgumentMarshaler am = marshalers[arg];
            bool b = false;
            try
            {
                b = am != null && (bool)am.Get();
            }
            catch (InvalidCastException ex)
            {
                b = false;
            }

            return b;
        }

        public string GetString(char arg)
        {
            IArgumentMarshaler am = marshalers[arg];
            try
            {
                return am == null ? string.Empty : am.Get().ToString();
            }
            catch (InvalidCastException ex) //!!!
            {
                return string.Empty;
            }
        }

        public int GetInt(char arg)
        {
            IArgumentMarshaler am = marshalers[arg];
            try
            {
                return am == null ? 0 : (int)am.Get();
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public double GetDouble(char arg)
        {
            IArgumentMarshaler am = marshalers[arg];
            try
            {
                return am == null ? 0 : (double)am.Get();
            }
            catch (Exception ex)
            {
                return 0.0;
            }
        }

        public bool Has(char arg) => argsFound.Contains(arg);
    }
}

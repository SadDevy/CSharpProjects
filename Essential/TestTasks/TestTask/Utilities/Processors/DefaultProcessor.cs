using Entities;
using System.Collections.Generic;
using Utilities.Formatters;
using Utilities.Parsers;
using Utilities.Writers;

namespace Utilities.Processors
{
    public class DefaultProcessor : IProcessor
    {
        private readonly IFormatter formatter;
        private readonly IWriter writer;

        private readonly string data;

        public DefaultProcessor(string data, IFormatter formatter, IWriter writer)
        {
            this.formatter = formatter;
            this.writer = writer;
            this.data = data;
        }

        public void Process()
        {
            if (string.IsNullOrEmpty(data))
                return;

            if (!BasketsParser.TryParseBaskets(data, out IEnumerable<Basket> baskets))
                return;

            WriteBasketsReport(baskets);
        }

        private void WriteBasketsReport(IEnumerable<Basket> baskets)
        {
            if (formatter == null || writer == null)
                return;

            int index = 0;
            foreach (Basket basket in baskets)
            {
                string formatted = formatter.Format(basket, ++index);
                writer.Write(formatted);
            }
        }
    }
}

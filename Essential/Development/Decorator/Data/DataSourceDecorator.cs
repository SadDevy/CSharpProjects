namespace Data
{
    public class DataSourceDecorator : IDataSource
    {
        private IDataSource wrapper;

        public DataSourceDecorator(IDataSource source) => wrapper = source;

        public virtual void WriteData(string data) => wrapper.WriteData(data);

        public virtual string ReadData() => wrapper.ReadData();
    }
}

namespace Data
{
    public interface IDataSource
    {
        public void WriteData(string data);
        public string ReadData();
    }
}

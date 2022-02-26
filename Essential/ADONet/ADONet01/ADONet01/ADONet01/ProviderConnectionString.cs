namespace ADONet01
{
    public class ProviderConnectionString
    {
        public ProviderType ProviderType { get; set; }
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }

        public ProviderConnectionString(ProviderType providerType, string providerName, string connectionString)
        {
            ProviderType = providerType;
            ProviderName = providerName;
            ConnectionString = connectionString;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ProviderConnectionString))
                return false;

            ProviderConnectionString a = (ProviderConnectionString)obj;
            return ProviderType == a.ProviderType && ConnectionString == a.ConnectionString;
        }

        public override int GetHashCode() => ProviderType.GetHashCode() ^ ConnectionString.GetHashCode();

        public override string ToString() => ProviderType.ToString();
    }
}

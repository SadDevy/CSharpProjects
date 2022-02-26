using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ADONet01
{
    public class ConfigManager
    {
        private const string defaultProviderName = "DefaultProvider";

        public ProviderConnectionString GetDefaultProvider()
        {
            IEnumerable<ProviderConnectionString> connetctionStrings = GetAllConnectionStrings();
            return connetctionStrings.First(n => n.ProviderType == DefaultProvider);
        }

        private ProviderType DefaultProvider
        {
            get
            {
                string defaultProviderTypeName = GetDefaultProviderTypeName();
                try
                {
                    return GetProviderType(defaultProviderTypeName);
                }
                catch (ConfigurationErrorsException ex)
                {
                    throw new ConfigurationErrorsException($"Провайдер по умолчанию {defaultProviderTypeName} не определен: {ex}.", ex);
                }
            }
        }

        private string GetDefaultProviderTypeName()
        {
            string defaultProvider = ConfigurationManager.AppSettings[defaultProviderName];
            if (string.IsNullOrEmpty(defaultProvider))
                throw new ConfigurationErrorsException($"Провайдер с ключом {defaultProviderName} не определен.");

            return defaultProvider;
        }

        public IEnumerable<ProviderConnectionString> GetAllConnectionStrings()
        {
            foreach (ConnectionStringSettings settings in GetConnectionStringSettingsCollection())
            {
                yield return GetProviderConnectionString(settings);
            }
        }

        private ProviderConnectionString GetProviderConnectionString(ConnectionStringSettings settings)
        {
            try
            {
                ProviderType providerType = GetProviderType(settings.Name);
                return new ProviderConnectionString(providerType, settings.ProviderName, settings.ConnectionString);
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new ConfigurationErrorsException($"Провайдер {settings.Name} не определен: {ex}.", ex);
            }
        }

        private ConnectionStringSettingsCollection GetConnectionStringSettingsCollection()
        {
            return ConfigurationManager.ConnectionStrings;
        }

        private ProviderType GetProviderType(string providerName)
        {
            if (!Enum.TryParse(providerName, out ProviderType providerType))
                throw new ConfigurationErrorsException();

            return providerType;
        }
    }
}

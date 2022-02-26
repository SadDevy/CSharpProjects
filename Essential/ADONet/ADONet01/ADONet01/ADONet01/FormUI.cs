using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;

namespace ADONet01
{
    public partial class FormUI : Form
    {
        private ConfigManager configManager;

        public FormUI() => InitializeComponents();

        private void InitializeComponents()
        {
            InitializeComponent();

            configManager = new ConfigManager();
        }

        private void FormUI_Load(object sender, EventArgs e)
        {
            SetupProvidersComboBox();
        }

        private void SetupProvidersComboBox()
        {
            try
            {
                SetupProviders();

                SetupSelectedProvider();

                ShowConnectionStringForSelectedProvider();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void SetupProviders()
        {
            IEnumerable<ProviderConnectionString> providerConnectionStrings = GetProviderConnectionStrings();
            SetProviders(providerConnectionStrings);
        }

        private void SetupSelectedProvider()
        {
            ProviderConnectionString selectedProviderConnectionString = null;
            try
            {
                selectedProviderConnectionString = GetDefaultProviderConnectionString();
            }
            catch (ConfigurationErrorsException ex)
            {
                selectedProviderConnectionString = configManager.GetAllConnectionStrings().First();
                throw new ConfigurationErrorsException(ex.Message, ex);
            }
            finally
            {
                SetSelectedProvider(selectedProviderConnectionString);
            }

        }

        private void ShowConnectionStringForSelectedProvider()
        {
            ProviderConnectionString providerConnectionString = (ProviderConnectionString)cbProviderType.SelectedItem;
            tbConnectionString.Text = providerConnectionString.ConnectionString;
        }

        private void SetProviders(IEnumerable<ProviderConnectionString> providerConnectionStrings)
        {
            cbProviderType.Items.Clear();
            cbProviderType.Items.AddRange(providerConnectionStrings.ToArray());
        }

        private void SetSelectedProvider(ProviderConnectionString selectedProviderConnectionString)
        {
            cbProviderType.SelectedItem = selectedProviderConnectionString;
        }

        private IEnumerable<ProviderConnectionString> GetProviderConnectionStrings()
        {
            return configManager.GetAllConnectionStrings();
        }

        private ProviderConnectionString GetDefaultProviderConnectionString()
        {
            return configManager.GetDefaultProvider();
        }

        private void cbProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowConnectionStringForSelectedProvider();
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
            => ShowServerVersion();

        private void ShowServerVersion()
        {
            string providerName = GetSelectedProviderName();
            string connectionString = GetSelectedConnectionString();
            try
            {
                string dBVersion = VersionProvider.GetServerVersion(providerName, connectionString);
                ShowResult(dBVersion);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowResult(string dBVersion)
        {
            tbDbmsVersion.Text = dBVersion;
        }

        private string GetSelectedConnectionString()
        {
            return tbConnectionString.Text;
        }

        private string GetSelectedProviderName()
        {
            return ((ProviderConnectionString)cbProviderType.SelectedItem)?.ProviderName;
        }

        private void ShowError(Exception ex)
        {
            string message = string.Empty;
            if (ex is ConfigurationErrorsException)
                message = $"Ошибка работы с файлом конфигурации: {ex.Message}";
            else if (ex is InvalidOperationException)
                message = $"Ошибка выполнения операции: {ex.Message}";
            else if (ex is SqlException)
                message = $"Ошибка работы с БД: {ex.Message}";
            else
                message = ex.Message;

            MessageBox.Show(message);
        }
    }
}

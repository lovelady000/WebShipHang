using System;
using System.Configuration;
using System.Web.Configuration;

namespace ShipShop.Common
{
    public class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string UpdateSetting(string key, string value)
        {
            //Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //configuration.AppSettings.Settings[key].Value = value;
            //configuration.Save();

            //ConfigurationManager.RefreshSection("appSettings");
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                string oldValue = config.AppSettings.Settings[key].Value;
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }
    }
}
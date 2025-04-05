using System.Configuration;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class AppSettingsConfigManager : IConfigManager
    {
        public string GetSetting(string key) => ConfigurationManager.AppSettings.Get(key) ?? throw new ArgumentNullException(nameof(key));
    }
}

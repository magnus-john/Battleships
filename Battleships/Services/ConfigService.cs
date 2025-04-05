using Battleships.Model.Enums;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class ConfigService(IConfigManager config) : IConfigService
    {
        private const string BoardSizeName = "BoardSize";
        private const string FleetTypeName = "FleetType";
        private const string SetupTypeName = "SetupType";
        private const string AllowCheatingName = "AllowCheating";

        public BoardSize BoardSize => GetEnumConfigValue<BoardSize>(BoardSizeName);
        public FleetType FleetType => GetEnumConfigValue<FleetType>(FleetTypeName);
        public SetupType SetupType => GetEnumConfigValue<SetupType>(SetupTypeName);
        public bool AllowCheating => GetBooleanConfigValue(AllowCheatingName);

        private bool GetBooleanConfigValue(string key)
        {
            var value = config.GetSetting(key);

            if (!bool.TryParse(value, out var result))
                throw new ArgumentOutOfRangeException(nameof(key));
            
            return result;
        }

        private T GetEnumConfigValue<T>(string key)
        {
            var value = config.GetSetting(key);

            if (!Enum.TryParse(typeof(T), value, true, out var result))
                throw new ArgumentOutOfRangeException(nameof(key));

            return (T)result;
        }
    }
}

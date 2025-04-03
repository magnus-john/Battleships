using System.Configuration;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class ConfigService : IConfigService
    {
        private const string BoardSizeName = "BoardSize";
        private const string FleetTypeName = "FleetType";
        private const string SetupTypeName = "SetupType";
        private const string AllowCheatingName = "AllowCheating";

        private BoardSize? _boardSize;
        private FleetType? _fleetType;
        private SetupType? _setupType;
        private bool? _allowCheating;

        public BoardSize BoardSize => _boardSize ??= GetEnumConfigValue<BoardSize>(BoardSizeName);
        public FleetType FleetType => _fleetType ??= GetEnumConfigValue<FleetType>(FleetTypeName);
        public SetupType SetupType => _setupType ??= GetEnumConfigValue<SetupType>(SetupTypeName);
        public bool AllowCheating => _allowCheating ??= GetBooleanConfigValue(AllowCheatingName);

        private static bool GetBooleanConfigValue(string key)
        {
            var value = GetConfigValue(key);

            if (!bool.TryParse(value, out var result))
                throw new ArgumentOutOfRangeException(nameof(key));
            
            return result;
        }

        private static T GetEnumConfigValue<T>(string key)
        {
            var value = GetConfigValue(key);

            if (!Enum.TryParse(typeof(T), value, true, out var result))
                throw new ArgumentOutOfRangeException(nameof(key));

            return (T)result;
        }

        private static string GetConfigValue(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);

            if (value == null)
                throw new ArgumentNullException(nameof(key));

            return value;
        }
    }
}

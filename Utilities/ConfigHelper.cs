using System.IO;
using Newtonsoft.Json.Linq;

namespace ProyectoFinalSMV.Utilities
{
    public static class ConfigHelper
    {
        private static readonly JObject config;

        static ConfigHelper()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            var jsonPath = Path.Combine(projectRoot, "appsettings.json");

            var json = File.ReadAllText(jsonPath);
            config = JObject.Parse(json);
        }

        public static string GetSetting(string key)
        {
            var env = config["ActiveEnvironment"]?.ToString();

            if (string.IsNullOrEmpty(env))
                throw new InvalidOperationException("ActiveEnvironment no está definido en appsettings.json.");

            var value = config["Environments"]?[env]?[key]?.ToString();

            if (string.IsNullOrEmpty(value))
                throw new KeyNotFoundException($"La clave '{key}' no existe en el ambiente '{env}'.");

            return value;
        }
    }
}

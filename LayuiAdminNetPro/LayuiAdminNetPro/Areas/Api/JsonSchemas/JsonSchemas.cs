using CodeHelper.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LayuiAdminNetPro.Areas.Api.Schemas
{
    public class JsonSchemas
    {
        private static JsonSchemas? _instance;

        /// <summary>
        /// JsonSchema 配置路径
        /// </summary>
        private static readonly string schemaPath = AppDomain.CurrentDomain.BaseDirectory + @"Areas\Api\JsonSchemas\" + "JsonSchemas.json";

        /// <summary>
        /// JsonSchema 配置信息
        /// </summary>
        private JObject? _schemas;

        private JsonSchemas()
        {
        }

        private static async Task Instance()
        {
            _instance ??= new JsonSchemas()
            {
                _schemas = await JsonHelper.ReadJsonFileToJObjectAsync(schemaPath)
            };
        }

        private static void InstanceSync()
        {
            _instance ??= new JsonSchemas()
            {
                _schemas = JsonHelper.ReadJsonFileToJObjectSync(schemaPath)
            };
        }

        public static async Task<string> GetSchema(string key)
        {
            await Instance();
            return JsonConvert.SerializeObject(_instance!._schemas![key]);
        }

        public static string GetSchemaSync(string key)
        {
            InstanceSync();
            return JsonConvert.SerializeObject(_instance!._schemas![key]);
        }

        /// <summary>
        /// 初始化/重载配置
        /// </summary>
        public static void Init()
        {
            if (_instance != null)
            {
                _instance = null;
            }
        }
    }
}
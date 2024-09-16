using System.IO;
using System.Linq;
using Codebase.Data;
using Codebase.Infrastructure.Abstract;
using Newtonsoft.Json;
using UnityEngine;

namespace Codebase.Infrastructure.Implementations
{
    public class FieldDataSource : IFieldDataSource
    {
        public string[] GetFieldsNames()
        {
            var path = Application.dataPath + "/StreamingAssets/Levels/";
            var files = Directory.GetFiles(path, "*.json");

            return files.Select(Path.GetFileNameWithoutExtension).ToArray();
        }

        public FieldData[] GetFieldData(string name)
        {
            var path = Application.dataPath + $"/StreamingAssets/Levels/{name}.json";
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<FieldData[]>(json);
        }
    }
}
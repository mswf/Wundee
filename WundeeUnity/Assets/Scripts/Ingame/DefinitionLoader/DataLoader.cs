

using System.Collections.Generic;

using System.IO;

using UnityEngine;

using LitJson;

using JsonData = LitJson.JsonData;


namespace Wundee
{
	public class DataLoader
	{
		public Dictionary<string, Dictionary<string, JsonData>> _loadedData;

		public DataLoader()
		{
			_loadedData = new Dictionary<string, Dictionary<string, JsonData>>();
		}

		public void AddDataPath(string dataTypeKey, string relativePath)
		{
			var fullPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "Definitions" +
						   Path.DirectorySeparatorChar + relativePath;
			var jsonFilePaths = Directory.GetFiles(fullPath, "*.json");

			var dataDictionary = _loadedData.ContainsKey(dataTypeKey) ? 
			_loadedData[dataTypeKey] : new Dictionary<string, JsonData>();

			foreach (var filePath in jsonFilePaths)
			{
				var jsonString = File.ReadAllText(filePath);
				var reader = JsonMapper.ToObject(jsonString);

				foreach (var dataKey in reader.Keys)
				{
					dataDictionary[dataKey] = reader[dataKey];
				}
			}


			_loadedData[dataTypeKey] = dataDictionary;
			
		}

	}

	public class DefinitionTypes
	{
		public const string STORY = "STORY";
	}

}

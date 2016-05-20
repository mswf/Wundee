

using System.Collections.Generic;

using System.IO;

using UnityEngine;

using LitJson;
using Wundee.Stories;

namespace Wundee
{
	public class DataLoader
	{
		public DefinitionLoader<StoryDefinition> storyDefinitions;
		public DefinitionLoader<StoryNodeDefinition> storyNodeDefinitions;

		public Dictionary<System.Type, System.Object> definitionLoaderMapper; 
		

		public DataLoader()
		{
			storyDefinitions = new DefinitionLoader<StoryDefinition>(this);
			storyNodeDefinitions = new DefinitionLoader<StoryNodeDefinition>(this);

			this.definitionLoaderMapper = new Dictionary<System.Type, object>();

			definitionLoaderMapper[typeof (StoryDefinition)] = storyDefinitions;
			definitionLoaderMapper[typeof (StoryNodeDefinition)] = storyNodeDefinitions;

		}

		public LitJson.JsonData GetJsonDataFromFile(string filePath)
		{
			var jsonString = File.ReadAllText(filePath);

			var reader = new JsonReader(jsonString);
			reader.AllowComments = true;

			return JsonMapper.ToObject(reader);
		}

		public string[] GetAllJsonFilePaths(string relativePath)
		{
			var fullPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "Definitions" +
			   Path.DirectorySeparatorChar + relativePath;
			return Directory.GetFiles(fullPath, "*.json");
		}
		

	}
}

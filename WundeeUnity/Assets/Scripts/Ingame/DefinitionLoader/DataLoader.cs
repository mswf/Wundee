

using System.Collections.Generic;

using System.IO;

using UnityEngine;

using LitJson;
using Wundee.Stories;

namespace Wundee
{
	public class DataLoader
	{
		public DefinitionLoader<StoryDefinition, Story> storyDefinitions;
		public DefinitionLoader<StoryNodeDefinition, StoryNode> storyNodeDefinitions;

		public DefinitionLoader<EffectDefinition, EffectBase> effectDefinitions;


		public Dictionary<System.Type, System.Object> definitionLoaderMapper; 
		

		public DataLoader()
		{
			storyDefinitions = new DefinitionLoader<StoryDefinition, Story>(this);
			storyNodeDefinitions = new DefinitionLoader<StoryNodeDefinition, StoryNode>(this);

			effectDefinitions = new DefinitionLoader<EffectDefinition, EffectBase>(this);

			this.definitionLoaderMapper = new Dictionary<System.Type, object>();

			definitionLoaderMapper[typeof (StoryDefinition)] = storyDefinitions;
			definitionLoaderMapper[typeof (StoryNodeDefinition)] = storyNodeDefinitions;

			definitionLoaderMapper[typeof (EffectDefinition)] = effectDefinitions;

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

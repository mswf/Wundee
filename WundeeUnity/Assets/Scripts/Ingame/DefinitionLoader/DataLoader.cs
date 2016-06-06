

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public DefinitionLoader<StoryTriggerDefinition, StoryTrigger> storyTriggerDefinitions;

		public DefinitionLoader<EffectDefinition, BaseEffect> effectDefinitions;
		public DefinitionLoader<ConditionDefinition, BaseCondition> conditionDefinitions;
		

		public Dictionary<Type, object> definitionLoaderMapper; 		

		public DataLoader()
		{
			storyDefinitions = new DefinitionLoader<StoryDefinition, Story>(this);
			storyNodeDefinitions = new DefinitionLoader<StoryNodeDefinition, StoryNode>(this);
			storyTriggerDefinitions = new DefinitionLoader<StoryTriggerDefinition, StoryTrigger>(this);

			effectDefinitions = new DefinitionLoader<EffectDefinition, BaseEffect>(this);
			conditionDefinitions = new DefinitionLoader<ConditionDefinition, BaseCondition>(this);
			
			this.definitionLoaderMapper = new Dictionary<Type, object>();

			definitionLoaderMapper[typeof (StoryDefinition)] = storyDefinitions;
			definitionLoaderMapper[typeof (StoryNodeDefinition)] = storyNodeDefinitions;
			definitionLoaderMapper[typeof (StoryTriggerDefinition)] = storyTriggerDefinitions;

			definitionLoaderMapper[typeof (EffectDefinition)] = effectDefinitions;
			definitionLoaderMapper[typeof (ConditionDefinition)] = conditionDefinitions;
		}

		public JsonData GetJsonDataFromFile(string filePath)
		{
			var jsonString = File.ReadAllText(filePath);

			var reader = new JsonReader(jsonString)
			{
				AllowComments = true
			};

			return JsonMapper.ToObject(reader);
		}

		public string[] GetAllJsonFilePaths(string relativePath)
		{
			var fullPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "Definitions" +
			   Path.DirectorySeparatorChar + relativePath;

			return GetAllJsonFilePathsRecursively(fullPath).ToArray();
		}

		private static List<string> GetAllJsonFilePathsRecursively(string absolutePath)
		{
			var subDirectories = Directory.GetDirectories(absolutePath);

			var jsonFilePaths = new List<string>();

			for (int i = 0; i < subDirectories.Length; i++)
			{
				var directoryFilePath = subDirectories[i];

				var filesFromSubdirectory = GetAllJsonFilePathsRecursively(directoryFilePath);
				for (int j = 0; j < filesFromSubdirectory.Count; j++)
				{
					var jsonFilePath = filesFromSubdirectory[j];
					jsonFilePaths.Add(jsonFilePath);
				}
			}

			var files = Directory.GetFiles(absolutePath, "*.json");
			for (int i = 0; i < files.Length; i++)
			{
				var jsonFilePath = files[i];
				jsonFilePaths.Add(jsonFilePath);
			}

			return jsonFilePaths;
		}
	}
}

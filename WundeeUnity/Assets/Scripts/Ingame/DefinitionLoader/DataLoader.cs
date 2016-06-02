

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
			definitionLoaderMapper[typeof(ConditionDefinition)] = conditionDefinitions;
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

		[Conditional("DEBUG_CONTENT")]
		public static void VerifyKey(JsonData jsonData, string key, string ownerKey)
		{
			if (!jsonData.Keys.Contains(key))
			{
				Logger.Error("Missing key <b>" + key + "</b> in jsonData with key <b>" + ownerKey + "</b>", 1);
			}
		}

		[Conditional("DEBUG_CONTENT")]
		public static void VerifyArrayLength(System.Object[] array, int length, string ownerKey)
		{
			if (array.Length != length)
			{
				Logger.Error("Invalid length <b>" + length + "</b> in jsonData with key <b>" + ownerKey + "</b>", 1);
			}
		}

		[Conditional("DEBUG_CONTENT")]
		public static void VerifyMaxArrayLength(System.Object[] array, int length, string ownerKey)
		{
			if (array.Length > length)
			{
				Logger.Error("Invalid length <b>" + length + "</b> in jsonData with key <b>" + ownerKey + "</b>", 1);
			}
		}


		[Conditional("DEBUG_CONTENT")]
		public static void VerifyType(Dictionary<string, System.Type> typeDictionary, string type, string ownerKey)
		{
			if (!typeDictionary.ContainsKey(type))
			{
				Logger.Error("Invalid type <b>" + type + "</b> for effect with key <b>" + ownerKey + "</b>", 1);
			}
		}


	}
}

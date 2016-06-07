

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

using LitJson;
using Wundee.Stories;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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

		private Serializer jsonSerializer;
		private Deserializer yamlDeserializer;

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


			jsonSerializer = new Serializer(SerializationOptions.JsonCompatible);
			yamlDeserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());

			yamlDeserializer.TypeResolvers.Add(new ScalarYamlNodeTypeResolver());
		}

		public JsonData GetJsonDataFromFile(string filePath)
		{
			var jsonString = File.ReadAllText(filePath);

			return GetJsonDataFromString(jsonString);
		}

		public JsonData GetJsonDataFromString(string jsonString)
		{
			var reader = new JsonReader(jsonString)
			{
				AllowComments = true
			};

			return JsonMapper.ToObject(reader);
		}

		public JsonData GetJsonDataFromYamlFile(string filePath)
		{
			// convert string/file to YAML object
			var reader = new StreamReader(filePath);
			var yamlObject = yamlDeserializer.Deserialize(reader);
			reader.Close();

			var writer = new StringWriter();
			jsonSerializer.Serialize(writer, yamlObject);
			var jsonString = writer.ToString();
			writer.Close();

			return GetJsonDataFromString(jsonString);
		}

		public string[] GetAllContentFilePaths(string relativePath, string extension)
		{
			var fullPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "Definitions" +
			   Path.DirectorySeparatorChar + relativePath;

			return GetAllFilePathsRecursively(fullPath, "*." + extension).ToArray();
		}

		private static List<string> GetAllFilePathsRecursively(string absolutePath, string extension)
		{
			var subDirectories = Directory.GetDirectories(absolutePath);

			var jsonFilePaths = new List<string>();

			for (int i = 0; i < subDirectories.Length; i++)
			{
				var directoryFilePath = subDirectories[i];

				var filesFromSubdirectory = GetAllFilePathsRecursively(directoryFilePath, extension);
				for (int j = 0; j < filesFromSubdirectory.Count; j++)
				{
					var jsonFilePath = filesFromSubdirectory[j];
					jsonFilePaths.Add(jsonFilePath);
				}
			}

			var files = Directory.GetFiles(absolutePath, extension);
			for (int i = 0; i < files.Length; i++)
			{
				var jsonFilePath = files[i];
				jsonFilePaths.Add(jsonFilePath);
			}

			return jsonFilePaths;
		}
	}



	internal class ScalarYamlNodeTypeResolver : INodeTypeResolver
	{
		// Expressions taken from https://github.com/aaubry/YamlDotNet/blob/feat-schemas/YamlDotNet/Core/Schemas/JsonSchema.cs
		private Regex isIntRegex = new Regex(@"^-?(0|[1-9][0-9]*)$", RegexOptions.IgnorePatternWhitespace);
		private Regex isDoubleRegex = new Regex(@"^-?(0|[1-9][0-9]*)(\.[0-9]*)?([eE][-+]?[0-9]+)?$", RegexOptions.IgnorePatternWhitespace);

		public bool Resolve(NodeEvent nodeEvent, ref System.Type currentType)
		{
			var scalar = nodeEvent as Scalar;
			if ((scalar != null) && (scalar.Style == ScalarStyle.Plain))
			{
				if (isIntRegex.IsMatch(scalar.Value))
				{
					currentType = typeof(int);
					return true;
				}

				if (isDoubleRegex.IsMatch(scalar.Value))
				{
					currentType = typeof(double);
					return true;
				}
			}

			return false;
		}
	}
}

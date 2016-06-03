

using System;
using System.Collections.Generic;
using LitJson;
using Wundee.Stories;

using System.Diagnostics;


namespace Wundee
{
	public static class ContentHelper
	{
		public static Definition<ConcreteType> GetDefinition<DefinitionType, ConcreteType>(JsonData definitionData, string definitionKey, string definitionKeyExtension, int definitionCount = 0) where DefinitionType : Definition<ConcreteType>, new()
		{
			if (definitionData.IsString)
				return new DefinitionPromise<DefinitionType, ConcreteType>(definitionData.ToString());
			else
			{
				var definition = new DefinitionType();
				definition.ParseDefinition(definitionKey + definitionKeyExtension + definitionCount, definitionData);

				return definition;
			}
		}

		public static Definition<ConcreteType>[] GetDefinitions<DefinitionType, ConcreteType>(JsonData definitionData, string definitionKey, string definitionKeyExtension) where DefinitionType : Definition<ConcreteType>, new()
		{
			var returnValue = new Definition<ConcreteType>[definitionData.Count];

			for (int i = 0; i < definitionData.Count; i++)
			{
				returnValue[i] = GetDefinition<DefinitionType, ConcreteType>(
					definitionData[i], definitionKey, definitionKeyExtension, i
				);
			}
			
			return returnValue;
		}

		public static void ExecuteRewards<T>(this T[] rewards) where T : BaseReward
		{
			for (int i = 0; i < rewards.Length; i++)
			{
				rewards[i].Execute();
			}
		}

		public static void TickEffects<T>(this T[] effects) where T : BaseEffect
		{
			for (int i = 0; i < effects.Length; i++)
			{
				effects[i].Tick();
			}
		}

		public static bool CheckConditions<T>(this T[] conditions) where T : BaseCondition
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i].Check() == false)
					return false;
			}

			return true;
		}

		public static bool CheckOrConditions<T>(this T[] conditions) where T : BaseCondition
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i].Check() == true)
					return true;
			}

			return false;
		}

		public static T[] GetConcreteTypes<T>(this Definition<T>[] definitions, Object parent)
		{
			var concreteTypes = new T[definitions.Length];

			for (int i = 0; i < definitions.Length; i++)
			{
				concreteTypes[i] = definitions[i].GetConcreteType(parent);
			}

			return concreteTypes;
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


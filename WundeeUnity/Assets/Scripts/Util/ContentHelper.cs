
using System;
using System.Collections.Generic;
using LitJson;
using Wundee.Stories;

using System.Diagnostics;
using System.Linq;
using NCalc;

namespace Wundee
{
	public static class ContentHelper
	{
		public static Definition<TConcrete> GetDefinition<TDefinition, TConcrete>(JsonData definitionData, string definitionKey, string definitionKeyExtension, int definitionCount = 0) where TDefinition : Definition<TConcrete>, new()
		{
			if (definitionData.IsString)
				return new DefinitionPromise<TDefinition, TConcrete>(definitionData.ToString());
			else
			{
				var definition = new TDefinition();
				definition.ParseDefinition(definitionKey + definitionKeyExtension + definitionCount, definitionData);

				return definition;
			}
		}

		public static Definition<TConcrete>[] GetDefinitions<TDefinition, TConcrete>(JsonData definitionData, string definitionKey, string definitionKeyExtension) where TDefinition : Definition<TConcrete>, new()
		{
			var returnValue = new Definition<TConcrete>[definitionData.Count];

			for (int i = 0; i < definitionData.Count; i++)
			{
				returnValue[i] = GetDefinition<TDefinition, TConcrete>(
					definitionData[i], definitionKey, definitionKeyExtension, i
				);
			}
			
			return returnValue;
		}

		public static void ExecuteEffects<T>(this T[] effects) where T : BaseEffect
		{
			for (int i = 0; i < effects.Length; i++)
			{
				effects[i].ExecuteEffect();
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

		public static double ParseDouble(JsonData jsonData, string key, double defaultValue)
		{
			if (jsonData.Keys.Contains(key))
			{
				var value = jsonData[key];
				if (value.IsDouble)
					return (double) value;
				else if (value.IsInt)
					return (int) value;
				else
				{
					// TODO: lookup string in dictionary of defined constants

					var expression = new Expression(value.ToString());
					return (double) expression.Evaluate();
				}

			}

			return defaultValue;
		}

		public static int ParseInt(JsonData jsonData, string key, int defaultValue)
		{
			if (jsonData.Keys.Contains(key))
			{
				var value = jsonData[key];
				if (value.IsInt)
					return (int)value;
				else if (value.IsDouble)
				{
					Logger.Error("Tried parsing double as an int with key " + key + " and defaultvalue " + defaultValue);
					return (int)(double)value;
				}
				else
				{
					// TODO: lookup string in dictionary of defined constants

					var expression = new Expression(value.ToString());
					return (int)expression.Evaluate();
				}

			}

			return defaultValue;
		}

		public static int ParseNeedIndex(JsonData jsonData, string key)
		{
			if (jsonData.Keys.Contains(key))
			{
				var need = jsonData[key].ToString();

				for (int i = 0; i < GameData.Needs.BaseNeeds.Length; i++)
				{
					if (GameData.Needs.BaseNeeds[i] == need)
						return i;
				}
			}

			return -1;
		}

		public static Operator ParseOperator(JsonData jsonData, string key = D.OPERATOR)
		{
			return OperatorExtensions.stringToOperator[jsonData[key].ToString()];
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

		[Conditional("DEBUG_CONTENT")]
		public static void VerifyOperator(JsonData jsonData, string key, string ownerKey)
		{
			VerifyKey(jsonData, key, ownerKey);

			var enumValue = jsonData[key].ToString();

			if (!OperatorExtensions.stringToOperator.Keys.Contains(enumValue))
			{
				Logger.Error("Invalid operator <b>" + enumValue + "</b> in jsonData with key <b>" + ownerKey + "</b>", 1);
			}
		}

		[Conditional("DEBUG_CONTENT")]
		public static void VerifyDouble(JsonData jsonData, string key, string ownerKey)
		{
			VerifyKey(jsonData, key, ownerKey);

			var doubleValue = jsonData[key];

			if (!doubleValue.IsDouble && !doubleValue.IsInt)
			{
				Logger.Error("Invalid double <b>" + key + "</b>, " + doubleValue.ToString() + " in jsonData with key <b>" + ownerKey + "</b>", 1);
			}
		}
	}
}


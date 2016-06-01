using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using LitJson;

namespace Wundee
{
	public abstract class DefinitionBase<ConcreteType>
	{
		protected const string D_TYPE = "type";
		protected const string D_PARAMS = "params";


		public abstract void ParseDefinition(string definitionKey, JsonData jsonData);
		public abstract ConcreteType GetConcreteType(System.Object parent = null);

		[Conditional("DEBUG_CONTENT")]
		internal static void VerifyKey(JsonData jsonData, string key, string ownerKey)
		{
			if (!jsonData.Keys.Contains(key))
			{
				Logger.Error("Missing key <b>" + key + "</b> in jsonData with key <b>" + ownerKey + "</b>", 1);
			}
		}

		[Conditional("DEBUG_CONTENT")]
		internal static void VerifyType(Dictionary<string, System.Type> typeDictionary, string type, string ownerKey)
		{
			if (!typeDictionary.ContainsKey(type))
			{
				Logger.Error("Invalid type <b>" + type + "</b> for effect with key <b>" + ownerKey + "</b>", 1);
			}
		}
	}
}
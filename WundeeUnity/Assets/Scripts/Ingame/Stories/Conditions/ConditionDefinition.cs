using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public class ConditionDefinition : DefinitionBase<ConditionBase>
	{
		private ConditionBase masterCopy;

#if DEBUG_CONTENT
		public string definitionKey;
#endif

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
#if DEBUG_CONTENT
			this.definitionKey = definitionKey;
#endif

			VerifyKey(jsonData, D_TYPE, definitionKey);
			VerifyType(stringToType, jsonData[D_TYPE].ToString(), definitionKey);

			var type = stringToType[jsonData[D_TYPE].ToString()];
			masterCopy = System.Activator.CreateInstance(type) as ConditionBase;

			var paramsObject = jsonData[D_PARAMS];

			if (paramsObject != null)
			{
				masterCopy.ParseParams(paramsObject);
			}


		}

		public override ConditionBase GetConcreteType(object parent = null)
		{
			var newCondition = masterCopy.GetClone();

			newCondition.parentStoryNode = parent as StoryNode;
			if (newCondition.parentStoryNode == null)
			{
#if DEBUG_CONTENT
				Logger.Log("[ConditionDefinition] Invalid parent StoryNode provided for new Condition " + definitionKey);
#else
				Logger.Log("[ConditionDefinition] Invalid parent StoryNode provided for new Condition with type " + this.type.ToString());
#endif
			}

			return newCondition;
		}

		public static Dictionary<string, System.Type> stringToType
		{
			get
			{
				if (_stringToType == null)
				{
					_stringToType = new Dictionary<string, Type>();

					var effectTypes = Helper.ReflectiveEnumerator.GetEnumerableOfType<ConditionBase>();

					foreach (var effectType in effectTypes)
					{
						_stringToType[effectType.GetType().Name] = effectType.GetType();
					}

				}

				return _stringToType;
			}
		}

		private static Dictionary<string, System.Type> _stringToType;

	}
}
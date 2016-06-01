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

			VerifyKey(jsonData, D.TYPE, definitionKey);
			VerifyType(stringToType, jsonData[D.TYPE].ToString(), definitionKey);

			var type = stringToType[jsonData[D.TYPE].ToString()];
			masterCopy = System.Activator.CreateInstance(type) as ConditionBase;

			var paramsObject = jsonData[D.PARAMS];

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

		public static DefinitionBase<ConditionBase>[] ParseDefinitions(JsonData conditionData, string definitionKey = "C")
		{
			var tempConditionDefinitions = new List<DefinitionBase<ConditionBase>>();

			for (int i = 0; i < conditionData.Count; i++)
			{
				DefinitionBase<ConditionBase> conditionDefinition;
				var condition = conditionData[i];
				if (condition.IsString)
				{
					conditionDefinition = new DefinitionPromise<ConditionDefinition, ConditionBase>(condition.ToString());
				}
				else
				{
					conditionDefinition = new ConditionDefinition();
					conditionDefinition.ParseDefinition(definitionKey + "_CONDITION_" + i, conditionData[i]);
				}

				tempConditionDefinitions.Add(conditionDefinition);
			}

			return tempConditionDefinitions.ToArray();

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
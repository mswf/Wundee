
using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public class ConditionDefinition : StoryElementDefinition<BaseCondition>
	{


		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			base.ParseDefinition(definitionKey, jsonData);

		}
		
		public static Definition<BaseCondition>[] ParseDefinitions(JsonData conditionData, string definitionKey = "C")
		{
			var tempConditionDefinitions = new List<Definition<BaseCondition>>();

			for (int i = 0; i < conditionData.Count; i++)
			{
				Definition<BaseCondition> conditionDefinition;
				var condition = conditionData[i];
				if (condition.IsString)
				{
					conditionDefinition = new DefinitionPromise<ConditionDefinition, BaseCondition>(condition.ToString());
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
	}
}
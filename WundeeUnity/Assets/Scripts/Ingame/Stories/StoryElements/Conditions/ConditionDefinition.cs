
using LitJson;


namespace Wundee.Stories
{
	public class ConditionDefinition : StoryElementDefinition<BaseCondition>
	{
		public static Definition<BaseCondition>[] ParseDefinitions(JsonData conditionData, string definitionKey = "C")
		{
			var tempConditionDefinitions = new Definition<BaseCondition>[conditionData.Count];

			for (int i = 0; i < conditionData.Count; i++)
			{
				tempConditionDefinitions[i] = WundeeHelper.GetDefinition<ConditionDefinition, BaseCondition>(conditionData[i],
					definitionKey, KEYS.CONDITION, i);
			}

			return tempConditionDefinitions;
		}
	}
}
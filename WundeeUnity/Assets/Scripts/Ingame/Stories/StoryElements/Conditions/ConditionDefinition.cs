
using LitJson;


namespace Wundee.Stories
{
	public class ConditionDefinition : StoryElementDefinition<BaseCondition>
	{
		public static Definition<BaseCondition>[] ParseDefinitions(JsonData conditionData, string definitionKey)
		{
			return ContentHelper.GetDefinitions<ConditionDefinition, BaseCondition>
				(conditionData, definitionKey, KEYS.CONDITION);
		}
	}
}
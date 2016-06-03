

using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public class StoryTriggerDefinition : Definition<StoryTrigger>
	{
		private Definition<BaseCondition>[] _conditionDefinitions;
		private Definition<BaseReward>[] _rewardDefinitions;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			_conditionDefinitions = ConditionDefinition.ParseDefinitions(jsonData[D.CONDITIONS], definitionKey);
			_rewardDefinitions = RewardDefinition.ParseDefinitions(jsonData[D.REWARDS], definitionKey);
		}

		public override StoryTrigger GetConcreteType(object parent = null)
		{
			var newStoryTrigger = new StoryTrigger();

			newStoryTrigger.definition = this;
			
			newStoryTrigger.parentStoryNode = parent as StoryNode;
			if (newStoryTrigger.parentStoryNode == null)
				Logger.Log("[StoryTriggerDefinition] Invalid parent StoryNode provided for new StoryTrigger");

			newStoryTrigger.conditions = _conditionDefinitions.GetConcreteTypes(newStoryTrigger.parentStoryNode);
			newStoryTrigger.rewards = _rewardDefinitions.GetConcreteTypes(newStoryTrigger.parentStoryNode);

			return newStoryTrigger;
		}

		public static Definition<StoryTrigger>[] ParseDefinitions(JsonData storyTriggerData, string definitionKey = "ST")
		{
			return ContentHelper.GetDefinitions<StoryTriggerDefinition, StoryTrigger>
				(storyTriggerData, definitionKey, KEYS.STORYTRIGGER);
		}
	}
}


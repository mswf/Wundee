

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

			var conditions = new List<BaseCondition>();

			newStoryTrigger.conditions = new BaseCondition[_conditionDefinitions.Length];
			for (int i = 0; i < _conditionDefinitions.Length; i++)
				newStoryTrigger.conditions[i] = _conditionDefinitions[i].GetConcreteType(newStoryTrigger.parentStoryNode);

			newStoryTrigger.rewards = new BaseReward[_rewardDefinitions.Length];
			for (int i = 0; i < _rewardDefinitions.Length; i++)
				newStoryTrigger.rewards[i] = _rewardDefinitions[i].GetConcreteType(newStoryTrigger.parentStoryNode);

			return newStoryTrigger;
		}

		public static Definition<StoryTrigger>[] ParseDefinitions(JsonData effectData, string definitionKey = "ST")
		{
			var tempEffectDefinitions = new List<Definition<StoryTrigger>>();

			for (int i = 0; i < effectData.Count; i++)
			{
				Definition<StoryTrigger> storyDefinition;
				var effect = effectData[i];
				if (effect.IsString)
				{
					storyDefinition = new DefinitionPromise<StoryTriggerDefinition, StoryTrigger>(effect.ToString());
				}
				else
				{
					storyDefinition = new StoryTriggerDefinition();
					storyDefinition.ParseDefinition(definitionKey + "_STORYTRIGGER_" + i, effectData[i]);
				}

				tempEffectDefinitions.Add(storyDefinition);
			}


			return tempEffectDefinitions.ToArray();

		}
	}
}


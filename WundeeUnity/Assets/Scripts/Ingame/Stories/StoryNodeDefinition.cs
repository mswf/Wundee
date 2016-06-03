
using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public class StoryNodeDefinition : Definition<StoryNode>
	{
		private Definition<BaseEffect>[] _effectDefinitions;
		private Definition<StoryTrigger>[] _storyTriggerDefinitions;

		private Definition<BaseReward>[] _onStartRewardDefinitions;
		private Definition<BaseReward>[] _onCompleteRewardDefinitions;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			if (jsonData.Keys.Contains(D.EFFECTS))
				this._effectDefinitions = EffectDefinition.ParseDefinitions(jsonData[D.EFFECTS], definitionKey);
			else
				this._effectDefinitions = new Definition<BaseEffect>[0];

			if (jsonData.Keys.Contains(D.STORYTRIGGERS))
				this._storyTriggerDefinitions = StoryTriggerDefinition.ParseDefinitions(jsonData[D.STORYTRIGGERS], definitionKey);
			else
				this._storyTriggerDefinitions = new Definition<StoryTrigger>[0];

			if (jsonData.Keys.Contains(D.REWARDS_ON_START))
				this._onStartRewardDefinitions = RewardDefinition.ParseDefinitions(jsonData[D.REWARDS_ON_START], definitionKey);
			else
				this._onStartRewardDefinitions = new Definition<BaseReward>[0];

			if (jsonData.Keys.Contains(D.REWARDS_ON_COMPLETE))
				this._onCompleteRewardDefinitions = RewardDefinition.ParseDefinitions(jsonData[D.REWARDS_ON_COMPLETE], definitionKey);
			else
				this._onCompleteRewardDefinitions = new Definition<BaseReward>[0];
		}

		public override StoryNode GetConcreteType(System.Object parent)
		{
			var newStoryNode = new StoryNode();
			newStoryNode.definition = this;

			newStoryNode.parentStory = parent as Story;
			if (newStoryNode.parentStory == null)
				Logger.Log("[StoryNodeDefinition] Invalid parent Story provided for new StoryNode");

			newStoryNode.effects = _effectDefinitions.GetConcreteTypes(newStoryNode);
			newStoryNode.storyTriggers = _storyTriggerDefinitions.GetConcreteTypes(newStoryNode);
			newStoryNode.onStartRewards = _onStartRewardDefinitions.GetConcreteTypes(newStoryNode);
			newStoryNode.onCompleteRewards = _onCompleteRewardDefinitions.GetConcreteTypes(newStoryNode);

			return newStoryNode;
		}
	}
}

using LitJson;


namespace Wundee.Stories
{
	public class RewardDefinition : StoryElementDefinition<BaseReward>
	{
		public static Definition<BaseReward>[] ParseDefinitions(JsonData rewardData, string definitionKey = "R")
		{
			return ContentHelper.GetDefinitions<RewardDefinition, BaseReward>
				(rewardData, definitionKey, KEYS.REWARD);
		}
	}
}
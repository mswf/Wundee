
using LitJson;


namespace Wundee.Stories
{
	public class RewardDefinition : StoryElementDefinition<BaseReward>
	{
		public static Definition<BaseReward>[] ParseDefinitions(JsonData rewardData, string definitionKey = "R")
		{
			var tempRewardDefinitions = new Definition<BaseReward>[rewardData.Count];

			for (int i = 0; i < rewardData.Count; i++)
			{
				tempRewardDefinitions[i] = WundeeHelper.GetDefinition<RewardDefinition, BaseReward>(rewardData[i],
					definitionKey, KEYS.REWARD, i);
			}

			return tempRewardDefinitions;
		}
	}
}
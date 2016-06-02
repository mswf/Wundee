
using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public class RewardDefinition : StoryElementDefinition<BaseReward>
	{
		public static Definition<BaseReward>[] ParseDefinitions(JsonData rewardData, string definitionKey = "R")
		{
			var tempEffectDefinitions = new List<Definition<BaseReward>>();

			for (int i = 0; i < rewardData.Count; i++)
			{
				Definition<BaseReward> rewardDefinition;
				var reward = rewardData[i];
				if (reward.IsString)
				{
					rewardDefinition = new DefinitionPromise<RewardDefinition, BaseReward>(reward.ToString());
				}
				else
				{
					rewardDefinition = new RewardDefinition();
					rewardDefinition.ParseDefinition(definitionKey + "_REWARD_" + i, reward);
				}

				tempEffectDefinitions.Add(rewardDefinition);

			}


			return tempEffectDefinitions.ToArray();
		}
	}
}
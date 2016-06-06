
using System;
using LitJson;

namespace Wundee.Stories
{
	public abstract class BaseReward : StoryElement<BaseReward>
	{
		public abstract void Execute();
	}

	public abstract class CollectionReward : BaseReward
	{
		protected Definition<BaseReward>[] _rewardDefinitions;
		protected BaseReward[] rewards;
	}

	public class PrintReward : BaseReward
	{
		public string messageToPrint;

		public override void ParseParams(JsonData parameters)
		{
			messageToPrint = parameters.ToString();
		}

		public override void Execute()
		{
			Logger.Print(messageToPrint);
		}
	}

	public class RandomReward : CollectionReward
	{
		public override void ParseParams(JsonData parameters)
		{
			_rewardDefinitions = RewardDefinition.ParseDefinitions(parameters[D.REWARDS], definition.definitionKey);

		}

		public override BaseReward GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as RandomReward;

			retValue.rewards = _rewardDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override void Execute()
		{
			var randomNumber = R.generator.Next(rewards.Length);
			rewards[randomNumber].Execute();
		}
	}

	public class TestReward : BaseReward
	{
		public override void ParseParams(JsonData parameters)
		{

		}

		public override void Execute()
		{
		
			//Logger.Print("Executing TestReward");
		}
	}

	
}


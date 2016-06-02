

namespace Wundee.Stories
{
	public class StoryNode
	{
		public enum StoryNodeState
		{
			Running,
			Finished
		}

		public StoryNodeDefinition definition;
		public Story parentStory;
		
		public BaseEffect[] effects;

		public BaseReward[] onStartRewards;
		public BaseReward[] onCompleteRewards;

		public StoryTrigger[] storyTriggers;

		public StoryNodeState Tick()
		{
			for (var i = 0; i < effects.Length; i++)
			{
				effects[i].Tick();
			}

			for (int i = 0; i < storyTriggers.Length; i++)
			{
				if (storyTriggers[i].IsTriggered())
				{
					return StoryNodeState.Finished;	
				}
			}

			return StoryNodeState.Running;
		}

		public void OnStart()
		{
			for (int i = 0; i < onStartRewards.Length; i++)
				onStartRewards[i].Execute();
		}

		public void OnComplete()
		{
			for (int i = 0; i < onCompleteRewards.Length; i++)
				onCompleteRewards[i].Execute();
		}
	}
}


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
			for (int i = 0; i < storyTriggers.Length; i++)
			{
				if (storyTriggers[i].CheckTrigger())
				{
					return StoryNodeState.Finished;
				}
			}

			effects.TickEffects();
			
			return StoryNodeState.Running;
		}

		public void OnStart()
		{
			onStartRewards.ExecuteRewards();
		}

		public void OnComplete()
		{
			onCompleteRewards.ExecuteRewards();
		}
	}
}
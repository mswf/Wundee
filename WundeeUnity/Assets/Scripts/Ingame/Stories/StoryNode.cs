

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

		public string testProperty;
		public BaseEffect[] baseEffects;

		public StoryTrigger[] storyTriggers;

		public StoryNodeState Tick()
		{
			for (var i = 0; i < baseEffects.Length; i++)
			{
				baseEffects[i].Tick();
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

		public void OnComplete()
		{
			
		}
	}
}
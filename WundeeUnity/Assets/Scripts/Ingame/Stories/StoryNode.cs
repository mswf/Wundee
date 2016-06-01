

namespace Wundee.Stories
{
	public class StoryNode
	{
		public enum State
		{
			Running,
			Suspended,
			Finished
		}

		public StoryNodeDefinition definition;
		public Story parentStory;

		public string testProperty;
		public BaseEffect[] baseEffects;

		public State Tick()
		{
			for (var i = 0; i < baseEffects.Length; i++)
			{
				baseEffects[i].Tick();
			}

			return State.Running;
		}
	}
}


using System;

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
		public EffectBase[] effects;

		public State Tick()
		{
			for (var i = 0; i < effects.Length; i++)
			{
				effects[i].Tick();
			}

			return State.Running;
		}
	}
}
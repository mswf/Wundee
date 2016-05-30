

using System.Collections.Generic;

namespace Wundee.Stories
{

	public class StoryHolder
	{
		private Settlement owner;

		private List<Story> activeStories; 

		public StoryHolder(Settlement owner)
		{
			this.owner = owner;
			activeStories = new List<Story>();
		}

		public void Tick()
		{
			for (int index = 0; index < activeStories.Count; index++)
			{
				activeStories[index].Tick();
			}
		}

		public void AddStory(string definitionKey)
		{
			var storyDefinition = Game.instance.definitions.storyDefinitions[definitionKey];

			var newStory = storyDefinition.GetConcreteType(owner);

			activeStories.Add(newStory);

		}
	}


}
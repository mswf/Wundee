namespace Wundee.Stories
{
	public class Story
	{
		public StoryNode currentNode;
		public StoryDefinition definition;

		public Settlement parentSettlement;

		public void Tick()
		{

			if (currentNode != null)
			{
				var result = currentNode.Tick();

				if (result == StoryNode.StoryNodeState.Finished)
				{
					currentNode = null;
				}
			}

		}
	}
}
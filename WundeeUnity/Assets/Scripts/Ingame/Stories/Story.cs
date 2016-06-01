namespace Wundee.Stories
{
	public class Story
	{
		public StoryNode currentNode;
		public StoryDefinition definition;

		public Settlement parentSettlement;

		public void Tick()
		{
			var result = currentNode.Tick();
		}
	}
}
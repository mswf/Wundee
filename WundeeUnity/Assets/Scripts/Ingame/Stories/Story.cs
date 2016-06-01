

namespace Wundee.Stories
{
	public class Story
	{
		public StoryDefinition definition;

		public StoryNode currentNode;

		public Settlement parentSettlement;

		public void Tick()
		{
			var result = currentNode.Tick();

			
		}
	}
}
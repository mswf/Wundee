using System.Collections;
using LitJson;


namespace Wundee.Stories
{
	public class StoryNodeDefinition : DefinitionBase<StoryNode>
	{
		private string testProperty;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			testProperty = jsonData["property"].ToString();

			//throw new System.NotImplementedException();
		}

		public override StoryNode GetConcreteType()
		{
			var newNode = new StoryNode();

			newNode.testProperty = this.testProperty;

			return newNode;
		}
	}
}
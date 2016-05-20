
using System.Collections;
using LitJson;


namespace Wundee.Stories
{
	public class StoryDefinition : DefinitionBase<Story>
	{
		private static string D_START_NODE = "startNode";

		private DefinitionBase<StoryNode> _startNode;	


		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			var startNode = jsonData[D_START_NODE];
			if (startNode.IsString)
				this._startNode = new DefinitionPromise<StoryNodeDefinition, StoryNode>(startNode.ToString());
			else
			{
				this._startNode = new StoryNodeDefinition();
				_startNode.ParseDefinition(definitionKey + "_NODE", startNode);
			}
			

			//throw new System.NotImplementedException();
		}

		public override Story GetConcreteType()
		{

			var newStory = new Story();

			newStory.startNode = _startNode.GetConcreteType();

			return newStory;
		}
	}

}


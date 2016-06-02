
using LitJson;


namespace Wundee.Stories
{
	public class StoryDefinition : Definition<Story>
	{
		private const string D_START_NODE = "startNode";

		private Definition<StoryNode> _startNode;	


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

		public override Story GetConcreteType(System.Object parent = null)
		{

			var newStory = new Story();

			newStory.definition = this;


			newStory.parentSettlement = parent as Settlement;
			if (newStory.parentSettlement == null)
				Logger.Log("[StoryDefinition] Invalid parent Settlement provided for new Story");

			newStory.SetCurrentStoryNode(_startNode.GetConcreteType(newStory));

			return newStory;
		}
	}

}


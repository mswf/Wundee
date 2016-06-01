using LitJson;


namespace Wundee.Stories
{
	public class StoryNodeDefinition : Definition<StoryNode>
	{
		private const string D_EFFECTS = "effects";

		private string testProperty;

		private Definition<BaseEffect>[] _effectDefinitions;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			testProperty = jsonData["property"].ToString();

			if (jsonData.Keys.Contains(D_EFFECTS))
				this._effectDefinitions = EffectDefinition.ParseDefinitions(jsonData[D_EFFECTS], definitionKey);
			else
				this._effectDefinitions = new Definition<BaseEffect>[0];

		}

		public override StoryNode GetConcreteType(System.Object parent)
		{
			var newNode = new StoryNode();

			newNode.definition = this;

			newNode.parentStory = parent as Story;
			if (newNode.parentStory == null)
				Logger.Log("[StoryNodeDefinition] Invalid parent Story provided for new StoryNode");

			newNode.testProperty = this.testProperty;


			if (_effectDefinitions != null)
			{
				newNode.baseEffects = new BaseEffect[_effectDefinitions.Length];

				for (int i = 0; i < _effectDefinitions.Length; i++)
				{
					newNode.baseEffects[i] = _effectDefinitions[i].GetConcreteType(newNode);
				}
			}
			else
			{
				newNode.baseEffects = new BaseEffect[0];
			}


			return newNode;
		}
	}
}
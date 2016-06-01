using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public class StoryNodeDefinition : DefinitionBase<StoryNode>
	{
		private const string D_EFFECTS = "effects";

		private string testProperty;

		private DefinitionBase<EffectBase>[] effectDefinitions;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			testProperty = jsonData["property"].ToString();

			if (jsonData.Keys.Contains(D_EFFECTS))
				this.effectDefinitions = EffectDefinition.ParseDefinitions(jsonData[D_EFFECTS], definitionKey);
			else
				this.effectDefinitions = new DefinitionBase<EffectBase>[0];

		}

		public override StoryNode GetConcreteType(System.Object parent)
		{
			var newNode = new StoryNode();

			newNode.definition = this;

			newNode.parentStory = parent as Story;
			if (newNode.parentStory == null)
				Logger.Log("[StoryNodeDefinition] Invalid parent Story provided for new StoryNode");

			newNode.testProperty = this.testProperty;


			if (effectDefinitions != null)
			{
				newNode.effects = new EffectBase[effectDefinitions.Length];

				for (int i = 0; i < effectDefinitions.Length; i++)
				{
					newNode.effects[i] = effectDefinitions[i].GetConcreteType(newNode);
				}
			}
			else
			{
				newNode.effects = new EffectBase[0];
			}


			return newNode;
		}
	}
}
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

			var tempEffectDefinitions = new List<DefinitionBase<EffectBase>>();

			if (jsonData.Keys.Contains(D_EFFECTS))
			{
				var effects = jsonData[D_EFFECTS];

				for (int i = 0; i < effects.Count; i++)
				{
					DefinitionBase<EffectBase> effectDefinition;
					var effect = effects[i];
					if (effect.IsString)
					{
						effectDefinition = new DefinitionPromise<EffectDefinition, EffectBase>(effect.ToString());
					}
					else
					{
						effectDefinition = new EffectDefinition();
						effectDefinition.ParseDefinition(definitionKey + "_EFFECT_" + i, effects[i]);
					}

					tempEffectDefinitions.Add(effectDefinition);

				}

				this.effectDefinitions = tempEffectDefinitions.ToArray();

			}




			//throw new System.NotImplementedException();
		}

		public override StoryNode GetConcreteType(System.Object parent)
		{
			var newNode = new StoryNode();

			newNode.definition = this;

			newNode.parent = parent as Story;
			if (newNode.parent == null)
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
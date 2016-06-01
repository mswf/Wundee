
using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public class EffectDefinition : StoryElementDefinition<BaseEffect>
	{
		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			base.ParseDefinition(definitionKey, jsonData);
		}
		
		public static Definition<BaseEffect>[] ParseDefinitions(JsonData effectData, string definitionKey = "E")
		{
			var tempEffectDefinitions = new List<Definition<BaseEffect>>();
			
			for (int i = 0; i < effectData.Count; i++)
			{
				Definition<BaseEffect> effectDefinition;
				var effect = effectData[i];
				if (effect.IsString)
				{
					effectDefinition = new DefinitionPromise<EffectDefinition, BaseEffect>(effect.ToString());
				}
				else
				{
					effectDefinition = new EffectDefinition();
					effectDefinition.ParseDefinition(definitionKey + "_EFFECT_" + i, effectData[i]);
				}

				tempEffectDefinitions.Add(effectDefinition);

			}
			

			return tempEffectDefinitions.ToArray();

		}
		
	}
}
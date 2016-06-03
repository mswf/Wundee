
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
		
		public static Definition<BaseEffect>[] ParseDefinitions(JsonData effectData, string definitionKey)
		{
			return ContentHelper.GetDefinitions<EffectDefinition, BaseEffect>
				(effectData, definitionKey, KEYS.EFFECT);

		}
		
	}
}
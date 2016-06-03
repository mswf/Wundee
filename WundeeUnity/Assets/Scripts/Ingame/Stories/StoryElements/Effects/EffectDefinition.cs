
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
			var tempEffectDefinitions = new Definition<BaseEffect>[effectData.Count];
			
			for (int i = 0; i < effectData.Count; i++)
			{
				tempEffectDefinitions[i] = WundeeHelper.GetDefinition<EffectDefinition, BaseEffect>(effectData[i],
					definitionKey, KEYS.EFFECT, i);
			}
			
			return tempEffectDefinitions;
		}
		
	}
}
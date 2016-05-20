using UnityEngine;
using System.Collections;
using LitJson;

namespace Wundee.Stories
{
	public class EffectDefinition : DefinitionBase<EffectBase>
	{
		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			throw new System.NotImplementedException();
		}

		public override EffectBase GetConcreteType()
		{
			throw new System.NotImplementedException();
		}
	}
}
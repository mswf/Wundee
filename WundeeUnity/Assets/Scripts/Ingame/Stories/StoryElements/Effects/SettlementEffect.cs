using UnityEngine;
using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public interface ISettlementEffect
	{
		
	}

	public abstract class SettlementEffect : Effect, ISettlementEffect
	{
		private Definition<Condition>[] _conditionDefinitions;
		private Definition<Effect>[] _effectDefinitions;  

		public override void ParseParams(JsonData parameters)
		{
			var keys = parameters.Keys;

			if (keys.Contains(D.CONDITIONS))
				_conditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS], definition.definitionKey);
			else
				_conditionDefinitions = new Definition<Condition>[0];

			if (keys.Contains(D.EFFECTS))
				_effectDefinitions = EffectDefinition.ParseDefinitions(parameters[D.EFFECTS], definition.definitionKey);
			else
				_effectDefinitions = new Definition<Effect>[0];
		}

		protected void ExecuteOnSettlements(List<Settlement> settlements)
		{
			for (int i = 0; i < settlements.Count; i++)
			{
				if (settlements[i].CheckConditionFromDefinition(ref _conditionDefinitions))
					settlements[i].ExecuteEffectFromDefinition(ref _effectDefinitions);
			}
		}
	}

	public class NearbySettlementEffect : SettlementEffect
	{
		private float _effectRange = 50f;

		public override void ParseParams(JsonData parameters)
		{
			base.ParseParams(parameters);

			_effectRange = ContentHelper.ParseFloat(parameters, D.RANGE, _effectRange);
		}

		public override void ExecuteEffect()
		{
			

		}
	}

}


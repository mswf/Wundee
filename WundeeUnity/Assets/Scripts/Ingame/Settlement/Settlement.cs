
using System.Collections.Generic;
using UnityEngine.Networking.Match;
using Wundee.Stories;

namespace Wundee
{
	public class Settlement : IHabitatOccupant
	{
		public ActiveSettlement activeSettlement
		{
			get
			{
				if (_activeSettlement == null)
					_activeSettlement = new ActiveSettlement(this);
				
				return _activeSettlement;
			}
		}

		private readonly WorldSettlement _worldSettlement;
		private ActiveSettlement _activeSettlement;
		
		public readonly Habitat habitat;
		public readonly StoryHolder storyHolder;

		public Need[] needs;
		public Dictionary<string, Need> needsDictionary;

		private double _timeOfPreviousUpdate;

		private HashSet<ushort> _settlementFlags = new HashSet<ushort>();  

		public Settlement(Habitat habitat)
		{
			this.habitat = habitat;

			this.storyHolder = new StoryHolder(this);

			this._worldSettlement = new WorldSettlement(this);
			// The active settlement is only generated when needed	
			this._activeSettlement = null;
			
			var needParams = Game.instance.@params.needParams;

			this.needs = new Need[needParams.needs.Length];
			this.needsDictionary = new Dictionary<string, Need>(needParams.needs.Length);

			for (int i = 0; i < needParams.needs.Length; i++)
			{
				needs[i] = new Need(this, needParams.needs[i]);
				needsDictionary[needParams.needs[i]] = needs[i];
			}
		}
		
		public void Tick()
		{
			var deltaTime = Time.fixedGameTime - _timeOfPreviousUpdate;

			storyHolder.Tick();

			// Advance high level simulation
			_worldSettlement.Tick(deltaTime);

			if (_activeSettlement != null)
				_activeSettlement.Tick(deltaTime);

			_timeOfPreviousUpdate = Time.fixedGameTime;
		}

		public void ExecuteEffectFromDefinition(ref Definition<Effect>[] effectDefinitions)
		{
			var effects = effectDefinitions.GetConcreteTypes(storyHolder.lifeStoryNode);
			effects.ExecuteEffects();
		}

		public bool CheckConditionFromDefinition(ref Definition<Condition>[] conditionDefinitions)
		{
			var conditions = conditionDefinitions.GetConcreteTypes(storyHolder.lifeStoryNode);
			return conditions.CheckConditions();
		}

		public void AddFlag(ushort flag)
		{
			_settlementFlags.Add(flag);
		}

		public void RemoveFlag(ushort flag)
		{
			_settlementFlags.RemoveWhere((ushort flagToTest) =>
			{
				if (flagToTest == flag)
					return true;

				return false;
			});
		}

		public bool HasFlag(ushort flag)
		{
			return _settlementFlags.Contains(flag);
		}
	}

}

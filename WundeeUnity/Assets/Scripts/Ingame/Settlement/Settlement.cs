
using System.Collections.Generic;
using Wundee.Stories;

namespace Wundee
{
	public class Settlement
	{
		private WorldSettlement _worldSettlement;
		private ActiveSettlement _activeSettlement;

		private double _timeOfPreviousUpdate;


		public readonly Habitat habitat;

		public Dictionary<string, Need> needs;

		public readonly StoryHolder storyHolder;  

		public Settlement(Habitat habitat)
		{
			this.storyHolder = new StoryHolder(this);
			this._worldSettlement = new WorldSettlement(this);
			
			// The active settlement is only generated when needed	
			this._activeSettlement = null;

			this.habitat = habitat;

			this.needs = new Dictionary<string, Need>(GameData.Needs.BaseNeeds.Length);

			for (int i = 0; i < GameData.Needs.BaseNeeds.Length; i++)
			{
				var newNeed = new Need(this, GameData.Needs.BaseNeeds[i]);

				needs[GameData.Needs.BaseNeeds[i]] = newNeed;
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






	}

}

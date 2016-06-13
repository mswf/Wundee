
using System.Collections.Generic;
using UnityEngine.Networking.Match;
using Wundee.Stories;

namespace Wundee
{
	public class Settlement
	{
		private WorldSettlement _worldSettlement;
		private ActiveSettlement _activeSettlement;

		private double _timeOfPreviousUpdate;


		public readonly Habitat habitat;

		public Need[] needs;
		public Dictionary<string, Need> needsDictionary; 

		public readonly StoryHolder storyHolder;  

		public Settlement(Habitat habitat)
		{
			this.storyHolder = new StoryHolder(this);
			this._worldSettlement = new WorldSettlement(this);
			
			// The active settlement is only generated when needed	
			this._activeSettlement = null;

			this.habitat = habitat;

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






	}

}

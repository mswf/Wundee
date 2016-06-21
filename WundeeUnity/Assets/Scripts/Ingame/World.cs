using System.Collections.Generic;

using Wundee.Generation;

namespace Wundee
{
	public class World
	{
		private List<Habitat> _habitats;
		private List<Settlement> _settlements;

		public List<Habitat> habitats 
		{
			get { return _habitats; }
		}

		public List<Settlement> settlements
		{
			get { return _settlements; }
		}

		public World()
		{
			this._habitats = new List<Habitat>();
			this._settlements = new List<Settlement>();
		}

		public void Tick()
		{
			var numSettlements = _settlements.Count;

			for (int i = 0; i < numSettlements; i++)
			{
				settlements[i].Tick();
			}
		}
		
		public void GenerateMap()
		{
			//throw new System.NotImplementedException();
		}

		public void GenerateHabitats()
		{
			var gameParams = Game.instance.@params;

			///*

			var distributedPoints = UniformPoissonDiskSampler
				.SampleRectangle(new Vector2(-gameParams.worldWidth / 2f, -gameParams.worldHeight / 2f), 
								 new Vector2( gameParams.worldWidth / 2f,  gameParams.worldHeight / 2f), 
								 gameParams.habitatMinDistance)
				.ToArray();
			//*/

			/*
			var distributedPoints = new Vector2[]
			{
				new Vector2(0, 0)	
			};
			*/

			// Cap the number of points we use in case something explodes
			var numberOfSettlements = MathHelper.Min(distributedPoints.Length, gameParams.habitatCap);

			for (int i = 0; i < numberOfSettlements; i++)
			{
				var newHabitat = new Habitat
				{
					position = distributedPoints[i]
				};
				
				_habitats.Add(newHabitat);
			}
		}

		public void GenerateSettlements()
		{
			var settlementDefinition = Game.instance.definitions.settlementDefinitions["SETTLEMENT_DEFAULT_01"];

			var random = new System.Random();

			foreach (var habitat in _habitats)
			{
				if (random.Next(0, 100) > 50 || true)
				{
					var newSettlement = settlementDefinition.GetConcreteType(habitat);
					
					_settlements.Add(newSettlement);
				}
			}
		}
	}
}


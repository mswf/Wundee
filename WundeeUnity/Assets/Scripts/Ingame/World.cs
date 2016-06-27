using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Wundee.Generation;

namespace Wundee
{
	public class World
	{
		public readonly Rectangle worldBounds;

		private List<Entity> _entities; 

		private List<Habitat> _habitats;
		private List<Settlement> _settlements;

		public FarseerPhysics.Dynamics.World physicsWorld;

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
			physicsWorld = new FarseerPhysics.Dynamics.World(new Vector2(0,0));

			this._entities = new List<Entity>();
			this._habitats = new List<Habitat>();
			this._settlements = new List<Settlement>();


			var gameParams = Game.instance.@params;

			worldBounds = new Rectangle(-gameParams.worldWidth / 2, -gameParams.worldHeight / 2,
										 gameParams.worldWidth    ,  gameParams.worldHeight);
		}

		public void Tick()
		{
			var numSettlements = _settlements.Count;

			for (int i = 0; i < numSettlements; i++)
			{
				settlements[i].Tick();
			}

			physicsWorld.Step(Time.fixedDT);

			var numEntities = _entities.Count;
			for (int i = 0; i < numEntities; i++)
			{
				_entities[i].LateUpdate();
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
				.SampleRectangle(new Vector2(worldBounds.X, worldBounds.Y), 
								 new Vector2(worldBounds.Right,  worldBounds.Bottom), 
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
				var newHabitat = new Habitat(this, distributedPoints[i]);
				
				_habitats.Add(newHabitat);
				_entities.Add(newHabitat);
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


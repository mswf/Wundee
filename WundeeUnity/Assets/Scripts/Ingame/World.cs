using System;
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

		private HashSet<ushort> _worldFlags = new HashSet<ushort>();


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

			worldBounds = new Rectangle(0,0,
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
					habitat.occupant = newSettlement;

					_settlements.Add(newSettlement);
				}
			}
		}

		public Vector2 GetShortestLineTo(Vector2 originPos, Vector2 targetPos)
		{
			float xDir;
			float yDir;

			var originToTargetX = targetPos.X - originPos.X;
			if (originPos.X < targetPos.X)
			{
				var originToLeft = Math.Abs(originPos.X - worldBounds.Left);
				var targetToRight = Math.Abs(targetPos.X - worldBounds.Right);

				if (originToLeft + targetToRight > Math.Abs(originToTargetX))
					xDir = originToTargetX;
				else
					xDir = (originToLeft + targetToRight)*Math.Sign(originToTargetX)*-1f;
			}
			else
			{
				var targetToLeft = Math.Abs(targetPos.X - worldBounds.Left);
				var originToRight = Math.Abs(originPos.X - worldBounds.Right);

				if (targetToLeft + originToRight > Math.Abs(originToTargetX))
					xDir = originToTargetX;
				else
					xDir = (targetToLeft + originToRight) * Math.Sign(originToTargetX)*-1f;
			}

			var originToTargetY = targetPos.Y - originPos.Y;
			if (originPos.Y < targetPos.Y)
			{
				var originToTop = Math.Abs(originPos.Y - worldBounds.Top);
				var targetToBottom = Math.Abs(targetPos.Y - worldBounds.Bottom);

				if (originToTop + targetToBottom > Math.Abs(originToTargetY))
					yDir = originToTargetY;
				else
					yDir = (originToTop + targetToBottom) * Math.Sign(originToTargetY)*-1f;
			}
			else
			{
				var targetToTop = Math.Abs(targetPos.Y - worldBounds.Top);
				var originToBottom = Math.Abs(originPos.Y - worldBounds.Bottom);

				if (targetToTop + originToBottom > Math.Abs(originToTargetY))
					yDir = originToTargetY;
				else
					yDir = (targetToTop + originToBottom) * Math.Sign(originToTargetY)*-1f;
			}

			return new Vector2(xDir, yDir);
		}

		public void AddFlag(ushort flag)
		{
			_worldFlags.Add(flag);
		}

		public void RemoveFlag(ushort flag)
		{
			_worldFlags.RemoveWhere((ushort flagToTest) =>
			{
				if (flagToTest == flag)
					return true;

				return false;
			});
		}

		public bool HasFlag(ushort flag)
		{
			return _worldFlags.Contains(flag);
		}
	}
}


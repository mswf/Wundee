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

		public void GenerateMap()
		{
			//throw new System.NotImplementedException();
		}

		public void GenerateHabitats()
		{
			var distributedPoints = UniformPoissonDiskSampler.SampleRectangle(new Vector2(-GameParams.WORLD_WIDHT/2f, -GameParams.WORLD_HEIGHT/2f), new Vector2(GameParams.WORLD_WIDHT/2f, GameParams.WORLD_HEIGHT/2f),
				220f).ToArray();


			// Cap the number of points we use in case something explodes
			var numberOfSettlements = MathHelper.Min(distributedPoints.Length, 500);

			for (int i = 0; i < numberOfSettlements; i++)
			{
				var newHabitat = new Habitat();

				newHabitat.position = distributedPoints[i];

				_habitats.Add(newHabitat);
			}
		}

		public void GenerateSettlements()
		{
			var random = new System.Random();

			foreach (var habitat in _habitats)
			{
				if (random.Next(0, 100) > 50)
				{
					var newSettlement = new Settlement(habitat);

					_settlements.Add(newSettlement);
				}
			}
		}
	}
}


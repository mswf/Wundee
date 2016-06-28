

using Microsoft.Xna.Framework;

namespace Wundee
{
	public class Habitat : Entity
	{
		public IHabitatOccupant occupant;

		public bool IsOccupied()
		{
			if (occupant != null)
				return true;

			return false;
		}

		public Habitat(World world, Vector2 startPosition) : base(world, startPosition)
		{

		}
	}

	public interface IHabitatOccupant
	{
		
	}
}

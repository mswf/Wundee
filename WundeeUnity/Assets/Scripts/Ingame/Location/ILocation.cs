

using Microsoft.Xna.Framework;

namespace Wundee.Locations
{
	public interface ILocation
	{
		Vector2 GetPosition();
		Vector2 GetDirection(Vector2 origin);
	}



	public class PositionLocation : ILocation
	{
		private Vector2 position;

		public PositionLocation(float x, float y)
		{
			position = new Vector2(x, y);
		}

		public Vector2 GetPosition()
		{
			return position;
		}

		public Vector2 GetDirection(Vector2 origin)
		{
			return Game.instance.world.GetShortestLineTo(origin, GetPosition());
		}
	}

	public class DirectionalLocation : ILocation
	{
		private Vector2 direction;

		public DirectionalLocation(float x, float y)
		{
			direction = new Vector2(x, y);
		}

		public Vector2 GetPosition()
		{
			return direction;
		}

		public Vector2 GetDirection(Vector2 origin)
		{
			return direction;
		}
	}

	public class TargetEntityLocation : ILocation
	{
		private Entity target;

		public TargetEntityLocation(Entity target)
		{
			this.target = target;
		}

		public Vector2 GetPosition()
		{
			return target.position;
		}

		public Vector2 GetDirection(Vector2 origin)
		{
			return Game.instance.world.GetShortestLineTo(origin, GetPosition());
			//return GetPosition() - origin;
		}
	}

}


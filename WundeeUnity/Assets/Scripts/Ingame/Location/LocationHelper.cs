

using LitJson;
using Microsoft.Xna.Framework;

namespace Wundee.Locations
{
	public static class LocationHelper
	{

		public static ILocation ParseLocation(JsonData data)
		{
			var keys = data.Keys;

			if (keys.Contains(D.X) && keys.Contains(D.Y))
			{
				var xPos = ContentHelper.ParseFloat(data, D.X, 0f);
				var yPos = ContentHelper.ParseFloat(data, D.Y, 0f);

				return new PositionLocation(xPos, yPos);
			}

			if (keys.Contains(D.X_DIR) && keys.Contains(D.Y_DIR))
			{
				var xDir = ContentHelper.ParseFloat(data, D.X_DIR, 0f);
				var yDir = ContentHelper.ParseFloat(data, D.Y_DIR, 0f);

				return new DirectionalLocation(xDir, yDir);
			}

			if (keys.Contains(D.HAS_FLAG))
			{
				var flag = ContentHelper.ParseSettlementFlag(data, D.HAS_FLAG);

				var settlements = Game.instance.world.settlements;
				var numSettlements = settlements.Count;

				for (int i = 0; i < numSettlements; i++)
				{
					if (settlements[i].HasFlag(flag))
					{
						return new TargetEntityLocation(settlements[i].habitat);
					}
				}
			}



			Logger.Error("Could not parse a valid Location from data");
			return new PositionLocation(0f,0f);

		}

		public static Vector2 GetDirection(this ILocation location, Vector2 targetPosition)
		{
			return location.GetPosition() - targetPosition;
		}

	}
}


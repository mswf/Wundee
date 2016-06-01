

namespace Wundee
{
	public class GameParams
	{
		public bool parseDefinitions = true;


		public bool generateWorld = true;
		public bool generateSettlements = true;
		public bool generatePlayer = true;
		
		public GameParams()
		{
			
		}


		public const float WORLD_WIDHT = 1600f/0.7f;
		public const float WORLD_HEIGHT = 900f/0.7f;

	}

	public static class D
	{
		public const string EFFECTS = "effects";
		public const string CONDITIONS = "conditions";

		public const string TYPE = "type";
		public const string PARAMS = "params";
	}
}

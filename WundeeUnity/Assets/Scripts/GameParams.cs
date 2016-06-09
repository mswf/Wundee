// ReSharper disable InconsistentNaming


using LitJson;

namespace Wundee
{
	public class GameParams
	{
		public bool parseDefinitions = true;

		public bool generateWorld = true;
		public bool generateSettlements = true;
		public bool generatePlayer = true;

		public float worldWidth = 1600f/0.7f;
		public float worldHeight = 900f/0.7f;

		public NeedParams needParams = new NeedParams();
		
		public void InitializeFromData(JsonData gameParamData)
		{
			worldWidth = ContentHelper.ParseFloat(gameParamData, "worldWidth", this.worldWidth);
			worldHeight = ContentHelper.ParseFloat(gameParamData, "worldHeight", this.worldHeight);

			Logger.Log(gameParamData.ToJson());

			generateWorld		= ContentHelper.ParseBool(gameParamData, "generateWorld", this.generateWorld);
			generateSettlements = ContentHelper.ParseBool(gameParamData, "generateSettlements", this.generateSettlements);
			generatePlayer		= ContentHelper.ParseBool(gameParamData, "generatePlayer", this.generatePlayer);

		}
	}

	public class NeedParams
	{
		
	}

	// Definition keys, for lookup in Json files
	public static class D
	{
		// Stories
		public const string START_NODE = "startNode";
		public const string STORYTRIGGERS = "storyTriggers";

		public const string EFFECTS = "effects";
		public const string CONDITIONS = "conditions";
		public const string REWARDS = "rewards";

		public const string REWARDS_ON_START = "onStartRewards";
		public const string REWARDS_ON_COMPLETE = "onCompleteRewards";

		// StoryElements
		public const string TYPE = "type";
		public const string PARAMS = "params";

		public const string OPERATOR = "operator";
		public const string AMOUNT = "amount";

		public const string RATE = "rate";

		// Effects
		public const string SPEED = "speed";

		public const string NEED = "need";

		// StoryEffects
		public const string STORY_KEY = "storyKey";
	}

	// Strings that are used to extend the definitionKey for definitions that are declared within its parent
	public static class KEYS
	{
		public const string STORYNODE = "_STORYNODE_";
		public const string STORYTRIGGER = "_STORYTRIGGER_";

		public const string EFFECT = "_EFFECT_";
		public const string CONDITION = "_CONDITION_";
		public const string REWARD = "_REWARD_";


	}
}

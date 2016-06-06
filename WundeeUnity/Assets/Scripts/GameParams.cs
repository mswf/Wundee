// ReSharper disable InconsistentNaming


namespace Wundee
{
	public class GameParams
	{
		public bool parseDefinitions = true;

		public bool generateWorld = true;
		public bool generateSettlements = true;
		public bool generatePlayer = true;

		public const float WORLD_WIDHT = 1600f/0.7f;
		public const float WORLD_HEIGHT = 900f/0.7f;
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

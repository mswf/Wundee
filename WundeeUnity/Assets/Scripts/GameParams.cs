// ReSharper disable InconsistentNaming


using System.Collections.Generic;
using LitJson;

namespace Wundee
{
	public class GameParams
	{
		public bool parseDefinitions = true;

		public bool generateWorld = true;
		public bool generateSettlements = true;
		public bool generatePlayer = true;

		public int worldWidth = 1600;
		public int worldHeight = 900;

		public float habitatMinDistance = 220f;
		public int habitatCap = 500;

		public Dictionary<string, System.Object> constants = new Dictionary<string, System.Object>(100); 

		public NeedParams needParams = new NeedParams();
		public double timeMultiplier = 1d;

		public void InitializeFromData(JsonData gameParamData)
		{
			var constantsData = gameParamData[P.CONSTANTS];
			var constantKeys = constantsData.Keys;
			
			foreach (var constantKey in constantKeys)
			{
				constants[constantKey] = ContentHelper.ParseDouble(constantsData, constantKey, 0d);
			}
			
			worldWidth = ContentHelper.ParseInt(gameParamData, P.WORLD_WIDTH, this.worldWidth);
			worldHeight = ContentHelper.ParseInt(gameParamData, P.WORLD_HEIGHT, this.worldHeight);
			habitatMinDistance = ContentHelper.ParseFloat(gameParamData, P.HABITAT_MIN_DISTANCE,
				this.habitatMinDistance);
			habitatCap = ContentHelper.ParseInt(gameParamData, P.HABITAT_CAP, this.habitatCap);

			generateWorld		= ContentHelper.ParseBool(gameParamData, P.GENERATE_WORLD, this.generateWorld);
			generateSettlements = ContentHelper.ParseBool(gameParamData, P.GENERATE_SETTLEMENTS, this.generateSettlements);
			generatePlayer		= ContentHelper.ParseBool(gameParamData, P.GENERATE_PLAYER, this.generatePlayer);

			timeMultiplier = ContentHelper.ParseDouble(gameParamData, P.TIME_MULTIPLIER, this.timeMultiplier);

			if (gameParamData.Keys.Contains(P.NEED_PARAMS))
			{
				var needParamData = gameParamData[P.NEED_PARAMS];

				if (needParamData.Keys.Contains(P.NEEDS))
				{
					var needsData = needParamData[P.NEEDS];

					if (needsData.IsArray)
					{
						var count = needsData.Count;
						needParams.needs = new string[count];

						for (int i = 0; i < count; i++)
						{
							needParams.needs[i] = needsData[i].ToString();
						}
					}
				}
			}

		}

		public bool IsValidNeed(string potentialNeed)
		{
			for (int i = 0; i < needParams.needs.Length; i++)
			{
				if (needParams.needs[i] == potentialNeed)
					return true;
			}

			return false;
		}
	}

	public class NeedParams
	{
		public string[] needs = new string[]
		{
			"Population",
			"Goods",
			"Defense",
			"Satisfaction",
		};
	}

	public static class P
	{
		public const string TIME_MULTIPLIER = "timeMultiplier";
		public const string CONSTANTS = "constants";

		public const string RANDOM = "RANDOM";

		public const string WORLD_WIDTH = "worldWidth";
		public const string WORLD_HEIGHT = "worldHeight";
		public const string HABITAT_MIN_DISTANCE = "habitatMinDistance";

		public const string HABITAT_CAP = "habitatCap";


		public const string GENERATE_WORLD = "generateWorld";
		public const string GENERATE_SETTLEMENTS = "generateSettlements";
		public const string GENERATE_PLAYER = "generatePlayer";


		public const string NEED_PARAMS = "needParams";
		public const string NEEDS = "needs";

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

		public const string STATEMENTS = "statements";

		public const string SPEED = "speed";

		public const string NEED = "need";

		public const string RANGE = "range";


		// StoryEffects
		public const string STORY_KEY = "storyKey";

		// Conditions
		public const string FLAG = "flag";


		// Location
		public const string TARGET_LOCATION = "targetLocation";

		public const string X = "x";
		public const string Y = "y";

		public const string X_DIR = "xDir";
		public const string Y_DIR = "yDir";

		public const string HAS_FLAG = "hasFlag";


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

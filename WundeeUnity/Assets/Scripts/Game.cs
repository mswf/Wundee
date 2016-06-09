

using UnityEngine;

namespace Wundee
{
	public class Game
	{
		#region Private Fields

		private bool _isPlaying = true;

		private WundeeUnity.GameEntry _mainMonoBehaviour;

		public DataLoader definitions; 

		#endregion

		#region Public Fields

		public GameParams @params;
		public World world;

		#endregion

		#region Public Properties

		public static Game instance;

		public bool isPlaying
		{
			get { return _isPlaying; }
		}

		#endregion

		public Game(string gameParamsKey, WundeeUnity.GameEntry mainMonoBehaviour)
		{
			Game.instance = this;
			
			this.definitions = new DataLoader();

			var gameParams = new GameParams();

			var gameParamsData = definitions.GetJsonDataFromYamlFile(DataLoader.GetContentFilePath() + "GameParams.yaml");
			ContentHelper.VerifyKey(gameParamsData, gameParamsKey, "PARAMS_READER");
			gameParams.InitializeFromData(gameParamsData[gameParamsKey]);

			this.@params = gameParams;
			this._mainMonoBehaviour = mainMonoBehaviour;

			Time.gameTime = 0d;
			Time.realTime = 0d;

			this.world = new World();

		}

		public void Initialize()
		{
			if (@params.parseDefinitions)
			{
				definitions.storyDefinitions.AddFolder("Story");
				definitions.storyNodeDefinitions.AddFolder("StoryNode");
				definitions.storyTriggerDefinitions.AddFolder("StoryTrigger");

				definitions.effectDefinitions.AddFolder("Effect");
				definitions.effectDefinitions.AddFolder("Reward");

				definitions.conditionDefinitions.AddFolder("Condition");
			}

			if (@params.generateWorld)
			{
				world.GenerateMap();

				world.GenerateHabitats();
			}

			if (@params.generateSettlements)
			{
				world.GenerateSettlements();
				// do faction generation here
			}

			if (@params.generatePlayer)
			{
				// do player generation here
			}
		}

		public void Update(float dt)
		{
			Time.dt = dt;

			if (_isPlaying)
			{
				Time.gameTime += dt;
			}

			Time.realTime += dt;
		}

		public void FixedUpdate(float fixedDT)
		{
			Time.fixedDT = fixedDT;

			if (_isPlaying)
			{
				Time.fixedGameTime += fixedDT;

				world.Tick();
			}

			Time.fixedRealTime += fixedDT;
		}
	}
}


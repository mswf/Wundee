

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

		public Game(GameParams @params, WundeeUnity.GameEntry mainMonoBehaviour)
		{
			Game.instance = this;

			this.@params = @params;
			this._mainMonoBehaviour = mainMonoBehaviour;

			Time.gameTime = 0d;
			Time.realTime = 0d;

			this.world = new World();

			this.definitions = new DataLoader();
		}

		public void Initialize()
		{
			if (@params.parseDefinitions)
			{
				definitions.storyDefinitions.AddFolder("Story");
				definitions.storyNodeDefinitions.AddFolder("StoryNode");
				definitions.storyTriggerDefinitions.AddFolder("StoryTrigger");

				definitions.effectDefinitions.AddFolder("Effect");
				definitions.conditionDefinitions.AddFolder("Condition");
				definitions.rewardDefinitions.AddFolder("Reward");



				/*
				// Validate that some basic information got parsed
				var story_1 = definitions.storyDefinitions["STORY_TEST_1"].GetConcreteType();
				UnityEngine.Debug.Log(story_1.currentNode.testProperty);

				var story_2 = definitions.storyDefinitions["STORY_TEST_2"].GetConcreteType();
				UnityEngine.Debug.Log(story_2.currentNode.testProperty);
				//*/

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


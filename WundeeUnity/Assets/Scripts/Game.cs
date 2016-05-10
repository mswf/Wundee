

namespace Wundee
{
	public class Game
	{
		#region Private Fields

		private bool _isPlaying = true;

		private WundeeUnity.GameEntry _mainMonoBehaviour;

		#endregion

		#region Public Fields

		public GameParameters parameters;

		#endregion

		#region Public Properties

		public static Game instance;

		public bool isPlaying
		{
			get { return _isPlaying; }
		}

		#endregion

		public Game(GameParameters parameters, WundeeUnity.GameEntry mainMonoBehaviour)
		{
			Game.instance = this;

			this.parameters = parameters;
			this._mainMonoBehaviour = mainMonoBehaviour;

			Time.gameTime = 0d;
			Time.realTime = 0d;
		}

		public void Initialize()
		{
			if (parameters.generateWorld)
			{
				// do world generation here
			}

			if (parameters.generateFactions)
			{
				// do faction generation here
			}

			if (parameters.generatePlayer)
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
			}

			Time.fixedRealTime += fixedDT;
		}
	}
}


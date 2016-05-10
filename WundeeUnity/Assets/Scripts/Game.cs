

namespace Wundee
{
	public class Game
	{
		#region Private Fields

		private bool _isPlaying = true;

		private WundeeUnity.Main _mainMonoBehaviour;

		#endregion

		#region Public Properties

		public static Game instance;

		public bool isPlaying
		{
			get { return _isPlaying; }
		}

		#endregion

		public Game(WundeeUnity.Main mainMonoBehaviour)
		{
			Game.instance = this;

			this._mainMonoBehaviour = mainMonoBehaviour;

			Time.gameTime = 0d;
			Time.realTime = 0d;
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


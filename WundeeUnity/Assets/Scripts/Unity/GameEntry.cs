using UnityEngine;
using System.Collections;
using Wundee;

namespace WundeeUnity
{
	public class GameEntry : MonoBehaviour
	{
		public Game game;

		protected void Awake()
		{
			var gameParams = new GameParameters();

			this.game = new Game(gameParams, this);
		}

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		protected void Update()
		{
			game.Update(UnityEngine.Time.deltaTime);
		}

		protected void FixedUpdate()
		{
			game.FixedUpdate(UnityEngine.Time.fixedDeltaTime);
		}
	}

}

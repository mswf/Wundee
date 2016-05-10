using UnityEngine;
using System.Collections;
using Wundee;

namespace WundeeUnity
{
	public class Main : MonoBehaviour
	{
		public Game game;

		protected void Awake()
		{
			this.game = new Game(this);
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

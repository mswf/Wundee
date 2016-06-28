using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Wundee;
using Random = UnityEngine.Random;

namespace WundeeUnity
{
	public class GameEntry : MonoBehaviour
	{
		public Game game;
		public string gameParamsKey = "default";

		[Header("Temp Debug")] public GameObject dockObject;

		protected void Awake()
		{
			//var gameParams = new GameParams();

			this.game = new Game(gameParamsKey, this);
		}

		// Use this for initialization
		void Start()
		{
			game.Initialize();

			foreach (var settlement in game.world.settlements)
			{
				var habitat = settlement.habitat;

				var newDocks = GameObject.Instantiate(dockObject, new UnityEngine.Vector3(habitat.position.X, 0, habitat.position.Y), UnityEngine.Quaternion.identity) as GameObject;

				newDocks.SetActive(true);

				var settlementComponent = newDocks.AddComponent<WundeeUnity.Settlement>();
				settlementComponent.settlement = settlement;
			}
		}



		// Update is called once per frame
		protected void Update()
		{
			var dt = UnityEngine.Time.deltaTime;

			game.Update(dt);

			var settlementColor = UnityEngine.Color.yellow;

			var habitats = game.world.habitats;
			var numHabitats = habitats.Count;
			for (var i = 0; i < numHabitats; i++)
			{
				var habitatPos = habitats[i].position;
				DebugExtension.DebugCircle(new UnityEngine.Vector3(habitatPos.X, 0, habitatPos.Y), settlementColor, 30f, dt);
			}
		}

		protected void FixedUpdate()
		{
			if (Input.GetKey(KeyCode.LeftBracket))
			{
				Wundee.Time.multiplier -= 0.5d;
				Debug.Log(Wundee.Time.multiplier);

			}

			if (Input.GetKey(KeyCode.RightBracket))
			{
				Wundee.Time.multiplier += 0.5d;
				Debug.Log(Wundee.Time.multiplier);
			}

			game.FixedUpdate(UnityEngine.Time.fixedDeltaTime);

			if (Input.GetKeyUp(KeyCode.R))
			{
				Game.instance.reloadGame = true;
				return;
			}


		}
	}

}

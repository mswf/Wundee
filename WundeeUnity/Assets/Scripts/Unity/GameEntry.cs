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

			/*
			foreach (var settlement in game.world.settlements)
			{
				var habitat = settlement.habitat;

				var newDocks = GameObject.Instantiate(dockObject, new UnityEngine.Vector3(habitat.position.X, 0, habitat.position.Y), UnityEngine.Quaternion.identity) as GameObject;

				newDocks.SetActive(true);

				var settlementComponent = newDocks.AddComponent<WundeeUnity.Settlement>();
				settlementComponent.settlement = settlement;
			}
			//*/
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

			var isRedColor = UnityEngine.Color.red;
			var settlements = game.world.settlements;
			var numSettlements = settlements.Count;

			var hasSpeedBoostFlag = ContentHelper.ParseSettlementFlag("HAS_SPEED_BOOST");
			const string affectNeighboursStoryKey = "STORY_TEST_AFFECT_NEIGHBOURS";

			for (int i = 0; i < numSettlements; i++)
			{
				var settlement = settlements[i];

				var lineairVelocity = settlement.habitat.body.LinearVelocity;
				var habitatPos = settlement.habitat.position;
				var habitPos3d = new UnityEngine.Vector3(habitatPos.X, 0, habitatPos.Y);

				DebugExtension.DebugArrow(habitPos3d,
					new UnityEngine.Vector3(lineairVelocity.X, 0, lineairVelocity.Y), isRedColor, dt);

				if (settlement.HasFlag(hasSpeedBoostFlag))
				{
					if (settlement.storyHolder.IsStoryActive(affectNeighboursStoryKey))
					{
						DebugExtension.DebugCircle(habitPos3d, isRedColor, 18f, dt);
						DebugExtension.DebugPoint(habitPos3d, isRedColor, 22f, dt);
					}
					else
						DebugExtension.DebugCircle(habitPos3d, isRedColor, 20f, dt);
				}
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

using UnityEngine;
using System.Collections;
using Wundee;

namespace WundeeUnity
{
	public class GameEntry : MonoBehaviour
	{
		public Game game;

		[Header("Temp Debug")] public GameObject dockObject;

		protected void Awake()
		{
			var gameParams = new GameParams();

			this.game = new Game(gameParams, this);
		}

		// Use this for initialization
		void Start()
		{
			game.Initialize();

			foreach (var habitat in game.world.habitats)
			{
				var newDocks = GameObject.Instantiate(dockObject, new UnityEngine.Vector3(habitat.position.X, 0, habitat.position.Y), UnityEngine.Quaternion.identity) as GameObject;


				var rotation = Mathf.Round(Random.value*4f);

				newDocks.transform.Rotate(UnityEngine.Vector3.up, rotation*90f);
			}
		}



		// Update is called once per frame
		protected void Update()
		{
			var dt = UnityEngine.Time.deltaTime;

			game.Update(dt);

			var habitatColor = UnityEngine.Color.black;

			foreach (var habitat in game.world.habitats)
			{
				DebugExtension.DebugPoint(new UnityEngine.Vector3(habitat.position.X, 0, habitat.position.Y), habitatColor, 5f, dt);
			}


			var settlementColor = UnityEngine.Color.yellow;

			foreach (var settlement in game.world.settlements)
			{
				DebugExtension.DebugCircle(new UnityEngine.Vector3(settlement.habitat.position.X, 0, settlement.habitat.position.Y), settlementColor, 4f, dt);
			}
		}

		protected void FixedUpdate()
		{
			game.FixedUpdate(UnityEngine.Time.fixedDeltaTime);
		}
	}

}

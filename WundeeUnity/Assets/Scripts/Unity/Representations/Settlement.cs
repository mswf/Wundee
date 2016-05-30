using UnityEngine;
using System.Collections;


namespace WundeeUnity
{
	public class Settlement : MonoBehaviour
	{
		public Wundee.Settlement settlement;
		
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			transform.position = new Vector3(settlement.habitat.position.X, 0, settlement.habitat.position.Y);
		}
	}


}

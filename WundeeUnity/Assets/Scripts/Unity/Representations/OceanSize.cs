using UnityEngine;
using System.Collections;

namespace WundeeUnity
{
	public class OceanSize : MonoBehaviour
	{
		public Transform[] oceanEntities;

		// Use this for initialization
		void Start()
		{
			var worldRect = Wundee.Game.instance.world.worldBounds;
			var scaleVector = new Vector3(worldRect.Width/10f, 1f, worldRect.Height/10f);
			
			for (int i = 0; i < oceanEntities.Length; i++)
			{
				oceanEntities[i].localScale = scaleVector;
				oceanEntities[i].localPosition = new Vector3(scaleVector.x * 5f, oceanEntities[i].localPosition.y, scaleVector.z * 5f);

			}
		}

	}
}
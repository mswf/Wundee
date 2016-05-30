
using System.Collections;


namespace Wundee.Stories
{
	public abstract class EffectBase
	{
		public abstract void Tick();
	}

	public class TestEffect : EffectBase
	{
		public override void Tick()
		{
			Logger.Log("running TestEffect");
		}
	}

	///*
	public class TestEffect2 : EffectBase
	{
		public override void Tick()
		{
			Logger.Log("running TestEffect 2");
		}
	}
	//*/
}


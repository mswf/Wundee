
using System.Collections;


namespace Wundee.Stories
{
	public abstract class EffectBase
	{
		public StoryNode parent;
		public abstract void Tick();
	}

	public class TestEffect : EffectBase
	{
		public override void Tick()
		{
			//Logger.Print("running TestEffect");
		}
	}

	///*
	public class TestEffect2 : EffectBase
	{
		public override void Tick()
		{
			//Logger.Print("running TestEffect 2");
		}
	}
	//*/
}


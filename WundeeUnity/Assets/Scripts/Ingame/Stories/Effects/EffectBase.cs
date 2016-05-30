
using System.Collections;


namespace Wundee.Stories
{
	public abstract class EffectBase
	{
		public StoryNode parentStoryNode;
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

	public class MoveEffect : EffectBase
	{
		public override void Tick()
		{
			parentStoryNode.parent.parentSettlement.habitat.position.X += 0.075f;
		}
	}
}


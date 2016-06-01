
using System.Collections;
using LitJson;


namespace Wundee.Stories
{
	public abstract class EffectBase
	{
		public StoryNode parentStoryNode;

		public abstract void ParseParams(JsonData parameters);

		public EffectBase GetClone()
		{
			return (EffectBase)MemberwiseClone();
		}

		public abstract void Tick();
	}

	public class TestEffect : EffectBase
	{
		public override void ParseParams(JsonData parameters)
		{

		}

		public override void Tick()
		{
			//Logger.Print("running TestEffect");
		}
	}

	public class MoveEffect : EffectBase
	{
		private const string D_SPEED = "speed";

		private float movementSpeed = 1f;



		public override void ParseParams(JsonData parameters)
		{
			var speed = parameters[D_SPEED];

			if (speed != null)
			{
				movementSpeed = (float)(double) speed;
			}
		}

		public override void Tick()
		{
			parentStoryNode.parentStory.parentSettlement.habitat.position.X += 0.075f * movementSpeed;
		}
	}
}


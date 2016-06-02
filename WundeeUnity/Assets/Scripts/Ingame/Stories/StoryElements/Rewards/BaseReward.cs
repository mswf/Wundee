
using LitJson;

namespace Wundee.Stories
{
	public abstract class BaseReward : StoryElement<BaseReward>
	{
		public abstract void Execute();


	}

	
	public class TestReward : BaseReward
	{
		public override void ParseParams(JsonData parameters)
		{

		}

		public override void Execute()
		{
		
			Logger.Print("Executing TestReward");
		}
	}
	
}


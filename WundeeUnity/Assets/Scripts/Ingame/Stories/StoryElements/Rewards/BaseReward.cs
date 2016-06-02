
using LitJson;

namespace Wundee.Stories
{
	public abstract class BaseReward : StoryElement<BaseReward>
	{
		public abstract void Execute();


	}

	public class PrintReward : BaseReward
	{
		public string messageToPrint;

		public override void ParseParams(JsonData parameters)
		{
			messageToPrint = parameters.ToString();
		}

		public override void Execute()
		{
			Logger.Print(messageToPrint);
		}
	}

	public class TestReward : BaseReward
	{
		public override void ParseParams(JsonData parameters)
		{

		}

		public override void Execute()
		{
		
			//Logger.Print("Executing TestReward");
		}
	}

	
}


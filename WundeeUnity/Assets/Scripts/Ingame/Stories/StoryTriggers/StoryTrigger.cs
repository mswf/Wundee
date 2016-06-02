
namespace Wundee.Stories
{
	public class StoryTrigger
	{
		public StoryTriggerDefinition definition;

		public BaseCondition[] conditions;
		public StoryNode parentStoryNode;
		//public BaseReward[] rewards;


		public bool IsTriggered()
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i].IsValid() == false)
				{
					return false;
				}
			}

			/*
			for (int i = 0; i < rewards.Length; i++)
			{
				rewards[i].Execute();
			}
			*/

			return true;
		}
	}

}

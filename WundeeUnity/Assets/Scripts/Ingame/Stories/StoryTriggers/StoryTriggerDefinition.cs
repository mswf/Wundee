

using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public class StoryTriggerDefinition : Definition<StoryTrigger>
	{
		private Definition<BaseCondition>[] _conditionDefinitions;
		// private RewardDefinition[] _rewards;
		
		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			_conditionDefinitions = ConditionDefinition.ParseDefinitions(jsonData, definitionKey);
		}

		public override StoryTrigger GetConcreteType(object parent = null)
		{
			var newStoryTrigger = new StoryTrigger();

			newStoryTrigger.definition = this;
			
			newStoryTrigger.parentStoryNode = parent as StoryNode;
			if (newStoryTrigger.parentStoryNode == null)
				Logger.Log("[StoryTriggerDefinition] Invalid parent StoryNode provided for new StoryTrigger");

			var conditions = new List<BaseCondition>();

			for (int i = 0; i < _conditionDefinitions.Length; i++)
			{
				conditions.Add(_conditionDefinitions[i].GetConcreteType(newStoryTrigger.parentStoryNode));
			}
			
			return newStoryTrigger;
		}
	}
}


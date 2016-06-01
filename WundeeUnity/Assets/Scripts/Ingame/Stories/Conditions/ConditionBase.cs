
using LitJson;


namespace Wundee.Stories
{
	public abstract class ConditionBase
	{
		public StoryNode parentStoryNode;

		public abstract void ParseParams(JsonData parameters);

		public ConditionBase GetClone()
		{
			return (ConditionBase)MemberwiseClone();
		}


		public abstract bool IsValid();
	}

	public abstract class CollectionCondition : ConditionBase
	{
		protected ConditionBase[] _childConditions;

		protected void _ParseChildConditions(JsonData parameters)
		{
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);

			_childConditions = new ConditionBase[conditions.Length];

			for (int i = 0; i < conditions.Length; i++)
			{
				_childConditions[i] = conditions[i].GetConcreteType();
			}
		}
	}

	public abstract class DecoratorCondition : ConditionBase
	{
		protected ConditionBase _childCondition;

		protected void _ParseChildCondition(JsonData parameters)
		{
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);

			_childCondition = conditions[0].GetConcreteType();
		}
	}

	public class AndCondition : CollectionCondition
	{
		public override void ParseParams(JsonData parameters)
		{
			_ParseChildConditions(parameters);
		}

		public override bool IsValid()
		{
			for (int i = 0; i < _childConditions.Length; i++)
			{
				if (_childConditions[i].IsValid() == false)
					return false;
			}

			return true;
		}
	}

	public class OrCondition : CollectionCondition
	{
		public override void ParseParams(JsonData parameters)
		{
			_ParseChildConditions(parameters);
		}

		public override bool IsValid()
		{
			for (int i = 0; i < _childConditions.Length; i++)
			{
				if (_childConditions[i].IsValid() == true)
					return true;
			}

			return false;
		}
	}

	public class NotCondition : DecoratorCondition
	{
		public override void ParseParams(JsonData parameters)
		{
			_ParseChildCondition(parameters);
		}

		public override bool IsValid()
		{
			return !_childCondition.IsValid();
		}
	}
}


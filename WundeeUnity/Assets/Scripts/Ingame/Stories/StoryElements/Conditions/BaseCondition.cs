
using LitJson;


namespace Wundee.Stories
{
	public abstract class BaseCondition : StoryElement<BaseCondition>
	{
		public abstract bool IsValid();
	}

	public abstract class CollectionCondition : BaseCondition
	{
		protected BaseCondition[] childBaseConditions;

		protected void _ParseChildConditions(JsonData parameters)
		{
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);

			childBaseConditions = new BaseCondition[conditions.Length];

			for (int i = 0; i < conditions.Length; i++)
			{
				childBaseConditions[i] = conditions[i].GetConcreteType();
			}
		}
	}

	public abstract class DecoratorCondition : BaseCondition
	{
		protected BaseCondition childBaseCondition;

		protected void _ParseChildCondition(JsonData parameters)
		{
			DataLoader.VerifyKey(parameters, D.CONDITIONS, "NOPE");
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);

			DataLoader.VerifyMaxArrayLength(conditions, 1, "NOPE");
			if (conditions.Length == 1)
				childBaseCondition = conditions[0].GetConcreteType();
			else
				childBaseCondition = new TrueCondition();
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
			for (int i = 0; i < childBaseConditions.Length; i++)
			{
				if (childBaseConditions[i].IsValid() == false)
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
			for (int i = 0; i < childBaseConditions.Length; i++)
			{
				if (childBaseConditions[i].IsValid() == true)
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
			return !childBaseCondition.IsValid();
		}
	}


	public class TrueCondition : BaseCondition
	{
		public override void ParseParams(JsonData parameters)
		{}

		public override bool IsValid()
		{
			return true;
		}
	}
}


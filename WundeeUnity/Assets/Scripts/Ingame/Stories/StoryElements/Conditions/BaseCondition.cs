
using LitJson;
using UnityEngine;


namespace Wundee.Stories
{
	public abstract class BaseCondition : StoryElement<BaseCondition>
	{
		public abstract bool Check();
	}

	public abstract class CollectionCondition : BaseCondition
	{
		protected Definition<BaseCondition>[] _childConditionDefinitions;
		protected BaseCondition[] childConditions;
	}

	public abstract class DecoratorCondition : BaseCondition
	{
		private Definition<BaseCondition> _childConditionDefinition;
		protected BaseCondition childCondition;

		protected void _ParseChildCondition(JsonData parameters)
		{
			DataLoader.VerifyKey(parameters, D.CONDITIONS, definition.definitionKey);
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);

			DataLoader.VerifyMaxArrayLength(conditions, 1, definition.definitionKey);
			if (conditions.Length == 1)
				_childConditionDefinition = conditions[0];
		}

		public override BaseCondition GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as DecoratorCondition;

			if (_childConditionDefinition != null)
				retValue.childCondition = _childConditionDefinition.GetConcreteType(parent);
			else
				retValue.childCondition = new TrueCondition();

			return retValue;
		}
	}

	public class AndCondition : CollectionCondition
	{
		public override void ParseParams(JsonData parameters)
		{
			_childConditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);
		}

		public override BaseCondition GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as AndCondition;

			retValue.childConditions = _childConditionDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override bool Check()
		{
			return childConditions.CheckConditions();
		}
	}

	public class OrCondition : CollectionCondition
	{
		public override void ParseParams(JsonData parameters)
		{
			_childConditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);
		}

		public override BaseCondition GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as OrCondition;

			retValue.childConditions = _childConditionDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override bool Check()
		{
			return childConditions.CheckOrConditions();
		}
	}

	public class NotCondition : DecoratorCondition
	{
		public override void ParseParams(JsonData parameters)
		{
			_ParseChildCondition(parameters);
		}

		public override bool Check()
		{
			return !childCondition.Check();
		}
	}


	public class TrueCondition : BaseCondition
	{
		public override void ParseParams(JsonData parameters)
		{}

		public override bool Check()
		{
			return true;
		}
	}

	public class KeyHeldCondition : BaseCondition
	{
		public override void ParseParams(JsonData parameters)
		{}

		public override bool Check()
		{
			if (UnityEngine.Input.GetKey(KeyCode.Q))
				return true;
			return false;
		}
	}
}


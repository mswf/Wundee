﻿
using LitJson;


namespace Wundee.Stories
{
	public abstract class Condition : StoryElement<Condition>
	{
		public abstract bool Check();
	}

	public abstract class CollectionCondition : Condition
	{
		protected Definition<Condition>[] _childConditionDefinitions;
		protected Condition[] childConditions;
	}

	public abstract class DecoratorCondition : Condition
	{
		private Definition<Condition> _childConditionDefinition;
		protected Condition childCondition;

		protected void _ParseChildCondition(JsonData parameters)
		{
			ContentHelper.VerifyKey(parameters, D.CONDITIONS, definition.definitionKey);
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS], definition.definitionKey);

			ContentHelper.VerifyMaxArrayLength(conditions, 1, definition.definitionKey);
			if (conditions.Length == 1)
				_childConditionDefinition = conditions[0];
		}

		public override Condition GetClone(StoryNode parent)
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
			_childConditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS], definition.definitionKey);
		}

		public override Condition GetClone(StoryNode parent)
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
			_childConditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS], definition.definitionKey);
		}

		public override Condition GetClone(StoryNode parent)
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


	public class TrueCondition : Condition
	{
		public override void ParseParams(JsonData parameters)
		{}

		public override bool Check()
		{
			return true;
		}
	}

	public class KeyHeldCondition : Condition
	{
		public override void ParseParams(JsonData parameters)
		{}

		public override bool Check()
		{
			if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Q))
				return true;
			return false;
		}
	}
}

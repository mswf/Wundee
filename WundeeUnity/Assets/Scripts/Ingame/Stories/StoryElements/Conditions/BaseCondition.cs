
using LitJson;
using UnityEngine;


namespace Wundee.Stories
{
	public abstract class BaseCondition : StoryElement<BaseCondition>
	{
		public abstract bool IsValid();
	}

	public abstract class CollectionCondition : BaseCondition
	{
		protected BaseCondition[] childBaseConditions;

		private Definition<BaseCondition>[] _childBaseConditionDefinitions;

		protected void _ParseChildConditions(JsonData parameters)
		{
			_childBaseConditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);
		}

		public override BaseCondition GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as CollectionCondition;

			retValue.childBaseConditions = new BaseCondition[_childBaseConditionDefinitions.Length];

			for (int i = 0; i < _childBaseConditionDefinitions.Length; i++)
			{
				retValue.childBaseConditions[i] = _childBaseConditionDefinitions[i].GetConcreteType(parent);
			}

			return retValue;
		}
	}

	public abstract class DecoratorCondition : BaseCondition
	{
		protected BaseCondition childBaseCondition;

		private Definition<BaseCondition> _childBaseConditionDefinition;

		protected void _ParseChildCondition(JsonData parameters)
		{
			parameters = parameters;

			DataLoader.VerifyKey(parameters, D.CONDITIONS, definition.definitionKey);
			var conditions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);

			DataLoader.VerifyMaxArrayLength(conditions, 1, definition.definitionKey);
			if (conditions.Length == 1)
				_childBaseConditionDefinition = conditions[0];
		}

		public override BaseCondition GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as DecoratorCondition;

			if (_childBaseConditionDefinition != null)
				retValue.childBaseCondition = _childBaseConditionDefinition.GetConcreteType(parent);
			else
				retValue.childBaseCondition = new TrueCondition();

			return retValue;
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

	public class KeyHeldCondition : BaseCondition
	{
		public override void ParseParams(JsonData parameters)
		{
		}

		public override bool IsValid()
		{
			if (UnityEngine.Input.GetKey(KeyCode.A))
				return true;
			return false;
		}
	}
}


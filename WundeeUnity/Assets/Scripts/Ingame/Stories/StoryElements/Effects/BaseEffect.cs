using LitJson;

namespace Wundee.Stories
{
	public abstract class BaseEffect : StoryElement<BaseEffect>
	{
		public abstract void ExecuteEffect();
	}

	public abstract class CollectionEffect : BaseEffect
	{
		protected Definition<BaseEffect>[] _effectDefinitions;
		protected BaseEffect[] effects;
	}

	public class TestEffect : BaseEffect
	{
		public override void ParseParams(JsonData parameters)
		{
		}

		public override void ExecuteEffect()
		{
			//Logger.Print("running TestEffect");
		}
	}

	public class ConditionalEffect : CollectionEffect
	{
		protected BaseCondition[] conditions;

		private Definition<BaseCondition>[] _conditionDefinitions;

		public override void ParseParams(JsonData parameters)
		{
			_effectDefinitions = EffectDefinition.ParseDefinitions(parameters[D.EFFECTS], definition.definitionKey);
			_conditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS], definition.definitionKey);
		}

		public override BaseEffect GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as ConditionalEffect;

			retValue.conditions = _conditionDefinitions.GetConcreteTypes(parent);
			retValue.effects = _effectDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override void ExecuteEffect()
		{
			if (conditions.CheckConditions())
			{
				effects.ExecuteEffects();
			}
		}
	}

	public class RandomEffect : CollectionEffect
	{
		public override void ParseParams(JsonData parameters)
		{
			_effectDefinitions= EffectDefinition.ParseDefinitions(parameters[D.EFFECTS], definition.definitionKey);

		}

		public override BaseEffect GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as RandomEffect;

			retValue.effects = _effectDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override void ExecuteEffect()
		{
			var randomNumber = R.generator.Next(effects.Length);
			effects[randomNumber].ExecuteEffect();
		}
	}

	public class PrintEffect : BaseEffect
	{
		public string messageToPrint;

		public override void ParseParams(JsonData parameters)
		{
			messageToPrint = parameters.ToString();
		}

		public override void ExecuteEffect()
		{
			Logger.Print(messageToPrint);
		}
	}

	public class MoveEffect : BaseEffect
	{
		private float movementSpeed = 1f;


		public override void ParseParams(JsonData parameters)
		{
			var speed = parameters[D.SPEED];

			if (speed != null)
			{
				movementSpeed = (float) (double) speed;
			}
		}

		public override void ExecuteEffect()
		{
			parentStoryNode.parentStory.parentSettlement
				.habitat.position.X += 0.075f*movementSpeed;
		}
	}

	public class MoveEffect2 : BaseEffect
	{
		private float movementSpeed;

		public override void ParseParams(JsonData parameters)
		{
			ContentHelper.VerifyKey(parameters, D.SPEED, definition.definitionKey);
			movementSpeed = (float)ContentHelper.ParseDouble(parameters, D.SPEED, 1f);
		}

		public override void ExecuteEffect()
		{
			parentStoryNode.parentStory.parentSettlement
				.habitat.position.X += 0.075f * movementSpeed;
		}
	}
}
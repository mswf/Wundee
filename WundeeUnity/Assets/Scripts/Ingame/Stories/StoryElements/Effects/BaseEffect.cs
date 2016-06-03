using LitJson;

namespace Wundee.Stories
{
	public abstract class BaseEffect : StoryElement<BaseEffect>
	{
		public abstract void Tick();
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

		public override void Tick()
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
			_effectDefinitions = EffectDefinition.ParseDefinitions(parameters[D.EFFECTS]);
			_conditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS]);
		}

		public override BaseEffect GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as ConditionalEffect;

			retValue.conditions = _conditionDefinitions.GetConcreteTypes(parent);
			retValue.effects = _effectDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override void Tick()
		{
			if (conditions.CheckConditions())
			{
				effects.TickEffects();
			}
		}
	}

	public class MoveEffect : BaseEffect
	{
		private const string D_SPEED = "speed";

		private float movementSpeed = 1f;


		public override void ParseParams(JsonData parameters)
		{
			var speed = parameters[D_SPEED];

			if (speed != null)
			{
				movementSpeed = (float) (double) speed;
			}
		}

		public override void Tick()
		{
			parentStoryNode.parentStory.parentSettlement
				.habitat.position.X += 0.075f*movementSpeed;
		}
	}
}
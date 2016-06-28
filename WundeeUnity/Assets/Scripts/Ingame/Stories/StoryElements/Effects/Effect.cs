using LitJson;
using Microsoft.Xna.Framework;

namespace Wundee.Stories
{
	public abstract class Effect : StoryElement<Effect>
	{
		public abstract void ExecuteEffect();
	}

	public abstract class CollectionEffect : Effect
	{
		protected Definition<Effect>[] _effectDefinitions;
		protected Effect[] effects;
	}

	public class NullEffect : Effect
	{
		public override void ParseParams(JsonData parameters)
		{
		}

		public override void ExecuteEffect()
		{

		}
	}

	public class ConditionalEffect : CollectionEffect
	{
		protected Condition[] conditions;

		private Definition<Condition>[] _conditionDefinitions;

		public override void ParseParams(JsonData parameters)
		{
			_effectDefinitions = EffectDefinition.ParseDefinitions(parameters[D.EFFECTS], definition.definitionKey);
			_conditionDefinitions = ConditionDefinition.ParseDefinitions(parameters[D.CONDITIONS], definition.definitionKey);
		}

		public override Effect GetClone(StoryNode parent)
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

		public override Effect GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as RandomEffect;

			retValue.effects = _effectDefinitions.GetConcreteTypes(parent);

			return retValue;
		}

		public override void ExecuteEffect()
		{
			var randomNumber = R.Content.Next(effects.Length);
			effects[randomNumber].ExecuteEffect();
		}
	}

	public class PrintEffect : Effect
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

	public class MoveEffect : Effect
	{
		private float movementSpeed;


		public override void ParseParams(JsonData parameters)
		{
			ContentHelper.VerifyKey(parameters, D.SPEED, definition.definitionKey);
			movementSpeed = (float) ContentHelper.ParseDouble(parameters, D.SPEED, 1f);
		}

		public override void ExecuteEffect()
		{
			var body = parentStoryNode.parentStory.parentSettlement.habitat.body;
			body.LinearVelocity += new Vector2(0.075f * movementSpeed);

			//body.SetTransform(body.Position + new Vector2(0.075f * movementSpeed), 0f);
			
		}
	}

	public class MoveEffect2 : Effect
	{
		private float movementSpeed;

		public override void ParseParams(JsonData parameters)
		{
			ContentHelper.VerifyKey(parameters, D.SPEED, definition.definitionKey);
			movementSpeed = (float)ContentHelper.ParseDouble(parameters, D.SPEED, 1f);
		}

		public override void ExecuteEffect()
		{
			

			var body = parentStoryNode.parentStory.parentSettlement.habitat.body;
			body.LinearVelocity += new Vector2(0.075f*movementSpeed);
			//body.SetTransform(body.Position + new Vector2(0.075f * movementSpeed), 0f);
		}
	}
}
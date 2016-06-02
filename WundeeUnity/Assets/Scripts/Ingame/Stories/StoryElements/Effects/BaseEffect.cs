﻿using LitJson;

namespace Wundee.Stories
{
	public abstract class BaseEffect : StoryElement<BaseEffect>
	{
		public abstract void Tick();
	}

	public abstract class CollectionEffect : BaseEffect
	{
		protected BaseEffect[] childBaseEffects;

		private Definition<BaseEffect>[] _effectDefinitions;

		protected void _ParseChildConditions(JsonData parameters)
		{
			_effectDefinitions = EffectDefinition.ParseDefinitions(parameters);


		}

		public override BaseEffect GetClone(StoryNode parent)
		{
			var retValue = base.GetClone(parent) as CollectionEffect;

			retValue.childBaseEffects = new BaseEffect[_effectDefinitions.Length];

			for (int i = 0; i < _effectDefinitions.Length; i++)
			{
				retValue.childBaseEffects[i] = _effectDefinitions[i].GetConcreteType(parent);
			}

			return retValue;
		}
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
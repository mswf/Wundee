﻿
using LitJson;

namespace Wundee.Stories
{
	public class AddSettlementFlagEffect : Effect
	{
		private short _settlementFlag;

		public override void ParseParams(JsonData parameters)
		{
			_settlementFlag = ContentHelper.ParseSettlementFlag(parameters);
		}

		public override void ExecuteEffect()
		{
			parentStoryNode.parentStory.parentSettlement.AddFlag(_settlementFlag);
		}
	}

	public class RemoveSettlementFlagEffect : Effect
	{
		private short _settlementFlag;

		public override void ParseParams(JsonData parameters)
		{
			_settlementFlag = ContentHelper.ParseSettlementFlag(parameters);
		}

		public override void ExecuteEffect()
		{
			parentStoryNode.parentStory.parentSettlement.RemoveFlag(_settlementFlag);
		}
	}

	public class AddWorldFlagEffect : Effect
	{
		private short _worldFlag;

		public override void ParseParams(JsonData parameters)
		{
			_worldFlag = ContentHelper.ParseSettlementFlag(parameters);
		}

		public override void ExecuteEffect()
		{
			Game.instance.world.AddFlag(_worldFlag);
		}
	}

	public class RemoveWorldFlagEffect : Effect
	{
		private short _worldFlag;

		public override void ParseParams(JsonData parameters)
		{
			_worldFlag = ContentHelper.ParseSettlementFlag(parameters);
		}

		public override void ExecuteEffect()
		{
			Game.instance.world.RemoveFlag(_worldFlag);
		}
	}
}


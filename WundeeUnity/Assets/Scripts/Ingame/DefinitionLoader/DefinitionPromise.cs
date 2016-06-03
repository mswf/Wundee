using LitJson;

namespace Wundee
{
	public class DefinitionPromise<DefinitionType, ConcreteType> : Definition<ConcreteType>
		where DefinitionType : Definition<ConcreteType>, new()
	{
		private readonly PromiseType _promiseType;

		private DefinitionType _definition;

		public DefinitionPromise(string definitionKey)
		{
			_promiseType = PromiseType.HoldsKey;

			this.definitionKey = definitionKey;
		}

		public DefinitionPromise(JsonData definitionData)
		{
			_promiseType = PromiseType.HoldsData;

			_definition = new DefinitionType();
			_definition.ParseDefinition(definitionData["key"].ToString(), definitionData);
		}

		public DefinitionType GetDefinition()
		{
			if (_promiseType == PromiseType.HoldsData || _definition != null)
			{
				return _definition;
			}
			else
			{
				// this global misuse = best global misuse
				var newDefinition =
					(Game.instance.definitions.definitionLoaderMapper[typeof (DefinitionType)] as
						DefinitionLoader<DefinitionType, ConcreteType>)[definitionKey];

				_definition = newDefinition;

				return newDefinition;
			}
		}

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			throw new System.NotImplementedException();
		}

		public override ConcreteType GetConcreteType(System.Object parent)
		{
			return GetDefinition().GetConcreteType(parent);
		}

		private enum PromiseType
		{
			HoldsKey,
			HoldsData
		}
	}
}
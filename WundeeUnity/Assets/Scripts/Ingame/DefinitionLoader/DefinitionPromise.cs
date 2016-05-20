using UnityEngine;
using System.Collections;
using LitJson;


namespace Wundee
{
	public class DefinitionPromise<DefinitionType, ConcreteType> : DefinitionBase<ConcreteType> where DefinitionType : DefinitionBase<ConcreteType>, new()
	{
		private enum PromiseType
		{
			HoldsKey,
			HoldsData	
		}

		private PromiseType _promiseType;

		private DefinitionType _definition;
		private string _definitionKey;
		

		public DefinitionPromise(string definitionKey)
		{
			_promiseType = PromiseType.HoldsKey;

			this._definitionKey = definitionKey;

		}

		public DefinitionPromise(JsonData definitionData)
		{
			_promiseType = PromiseType.HoldsData;

			_definition =  new DefinitionType();
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
				var newDefinition = (Game.instance.definitions.definitionLoaderMapper[typeof(DefinitionType)] as DefinitionLoader<DefinitionType>)[_definitionKey];

				_definition = newDefinition;

				return newDefinition;
			}

		}

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			throw new System.NotImplementedException();
		}

		public override ConcreteType GetConcreteType()
		{
			return GetDefinition().GetConcreteType();
		}
	}

}
using UnityEngine;
using System.Collections;
using LitJson;

namespace Wundee
{
	public abstract class DefinitionBase<ConcreteType> : IDefinition
	{
		public abstract void ParseDefinition(string definitionKey, JsonData jsonData);
		public abstract ConcreteType GetConcreteType();
	}

	public interface IDefinition
	{
		void ParseDefinition(string definitionKey, JsonData jsonData);

	}
}
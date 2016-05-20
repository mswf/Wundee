using UnityEngine;
using System.Collections;
using LitJson;

namespace Wundee
{
	public abstract class DefinitionBase<ConcreteType>
	{
		public abstract void ParseDefinition(string definitionKey, JsonData jsonData);
		public abstract ConcreteType GetConcreteType();
	}
}
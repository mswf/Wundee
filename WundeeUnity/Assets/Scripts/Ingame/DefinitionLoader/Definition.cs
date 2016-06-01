﻿
using System.Collections.Generic;
using System.Diagnostics;
using LitJson;

namespace Wundee
{
//	public abstract class Definition<ConcreteType, ChildDefinitionType> where ChildDefinitionType : Definition<ConcreteType, ChildDefinitionType>
	public abstract class Definition<ConcreteType> 
	{
		public abstract void ParseDefinition(string definitionKey, JsonData jsonData);
		public abstract ConcreteType GetConcreteType(System.Object parent = null);
	}
}
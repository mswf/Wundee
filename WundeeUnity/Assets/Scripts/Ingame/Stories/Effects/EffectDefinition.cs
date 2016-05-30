
using System;
using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public class EffectDefinition : DefinitionBase<EffectBase>
	{
		private const string D_TYPE = "type";
		private const string D_PARAMS = "params";

		private Type type;

		public static Dictionary<string, Type> stringToType
		{
			get
			{
				if (_stringToType == null)
				{
					_stringToType = new Dictionary<string, Type>();

					var effectTypes = Helper.ReflectiveEnumerator.GetEnumerableOfType<EffectBase>();

					foreach (var effectType in effectTypes)
					{
						_stringToType[effectType.GetType().Name] = effectType.GetType();
					}

				}

				return _stringToType;
			}
		}

		private static Dictionary<string, System.Type> _stringToType;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			VerifyKey(jsonData, D_TYPE, definitionKey);
			VerifyType(stringToType, jsonData[D_TYPE].ToString(), definitionKey);

			type = stringToType[jsonData[D_TYPE].ToString()];
		}

		public override EffectBase GetConcreteType(System.Object parent = null)
		{
			var newType = System.Activator.CreateInstance(type) as EffectBase;

			return newType;
		}
	}
}
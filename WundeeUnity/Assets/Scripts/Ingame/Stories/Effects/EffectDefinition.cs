﻿
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

#if DEBUG_CONTENT
		public string definitionKey;
#endif

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
#if DEBUG_CONTENT
			this.definitionKey = definitionKey; 
#endif

			VerifyKey(jsonData, D_TYPE, definitionKey);
			VerifyType(stringToType, jsonData[D_TYPE].ToString(), definitionKey);

			type = stringToType[jsonData[D_TYPE].ToString()];
		}

		public override EffectBase GetConcreteType(System.Object parent = null)
		{
			var newEffect = System.Activator.CreateInstance(type) as EffectBase;

			newEffect.parent = parent as StoryNode;
			if (newEffect.parent == null)
			{
#if DEBUG_CONTENT
				Logger.Log("[EffectDefinition] Invalid parent StoryNode provided for new Effect " + definitionKey);
#else
				Logger.Log("[EffectDefinition] Invalid parent StoryNode provided for new Effect with type " + this.type.ToString());
#endif
			}


			return newEffect;
		}


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
	}
}
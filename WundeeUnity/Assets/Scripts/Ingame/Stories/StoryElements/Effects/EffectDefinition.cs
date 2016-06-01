
using System;
using System.Collections.Generic;
using LitJson;

namespace Wundee.Stories
{
	public class EffectDefinition : Definition<BaseEffect>
	{
		private BaseEffect masterCopy;

#if DEBUG_CONTENT
		public string definitionKey;
#endif

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
#if DEBUG_CONTENT
			this.definitionKey = definitionKey; 
#endif

			DataLoader.VerifyKey(jsonData, D.TYPE, definitionKey);
			DataLoader.VerifyType(stringToType, jsonData[D.TYPE].ToString(), definitionKey);

			var type = stringToType[jsonData[D.TYPE].ToString()];
			masterCopy = System.Activator.CreateInstance(type) as BaseEffect;

			var paramsObject = jsonData[D.PARAMS];

			if (paramsObject != null)
			{
				masterCopy.ParseParams(paramsObject);
			}


		}

		public override BaseEffect GetConcreteType(System.Object parent = null)
		{
			var newEffect = masterCopy.GetClone();

			newEffect.parentStoryNode = parent as StoryNode;
			if (newEffect.parentStoryNode == null)
			{
#if DEBUG_CONTENT
				Logger.Log("[EffectDefinition] Invalid parent StoryNode provided for new Effect " + definitionKey);
#else
				Logger.Log("[EffectDefinition] Invalid parent StoryNode provided for new Effect with type " + this.type.ToString());
#endif
			}

			return newEffect;
		}

		public static Definition<BaseEffect>[] ParseDefinitions(JsonData effectData, string definitionKey = "E")
		{
			var tempEffectDefinitions = new List<Definition<BaseEffect>>();
			
			for (int i = 0; i < effectData.Count; i++)
			{
				Definition<BaseEffect> effectDefinition;
				var effect = effectData[i];
				if (effect.IsString)
				{
					effectDefinition = new DefinitionPromise<EffectDefinition, BaseEffect>(effect.ToString());
				}
				else
				{
					effectDefinition = new EffectDefinition();
					effectDefinition.ParseDefinition(definitionKey + "_EFFECT_" + i, effectData[i]);
				}

				tempEffectDefinitions.Add(effectDefinition);

			}
			

			return tempEffectDefinitions.ToArray();

		}
		
		public static Dictionary<string, System.Type> stringToType
		{
			get
			{
				if (_stringToType == null)
				{
					_stringToType = new Dictionary<string, Type>();

					var effectTypes = Helper.ReflectiveEnumerator.GetEnumerableOfType<BaseEffect>();

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

using System;
using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public abstract class StoryElementDefinition<TConcrete> : Definition<TConcrete> where TConcrete : StoryElement<TConcrete>
	{
		protected TConcrete masterCopy;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			this.definitionKey = definitionKey;

			ContentHelper.VerifyKey(jsonData, D.TYPE, definitionKey);
			ContentHelper.VerifyType(stringToType, jsonData[D.TYPE].ToString(), definitionKey);

			var type = stringToType[jsonData[D.TYPE].ToString()];
			masterCopy = System.Activator.CreateInstance(type) as TConcrete;

			masterCopy.definition = this;

			var paramsObject = jsonData[D.PARAMS];
			if (paramsObject != null)
				masterCopy.ParseParams(paramsObject);

		}

		public override TConcrete GetConcreteType(System.Object parent = null)
		{
			var storyNodeParent = parent as StoryNode;

			if (storyNodeParent == null)
			{
				Logger.Log("[StoryElementDefinition] Invalid parent StoryNode provided for new storyElement " + definitionKey);
			}

			var newConcreteType = masterCopy.GetClone(storyNodeParent);

			return newConcreteType;
		}
		
		public static Dictionary<string, System.Type> stringToType
		{
			get
			{
				if (_stringToType == null)
				{
					_stringToType = new Dictionary<string, Type>();

					var effectTypes = Helper.ReflectiveEnumerator.GetEnumerableOfType<TConcrete>();

					// get the str-length of the base type name (Effect) to add an alternate entry 
					// for its derived classes without the suffix (PrintEffect + Print become valid)
					var postFixLength = typeof (TConcrete).Name.Length;

					foreach (var effectType in effectTypes)
					{
						var name = effectType.GetType().Name;
						_stringToType[name] = effectType.GetType();
						_stringToType[name.Remove(name.Length - postFixLength)] = effectType.GetType();

					}

				}

				return _stringToType;
			}
		}

		private static Dictionary<string, System.Type> _stringToType;
	}

}
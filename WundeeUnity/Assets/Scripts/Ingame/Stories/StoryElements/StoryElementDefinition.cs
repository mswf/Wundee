
using System;
using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public abstract class StoryElementDefinition<ConcreteType> : Definition<ConcreteType> where ConcreteType : StoryElement<ConcreteType>
	{

#if DEBUG_CONTENT
		public string definitionKey;
#endif
		protected ConcreteType masterCopy;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
#if DEBUG_CONTENT
			this.definitionKey = definitionKey;
#endif

			DataLoader.VerifyKey(jsonData, D.TYPE, definitionKey);
			DataLoader.VerifyType(stringToType, jsonData[D.TYPE].ToString(), definitionKey);

			var type = stringToType[jsonData[D.TYPE].ToString()];
			masterCopy = System.Activator.CreateInstance(type) as ConcreteType;

			masterCopy.definition = this;

			var paramsObject = jsonData[D.PARAMS];
			if (paramsObject != null)
				masterCopy.ParseParams(paramsObject);

		}

		public override ConcreteType GetConcreteType(System.Object parent = null)
		{
			var storyNodeParent = parent as StoryNode;

			if (storyNodeParent == null)
			{
#if DEBUG_CONTENT
				Logger.Log("[StoryElementDefinition] Invalid parent StoryNode provided for new storyElement " + definitionKey);
#else
				Logger.Log("[StoryElementDefinition] Invalid parent StoryNode provided for new storyElement with type " + this.type.ToString());
#endif
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

					var effectTypes = Helper.ReflectiveEnumerator.GetEnumerableOfType<ConcreteType>();

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

using System;
using System.Collections.Generic;
using LitJson;


namespace Wundee.Stories
{
	public abstract class StoryElementDefinition<ConcreteType> : Definition<ConcreteType> where ConcreteType : StoryElement<ConcreteType>
	{
		protected ConcreteType masterCopy;

		public override void ParseDefinition(string definitionKey, JsonData jsonData)
		{
			this.definitionKey = definitionKey;

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
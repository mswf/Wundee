﻿
using System;
using System.Collections.Generic;
using System.Linq;
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

					var effectTypes = Helper.ReflectiveEnumerator.GetEnumerableOfType<TConcrete>();

					var enumerable = effectTypes as IList<TConcrete> ?? effectTypes.ToList();
					_stringToType = new Dictionary<string, Type>(enumerable.Count*2);
					
					// get the name of the base type (Effect) to add an alternate entry 
					// for its derived classes without the suffix (PrintEffect + Print become valid)
					var postFix = typeof (TConcrete).Name;

					foreach (var effectInfo in enumerable)
					{
						var effectType = effectInfo.GetType();
						var name = effectType.Name;
						
						try
						{
							_stringToType.Add(name, effectType);
							_stringToType.Add(name.Replace(postFix, ""), effectType);
						}
						catch (ArgumentException)
						{
							Logger.Warning("Duplicate StoryElement with named "+ name);
						}
					}
				}

				return _stringToType;
			}
		}

		private static Dictionary<string, System.Type> _stringToType;
	}

}
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LitJson;

public class WeakReferenceT<T> where T : class
{
	private readonly WeakReference _reference;

	public WeakReferenceT(T target)
	{
		_reference = new WeakReference(target);
	}

	public bool IsAlive
	{
		get { return _reference.IsAlive; }
	}

	public T Target
	{
		get { return _reference.Target as T; }
		set { _reference.Target = value; }
	}
}

public static class Helper
{
	private static readonly System.Random _randomArrayRandom = new System.Random();

	public static void RandomizeArray<T>(ref T[] arr)
	{
		var arrayCopy = arr;

		var arrayLength = arr.Length;
		var remainingIndexes = arrayLength;

		var indexList = new List<int>(arrayLength);
		for (var i = 0; i < arrayLength; i++)
		{
			indexList.Add(i);
		}

		for (var i = 0; i < arrayLength; i++)
		{
			var randomIndex = _randomArrayRandom.Next(0, remainingIndexes);

			arr[arrayLength - remainingIndexes] = arrayCopy[indexList[randomIndex]];
			indexList.RemoveAt(randomIndex);

			remainingIndexes--;
		}

		/*
		var arrayLength = arr.Length;
		var list = new List<KeyValuePair<int, T>>(arrayLength);
		list.AddRange(arr.Select(arrItem => new KeyValuePair<int, T>(_randomArrayRandom.Next(arrayLength), arrItem)));
		// Add all strings from array
		// Add new random int each time
		// Sort the list by the random number
		var sorted = list.OrderBy(item => item.Key);
		// Allocate new string array
		// Copy values to array
		var index = 0;
		foreach (var pair in sorted)
		{
			arr[index] = pair.Value;
			index++;
		}

		*/

	}

	public static T[] RemoveAt<T>(this T[] source, int index)
	{
		T[] dest = new T[source.Length - 1];
		if (index > 0)
			Array.Copy(source, 0, dest, 0, index);

		if (index < source.Length - 1)
			Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

		return dest;
	}

	//http://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
	public static class ReflectiveEnumerator
	{
		static ReflectiveEnumerator() { }

		public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
		{
			List<T> objects = new List<T>();
			foreach (Type type in
				Assembly.GetAssembly(typeof(T)).GetTypes()
				.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
			{
				objects.Add((T)Activator.CreateInstance(type, constructorArgs));
			}
			return objects;
		}
	}
}


namespace Wundee
{
	public static class WundeeHelper
	{

		public static Definition<ConcreteType> GetDefinition<DefinitionType, ConcreteType>(JsonData definitionData, string definitionKey, string definitionKeyExtension, int definitionCount = 0) where DefinitionType : Definition<ConcreteType>, new()
		{
			if (definitionData.IsString)
				return new DefinitionPromise<DefinitionType, ConcreteType>(definitionData.ToString());
			else
			{
				var definition = new DefinitionType();
				definition.ParseDefinition(definitionKey + definitionKeyExtension + definitionCount, definitionData);

				return definition;
			}
		}

		public static void ExecuteRewards<T>(this T[] rewards) where T : Stories.BaseReward
		{
			for (int i = 0; i < rewards.Length; i++)
			{
				rewards[i].Execute();
			}
		}

		public static void TickEffects<T>(this T[] effects) where T : Stories.BaseEffect
		{
			for (int i = 0; i < effects.Length; i++)
			{
				effects[i].Tick();
			}
		}

		public static bool CheckConditions<T>(this T[] conditions) where T : Stories.BaseCondition
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i].Check() == false)
					return false;
			}

			return true;
		}

		public static bool CheckOrConditions<T>(this T[] conditions) where T : Stories.BaseCondition
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i].Check() == true)
					return true;
			}

			return false;
		}

		public static T[] GetConcreteTypes<T>(this Definition<T>[] definitions, System.Object parent)
		{
			var concreteTypes = new T[definitions.Length];

			for (int i = 0; i < definitions.Length; i++)
			{
				concreteTypes[i] = definitions[i].GetConcreteType(parent);
			}

			return concreteTypes;
		}

		// source: http://theinstructionlimit.com/squaring-the-thumbsticks
		public static Vector2 CircleToSquare(Vector2 point, float innerRoundness = 0f)
		{
			const float Pi = MathHelper.Pi;
			const float PiOverFour = MathHelper.Pi / 4f;

			// Determine the theta angle
			var angle = MathHelper.Atan2(point.Y, point.X) + Pi;

			Vector2 squared;

			// Scale according to which wall we're clamping to
			// X+ wall
			if (angle <= PiOverFour || angle > 7f * PiOverFour)
				squared = point * (1f / MathHelper.Cos(angle));
			// Y+ wall
			else if (angle > PiOverFour && angle <= 3f * PiOverFour)
				squared = point * (1f / MathHelper.Sin(angle));
			// X- wall
			else if (angle > 3f * PiOverFour && angle <= 5f * PiOverFour)
				squared = point * (-1f / MathHelper.Cos(angle));
			// Y- wall
			else if (angle > 5f * PiOverFour && angle <= 7f * PiOverFour)
				squared = point * (-1f / MathHelper.Sin(angle));
			else throw new System.InvalidOperationException("Invalid angle...?");

			// Early-out for a perfect square output
			if (innerRoundness == 0f)
				return squared;

			// Find the inner-roundness scaling factor and LERP
			var length = point.Length();
			var factor = MathHelper.Pow(length, innerRoundness);
			return Vector2.Lerp(point, squared, factor);
		}
	}
}


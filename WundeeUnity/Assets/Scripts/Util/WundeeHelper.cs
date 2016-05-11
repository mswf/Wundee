﻿
using System;
using System.Collections.Generic;

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
}


namespace Wundee
{
	public static class WundeeHelper
	{

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


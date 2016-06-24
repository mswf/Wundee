﻿// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using Microsoft.Xna.Framework;

namespace Wundee
{
	/// <summary>
	/// Contains commonly used precalculated values and mathematical operations.
	/// </summary>
	public static class MathExtension
    {
    	/// <summary>
        /// Represents the mathematical constant e(2.71828175).
        /// </summary>
        public const float E = (float)System.Math.E;
        
        /// <summary>
        /// Represents the log base ten of e(0.4342945).
        /// </summary>
        public const float Log10E = 0.4342945f;
        
        /// <summary>
        /// Represents the log base two of e(1.442695).
        /// </summary>
        public const float Log2E = 1.442695f;
        
        /// <summary>
        /// Represents the value of pi(3.14159274).
        /// </summary>
        public const float Pi = (float)System.Math.PI;
        
        /// <summary>
        /// Represents the value of pi divided by two(1.57079637).
        /// </summary>
        public const float PiOver2 = (float)(System.Math.PI / 2.0);
        
        /// <summary>
        /// Represents the value of pi divided by four(0.7853982).
        /// </summary>
        public const float PiOver4 = (float)(System.Math.PI / 4.0);
        
        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// </summary>
        public const float TwoPi = (float)(System.Math.PI * 2.0);
        
        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

	/// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

 	/// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        { 
            value = (value > max) ? max : value; 
            value = (value < min) ? min : value; 
            return value;
        }
        
        /// <summary>
        /// Calculates the absolute value of the difference of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>Distance between the two values.</returns>
        public static float Distance(float value1, float value2)
        {
            return System.Math.Abs(value1 - value2);
        }
        
        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">Source position.</param>
        /// <param name="tangent1">Source tangent.</param>
        /// <param name="value2">Source position.</param>
        /// <param name="tangent2">Source tangent.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }
        
        
        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns> 
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>value1 + (value2 - value1) * amount</code>.
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// See <see cref="MathHelper.LerpPrecise"/> for a less efficient version with more precision around edge cases.
        /// </remarks>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }


        /// <summary>
        /// Linearly interpolates between two values.
        /// This method is a less efficient, more precise version of <see cref="MathHelper.Lerp"/>.
        /// See remarks for more info.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns>
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>((1 - amount) * value1) + (value2 * amount)</code>.
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// This method does not have the floating point precision issue that <see cref="MathHelper.Lerp"/> has.
        /// i.e. If there is a big gap between value1 and value2 in magnitude (e.g. value1=10000000000000000, value2=1),
        /// right at the edge of the interpolation range (amount=1), <see cref="MathHelper.Lerp"/> will return 0 (whereas it should return 1).
        /// This also holds for value1=10^17, value2=10; value1=10^18,value2=10^2... so on.
        /// For an in depth explanation of the issue, see below references:
        /// Relevant Wikipedia Article: https://en.wikipedia.org/wiki/Linear_interpolation#Programming_language_support
        /// Relevant StackOverflow Answer: http://stackoverflow.com/questions/4353525/floating-point-linear-interpolation#answer-23716956
        /// </remarks>
        public static float LerpPrecise(float value1, float value2, float amount)
        {
            return ((1 - amount) * value1) + (value2 * amount);
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static int Max(int value1, int value2)
        {
            return value1 > value2 ? value1 : value2;
        }
        
        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static int Min(int value1, int value2)
        {
            return value1 < value2 ? value1 : value2;
        }
        
        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            float result = MathHelper.Clamp(amount, 0f, 1f);
            result = MathHelper.Hermite(value1, 0f, value2, 0f, result);

            return result;
        }
        
        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = 180 / pi
        /// </remarks>
        public static float ToDegrees(float radians)
        { 
            return (float)(radians * 57.295779513082320876798154814105);
        }
        
        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = pi / 180
        /// </remarks>
        public static float ToRadians(float degrees)
        { 
            return (float)(degrees * 0.017453292519943295769236907684886);
        }
	 
        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
	public static float WrapAngle(float angle)
	{
            angle = (float)System.Math.IEEERemainder((double)angle, 6.2831854820251465);
	    if (angle <= -3.14159274f)
	    {
		angle += 6.28318548f;
	    }
	    else
	    {
		if (angle > 3.14159274f)
		{
		   angle -= 6.28318548f;
		}
	    }
	    return angle;
	}

 	/// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
	public static bool IsPowerOfTwo(int value)
	{
	     return (value > 0) && ((value & (value - 1)) == 0);
	}



		// Steb Added

		/// <summary>
		/// 
		/// <para>
		/// Returns the sine of angle f in radians.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the cosine of angle f in radians.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the tangent of angle f in radians.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the arc-sine of f - the angle in radians whose sine is f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the arc-cosine of f - the angle in radians whose cosine is f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the arc-tangent of f - the angle in radians whose tangent is f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the angle in radians whose Tan is y/x.
		/// </para>
		/// 
		/// </summary>
		/// <param name="y"/><param name="x"/>
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns square root of f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the absolute value of f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the absolute value of value.
		/// </para>
		/// 
		/// </summary>
		/// <param name="value"/>
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}



		/// <summary>
		/// 
		/// <para>
		/// Returns f raised to power p.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/><param name="p"/>
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns e raised to the specified power.
		/// </para>
		/// 
		/// </summary>
		/// <param name="power"/>
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the logarithm of a specified number in a specified base.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/><param name="p"/>
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the natural (base e) logarithm of a specified number.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the base 10 logarithm of a specified number.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the smallest integer greater to or equal to f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the largest integer smaller to or equal to f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns f rounded to the nearest integer.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the smallest integer greater to or equal to f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the largest integer smaller to or equal to f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns f rounded to the nearest integer.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		/// <summary>
		/// 
		/// <para>
		/// Returns the sign of f.
		/// </para>
		/// 
		/// </summary>
		/// <param name="f"/>
		public static float Sign(float f)
		{
			return (double)f >= 0.0 ? 1f : -1f;
		}
	}
}

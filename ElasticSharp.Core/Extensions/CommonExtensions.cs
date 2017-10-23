/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: CommonExtensions.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.Linq;

namespace ElasticSharp.Core
{
    /// <summary>
    /// Common extension methods 
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// Converts given byte array to hex string
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// Converts given string to byte array
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns></returns>
        public static byte[] FromHex(this string str)
        {
            return Enumerable.Range(0, str.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                    .ToArray();
        }

        /// <summary>
        /// Returns bytes equivalent of a given long value
        /// </summary>
        /// <param name="val">Long value</param>
        /// <returns></returns>
        public static byte[] GetBytes(this long val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        /// <summary>
        /// Converts a given byte array to long value
        /// </summary>
        /// <param name="val">Byte array to convert</param>
        /// <returns></returns>
        public static long GetLongBytes(this byte[] val)
        {
            long longValue = BitConverter.ToInt64(val, 0);
            return longValue;
        }
        public static byte[] GetBytes(this int val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        /// <summary>
        /// Converts a byte array to integer
        /// </summary>
        /// <param name="val">Byte array to convert</param>
        /// <returns></returns>
        public static int GetBytes(this byte[] val)
        {
            int intValue = BitConverter.ToInt32(val, 0);
            return intValue;
        }
        
    }
}

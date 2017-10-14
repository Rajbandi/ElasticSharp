using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public static class CommonExtensions
    {
        public static string ToHex(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static byte[] FromHex(this string str)
        {
            return Enumerable.Range(0, str.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                    .ToArray();
        }
        public static byte[] GetBytes(this long val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }
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

        public static int GetBytes(this byte[] val)
        {
            int intValue = BitConverter.ToInt32(val, 0);
            return intValue;
        }
        
    }
}

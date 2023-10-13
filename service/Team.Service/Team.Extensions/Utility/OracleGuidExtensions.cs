using System;

namespace Team.Extensions.Utility
{
    public static class OracleGuidExtensions
    {
        public static byte[] ToOracleByteArray(this Guid guid)
        {
            return guid.ToByteArray().ReverseGuidEndianess();
        }

        public static byte[] ReverseGuidEndianess(this byte[] guidBytes)
        {
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
            Array.Reverse(guidBytes, 6, 2);
            return guidBytes;
        }
    }
}
namespace x42Client.Utils.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Checks Whether The Supplied Port Number Is Within A Valid Range
        /// </summary>
        public static bool IsValidPortRange(this uint port) => ((port > 0) && (port < 65535));

        /// <summary>
        /// Checks Whether The Supplied Port Number Is Within A Valid Range
        /// </summary>
        public static bool IsValidPortRange(this ushort port) => ((port > 0) && (port < 65535));
    }//emd pf c;ass
}

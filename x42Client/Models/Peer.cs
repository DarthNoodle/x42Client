
namespace x42Client.Models
{
    public class Peer
    {
        /// <summary>
        /// IP Address of The Peer
        /// </summary>
        public string Address;

        /// <summary>
        /// Peer Version Information
        /// </summary>
        public string Version;

        /// <summary>
        /// Peer Current Block Height
        /// </summary>
        public ulong TipHeight;
    }
}

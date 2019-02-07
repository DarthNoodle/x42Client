
namespace x42Client.Models
{
    public class Peer
    {
        /// <summary>
        /// IP Address of The Peer
        /// </summary>
        public string Address;

        /// <summary>
        /// Protocol Version of The Node
        /// </summary>
        public int ProtocolVersion;

        /// <summary>
        /// Will The Node Relay TX's
        /// </summary>
        public bool WillRelayTXs;

        /// <summary>
        /// Did The Node Connect To Us
        /// </summary>
        public bool InboundConnection;

        /// <summary>
        /// Ban Score of The Node
        /// </summary>
        public int BanScore;

        /// <summary>
        /// Peer Version Information
        /// </summary>
        public string Version;

        /// <summary>
        /// Peer Current Block Height
        /// </summary>
        public ulong TipHeight;

        /// <summary>
        /// What Services Are Offered By The Node
        /// </summary>
        public string Services;
    }
}

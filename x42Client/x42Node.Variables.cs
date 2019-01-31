using System.Collections.Generic;
using System.Net;
using System.Threading;
using x42Client.Models;
using x42Client.RestClient;

namespace x42Client
{
    public partial class x42Node
    {

        private x42RestClient _RestClient;
        private Timer _RefreshTimer;

        /// <summary>
        /// Current Block Height of The Node
        /// </summary>
        public ulong BlockTIP { private set; get; } = 0;


        /// <summary>
        /// Refresh Information Every X Seconds
        /// </summary>
        public int RefreshTime
        {
            get { return _RefreshTime; }
            set
            {
                _RefreshTime = value;
                _RefreshTimer.Dispose();
                _RefreshTimer = new Timer(RefreshNodeData, null, 0, _RefreshTime);
            }
        }//end of public int RefreshTime {
        private int _RefreshTime { get; set; } = 30;

        /// <summary>
        /// Peers The Node Is Connected To
        /// </summary>
        public List<Peer> Peers { private set; get; } = new List<Peer>();

        /// <summary>
        /// Connected Peers Count
        /// </summary>
        public int PeerCount { get { return Peers.Count; } } 

        /// <summary>
        /// Node Name
        /// </summary>
        public string Name { private set; get; } = string.Empty;

        /// <summary>
        /// Node IP Address
        /// </summary>
        public IPAddress Address { private set; get; }

        /// <summary>
        /// Node Port
        /// </summary>
        public ushort Port { private set; get; } = 0;

        /// <summary>
        /// Data Directory of The Node
        /// </summary>
        public string DataDirectory { private set; get; } = string.Empty;

        /// <summary>
        /// Version of Running Node
        /// </summary>
        public string NodeVersion { private set; get; } = string.Empty;

        /// <summary>
        /// Protocol Version of The Node
        /// </summary>
        public string ProtocolVersion { private set; get; } = string.Empty;

        /// <summary>
        /// Is The Node On The Testnet
        /// </summary>
        public bool IsTestNet { private set; get; } = false;

        /// <summary>
        /// Filesystem Path The Wallets Are Stored In
        /// </summary>
        public string WalletPath { private set; get; } = string.Empty;

        public List<string> WalletFiles { private set; get; } = new List<string>();
    }//end of public partial class x42Node.Variables
}

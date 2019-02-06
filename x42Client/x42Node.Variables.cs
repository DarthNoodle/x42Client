using System;
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
        /// Was There An Error Getting The FS Information
        /// </summary>
        private bool _Error_FS_Info = false;


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
                _RefreshTimer = new Timer(UpdateNodeData, null, 0, _RefreshTime);
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

        /// <summary>
        /// List of Wallet Files Present on The Node
        /// </summary>
        public List<string> WalletFiles { private set; get; } = new List<string>();

        /// <summary>
        /// List of Wallet Accounts (Key: Wallet Name, Value: List of Accounts)
        /// </summary>
        public Dictionary<string, List<string>> WalletAccounts { private set; get; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// List of All Accounts & Their Transactions on The Remote Node
        /// </summary>
        public Dictionary<string, List<Transaction>> AccountTXs { private set; get; } = new Dictionary<string, List<Transaction>>();

        /// <summary>
        /// Is The Node Staking
        /// </summary>
        public bool IsStaking { private set; get; } = false;

        /// <summary>
        /// Staking Weight Of The Network
        /// </summary>
        public long NetworkStakingWeight { private set; get; } = 0;

        /// <summary>
        /// Node Staking Weight
        /// </summary>
        public decimal NodeStakingWeight { private set; get; } = 0;

        /// <summary>
        /// Expected Time To Stake Reward
        /// </summary>
        public long ExpectedStakingTimeMins { private set; get; } = -1;

        /// <summary>
        /// The Current NetworkDifficulty
        /// </summary>
        public decimal NetworkDifficulty { private set; get; } = 0;
    }//end of public partial class x42Node.Variables
}

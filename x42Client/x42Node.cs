using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using x42Client.Models;
using x42Client.RestClient;
using x42Client.RestClient.Responses;
using x42Client.Utils.Extensions;
using x42Client.Utils.Validation;

namespace x42Client
{
    public class x42Node:IDisposable
    {


        private x42RestClient _RestClient;
        private Timer _RefreshTimer;

        /// <summary>
        /// Current Block Height of The Node
        /// </summary>
        public ulong BlockTIP { private set; get; }


        /// <summary>
        /// Refresh Information Every X Seconds
        /// </summary>
        public int RefreshTime {
            get { return _RefreshTime; }
            set {
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
        /// List of Connected Peers
        /// </summary>
        public int PeerCount { get { return Peers.Count; } }

        /// <summary>
        /// Node Name
        /// </summary>
        public string Name { private set; get;}

        /// <summary>
        /// Node IP Address
        /// </summary>
        public IPAddress Address { private set; get; }

        /// <summary>
        /// Node Port
        /// </summary>
        public ushort Port { private set; get; }

        /// <summary>
        /// Data Directory of The Node
        /// </summary>
        public string DataDirectory { private set; get; }

        /// <summary>
        /// Version of Running Node
        /// </summary>
        public string NodeVersion { private set; get; }

        /// <summary>
        /// Protocol Version of The Node
        /// </summary>
        public string ProtocolVersion { private set; get; }

        /// <summary>
        /// Is The Node On The Testnet
        /// </summary>
        public bool IsTestNet { private set; get; }

        public x42Node(string name, IPAddress address, ushort port)
        {
            _RestClient = new x42RestClient($"http://{address}:{port}/");
            _RefreshTimer = new Timer(RefreshNodeData, null, 0, _RefreshTime);
        }


        private async void RefreshNodeData(object timerState)
        {
            NodeStatusResponse statusData = await _RestClient.GetNodeStatus();
            Guard.Null(statusData, $"Node '{Name}' ({Address}:{Port}) Status Is Null!");

            //update a list of peers
            Peers = statusData.outboundPeers.ToPeersList();
            Peers.AddRange(statusData.inboundPeers.ToPeersList());

            //update current height (use consensus because they have been fully validated)
            BlockTIP = statusData.consensusHeight;

            DataDirectory = statusData.dataDirectoryPath;

            NodeVersion = statusData.version;
            ProtocolVersion = $"{statusData.protocolVersion}";
            IsTestNet = statusData.testnet;

        }//end of private async void RefreshNodeData(object timerState)





        #region IDisposable Code
        private bool _Disposed;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (_Disposed || !disposing) return;


                if (_RestClient != null)
                {
                    x42RestClient rc = _RestClient;
                    rc = null;
                    rc.Dispose();
                }
                _Disposed = true;
            } //end of private void Dispose(bool disposing)
        #endregion

    }//end of public class x42Node
}

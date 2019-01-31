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
    public partial class x42Node:IDisposable
    {



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

            //we have a new block, so fire off an event
            if(statusData.consensusHeight > BlockTIP) { OnNewBlock(statusData.consensusHeight); }

            //update current height (use consensus because they have been fully validated)
            BlockTIP = statusData.consensusHeight;

            DataDirectory = statusData.dataDirectoryPath;

            NodeVersion = statusData.version;
            ProtocolVersion = $"{statusData.protocolVersion}";
            IsTestNet = statusData.testnet;

            GetWalletFilesResponse filesData = await _RestClient.GetWalletFiles();
            Guard.Null(filesData, nameof(filesData), $"Node '{Name}' ({Address}:{Port}) File Data Is Null!");

            WalletPath = filesData.walletsPath;
            WalletFiles = new List<string>(filesData.walletsFiles);



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

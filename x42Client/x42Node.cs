using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using x42Client.Models;
using x42Client.RestClient;
using x42Client.RestClient.Responses;
using x42Client.Utils.Extensions;
using x42Client.Utils.Logging;
using x42Client.Utils.Validation;

namespace x42Client
{
    public partial class x42Node:IDisposable
    {
        public x42Node(string name, IPAddress address, ushort port)
        {
            SetupNodeConnection(name, address, port);
        }

        /// <summary>
        /// Sets Up The API Connection To The x42 Node
        /// </summary>
        private void SetupNodeConnection(string name, IPAddress address, ushort port)
        {
            _RestClient = new x42RestClient($"http://{address}:{port}/");
           // _RefreshTimer = new Timer(UpdateNodeData, null, 0, _RefreshTime);

            Name = name;

            UpdateStaticData();
        }

        //The Workhorse which refreshes all Node Data
        public async void UpdateNodeData(object timerState = null)
        {
            //############  Status Data #################
            NodeStatusResponse statusData = await _RestClient.GetNodeStatus();
            if(statusData == null) { Logger.Error($"Node '{Name}' ({Address}:{Port}) An Error Occured Getting Node Status!"); }
            else
            {
                //update a list of peers
                Peers = statusData.outboundPeers.ToPeersList();
                Peers.AddRange(statusData.inboundPeers.ToPeersList());

                //we have a new block, so fire off an event
                if (statusData.consensusHeight > BlockTIP) { OnNewBlock(statusData.consensusHeight); }

                //update current height (use consensus because they have been fully validated)
                BlockTIP = statusData.consensusHeight;

                DataDirectory = statusData.dataDirectoryPath;

                NodeVersion = statusData.version;
                ProtocolVersion = $"{statusData.protocolVersion}";
                IsTestNet = statusData.testnet;
            }//end of if(statusData == null)


            //############  TX History Processing #################
            UpdateWalletTXs();

            //############  Staking Info #################
            UpdateStakingInformation();

        }//end of private async void RefreshNodeData(object timerState)


        //Obtains Data that is NOT likely to change!
        public async void UpdateStaticData()
        {
            GetWalletFilesResponse filesData = await _RestClient.GetWalletFiles();
            if(filesData == null) {
                _Error_FS_Info = true; //an error occured, this data is relied upon in the "RefreshNodeData" Method
                Logger.Error($"Node '{Name}' ({Address}:{Port}), An Error Occured When Getting Node File Information!");
            }
            else
            {
                WalletPath = filesData.walletsPath;
                WalletFiles = new List<string>(filesData.walletsFiles);

                foreach (string wallet in WalletFiles)
                {
                    //parse MyWallet.wallet.json  to "MyWallet"
                    string walletName = wallet.Substring(0, wallet.IndexOf("."));

                    //Get a list of accounts
                    List<string> walletAccounts = await _RestClient.GetWalletAccounts(walletName);

                    if (walletAccounts == null) { Logger.Warn($"An Error Occured When Trying To Get Wallet Accounts For Wallet '{walletName}'"); }
                    if (walletAccounts.Count > 0)
                    {
                        //Are there already present wallets? if so overwrite the data, if not then lets add a new record
                        if (WalletAccounts.ContainsKey(walletName)) { WalletAccounts[walletName] = walletAccounts; }
                        else { WalletAccounts.Add(walletName, walletAccounts); }
                    }//end of if(walletAccounts.Count > 0)

                }//end of foreach

                _Error_FS_Info = false;
            }//end of if-else if (filesData == null)

        }//end of private async void GetStaticData()

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


                try
                {
                       if(_RestClient != null) { _RestClient.Dispose(); }
                       if(_SSHForwardPort != null)
                        {
                            _SSHForwardPort.Stop();
                            _SSHForwardPort.Dispose();
                        }//end of if(_SSHForwardPort != null)

                        if(_SSHClient != null)
                        {
                            _SSHClient.Disconnect();
                            _SSHClient.Dispose();
                        }//end of if (_SSHClient != null)
                }
                finally
                {
                    _Disposed = true;
                }//end of try-finally               
               
            } //end of private void Dispose(bool disposing)

            // no unmanaged code present
            //~x42Node()
            //{
            //    Dispose(true);
            //}
        #endregion


    }//end of public class x42Node
}

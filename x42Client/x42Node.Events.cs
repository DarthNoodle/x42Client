﻿using System.Collections.Generic;
using System.Net;
using x42Client.Enums;
using x42Client.Models;
using x42Client.Models.Event;
using x42Client.RestClient.Responses;
using x42Client.Utils.Extensions;
using x42Client.Utils.Validation;

namespace x42Client
{

    public delegate void NewBlockEventHandler(object sender, NewBlockEvent e);

    public delegate void NewTXEventHandler(object sender, NewTXEvent e);

    public delegate void OnConnected(object sender, ConnectDisconnectEvent e);

    public delegate void OnDisConnected(object sender, ConnectDisconnectEvent e);

    public partial class x42Node
    {
        /// <summary>
        /// Triggers When A New Block Is Detected
        /// </summary>
        public NewBlockEventHandler NewBlockEvent;

        /// <summary>
        /// Triggers When A New TX Is Detected
        /// </summary>
        public NewTXEventHandler NewTransactionEvent;

        /// <summary>
        /// Triggers When The Node Connects
        /// </summary>
        public OnConnected OnConnectedEvent;


        /// <summary>
        /// Triggers When The Node Connection Is Offline
        /// </summary>
        public OnDisConnected OnDisconnectedEvent;

        /// <summary>
        /// Used To Signal When A Connection To A Remote Node Is Sucessful
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public virtual void OnConnected(IPAddress address, ushort port, ConnectionType method)
        {
            Guard.Null(address, nameof(address), $"Cannot Fire 'OnConnected' Event, Supplied IP Address Is NULL!");
            Guard.AssertTrue(port.IsValidPortRange(), $"Cannot Fire 'OnConnected' Event, Supplied Port '{port}' Is Not Valid!");

            ConnectionMethod = method;
            Address = address;
            Port = port;

            OnConnectedEvent?.Invoke(this, new ConnectDisconnectEvent(true, address, port));
        }//end of public virtual void OnConnected(IPAddress address, ushort port)

        /// <summary>
        /// Used To When An Existing Connection Is Offline
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public virtual void OnDisconnected(IPAddress address, ushort port)
        {
            Guard.Null(address, nameof(address), $"Cannot Fire 'OnConnected' Event, Supplied IP Address Is NULL!");
            Guard.AssertTrue(port.IsValidPortRange(), $"Cannot Fire 'OnConnected' Event, Supplied Port '{port}' Is Not Valid!");

            ConnectionMethod = ConnectionType.Disconnected;

            OnDisconnectedEvent?.Invoke(this, new ConnectDisconnectEvent(false, address, port));
        }//end of public virtual void OnDisconnected(IPAddress address, ushort port)


        /// <summary>
        /// Used To Signal a New TX Has Been Detected
        /// </summary>
        /// <param name="txs"></param>
        public virtual void OnNewTX(string wallet, string account, List<Transaction> txs)
        {
            Guard.Null(txs, nameof(txs), $"Node '{Name}' ({Address}:{Port}) Detected A New TX But TX List Is NULL!");
            Guard.Null(wallet, nameof(wallet), $"Node '{Name}' ({Address}:{Port}) Detected A New TX But Wallet Name Is Null!");
            Guard.Null(account, nameof(account), $"Node '{Name}' ({Address}:{Port}) Detected A New TX But Account Name Is Null!");

            NewTransactionEvent?.Invoke(this, new NewTXEvent(wallet, account, txs));
        }//end of public virtual async void OnNewTX(List<Transaction> txs)


        /// <summary>
        /// Fires Off a "New Block" Event
        /// </summary>
        /// <param name="blockNumber">Block #</param>
        public virtual async void OnNewBlock(ulong blockNumber)
        {
            GetBlockResponse blockData = await _RestClient.GetBlock(blockNumber);

            Guard.Null(blockData, nameof(blockData), $"Node '{Name}' ({Address}:{Port}) Detected A New Block @ Height '{blockNumber}' But GetBlock Returned NULL!");

            this.NewBlockEvent?.Invoke(this, new NewBlockEvent(blockData.ToBlockHeader()));
        }//end of public virtual void OnNewBlock(ulong blockNumber)

    }//end of public partial class x42Node.Events
}

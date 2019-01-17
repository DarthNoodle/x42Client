﻿namespace x42Client.Responses
{
    public class NodeStatusResponse
    {
        public string agent { get; set; }
        public string version { get; set; }
        public string network { get; set; }
        public string coinTicker { get; set; }
        public int processId { get; set; }
        public int consensusHeight { get; set; }
        public int blockStoreHeight { get; set; }
        public object[] inboundPeers { get; set; }
        public Outboundpeer[] outboundPeers { get; set; }
        public string[] enabledFeatures { get; set; }
        public string dataDirectoryPath { get; set; }
        public string runningTime { get; set; }
        public float difficulty { get; set; }
        public int protocolVersion { get; set; }
        public bool testnet { get; set; }
        public int relayFee { get; set; }
    }
}

public class Outboundpeer
{
    public string version { get; set; }
    public string remoteSocketEndpoint { get; set; }
    public int tipHeight { get; set; }
}
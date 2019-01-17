namespace x42Client.Responses
{
    public class GetPeerInfoResponse
    {
        public int id { get; set; }
        public string addr { get; set; }
        public string addrlocal { get; set; }
        public string services { get; set; }
        public bool relaytxes { get; set; }
        public int lastsend { get; set; }
        public int lastrecv { get; set; }
        public int bytessent { get; set; }
        public int bytesrecv { get; set; }
        public int conntime { get; set; }
        public int timeoffset { get; set; }
        public int pingtime { get; set; }
        public int minping { get; set; }
        public int pingwait { get; set; }
        public int version { get; set; }
        public string subver { get; set; }
        public bool inbound { get; set; }
        public bool addnode { get; set; }
        public int startingheight { get; set; }
        public int banscore { get; set; }
        public int synced_headers { get; set; }
        public int synced_blocks { get; set; }
        public bool whitelisted { get; set; }
        public object inflight { get; set; }
        public object bytessent_per_msg { get; set; }
        public object bytesrecv_per_msg { get; set; }
    }//end of GetPeerInfoResponse
}

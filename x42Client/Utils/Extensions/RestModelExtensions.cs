using System.Collections.Generic;
using x42Client.Models;

namespace x42Client.Utils.Extensions
{
    public static class RestModelExtensions
    {

        public static List<Peer> ToPeersList(this Outboundpeer[] data)
        {
            List<Peer> peers = new List<Peer>();


            foreach (Outboundpeer peer in data)
            {
                peers.Add(new Peer
                {
                    Address = peer.remoteSocketEndpoint,
                    Version = peer.version,
                    TipHeight = peer.tipHeight
                });
            }//end of foreach

            return peers;
        }//end of public static List<Peer> ToPeersList(this Outboundpeer[] data)

    }//end of public static class RestModelExtensions
}

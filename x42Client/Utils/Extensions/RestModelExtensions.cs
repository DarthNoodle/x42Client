using System.Collections.Generic;
using x42Client.Models;
using x42Client.RestClient.Responses;
using x42Client.Utils.Validation;

namespace x42Client.Utils.Extensions
{
    public static class RestModelExtensions
    {

        /// <summary>
        /// Converts the API Peer List Data Structure To A More Friendly One
        /// </summary>
        public static Block ToBlock(this GetBlockResponse block)
        {
            Guard.Null(block, nameof(block));


            return new Block
            {
                Height = block.height,
                Version = block.version,
                MerkleRoot = block.merkleroot,
                Nonce = block.nonce,
                PreviousBlockHash = block.previousblockhash,
                Time = block.time
            };
        }//end of public static Block ToBlock(this GetBlockResponse block)


        /// <summary>
        /// Converts the API Peer List Data Structure To A More Friendly One
        /// </summary>
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

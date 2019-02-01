using System.Collections.Generic;
using x42Client.Models;
using x42Client.RestClient.Responses;
using x42Client.Utils.Logging;
using x42Client.Utils.Validation;

namespace x42Client.Utils.Extensions
{
    public static class RestModelExtensions
    {
        /// <summary>
        /// API Returns The Amount Without a Decimal Point (e.g. 10000000 should be 1.0000000)
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static decimal ParseAPIAmount(this long amount)
        {
            decimal returnValue = 0;
            string amountStr = $"{amount}";

            //are we dealing with non-whole numbers (e.g 0.10000000)
            if ($"{amount}".Length == 8)
            {
                if(decimal.TryParse($"0.{amount}", out returnValue))
                {
                    Logger.Error($"An Error Occured When Trying To Convert '{amount}' To the Proper Decimal Notation");
                    return -1;
                }

                return returnValue;
            }//end of if($"{amount}".Length == 8)



            //2000000000

            string newAmountWhole = amountStr.Substring(0, amountStr.Length - 8);
            string newAmountRemainder = amountStr.Substring(amountStr.Length - 8);

            if (decimal.TryParse($"{newAmountWhole}.{newAmountRemainder}", out returnValue))
            {
                Logger.Error($"An Error Occured When Trying To Convert '{amount}' To the Proper Decimal Notation");
                return -1;
            }

            return returnValue;
        }//end of public static decimal ParseAPIAmount(this long amount)

        /// <summary>
        /// Converts the API Peer List Data Structure To A More Friendly One
        /// </summary>
        public static BlockHeader ToBlockHeader(this GetBlockResponse block)
        {
            Guard.Null(block, nameof(block));


            return new BlockHeader
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

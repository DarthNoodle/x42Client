using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using x42Client.Responses;
using x42Client.Utils.Logging;
using x42Client.Utils.Validation;

namespace x42Client
{
    public partial class x42RestClient
    {

        /// <summary>
        /// Get Information On The Connected Peers
        /// </summary>
        /// <returns>List of Peers</returns>
        public async Task<List<GetPeerInfoResponse>> GetPeerInfo()
        {
            try
            {
                List<GetPeerInfoResponse> response = await base.SendGet<List<GetPeerInfoResponse>>("api/ConnectionManager/getpeerinfo");

                Guard.Null(response, nameof(response), "'api/ConnectionManager/getpeerinfo' API Response Was Null!");

                Logger.Debug($"Got '{response.Count}' Peers From 'getpeerinfo' API Request!");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Peer Information!", ex);

                throw; //pass it back up the stack? .. this seems memory intensive to me
            }//end of try-catch
        }//end of  public async Task<GetPeerInfoResponse> GetPeerInfo()


        /// <summary>
        /// Get The Block Header
        /// </summary>
        /// <param name="blockHash">Block Hash</param>
        public async Task<GetBlockHeaderResponse> GetBlockHeader(string blockHash)
        {
            try
            {
                Guard.Null(blockHash, nameof(blockHash), "Block Hash Cannot Be NULL/Empty!");

                GetBlockHeaderResponse response = await base.SendGet<GetBlockHeaderResponse>($"api/Node/getblockheader?hash={blockHash}&isJsonFormat=true");
                Guard.Null(response, nameof(response), "'api/Node/getblockheader' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Block Headers For Hash '{blockHash}'!", ex);

                throw;
            }//end of try-catch
        }//end of public async Task<GetBlockHeaderResponse> GetBlockHeader(string blockHash)

        /// <summary>
        /// Get The Block Header
        /// </summary>
        /// <param name="height">Block Height</param>
        public async Task<GetBlockHeaderResponse> GetBlockHeader(ulong height)
        {
            try
            {
                string blockHash = await this.GetBlockHash(height);

                Guard.Null(blockHash, nameof(blockHash), "Block Hash Cannot Be NULL/Empty!");

                GetBlockHeaderResponse response = await base.SendGet<GetBlockHeaderResponse>($"api/Node/getblockheader?hash={blockHash}&isJsonFormat=true");
                Guard.Null(response, nameof(response), "'api/Node/getblockheader' API Response Was Null!");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Block Headers For Height '{height}'!", ex);

                throw;
            }//end of try-catch
        }//end of public async Task<GetBlockHeaderResponse> GetBlockHeader(string blockHash)
    }//end of class
}

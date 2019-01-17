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
    }//end of class
}

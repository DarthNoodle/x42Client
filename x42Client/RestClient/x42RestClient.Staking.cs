using x42Client.Utils.Logging;
using x42Client.Utils.Validation;
using System;
using System.Threading.Tasks;
using x42Client.RestClient.Responses;

namespace x42Client.RestClient
{
    public partial class x42RestClient
    {

        /// <summary>
        /// Gets The Node Staking Information
        /// </summary>
        public async Task<GetStakingInfoResponse> GetStakingInfo()
        {
            try
            {
                GetStakingInfoResponse response = await base.SendGet<GetStakingInfoResponse>("api/Staking/getstakinginfo");

                Guard.Null(response, nameof(response), "'api/Staking/getstakinginfo' API Response Was Null!");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Staking Info!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<GetStakingInfoResponse> GetStakingInfo()
    }//end of class
}

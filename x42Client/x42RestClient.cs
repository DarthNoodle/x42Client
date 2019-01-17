using System;
using System.Threading.Tasks;
using x42Client.Responses;
using x42Client.Utils.Logging;
using x42Client.Utils.Validation;
using x42Client.Utils.Web;

namespace x42Client
{
    public partial class x42RestClient : APIClient
    {
        public x42RestClient(string baseURL) : base(baseURL)
        {
        }



        /// <summary>
        /// Gets Status Information For The Target Node
        /// </summary>
        public async Task<NodeStatusResponse> GetNodeStatus()
        {
            try
            {
                NodeStatusResponse response = await base.SendGet<NodeStatusResponse>("api/Node/status");

                Guard.Null(response, nameof(response), "'api/Node/status' API Response Was Null!");

                Logger.Debug($"Got Node Status Response!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting The Node Status!", ex);

                throw; //pass it back up the stack? .. this seems memory intensive to me
            }//end of try-catch
        }//end of public async Task<NodeStatusResponse> GetNodeStatus()






    }//end of public class x42RestClient:APIClient
}

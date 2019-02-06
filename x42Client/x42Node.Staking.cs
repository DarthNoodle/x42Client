using System;
using System.Threading.Tasks;
using x42Client.RestClient.Responses;
using x42Client.Utils.Extensions;
using x42Client.Utils.Logging;

namespace x42Client
{
    public partial class x42Node
    {
        /// <summary>
        /// Refreshes Staking Information
        /// </summary>
        public async void UpdateStakingInformation()
        {
            GetStakingInfoResponse stakingInfo = await _RestClient.GetStakingInfo();
            if (stakingInfo == null) { Logger.Error($"Node '{Name}' ({Address}:{Port}), An Error Occured When Getting Staking Information!"); }
            else
            {
                NetworkDifficulty = stakingInfo.difficulty;
                IsStaking = stakingInfo.staking;
                NetworkStakingWeight = stakingInfo.netStakeWeight;
                NodeStakingWeight = stakingInfo.weight.ParseAPIAmount();
                ExpectedStakingTimeMins = (stakingInfo.expectedTime / 60);//time is in seconds
            }//end of if (stakingInfo == null)
        }//end of public async void UpdateStakingInformation()


        /// <summary>
        /// Starts Staking
        /// </summary>
        /// <param name="wallet">Wallet To Enable</param>
        /// <param name="password">Wallet Password</param>
        public async Task<bool> StartStaking(string wallet, string password)
        {
            if (!WalletAccounts.ContainsKey(wallet)) { Logger.Error($"Node '{Name}' ({Address}:{Port}), Wallet Not Found, Run 'UpdateStaticData()' Manually"); return false; }

            //todo: Implement
            throw new NotImplementedException("IMPLEMENT ME BITCH!!");

            //return true;
        }//end of public async void StartStaking(string wallet, string password)

        /// <summary>
        /// Turn off Node Staking
        /// </summary>
        public async Task<bool> StopStaking()
        {

            throw new NotImplementedException("IMPLEMENT ME BITCH!!");

            //return true;
        }//end of public async Task<bool> StopStaking()
    }//end of x42Node.Staking
}

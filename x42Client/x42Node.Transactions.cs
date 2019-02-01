using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using x42Client.Enums;
using x42Client.Models;
using x42Client.RestClient.Responses;
using x42Client.Utils.Extensions;
using x42Client.Utils.Validation;

namespace x42Client
{
    public partial class x42Node
    {
        /// <summary>
        /// Processes the TX History For a Given Account
        /// </summary>
        /// <param name="wallet">wallet name</param>
        /// <param name="account">account name</param>
        private void ProcessAccountTXs(string wallet, string account, WalletTransactionshistory[] txs)
        {
            List<Transaction> accountTXs = new List<Transaction>();

            //this is the Key that will be used to store the TX's
            string walletAccountKey = $"{wallet}.{account}";

            foreach (WalletTransactionshistory tx in txs)
            {
                TXType txType = TXType.Staked;

                switch (tx.type)
                {
                    case "staked": txType = TXType.Staked; break;
                    case "received": txType = TXType.Received; break;
                    case "send": txType = TXType.Sent; break;
                }//end of switch (tx.type)

                //add the TX
                accountTXs.Add(new Transaction {
                     Address = tx.toAddress,
                     Amount = tx.amount.ParseAPIAmount(),
                     BlockID = tx.confirmedInBlock,
                     TXID = tx.id,
                     Timestamp = tx.timestamp,
                     Type = txType
                });
            }//end of foreach(WalletTransactionshistory tx in txs)

            if (AccountTXs.ContainsKey(walletAccountKey))
            {

                //get a list of new TX's
                List<Transaction> newTransactions = accountTXs.Except(AccountTXs[walletAccountKey]).ToList();
                
                //add to TX History
                AccountTXs[walletAccountKey].AddRange(newTransactions);

                //fire off a 'new TX' event
                OnNewTX(wallet, account, newTransactions);
            }
            else
            {
                AccountTXs.Add(walletAccountKey, accountTXs);
                
                //fire off a 'new TX' event
                OnNewTX(wallet, account, accountTXs);
            }//end of if-else if (AccountTXs.ContainsKey(walletAccountKey))

        }//end of private void ProcessAccountTX(string wallet, string account)


        /// <summary>
        /// Gets The Balance of The Wallet
        /// </summary>
        /// <param name="WalletName">Wallet Name</param>
        /// <param name="accountName">Account Name (Optional)</param>
        /// <returns>2 Balences, First Is Confirmed, Second Is Unconfirmed</returns>
        public async Task<Tuple<decimal, decimal>> GetWalletBalance(string walletName, string accountName = null)
        {

            GetWalletBalenceResponse walletBalance = await _RestClient.GetWalletBalance(walletName, accountName);
            Guard.Null(walletBalance, nameof(walletBalance), $"Node '{Name}' ({Address}:{Port}) An Error Occured When Trying To Get The Wallet Balance of Wallet '{walletName}' and Account '{accountName}'");

            decimal confirmedBalance = 0;
            decimal unConfirmedBalance = 0;

            foreach (AccountBalance accountBalence in walletBalance.balances)
            {
                confirmedBalance += accountBalence.amountConfirmed.ParseAPIAmount();
                unConfirmedBalance += accountBalence.amountUnconfirmed.ParseAPIAmount();
            }//end of foreach (AccountBalance accountBalence in walletBalence.balances)

            return new Tuple<decimal, decimal>(confirmedBalance, unConfirmedBalance);
        }//end of public decimal GetWalletBalence(string WalletName, string accountName)
    }//end of x42Node.Transactions
}

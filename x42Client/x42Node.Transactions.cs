using System.Collections.Generic;
using System.Linq;
using x42Client.Enums;
using x42Client.Models;
using x42Client.RestClient.Responses;

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
                     Amount = tx.amount,
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
    }//end of x42Node.Transactions
}

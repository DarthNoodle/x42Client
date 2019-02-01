﻿using System;
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
        /// Gets The Balence of The Wallet
        /// </summary>
        /// <param name="WalletName">Wallet Name</param>
        /// <param name="accountName">Account Name (Optional)</param>
        /// <returns>2 Balences, First Is Confirmed, Second Is Unconfirmed</returns>
        public async Task<Tuple<decimal, decimal>> GetWalletBalence(string walletName, string accountName = null)
        {

            GetWalletBalenceResponse walletBalence = await _RestClient.GetWalletBalence(walletName, accountName);
            Guard.Null(walletBalence, nameof(walletBalence), $"Node '{Name}' ({Address}:{Port}) An Error Occured When Trying To Get The Wallet Balence of Wallet '{walletName}' and Account '{accountName}'");

            decimal confirmedBalence = 0;
            decimal unConfirmedBalence = 0;

            foreach (AccountBalance accountBalence in walletBalence.balances)
            {
                confirmedBalence += accountBalence.amountConfirmed.ParseAPIAmount();
                unConfirmedBalence += accountBalence.amountUnconfirmed.ParseAPIAmount();
            }//end of foreach (AccountBalance accountBalence in walletBalence.balances)

            return new Tuple<decimal, decimal>(confirmedBalence, unConfirmedBalence);
        }//end of public decimal GetWalletBalence(string WalletName, string accountName)
    }//end of x42Node.Transactions
}

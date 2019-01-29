﻿using x42Client.Responses;
using x42Client.Utils.Logging;
using x42Client.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using x42Client.x42.Responses;
using x42Client.Requests;
using x42Client.Enums;

namespace x42Client
{
    /*
     [DONE] /api/Wallet/mnemonic
     /api/Wallet/create
     /api/Wallet/load
     /api/Wallet/recover
     /api/Wallet/recover-via-extpubkey
     [DONE] /api/Wallet/general-info
     [DONE] /api/Wallet/history
     [DONE] /api/Wallet/balance
     [DONE] /api/Wallet/received-by-address
     /api/Wallet/maxbalance
     /api/Wallet/spendable-transactions
     /api/Wallet/estimate-txfee
     [DONE] /api/Wallet/build-transaction
     /api/Wallet/send-transaction
     [DONE] /api/Wallet/files
     [DONE] /api/Wallet/account
     [DONE] /api/Wallet/accounts
     /api/Wallet/unusedaddress
     [DONE] /api/Wallet/unusedaddresses
     [DONE] /api/Wallet/addresses
     [DONE] /api/Node/validateaddress
     /api/Wallet/remove-transactions
     /api/Wallet/extpubkey
     /api/Wallet/sync
     /api/Wallet/syncfromdate
     /api/Wallet/splitcoins
     */
    public partial class x42RestClient
    {

        /// <summary>
        /// Generates A New Mnemonic Foe Use In Creating A Wallet
        /// </summary>
        /// <param name="language">What Language To Use (Default: English)</param>
        /// <param name="wordCount">How Many Words To Generate (Default 24)</param>
        public async Task<string> CreateMnemonic(MnemonicWordCount wordCount = MnemonicWordCount.Words_24, MnemomicLanguage language = MnemomicLanguage.English)
        {
            try
            {
                string languageParam = "English";

                switch (language)
                {
                    case MnemomicLanguage.ChineseSimplified: languageParam = "ChineseSimplified"; break;
                    case MnemomicLanguage.ChineseTraditional: languageParam = "ChineseTraditional"; break;
                    case MnemomicLanguage.English: break; // its the default
                    case MnemomicLanguage.French: languageParam = "French"; break;
                    case MnemomicLanguage.Japanese: languageParam = "Japanese"; break;
                    case MnemomicLanguage.Spanish: languageParam = "Spanish"; break;
                }//end of switch (language)

                string response = await base.SendGet<string>($"api/Wallet/mnemonic?language={languageParam}&wordCount=${wordCount}");

                Guard.Null(response, nameof(response), "'api/Wallet/mnemonic' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Creating a Mnemonic With The Paramiters Language '{language}', Word Count '{wordCount}'", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<string> CreateMnemonic(MnemomicLanguage language, MnemonicWordCount wordCount)

        /// <summary>
        /// Validates Whether The Supplied Address Is Correct
        /// </summary>
        /// <param name="address">x42 Address</param>
        public async Task<bool> ValidateAddress(string address)
        {
            try
            {
                Guard.Null(address, nameof(address), "Cannot Validate User Address, It Is Null/Empty!");

                Logger.Debug($"Validating Address '{address.Trim()}'");

                HttpStatusCode responseCode = await base.SendGet($"api/Node/validateaddress?address={address.Trim()}");


                switch (responseCode)
                {
                    case HttpStatusCode.OK:
                        Logger.Debug($"Address Validation '{address.Trim()}' Was Good!");
                        return true;
                    default:
                        Logger.Debug($"Address Validation '{address.Trim()}' Failed!");
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Validating Address '{address.Trim()}'", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<bool> ValidateAddress(string address)

        /// <summary>
        /// Get General Wallet Information
        /// </summary>
        /// <param name="walletName">Name of The Wallet</param>
        public async Task<WalletGeneralInfoResponse> GetWalletGeneralInfo(string walletName)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Get Wallet General Info, Provided Name Is NULL/Empty!");

                WalletGeneralInfoResponse response = await base.SendGet<WalletGeneralInfoResponse>($"api/Wallet/general-info?Name={walletName.Trim()}");

                Guard.Null(response, nameof(response), "'api/Wallet/general-info' API Response Was Null!");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting General Wallet Info, For Wallet '{walletName.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<WalletGeneralInfoResponse> GetWalletGeneralInfo()

        /// <summary>
        /// Generate A New Account In The Wallet
        /// </summary>
        /// <param name="walletName">Wallet Name</param>
        /// <param name="password">Wallet Password</param>
        /// <returns></returns>
        public async Task<string> CreateAccount(string walletName, string password)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Create Wallet Account, Provided Name Is NULL/Empty!");
                Guard.Null(password, nameof(password), "Unable To Create Wallet Account, Provided Password Is NULL/Empty!");

                CreateAccountRequest request = new CreateAccountRequest
                {
                    walletName = walletName,
                    password = password
                };

                string response = await base.SendPostJSON<string>("api/Wallet/account", request);

                Guard.Null(response, nameof(response), "'api/Wallet/account' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Creating Wallet Account Info, For Wallet '{walletName.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<string> CreateAccount(string walletName, string password)

        /// <summary>
        /// Gets The Physical Wallet File Paths
        /// </summary>
        public async Task<GetWalletFilesResponse> GetWalletFiles()
        {
            try
            {
                GetWalletFilesResponse response = await base.SendGet<GetWalletFilesResponse>("api/Wallet/files");

                Guard.Null(response, nameof(response), "'api/Wallet/files' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Wallet File Paths!!", ex);

                throw;
            }
        }//end of public async Task<GetWalletFilesResponse> GetWalletFiles()

        /// <summary>
        /// Gets All Accounts Within The Wallet
        /// </summary>
        /// <param name="walletName">Name of Wallet</param>
        public async Task<List<string>> GetWalletAccounts(string walletName)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Get Wallet Account Info, Provided Name Is NULL/Empty!");

                List<string> response = await base.SendGet<List<string>>($"api/Wallet/accounts?WalletName={walletName.Trim()}");

                Guard.Null(response, nameof(response), "'api/Wallet/general-info' API Response Was Null!");

                Guard.AssertTrue(response.Count > 0, $"No Account Information Returned For Wallet '{walletName.Trim()}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Wallet Account Info, For Wallet '{walletName.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<List<string>> GetWalletAccounts(string walletName)


        /// <summary>
        /// Generate A List of Unused Addresses
        /// </summary>
        /// <param name="walletName">Name of Wallet</param>
        /// <param name="account">Name of Account</param>
        /// <param name="count"># of Addresses To Generate</param>
        /// <returns></returns>
        public async Task<List<string>> GenerateUnusedWalletAddresses(string walletName, string account, int count)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Get Generate Unused Addresses, Provided Wallet Name Is NULL/Empty!");
                Guard.Null(account, nameof(account), "Unable To Get Generate Unused Addresses, Provided Account Name Is NULL/Empty!");
                Guard.AssertTrue(count > 0, $"Invalid Number '{count}' of Addresses Specified");

                List<string> response = await base.SendGet<List<string>>($"api/Wallet/unusedaddresses?WalletName={walletName.Trim()}&AccountName={Uri.EscapeDataString(account.Trim())}&Count={count}");

                Guard.Null(response, nameof(response), "'api/Wallet/unusedaddresses' API Response Was Null!");

                Guard.AssertTrue(response.Count > 0, $"No Unused Addresses Returned For Wallet '{walletName.Trim()}' and Account '{account.Trim()}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Unused Addresses, For Wallet '{walletName.Trim()}' and Account '{account.Trim()}'!", ex);
                throw;
            }//end of try-catch

        }//end of public async Task<List<string>> GenerateUnusedAddresses(string walletName, string account, int count)

        /// <summary>
        /// List All Addresses In A Wallet
        /// </summary>
        /// <param name="walletName">Name of Wallet</param>
        /// <param name="account">Name of Account</param>
        public async Task<GetWalletAddressesResponse> GetWalletAddresses(string walletName, string account)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Get Wallet Addresses, Provided Wallet Name Is NULL/Empty!");
                Guard.Null(account, nameof(account), "Unable To Get Wallet Addresses, Provided Account Name Is NULL/Empty!");

                GetWalletAddressesResponse response = await base.SendGet<GetWalletAddressesResponse>($"api/Wallet/addresses?WalletName={walletName.Trim()}&AccountName={Uri.EscapeDataString(account.Trim())}");

                Guard.Null(response, nameof(response), "'api/Wallet/unusedaddresses' API Response Was Null!");

                Guard.AssertTrue(response.addresses.Length > 0, $"No Addresses Returned For Wallet '{walletName.Trim()}' and Account '{account.Trim()}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Addresses, For Wallet '{walletName.Trim()}' and Account '{account.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }

        /// <summary>
        /// Gets The Balence Info For An Address In A Wallet
        /// </summary>
        /// <param name="address">Address To Check</param>
        /// <returns></returns>
        public async Task<GetRecievedAddressInfoResponse> GetRecievedAddressBalence(string address)
        {
            try
            {
                Guard.AssertTrue(await this.ValidateAddress(address.Trim()), $"Supplied Address '{address.Trim()}' Is Not Valid!");

                GetRecievedAddressInfoResponse response = await base.SendGet<GetRecievedAddressInfoResponse>($"api/Wallet/received-by-address?Address={address.Trim()}");

                Guard.Null(response, nameof(response), "'api/Wallet/received-by-address' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Addresses Balence, Address'{address.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<GetRecievedAddressInfoResponse> GetRecievedAddressBalence(string address)

        /// <summary>
        /// Gets The Balence of The Wallet
        /// </summary>
        /// <param name="walletName">Name of Wallet</param>
        /// <param name="account">Name of Account (Leave Blank for All)</param>
        /// <returns></returns>
        public async Task<GetWalletBalenceResponse> GetWalletBalence(string walletName, string account = null)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Get Wallet Addresses, Provided Wallet Name Is NULL/Empty!");

                StringBuilder queryURL = new StringBuilder($"api/Wallet/balance?WalletName={walletName.Trim()}");

                if (!string.IsNullOrWhiteSpace(account)) { queryURL.Append($"&AccountName={Uri.EscapeDataString(account.Trim())}"); }

                GetWalletBalenceResponse response = await base.SendGet<GetWalletBalenceResponse>(queryURL.ToString());

                Guard.Null(response, nameof(response), "'api/Wallet/balance' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting Ballence For Wallet '{walletName.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<string> GetWalletBalence(string walletName, string accountName = null)


        /// <summary>
        /// Get The Transaction History
        /// </summary>
        /// <param name="walletName">Wallet Name</param>
        /// <param name="account">Account Name (Leave Blank for all)</param>
        /// <param name="skip">Skip X Records (Leave Blank for all)</param>
        /// <param name="take">Take X Records (Leave Blank for all)</param>
        /// <param name="searchQuery">Search Query To Use (Leave Blank for all)</param>
        public async Task<GetWalletHistoryResponse> GetWalletHistory(string walletName, string account = null, int skip = -1, int take = -1, string searchQuery = null)
        {
            try
            {
                StringBuilder queryURL = new StringBuilder($"api/Wallet/history?WalletName={walletName.Trim()}");

                if (!string.IsNullOrWhiteSpace(account)) { queryURL.Append($"&AccountName={Uri.EscapeDataString(account.Trim())}"); }
                if (skip > -1) { queryURL.Append($"&Skip={skip}"); }
                if (take > -1) { queryURL.Append($"&Take={take}"); }
                if (!string.IsNullOrWhiteSpace(searchQuery)) { queryURL.Append($"&SearchQuery={Uri.EscapeDataString(searchQuery.Trim())}"); }

                GetWalletHistoryResponse response = await base.SendGet<GetWalletHistoryResponse>(queryURL.ToString());

                Guard.Null(response, nameof(response), "'api/Wallet/history' API Response Was Null!");

                return response;
            }
            catch (Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Getting History For Wallet '{walletName.Trim()}'!", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<GetWalletHistoryResponse> GetWalletHistory(string walletName, string account = null, int skip = -1, int take = -1, string searchQuery = null)


        /// <summary>
        /// Build a TX Hex String Ready To Be Broadcast on The Network
        /// </summary>
        /// <param name="walletName">Wallet Name</param>
        /// <param name="account">Account Name</param>
        /// <param name="password">Wallet Password</param>
        /// <param name="destinationAddress">Destination Address</param>
        /// <param name="amount">Amount To Send</param>
        /// <param name="allowUnconfirmed">Include Unconfirmed TX's</param>
        /// <param name="shuffleCoins">Coin Control?</param>
        /// <returns>TX Hex String</returns>
        public async Task<BuildTXResponse> BuildTransaction(string walletName, string account, string password, string destinationAddress, long amount, bool allowUnconfirmed = false, bool shuffleCoins = true)
        {
            try
            {
                Guard.Null(walletName, nameof(walletName), "Unable To Build TX, Provided Wallet Name Is NULL/Empty!");
                Guard.Null(account, nameof(account), "Unable To Build TX, Provided Account Name Is NULL/Empty!");
                Guard.Null(password, nameof(password), "Unable To Build TX, Provided Password Is NULL/Empty!");
                Guard.AssertTrue(await this.ValidateAddress(destinationAddress), $"Unable To Build TX, Destination Address '{destinationAddress.Trim()}' Is Not Valid!");

                GetWalletBalenceResponse accountBalence = await this.GetWalletBalence(walletName, account);
                Guard.Null(accountBalence, nameof(accountBalence), $"Unable To Build TX, Account '{account}' Balence Request Was NULL/Empty!");

                Guard.AssertTrue((accountBalence.balances[0].amountConfirmed > 0), $"Unable To Build TX, Insufficient Funds! Trying To Send '{amount}' When Account Only Has '{accountBalence.balances[0].amountConfirmed}'");

                BuildTXRequest buildRequest = new BuildTXRequest
                {
                    feeAmount = "0",
                    password = password,
                    walletName = walletName,
                    accountName = account,
                    recipients = new x42Recipient[] {new x42Recipient() { destinationAddress = destinationAddress, amount = amount.ToString()} }, 
                    allowUnconfirmed = allowUnconfirmed,
                    shuffleOutputs = shuffleCoins
                };

                BuildTXResponse response = await base.SendPostJSON<BuildTXResponse>("api/Wallet/build-transaction", buildRequest);

                Guard.Null(response, nameof(response), "'api/Wallet/build-transaction' API Response Was Null!");

                return response;
            }
            catch(Exception ex)
            {
                Logger.Fatal($"An Error '{ex.Message}' Occured When Building A TX! [Wallet: '{walletName.Trim()}', Account: '{account.Trim()}', To: '{destinationAddress.Trim()}', Amount: '{amount}'", ex);
                throw;
            }//end of try-catch
        }//end of public async Task<BuildTXResponse> BuildTransaction(string walletName, string account, string destinationAddress, decimal amount, bool allowUnconfirmed = false, bool shuffleCoins = true)

    }//end of class
}

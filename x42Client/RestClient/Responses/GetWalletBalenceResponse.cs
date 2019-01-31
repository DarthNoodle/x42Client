namespace x42Client.RestClient.Responses
{
    public class GetWalletBalenceResponse
    {
        public AccountBalance[] balances { get; set; }
    }

    public class AccountBalance
    {
        public string accountName { get; set; }
        public string accountHdPath { get; set; }
        public int coinType { get; set; }
        public long amountConfirmed { get; set; }
        public int amountUnconfirmed { get; set; }
    }
}



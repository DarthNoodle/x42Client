namespace x42Client.x42.Responses
{
    public class GetWalletAddressesResponse
    {
        public WalletAddress[] addresses { get; set; }
    }

    public class WalletAddress
    {
        public string address { get; set; }
        public bool isUsed { get; set; }
        public bool isChange { get; set; }
    }
}


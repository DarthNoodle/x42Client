namespace x42Client.RestClient.Responses
{
    public class GetRecievedAddressInfoResponse
    {
        public string address { get; set; }
        public int coinType { get; set; }
        public long amountConfirmed { get; set; }
        public int amountUnconfirmed { get; set; }
    }
}

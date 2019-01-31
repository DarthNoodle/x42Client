namespace x42Client.Requests
{
    public class CreateWalletRequest
    {
        public string mnemonic { get; set; }
        public string password { get; set; }
        public string passphrase { get; set; }
        public string name { get; set; }
    }
}


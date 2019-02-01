using x42Client.Enums;

namespace x42Client.Models
{
    public class Transaction
    {
        public TXType Type;
        public string Address;
        public string TXID;
        public decimal Amount;
        public ulong BlockID;
        public string Timestamp;
    }
}
/*
 * 
      "transactionsHistory": [
        {
          "type": "staked",
          "toAddress": "XQXeqrNFad2Uu7k3E9Dx5t4524fBsnEeSw",
          "id": "efed57068ecd26114e9da6870af6fafd6e835aa3513497d0d939f7c98ea26aad",
          "amount": 2000000000,
          "payments": [],
          "confirmedInBlock": 223988,
          "timestamp": "1549020768"
        }, 
*/

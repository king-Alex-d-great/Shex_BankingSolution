

namespace WebUI.domain.Model
{
    public class TransferViewModel 
    {
        public int SenderAccountNumber { get; set; }
        public int RecipientAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
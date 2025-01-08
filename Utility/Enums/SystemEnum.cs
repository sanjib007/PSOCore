namespace Utility.Enums
{
    public static class SystemEnum
    {

        public enum LinkedAccountType
        {
            Bank,
            Card
        } 
        public enum TransactionType
        {
            Payment,
            Addmoney,
            Void,
            Refund,
            Transfer
        }
        public enum TransactionCurrencyCode
        {
            BDT,
            USD
        }
        public enum BankLinkStatus
        {
            Processing,
            Pending,
            Linked,
            Delink
        }
        public enum TxnStatus
        {
            Initiate,
            Authorized,
            Settled,
            Voided,
            Fail,
            Processing,
            Pending,
            Refunded,
            PartialRefunded,
            Adjusted,
            Cancled,
            Cancel,
            Reversed,
            Success,

            ProcessingCore,
            CompletedCore

            

        }
    }
}



public enum LinkedAccountType
{
    Bank,
    Card,
    MFS
}
public enum TransactionType
{
    Payment,
    Addmoney,
    Void,
    Refund,
    Transfer
}
public enum TxnStatus
{
    Authorized,
    Settled,

    Initiate,
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

namespace PSO.Checkout.Web.Models.Request;

public record InitiateTransactionRequest(string Identifier, long ChannelTypeId);


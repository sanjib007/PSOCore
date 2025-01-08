using DAL.Models.Response;
using Utility;

namespace BLL.Services.Communication.Cybersource;

public interface ICybersourceCommunicationService
{
    Task<BankComAddmoneyResponse> InitatePayment(string transactionId, decimal transactionAmount);
}

public class CybersourceCommunicationService(HttpClient _httpClient, CybersourceSecurity _cybersourceSecurity) : ICybersourceCommunicationService
{
    #region Cybersource - visa

    public async Task<BankComAddmoneyResponse> InitatePayment(string transactionId, decimal transactionAmount)
    {
        var parameters = new Dictionary<string, string>
        {
            { "access_key", _cybersourceSecurity.ACCESS_KEY },
            { "profile_id", _cybersourceSecurity.PROFILE_ID },
            { "transaction_uuid", transactionId },
            { "reference_number",transactionId },
            { "auth_trans_ref_no", transactionId },
            { "consumer_id", "Dmoney" },
            { "signed_field_names", "access_key,profile_id,transaction_uuid,consumer_id,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,auth_trans_ref_no,amount,currency,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_country,bill_to_postal_code" },
            { "transaction_type", "sale" },
            { "currency", "BDT" },
            { "unsigned_field_names", "" },
            { "signed_date_time", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") },
            { "locale", "en" },
            { "amount", transactionAmount.ToTruncateDecimal().ToString() },
            { "bill_to_forename", "Mr" },
            { "bill_to_surname", "dmoney" },
            { "bill_to_email", "null@cybersource.com" },
            { "bill_to_phone", "00000000000" },
            { "bill_to_address_line1", "Gulshan" },
            { "bill_to_address_city", "Dhaka" },
            { "bill_to_address_country", "BD" },
            { "bill_to_postal_code", "0000" }

        };
        // Sign the parameters
        var signature = _cybersourceSecurity.Sign(parameters);
        parameters.Add("signature", signature);

        // Now you can proceed with your logic using the signature
        return new BankComAddmoneyResponse
        {
            StatusCode = 200,
            Message = "redirect this url",
            IsFormRedirect = true,
            RedirectUrl = _cybersourceSecurity.PayUrl,
            FormData = parameters

        };

    }


    #endregion

}

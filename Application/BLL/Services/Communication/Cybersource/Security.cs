using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services.Communication.Cybersource;

public class CybersourceSecurity
{
    public string HMAC_SHA256 = "sha256";
    public string? SECRET_KEY { get; private set; }
    public string? ACCESS_KEY { get; private set; }
    public string? PROFILE_ID { get; private set; }
    public string? PayUrl { get; private set; }
    private readonly IConfiguration _configuration;

    public CybersourceSecurity(IConfiguration configuration)
    {
        _configuration = configuration;
        this.SECRET_KEY = _configuration.GetSection("BankCommunication:Cybersource:SECRET_KEY").Value;
        this.ACCESS_KEY = _configuration.GetSection("BankCommunication:Cybersource:ACCESS_KEY").Value;
        this.PROFILE_ID = _configuration.GetSection("BankCommunication:Cybersource:PROFILE_ID").Value;
        this.PayUrl = _configuration.GetSection("BankCommunication:Cybersource:PayUrl").Value;
    }

    public string Sign(Dictionary<string, string> parameters)
    {
        return SignData(BuildDataToSign(parameters), SECRET_KEY);
    }

    private string SignData(string data, string secretKey)
    {
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash);
    }

    private string BuildDataToSign(Dictionary<string, string> parameters)
    {
        var signedFieldNames = parameters["signed_field_names"].Split(',');
        var dataToSign = new List<string>();
        foreach (var field in signedFieldNames)
        {
            dataToSign.Add(field + "=" + parameters[field]);
        }
        return CommaSeparate(dataToSign);
    }

    private string CommaSeparate(List<string> dataToSign)
    {
        return string.Join(",", dataToSign);
    }
}

namespace PSO.Merchant.Spa.Models.Response;

public class GetUseInfoReponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public GetUseInfoReponseData data { get; set; }
}

public class GetUseInfoReponseData
{
    public string fullName { get; set; }
    public string title { get; set; }
    public string fathersName { get; set; }
    public string mothersName { get; set; }
    public string gender { get; set; }
    public string nationalId { get; set; }
    public string phoneNumber { get; set; }
    public string status { get; set; }
    public string postalCode { get; set; }
    public string policeStation { get; set; }
    public string dateOfBirth { get; set; }
    public string companyName { get; set; }
    public string companyShortName { get; set; }
    public string companyPhone { get; set; }
}


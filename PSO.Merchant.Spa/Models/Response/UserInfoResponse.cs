namespace PSO.Merchant.Spa.Models.Response;

public class UserInfoResponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public UserInfoResponseData data { get; set; }
}

public class UserInfoResponseData
{
    public UserInfoResponsePersonalinfo personalInfo { get; set; }
    public UserInfoResponseBusinessinfo businessInfo { get; set; }
    public UserInfoResponseBankinfo bankInfo { get; set; }
}

public class UserInfoResponsePersonalinfo
{
    public string fullName { get; set; }
    public object fatherName { get; set; }
    public object motherName { get; set; }
    public object gender { get; set; }
    public object dateOfBirth { get; set; }
    public object phoneNumber { get; set; }
    public object city { get; set; }
    public object email { get; set; }
    public object nidNumber { get; set; }
    public object nidPIN { get; set; }
    public object address1 { get; set; }
    public object address2 { get; set; }
    public object profileImage { get; set; }
}

public class UserInfoResponseBusinessinfo
{
    public string title { get; set; }
    public object companyName { get; set; }
    public object companyPhone { get; set; }
    public object companyShortName { get; set; }
    public object companyType { get; set; }
    public object companyWebsite { get; set; }
    public object companyEmail { get; set; }
    public object companyLogo { get; set; }
    public object companyAddress1 { get; set; }
    public object companyAddress2 { get; set; }
}

public class UserInfoResponseBankinfo
{
    public string bankName { get; set; }
    public string bankAccountName { get; set; }
    public string bankAccountNo { get; set; }
    public string branch { get; set; }
    public string routingNo { get; set; }
}
namespace DAL.Models.Request;

public class VisaCancelRequest
{
    public string utf8 { get; set; }
    public string req_locale { get; set; }
    public string req_payer_authentication_indicator { get; set; }
    public string signature { get; set; }
    public string auth_trans_ref_no { get; set; }
    public string req_card_type_selection_indicator { get; set; }
    public string req_bill_to_surname { get; set; }
    public string req_bill_to_address_city { get; set; }
    public string req_bill_to_phone { get; set; }
    public string req_bill_to_forename { get; set; }
    public string req_payment_method { get; set; }
    public string req_payer_authentication_merchant_name { get; set; }
    public string req_amount { get; set; }
    public string req_bill_to_email { get; set; }
    public string req_currency { get; set; }
    public string decision { get; set; }
    public string message { get; set; }
    public string signed_field_names { get; set; }
    public string req_transaction_uuid { get; set; }
    public string req_consumer_id { get; set; }
    public string req_bill_to_address_country { get; set; }
    public string req_transaction_type { get; set; }
    public string req_access_key { get; set; }
    public string req_profile_id { get; set; }
    public string req_reference_number { get; set; }
    public string signed_date_time { get; set; }
    public string req_bill_to_address_line1 { get; set; }
}


namespace DAL.Models.Request;

public class VisaPaymentApproveRequest
{
    public string utf8 { get; set; }
    public string auth_cv_result { get; set; }
    public string req_locale { get; set; }
    public string req_payer_authentication_indicator { get; set; }
    public string payer_authentication_acs_transaction_id { get; set; }
    public string auth_trans_ref_no { get; set; }
    public string req_card_type_selection_indicator { get; set; }
    public string payer_authentication_enroll_veres_enrolled { get; set; }
    public string req_bill_to_surname { get; set; }
    public string req_card_expiry_date { get; set; }
    public string merchant_advice_code { get; set; }
    public string req_bill_to_phone { get; set; }
    public string card_type_name { get; set; }
    public string auth_amount { get; set; }
    public string auth_response { get; set; }
    public string bill_trans_ref_no { get; set; }
    public string req_payment_method { get; set; }
    public string req_payer_authentication_merchant_name { get; set; }
    public string auth_time { get; set; }
    public string payer_authentication_enroll_e_commerce_indicator { get; set; }
    public string transaction_id { get; set; }
    public string req_card_type { get; set; }
    public string payer_authentication_transaction_id { get; set; }
    public string payer_authentication_pares_status { get; set; }
    public string payer_authentication_cavv { get; set; }
    public string req_consumer_id { get; set; }
    public string auth_avs_code { get; set; }
    public string auth_code { get; set; }
    public string payer_authentication_specification_version { get; set; }
    public string req_bill_to_address_country { get; set; }
    public string auth_cv_result_raw { get; set; }
    public string req_profile_id { get; set; }
    public string signed_date_time { get; set; }
    public string req_bill_to_address_line1 { get; set; }
    public string req_card_number { get; set; }
    public string signature { get; set; }
    public string req_bill_to_address_city { get; set; }
    public string auth_cavv_result { get; set; }
    public string reason_code { get; set; }
    public string req_bill_to_forename { get; set; }
    public string req_payer_authentication_acs_window_size { get; set; }
    public string request_token { get; set; }
    public string auth_cavv_result_raw { get; set; }
    public string req_amount { get; set; }
    public string req_bill_to_email { get; set; }
    public string payer_authentication_reason_code { get; set; }
    public string auth_avs_code_raw { get; set; }
    public string req_currency { get; set; }
    public string decision { get; set; }
    public string message { get; set; }
    public string signed_field_names { get; set; }
    public string req_transaction_uuid { get; set; }
    public string payer_authentication_eci { get; set; }
    public string req_transaction_type { get; set; }
    public string payer_authentication_xid { get; set; }
    public string req_access_key { get; set; }
    public string req_reference_number { get; set; }
    public string payer_authentication_validate_result { get; set; }
    public string auth_reconciliation_reference_number { get; set; }
}


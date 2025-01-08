namespace PSO.Merchant.Spa.Models.Response;

public class DashbordReportResponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public DashbordReportResponseData data { get; set; }
}
public class DashbordReportResponseData
{
    public int todayCount { get; set; }
    public decimal todayAmount { get; set; }
    public int refundCount { get; set; }
}

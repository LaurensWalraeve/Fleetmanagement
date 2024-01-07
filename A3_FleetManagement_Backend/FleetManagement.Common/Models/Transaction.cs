namespace FleetManagement.Common.Models;

//extra klasse als er tijd over is: transacties van tankkaarten bijhouden.
public class Transaction
{
    public string TransactionID { get; set; }
    public DateTime DateTime { get; set; }
    public FuelCard FuelCard { get; set; }
}
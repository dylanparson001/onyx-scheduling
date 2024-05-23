namespace OnyxScheduling.Dtos;

public class InvoiceItemDto
{
    public int Id { get; set; }
    public int Category_Id { get; set; }
    public string Item_Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
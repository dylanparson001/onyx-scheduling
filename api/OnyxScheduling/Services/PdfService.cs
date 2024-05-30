using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OnyxScheduling.Controllers;

public class PdfService
{
    private string pdfFolderPath = "./Invoices/";
    public void GeneratePdf(Invoices invoice, User customer, User technician, List<Invoice_Items> itemsFromJob)
    {
        string fileName =
            $"./Invoices/{customer.FirstName} {customer.LastName} {invoice.ScheduledEndDateTime.ToLongDateString()}.pdf";
        if (!Directory.Exists("Invoices"))
        {
            Directory.CreateDirectory("./Invoices");
        }

        
        if (CheckFileExists(fileName))
        {
            string safeFirstName = customer.FirstName.Replace(":", "-").Replace("/", "-").Replace("\\", "-");
            string safeLastName = customer.LastName.Replace(":", "-").Replace("/", "-").Replace("\\", "-");
            string safeDate = invoice.ScheduledEndDateTime.ToLongDateString().Replace(":", "-").Replace("/", "-").Replace("\\", "-");
            string safeTime = invoice.ScheduledEndDateTime.ToShortTimeString().Replace(":", "-").Replace("/", "-").Replace("\\", "-");

            fileName = $"./Invoices/{safeFirstName} {safeLastName} {safeDate} {safeTime}.pdf";
        }
        // if (CheckFileExists(fileName))
        // {
        //     fileName =
        //         $"./Invoices/{customer.FirstName} {customer.LastName} {invoice.ScheduledEndDateTime.ToLongDateString()} {invoice.ScheduledEndDateTime.ToShortTimeString()}.pdf";
        // }
        
        Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Background(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));
        
                    page.Header()
                        .Text($"{customer.FirstName} {customer.LastName} Invoice:")
                        .SemiBold().FontSize(36);
        
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            x.Item().Text($"Your Technician: {technician.FirstName} {technician.LastName}");
                            x.Item().Text("Job Status");
                            x.Item().Text("Invoice Items: ");
                            foreach (var invoiceItems in itemsFromJob)
                            {
                                x.Item().Text($"{invoiceItems.Item_Name} \t {invoiceItems.Quantity} \t ${invoiceItems.Price}");
                            }

                            x.Item().Text($"Total: ${invoice.Total_Price}");
                        });
        
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Line("Onyx Solutions");
                            x.Span("Page");
                            x.CurrentPageNumber();
                            
                        });
                });
            })
            .GeneratePdf(fileName);
    }

    private bool CheckFileExists(string fileName)
    {
        if (File.Exists(fileName))
        {
            return true;
        }

        return false;
    }
    
    
}
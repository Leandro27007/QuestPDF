// See https://aka.ms/new-console-template for more information

using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

ReceiptDocument recibo = new ReceiptDocument();




// code in your main method
var document = Document.Create(container =>
{
    // container.Page(pagina =>
    // {
    //     pagina.Size(PageSizes.A4);
    //
    //     pagina.Margin(2f,Unit.Centimetre);
    //     
    //     pagina.Header().Text("Aprendamos .NET")
    //         .Bold()
    //         .FontSize(50).FontColor(Colors.Red.Medium).AlignCenter();
    //     
    //     
    //     pagina.Content().Column(columna =>
    //     {
    //         columna.Spacing(20);
    //         columna.Item().Text(Placeholders.LoremIpsum());
    //         columna.Item().Image(Placeholders.Image(200, 100));
    //         
    //         columna.Item().Row(row =>
    //         {
    //             row.Spacing(20);
    //             
    //             row.RelativeItem().Column(c1 =>
    //             {
    //                 c1.Item().Image(Placeholders.Image(200, 100));
    //                 c1.Item().Text(Placeholders.LoremIpsum());
    //             });
    //             row.RelativeItem().Column(c1 =>
    //             {
    //                 c1.Item().Image(Placeholders.Image(200, 100));
    //                 c1.Item().Text(Placeholders.LoremIpsum());
    //             });
    //             row.RelativeItem().Column(c1 =>
    //             {
    //                 c1.Item().Image(Placeholders.Image(200, 100));
    //                 c1.Item().Text(Placeholders.LoremIpsum());
    //             });
    //
    //         });
    //     });
    //
    //     pagina.Footer().Text(x =>
    //     {
    //         x.CurrentPageNumber();
    //         x.Span(" de ");
    //         x.TotalPages();
    //
    //     });
    //
    //
    // });

     container = recibo.Compose(container);
});

// instead of the standard way of generating a PDF file

// optionally, you can specify an HTTP port to communicate with the previewer host (default is 12500)
document.ShowInCompanion();
 
 
 public class ReceiptDocument 
{
    public IDocumentContainer Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(826, 967); 
            page.Margin(1f, Unit.Centimetre);
            page.DefaultTextStyle(x => x.FontSize(12));
            page.Header().ShowOnce().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().ShowIf(x => x.PageNumber == x.TotalPages).Element(d =>
            {
                d.Text("Gracias por su compra!").AlignCenter();
            });
            
        });

        return container;
    }

    void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text("Tienda XYZ").FontSize(20).Bold().AlignCenter();
            column.Item().Text("Dirección: Calle Falsa 12333333333333ddddddd").AlignCenter();
            column.Item().Text("Tel: (123) 456-7890").AlignCenter();
            column.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").AlignCenter();
            column.Item().Text($"Hora: {DateTime.Now:HH:mm:ss}").AlignCenter();
        });
    }

    void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(5);

            column.Item().Row(row =>
            {
                row.RelativeItem().Text("Producto").Bold(); 
                row.RelativeItem().Text("Cant.").Bold();
                row.RelativeItem().Text("Precio").Bold();
                row.RelativeItem().Text("Total").Bold();
            });

            // Datos ficticios de productos
            var products = new[]
            {
                new { Name = "Producto A", Quantity = 2, Price = 10.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto B", Quantity = 1, Price = 20.00 },
                new { Name = "Producto C", Quantity = 3, Price = 5.00 }
            };

            foreach (var product in products)
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text(product.Name);
                    row.RelativeItem().Text(product.Quantity.ToString());
                    row.RelativeItem().Text($"${product.Price:F2}");
                    row.RelativeItem().Text($"${product.Quantity * product.Price:F2}");
                });
            }

            column.Item().LineHorizontal(1).LineColor(Colors.Black);

            var total = products.Sum(p => p.Quantity * p.Price);
            column.Item().Row(row =>
            {
                row.RelativeItem().Text("Total").Bold();
                row.RelativeItem().Text($"${total:F2}").Bold();
            });
        });
    }

    void ComposeFooter(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text("Gracias por su compra!").AlignCenter();
            column.Item().Text("Visítenos en www.tiendaxyz.com").AlignCenter();
            column.Item().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" de ");
                x.TotalPages();
            });
        });
    }
}
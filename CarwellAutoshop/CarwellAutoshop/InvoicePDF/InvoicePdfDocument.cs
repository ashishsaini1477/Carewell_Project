using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CarwellAutoshop.InvoicePDF
{
    public class InvoicePdfDocument : IDocument
    {
        private readonly InvoicePdfResponse _model;
        private readonly string _logoPath;
        private readonly string _barcodePath;
        private readonly string _footerLogoPath;
        private readonly string? _stamp;

        public InvoicePdfDocument(
            InvoicePdfResponse model,
            string logoPath,
            string barcodePath,
            string footerLogoPath,
            string? stamp)
        {
            _model = model;
            _logoPath = logoPath;
            _barcodePath = barcodePath;
            _footerLogoPath = footerLogoPath;
            _stamp = stamp;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Content().Column(col =>
                {
                    col.Item().Element(Header);

                    col.Item().PaddingTop(10).Element(InvoiceTitleBar);
                    col.Item().PaddingTop(14);

                    col.Item().Element(BillHeader);
                    col.Item().PaddingVertical(6).LineHorizontal(1);
                    col.Item().Element(BillDetails);

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Element(CustomerOrderTable);

                    if (_model.LabourWorks?.Any() == true)
                        col.Item().PaddingTop(10).Element(LabourTable);

                    col.Item().PaddingTop(15).Element(TotalAndRemarkSection);

                    col.Item().PaddingTop(12).Element(TermsAndConditions);
                    //col.Item().PaddingTop(12).Element(FooterSection);
                    col.Item().Element(Footer);
                });
            });
        }

        // ================= HEADER =================

        void Header(IContainer container)
        {
            container.Row(row =>
            {
                //row.ConstantItem(100)
                //    .PaddingRight(12)
                //    .Image(_footerLogoPath);

                if (File.Exists(_footerLogoPath))
                    row.ConstantItem(100).PaddingRight(12).Image(_footerLogoPath);

                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(_model.GarageName).Bold().FontSize(16);
                    col.Item().Text($"Phone: {_model.GaragePhone}");
                    col.Item().Text(_model.GarageAddress);
                    col.Item().Text($"GSTIN: {_model.GstNumber}").Underline();
                });

                //row.ConstantItem(60)
                //    .AlignRight()
                //    .Image(_barcodePath);

                if (File.Exists(_barcodePath))
                    row.ConstantItem(60).AlignRight().Image(_barcodePath);
            });
        }

        void InvoiceTitleBar(IContainer container)
        {
            container.Background(Colors.Black)
                .PaddingVertical(10)
                .AlignCenter()
                .Text("INVOICE")
                .FontColor(Colors.White)
                .FontSize(13)
                .Bold();
        }

        // ================= BILL INFO =================

        void BillHeader(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn();
                    c.RelativeColumn();
                    c.RelativeColumn();
                });

                table.Cell().Text("Billed To:").Bold().FontSize(13).AlignLeft();
                table.Cell().Text("Vehicle:").Bold().FontSize(13).AlignCenter();
                table.Cell().Text("Invoice:").Bold().FontSize(13).AlignRight();
            });
        }

        void BillDetails(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().AlignLeft().Column(col =>
                {
                    col.Item().Text(_model.CustomerName);
                    col.Item().Text(_model.CustomerMobile);
                    col.Item().Text(_model.CustomerAddress);
                });

                row.RelativeItem().AlignCenter().Column(col =>
                {
                    col.Item().Text(_model.VehicleNumber);
                    col.Item().Text(_model.VehicleModel);
                    col.Item().Text($"{_model.Odometer} KM (Odometer)");
                });

                row.RelativeItem().AlignRight().Column(col =>
                {
                    col.Item().Text(_model.InvoiceNumber);
                    col.Item().Text(_model.InvoiceDate.ToString("dd-MMM-yyyy"));
                });
            });
        }

        // ================= CUSTOMER ORDER TABLE =================

        void CustomerOrderTable(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Table(table =>
                {
                    DefineColumns(table);

                    table.Header(header =>
                    {
                        HeaderCell(header, "Customer Order");
                        HeaderCell(header, "HSN/SAC");
                        HeaderCell(header, "Qty", true);
                        HeaderCell(header, "Unit", true);
                        HeaderCell(header, "Price/Unit", true);
                        HeaderCell(header, "SGST", true);
                        HeaderCell(header, "CGST", true);
                        HeaderCell(header, "Taxable Amt", true);
                        HeaderCell(header, "Dis %", true);
                        HeaderCell(header, "Total", true);
                    });


                    foreach (var item in _model.Items)
                        DataRow(table, item.OrderName, item);


                    // ===== TOTAL ROW (Customer Order) =====
                    table.Cell().AlignCenter()
                        .Padding(5)
                        .Text("Total").FontSize(12)
                        .Bold();

                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");

                    // Taxable Amount
                    table.Cell()
                        .Padding(5)
                        .AlignRight()
                        .Text($"{_model.Items.Sum(x => x.TaxableAmount):0.00}").FontSize(12)
                        .Bold();

                    // Discount column
                    table.Cell().AlignCenter().Padding(5).Text("-");

                    // Final Total
                    table.Cell()
                        .Padding(5)
                        .AlignRight()
                        .Text($"{_model.Items.Sum(x => x.TotalAmount):0.00}").FontSize(12)
                        .Bold();


                });
            });
        }

        // ================= LABOUR TABLE =================

        void LabourTable(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Table(table =>
                {
                    DefineColumns(table);

                    table.Header(header =>
                    {
                        HeaderCell(header, "Labour Charge");
                        HeaderCell(header, "HSN/SAC");
                        HeaderCell(header, "Qty", true);
                        HeaderCell(header, "Unit", true);
                        HeaderCell(header, "Price/Unit", true);
                        HeaderCell(header, "SGST", true);
                        HeaderCell(header, "CGST", true);
                        HeaderCell(header, "Taxable Amt", true);
                        HeaderCell(header, "Dis %", true);
                        HeaderCell(header, "Total", true);
                    });

                    foreach (var item in _model.LabourWorks)
                        DataRow(table, item.LabourCharge, item);

                    // ===== TOTAL ROW (Labour) =====
                    table.Cell().AlignCenter()
                        .Padding(5)
                        .Text("Total").FontSize(12)
                        .Bold();

                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");
                    table.Cell().AlignCenter().Padding(5).Text("-");

                    // Taxable Amount
                    table.Cell()
                        .Padding(5)
                        .AlignRight()
                        .Text($"{_model.LabourWorks.Sum(x => x.TaxableAmount):0.00}").FontSize(12)
                        .Bold();

                    // Discount column
                    table.Cell().AlignCenter().Padding(5).Text("-");

                    // Final Total
                    table.Cell()
                        .Padding(5)
                        .AlignRight()
                        .Text($"{_model.LabourWorks.Sum(x => x.TotalAmount):0.00}").FontSize(12)
                        .Bold();


                });
            });
        }

        // ================= TABLE HELPERS =================

        void DefineColumns(TableDescriptor table)
        {
            table.ColumnsDefinition(c =>
            {
                c.RelativeColumn(3);
                c.RelativeColumn(2);
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn(2);
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn(2);
                c.RelativeColumn();
                c.RelativeColumn(2);
            });
        }

        void HeaderCell(TableCellDescriptor header, string text, bool right = false)
        {
            var cell = header.Cell()
                .Background(Colors.Grey.Lighten3)
                .Padding(5)
                .AlignCenter()
                .Text(text)
                .Bold();

            if (right) cell.AlignRight();
        }

        void DataRow(TableDescriptor table, string description, InvoiceCommon item)
        {
            Cell(table, description);
            Cell(table, item.HsnSac);
            Cell(table, item.Qty.ToString(), true);
            Cell(table, "-", true);
            Cell(table, $"₹{item.UnitPrice:0.00}", true);
            Cell(table, item.SGSTPercent.ToString("0.##"), true);
            Cell(table, item.CGSTPercent.ToString("0.##"), true);
            Cell(table, $"₹{item.TaxableAmount:0.00}", true);
            Cell(table, item.Discount?.ToString("0.##") ?? "0", true);
            Cell(table, $"₹{item.TotalAmount:0.00}", true);
        }

        void Cell(TableDescriptor table, string text, bool right = false)
        {
            var cell = table.Cell()
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(4)
                .Text(text ?? "");

            if (right) cell.AlignRight();
        }

        // ================= TOTAL =================

        void TotalAndRemarkSection(IContainer container)
        {
            container.Row(row =>
            {
                // ================= LEFT : REMARK =================
                row.RelativeItem(2).Column(col =>
                {
                    if (!string.IsNullOrWhiteSpace(_model.Remarks))
                    {
                        col.Item().Text("Remark:")
                            .Bold()
                            .FontSize(15);

                        col.Item()
                            .PaddingTop(4)
                            .Text(_model.Remarks)
                            .FontSize(12);
                    }
                });

                // ================= RIGHT : TOTALS BOX =================
                row.RelativeItem(1).AlignRight().Column(col =>
                {
                    col.Item().Border(1).Padding(10).Column(box =>
                    {
                        KeyValue(box, "Sub-Total:", _model.SubTotal);
                        KeyValue(box, "Discount:", _model.Discount);
                        KeyValue(box, "SGST:", _model.SGST);
                        KeyValue(box, "CGST:", _model.CGST);

                        box.Item().PaddingVertical(5).LineHorizontal(1);

                        box.Item().Row(r =>
                        {
                            r.RelativeItem().Text("GRAND TOTAL:")
                                .Bold()
                                .FontSize(15);

                            r.ConstantItem(100).AlignRight()
                                .Text($"₹ {_model.GrandTotal:0.00}")
                                .Bold()
                                .FontSize(12);
                        });

                        box.Item().PaddingTop(6).Row(r =>
                        {
                            r.RelativeItem().Text("Received:");
                            r.ConstantItem(100).AlignRight()
                                .Text($"₹ {_model.GrandTotal:0.00}");
                        });

                        //box.Item().PaddingTop(4).Row(r =>
                        //{
                        //    r.RelativeItem().Text("Balance:")
                        //        .FontColor(Colors.Red.Lighten3)
                        //        .Bold();

                        //    r.ConstantItem(100).AlignRight()
                        //        .Text("0")
                        //        .FontColor(Colors.Red.Lighten3)
                        //        .Bold();
                        //});
                    });

                    // ================= PAYMENT MODE =================
                    col.Item().PaddingTop(8).Row(r =>
                    {
                        r.RelativeItem().Text("Payment Mode:")
                            .FontSize(11);

                        r.ConstantItem(100).AlignRight()
                            .Text(_model.PaymentMode)
                            .Bold()
                            .FontSize(11);
                    });

                    // ================= AMOUNT IN WORDS =================
                    col.Item()
                        .PaddingTop(6)
                        .Background(Colors.Grey.Lighten3)
                        .Padding(8)
                        .AlignCenter()
                        .Text(_model.AmountInWords)
                        .FontSize(11);
                });
            });
        }

        void KeyValue(ColumnDescriptor col, string label, decimal value)
        {
            col.Item().Row(r =>
            {
                r.RelativeItem().Text(label);
                r.ConstantItem(100).AlignRight()
                    .Text(value.ToString("0.00"))
                    .Bold();
            });
        }


        void TermsAndConditions(IContainer container)
        {
            container.PaddingTop(10).Column(col =>
            {
                // TERMS TITLE
                col.Item().Text("Terms & Conditions:")
                    .Bold()
                    .FontSize(10);

                col.Item().Text("• Subject to our home jurisdiction.");
                col.Item().Text("• Our responsibility ceases as soon as goods leave our premises.");
                col.Item().Text("• Goods once sold will not be taken back.");
                col.Item().Text("• If the bill is not paid within 30 days, extra interest will be charged.");

                // SPACE BEFORE SIGNATURE
                col.Item().PaddingTop(25);

                // SIGNATURE BLOCK (RIGHT ALIGNED)
                col.Item().AlignRight().Column(sig =>
                {
                    // ✅ STAMP IMAGE (ABOVE SIGNATURE)
                    if (File.Exists(_stamp) && _stamp != null && _stamp != "")
                        sig.Item()
                           .Height(60)                // adjust based on stamp size
                           .Width(120)
                           .Image(_stamp) // <-- YOUR STAMP PATH
                           .FitArea();

                    // SIGNATURE TEXT
                    sig.Item()
                       .PaddingTop(5)
                       .Text("Authorized Signature")
                       .Bold();

                    // SIGNATURE LINE
                    sig.Item()
                       .PaddingTop(5)
                       .LineHorizontal(1);
                });
            });
        }


        void Footer(IContainer container)
        {
            container.PaddingTop(10).Column(col =>
            {
                col.Item()
                   .AlignCenter()
                   .Height(30)
                   .Image(_footerLogoPath);

                col.Item()
                   .AlignCenter()
                   .Text("Powered By Carwell")
                   .FontSize(9)
                   .FontColor(Colors.Grey.Darken1);
            });
        }



    }
}

using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.InvoicePDF;
using CarwellAutoshop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace CarwellAutoshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMasterService _masterService;
        private readonly IWebHostEnvironment _env;

        public InvoiceController(IInvoiceService invoiceService, IMasterService masterService, IWebHostEnvironment env)
        {
            _invoiceService = invoiceService;
            _masterService = masterService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceRequestDto request)
        {
            try
            {
                if (!request.LineItems.Any())
                    return BadRequest("Invoice must have at least one item");

                var result = await _invoiceService.CreateInvoice(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet("{invoiceId}/pdf")]
        public async Task<IActionResult> DownloadInvoicePdf(int invoiceId)
        {
            try
            {
                var data = await _masterService.GetInvoicePdf(invoiceId);


                var root = _env.ContentRootPath;

                var logoPath = Path.Combine(root, "Assets", "Images", "Carwell_Logo.jfif");
                var barcodePath = Path.Combine(root, "Assets", "Images", "Carwell_Barcode.png");
                var footerLogoPath = Path.Combine(root, "Assets", "Images", "CarWell_Logo_Footer.png");

                //var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", "Carwell_Logo.jfif");

                //var barcodePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", "Carwell_Barcode.png");
                //var footerLogoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", "CarWell_Logo_Footer.png");
                string stamp = string.Empty;
                if (data.IsStamp)
                    stamp = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", "stamp.png");


                var document = new InvoicePdfDocument(data, logoPath, barcodePath, footerLogoPath, stamp);
                var pdfBytes = document.GeneratePdf();
                return File(pdfBytes, "application/pdf", $"Invoice_{data.InvoiceNumber}.pdf");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("{jobcardId}")]
        public async Task<IActionResult> GetInvoiceByJobCardId(int jobcardId)
        {
            var invoices = await _invoiceService.GetInvoiceByJobCardId(jobcardId);
            return Ok(invoices);
        }
    }
}

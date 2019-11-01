using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;

namespace scheduler.service.Common_Utilities
{
    public class CustomConfiguration
    {
        public string PageItem { get; set; }
    }
    public class Utilities
    {
        private static IOptions<CustomConfiguration> _config;
        private static IConfiguration Configuration;

        public static string GetConfiguration(string key)
        {

            PropertyInfo info = _config.Value.GetType().GetProperty(key);
            if (info == null)
                return null;
            else
            {
                var val = info.GetValue(_config.Value);
                if (val != null)
                    return val.ToString();
                else
                    return null;
            }
        }

        public static void Configure(IOptions<CustomConfiguration> config)
        {
            _config = config;
        }

        public static void Configure(IConfiguration config)
        {
            Configuration = config;
        }

        //to create authentication token based on username and data
        public static string GenerateToken(dynamic data, string Username)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(Username));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(signingCredentials);
            List<Claim> claims = new List<Claim>
            {
                new Claim("Username",Username),
                new Claim("Userrecord",JsonConvert.SerializeObject(data)),
            };
            JwtPayload payload = new JwtPayload(issuer: "*", audience: "*", issuedAt: DateTime.Now, expires: DateTime.Now.AddDays(7), claims: claims, notBefore: DateTime.Now);
            JwtSecurityToken token = new JwtSecurityToken(header: header, payload: payload);
            return tokenHandler.WriteToken(token);
        }
        //Common Functionality to send Emails. 
        public static void SendEmail(string email, string subject, string message)
        {//"terembingo@gmail.com", "terem32#"

            string EmailSystemAdminEmail = "terembingo@gmail.com";
            string EmailSystemAdminPassword = "terem32#";


            //   Attachment at1 = new Attachment(ms, name, mimeType);
            MailMessage mail = new MailMessage();

            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            NetworkCredential loginInfo = new NetworkCredential(EmailSystemAdminEmail, EmailSystemAdminPassword);
            mail.From = new MailAddress("terembingo@gmail.com", "terem org");
            mail.To.Add(new MailAddress(email));

            mail.Subject = subject; //EmailId will be parentId
            mail.Body = message;
            mail.IsBodyHtml = true;

            mail.Sender = new MailAddress("terembingo@gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("terembingo@gmail.com", "terem32#");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        // Global page size property for pagination. 
        public static int PageSize
        {
            get
            {
                //return mySettings.pageitem;

                //return Convert.ToInt32(Configuration.GetConnectionString("PageItem"));
                return 10;
            }
        }
        //// Global Funcationality to upload Images.
        //public static string SaveImage(ImageTypes type, dynamic Id, IFormFile file, IHostingEnvironment _hostingEnvironment)
        //{
        //    return SaveImage(type, Id, file, _hostingEnvironment, null);
        //}
        //public static string SaveImage(ImageTypes type, IFormFile file, IHostingEnvironment _hostingEnvironment, string filename)
        //{
        //    return SaveImage(type, null, file, _hostingEnvironment, filename);
        //}
        //private static string SaveImage(ImageTypes type, dynamic Id, IFormFile file, IHostingEnvironment _hostingEnvironment, string filename)
        //{
        //    string returnFileName = "";
        //    if (file != null)
        //    {
        //        var pathToSave = Path.Combine(_hostingEnvironment.WebRootPath, "AppData", "Images");
        //        if (filename == null)
        //        {
        //            if (Id != null)
        //            {
        //                FileInfo info = new FileInfo(file.FileName);
        //                filename = Convert.ToString(Id) + info.Extension;
        //                returnFileName = filename;
        //            }
        //        }
        //        else
        //            returnFileName = filename;
        //        switch (type)
        //        {
        //            case ImageTypes.Users:
        //                pathToSave = Path.Combine(pathToSave, "Users");
        //                break;
        //            case ImageTypes.Instruments:
        //                pathToSave = Path.Combine(pathToSave, "Instruments");
        //                break;
        //            case ImageTypes.Set:
        //                pathToSave = Path.Combine(pathToSave, "Sets");
        //                break;
        //            case ImageTypes.Logo:
        //                pathToSave = Path.Combine(pathToSave, "Logo");
        //                break;
        //            case ImageTypes.Category:
        //                pathToSave = Path.Combine(pathToSave, "Categories");
        //                break;
        //            case ImageTypes.MachineFileType:
        //                pathToSave = Path.Combine(pathToSave, "machinefiletype");
        //                break;
        //            default:
        //                break;
        //        }



        //        if (!Directory.Exists(pathToSave))
        //        {
        //            Directory.CreateDirectory(pathToSave);
        //        }
        //        pathToSave = Path.Combine(pathToSave, returnFileName);
        //        using (var stream = new FileStream(pathToSave, FileMode.Create))
        //        {
        //            file.CopyToAsync(stream);
        //        }
        //        return returnFileName;
        //    }
        //    return "";
        //}
        //// Global Functionality  to get Image SRC
        //public static string GetImage(ImageTypes type, IHostingEnvironment _hostingEnvironment, string filename)
        //{
        //    if (filename != null)
        //    {
        //        var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "AppData", "Images");
        //        switch (type)
        //        {
        //            case ImageTypes.Users:
        //                filepath = Path.Combine(filepath, "Users");
        //                break;
        //            case ImageTypes.Instruments:
        //                filepath = Path.Combine(filepath, "Instruments");
        //                break;
        //            case ImageTypes.Set:
        //                filepath = Path.Combine(filepath, "Sets");
        //                break;
        //            case ImageTypes.Logo:
        //                filepath = Path.Combine(filepath, "Logo");
        //                break;
        //            case ImageTypes.Category:
        //                filepath = Path.Combine(filepath, "Categories");
        //                break;
        //            case ImageTypes.MachineFileType:
        //                filepath = Path.Combine(filepath, "machinefiletype");
        //                break;
        //            default:
        //                break;
        //        }
        //        filepath = Path.Combine(filepath, filename);
        //        byte[] imageBytes = System.IO.File.ReadAllBytes(filepath);
        //        string base64String = Convert.ToBase64String(imageBytes);
        //        return base64String;
        //    }
        //    return "";
        //}

        //public static bool DeleteImage(ImageTypes type, IHostingEnvironment _hostingEnvironment, string filename)
        //{
        //    try
        //    {
        //        if (filename != null)
        //        {
        //            var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "AppData", "Images");
        //            switch (type)
        //            {
        //                case ImageTypes.Users:
        //                    filepath = Path.Combine(filepath, "Users");
        //                    break;
        //                case ImageTypes.Instruments:
        //                    filepath = Path.Combine(filepath, "Instruments");
        //                    break;
        //                case ImageTypes.Set:
        //                    filepath = Path.Combine(filepath, "Sets");
        //                    break;
        //                case ImageTypes.Logo:
        //                    filepath = Path.Combine(filepath, "Logo");
        //                    break;
        //                case ImageTypes.Category:
        //                    filepath = Path.Combine(filepath, "Categories");
        //                    break;
        //                case ImageTypes.MachineFileType:
        //                    filepath = Path.Combine(filepath, "machinefiletype");
        //                    break;
        //                default:
        //                    break;
        //            }
        //            filepath = Path.Combine(filepath, filename);
        //            if (File.Exists(filepath))
        //                File.Delete(filepath);
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }
        //}
        //Global functionality to Get Barcode Image
        //public static string GetBarcodeImage(string barcode)
        //{


        //    //return "data:image/png;base64," + newbarcode.GetBase64Image();
        //    var qrCodeWriter = new ZXing.BarcodeWriterPixelData
        //    {
        //        Format = ZXing.BarcodeFormat.CODE_128,
        //        Options = new QrCodeEncodingOptions { Height = 50, Width = 930 }
        //    };

        //    var pixelData = qrCodeWriter.Write(barcode);

        //    using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        //    using (var ms = new MemoryStream())
        //    {
        //        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
        //        System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //        try
        //        {
        //            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
        //            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
        //            pixelData.Pixels.Length);
        //        }
        //        finally
        //        {
        //            bitmap.UnlockBits(bitmapData);
        //        }
        //        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

        //        return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
        //    }
        //}

        //public static System.Drawing.Image GetBarcodeImageFile(string barcode)
        //{


        //    //return "data:image/png;base64," + newbarcode.GetBase64Image();
        //    var qrCodeWriter = new ZXing.BarcodeWriterPixelData
        //    {
        //        Format = ZXing.BarcodeFormat.CODE_128,
        //        Options = new QrCodeEncodingOptions { Height = 50, Width = 930 }
        //    };

        //    var pixelData = qrCodeWriter.Write(barcode);

        //    using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        //    using (var ms = new MemoryStream())
        //    {
        //        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
        //        System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //        try
        //        {
        //            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
        //            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
        //            pixelData.Pixels.Length);
        //        }
        //        finally
        //        {
        //            bitmap.UnlockBits(bitmapData);
        //        }
        //        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //        return bitmap.GetThumbnailImage(930, 50, null, IntPtr.Zero);
        //    }
        //}


        // Generate PDF labels

        //public static void  generatePDFLabel(System.Drawing.Image barcodeImageUp, System.Drawing.Image barcodeImageDown, string templatePath, IDictionary<string, string> mergeDataItems, System.IO.MemoryStream outputStream, string QRCodePictureFieldNameUp, string QRCodePictureFieldNameDownLeft, string QRCodePictureFieldNameDownRight)
        //{
        //    // Agggregate successive pages here:
        //    var pagesAll = new List<byte[]>();

        //    // Hold individual pages Here:
        //    byte[] pageBytes = null;

        //    foreach (var mergeItem in mergeDataItems)
        //    {


        //        // Read the form template for each item to be output:
        //        var templateReader = new iTextSharp.text.pdf.PdfReader(templatePath);
        //        var tempStream = new System.IO.MemoryStream();

        //        iTextSharp.text.Image barcodeImageForUpperLabel = iTextSharp.text.Image.GetInstance(barcodeImageUp, System.Drawing.Imaging.ImageFormat.Gif);
        //        iTextSharp.text.Image barcodeImageForLowerLabel = iTextSharp.text.Image.GetInstance(barcodeImageDown, System.Drawing.Imaging.ImageFormat.Gif);

        //        PdfStamper stamper = new PdfStamper(templateReader, tempStream);
        //        stamper.FormFlattening = true;
        //        AcroFields fields = stamper.AcroFields;
        //        stamper.Writer.CloseStream = false;

        //        PushbuttonField ad = fields.GetNewPushbuttonFromField(QRCodePictureFieldNameUp);
        //        ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
        //        ad.ProportionalIcon = true;
        //        ad.Image = barcodeImageForUpperLabel;
        //        fields.ReplacePushbuttonField(QRCodePictureFieldNameUp, ad.Field);

        //        //Add image in left and right image fields ----- Bottom image
        //        PushbuttonField adLeft = fields.GetNewPushbuttonFromField(QRCodePictureFieldNameDownLeft);
        //        adLeft.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
        //        adLeft.ProportionalIcon = true;
        //        adLeft.Image = barcodeImageForLowerLabel;
        //        fields.ReplacePushbuttonField(QRCodePictureFieldNameDownLeft, adLeft.Field);

        //        PushbuttonField adRight = fields.GetNewPushbuttonFromField(QRCodePictureFieldNameDownRight);
        //        adRight.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
        //        adRight.ProportionalIcon = true;
        //        adRight.Image = barcodeImageForLowerLabel;
        //        fields.ReplacePushbuttonField(QRCodePictureFieldNameDownRight, adRight.Field);

        //        // Walk the Dictionary keys, fnid teh matching AcroField, 
        //        // and set the value:
        //        foreach (string name in mergeDataItems.Keys)
        //        {
        //            fields.SetField(name, mergeDataItems[name]);
        //        }
        //        // If we had not set the CloseStream property to false, 
        //        // this line would also kill our memory stream:
        //        stamper.Close();

        //        // Reset the stream position to the beginning before reading:
        //        tempStream.Position = 0;

        //        // Grab the byte array from the temp stream . . .
        //        pageBytes = tempStream.ToArray();

        //        // And add it to our array of all the pages:
        //        pagesAll.Add(pageBytes);
        //    }

        //    // Create a document container to assemble our pieces in:
        //    Document mainDocument = new Document(iTextSharp.text.PageSize.A4);

        //    // Copy the contents of our document to our output stream:
        //    var pdfCopier = new PdfSmartCopy(mainDocument, outputStream);

        //    // Once again, don't close the stream when we close the document:
        //    pdfCopier.CloseStream = false;

        //    mainDocument.Open();
        //    mainDocument.NewPage();
        //    pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pagesAll[0]), 1));

        //    pdfCopier.Close();

        //    // Set stream position to the beginning before returning:
        //    outputStream.Position = 0;
        //}


    }
}

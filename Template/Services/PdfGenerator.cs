using DinkToPdf;
using DinkToPdf.Contracts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.Security;
using System;
using System.IO;
using System.Linq;

namespace Template.Services
{
    public class PdfGenerator
    {
        private readonly IConverter _converter;
        public PdfGenerator(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratorPdf(string html, string filepath, string filename, string pageOrientation,
            string templatePwd = "", string emailId = "")
        {
            try
            {
                string filePath = Path.Combine(filepath, filename + ".pdf");
                if (File.Exists(filePath))
                {
                    int counter = 1;
                    string newFilename = filename + " (" + counter.ToString() + ")";
                    filePath = Path.Combine(filepath, newFilename + ".pdf");

                    // Keep incrementing the counter until a unique filename is found
                    while (File.Exists(filePath))
                    {
                        counter++;
                        newFilename = filename + " (" + counter.ToString() + ")";
                        filePath = Path.Combine(filepath, newFilename + ".pdf");
                    }
                }
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = pageOrientation == "landscape" ? Orientation.Landscape : Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                    Out = filePath

                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontSize = 12, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                    FooterSettings = { FontSize = 12, Line = true, Right = "" + DateTime.Now }
                };
                var document = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };


                return _converter.Convert(document);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void setPassword(string pdfPath, string pwd, string emailId = "", string templatePWD = "")
        {

            string finalPWD = string.Empty;
            if (!string.IsNullOrEmpty(pwd))
            {
                string[] pwdValidation = pwd.Split('_');
                foreach (var item in pwdValidation)
                {
                    string cleanedFirstElement = new string(item.Where(char.IsLetterOrDigit).ToArray());

                    if (cleanedFirstElement.Length >= 4 && char.IsLetter(cleanedFirstElement[0]) && char.IsLetter(cleanedFirstElement[1])
                        && char.IsDigit(cleanedFirstElement[2]) && char.IsDigit(cleanedFirstElement[3]))
                    {
                        // Looks like a PAN number
                        string panNumber = cleanedFirstElement.Substring(0, 4) + cleanedFirstElement.Substring(5);
                        bool isPanValid = ValidatePANNumber(panNumber);
                        if (!isPanValid)
                        {
                            // handle invalid PAN number
                        }
                    }
                    if (string.IsNullOrEmpty(finalPWD))
                    {
                        finalPWD += cleanedFirstElement.Substring(0, 4) + "_";
                    }
                    else
                    {
                        finalPWD += cleanedFirstElement + "_";
                    }
                }
                finalPWD = finalPWD.ToUpper().TrimEnd('_');
            }
            pdfPath = pdfPath + ".pdf";

            if (!File.Exists(pdfPath))
            {
                return;
            }

            // Output file with the password
            string outputPath = Path.Combine(Path.GetDirectoryName(pdfPath), " " + Path.GetFileName(pdfPath));

            try
            {
                PdfDocument document = PdfReader.Open(pdfPath, "some text");
                PdfSecuritySettings securitySettings = document.SecuritySettings;
                securitySettings.UserPassword = finalPWD;
                securitySettings.OwnerPassword = finalPWD;

                securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = true;
                securitySettings.PermitFullQualityPrint = false;
                securitySettings.PermitModifyDocument = true;
                securitySettings.PermitPrint = false;

                // Save the document...
                document.Save(outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(emailId, "Password for PDF File", outputPath, "Your Generated Password Is : " + templatePWD);

        }


        private bool ValidatePANNumber(string panNumber)
        {
            // Step 1: Check if PAN number is of correct length
            if (panNumber.Length != 10)
            {
                return false;
            }

            // Step 2: Check if first five characters are letters
            for (int i = 0; i < 5; i++)
            {
                if (!char.IsLetter(panNumber[i]))
                {
                    return false;
                }
            }

            // Step 3: Check if next four characters are digits
            for (int i = 5; i < 9; i++)
            {
                if (!char.IsDigit(panNumber[i]))
                {
                    return false;
                }
            }

            // Step 4: Check if last character is a letter
            if (!char.IsLetter(panNumber[9]))
            {
                return false;
            }

            // All checks passed, PAN number is valid
            return true;
        }

    }
}

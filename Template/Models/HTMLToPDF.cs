//using DinkToPdf;
//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;

//namespace Template.Models
//{
//    public class HTMLToPDF
//    {

//        public string converthtmlTOPDF(string html, string filepath, string filename, string pageOrientation, string templatePwd = "", string emailId = "")
//        {
//            try
//            {
//                string finalPWD = string.Empty;

//                if (!string.IsNullOrEmpty(templatePwd))
//                {
//                    string[] pwdValidation = templatePwd.Split('_');
//                    foreach (var item in pwdValidation)
//                    {
//                        string cleanedFirstElement = new string(item.Where(char.IsLetterOrDigit).ToArray());

//                        if (cleanedFirstElement.Length >= 4 && char.IsLetter(cleanedFirstElement[0]) && char.IsLetter(cleanedFirstElement[1])
//                            && char.IsDigit(cleanedFirstElement[2]) && char.IsDigit(cleanedFirstElement[3]))
//                        {
//                            // Looks like a PAN number
//                            string panNumber = cleanedFirstElement.Substring(0, 4) + cleanedFirstElement.Substring(5);
//                            bool isPanValid = ValidatePANNumber(panNumber);
//                            if (!isPanValid)
//                            {
//                                // handle invalid PAN number
//                            }
//                        }
//                        finalPWD += cleanedFirstElement + "_";
//                    }
//                    finalPWD = finalPWD.ToUpper().TrimEnd('_');
//                }

//                EmailSender emailSender = new EmailSender();
//                //emailSender.SendEmail();

//                string htmlString = html;
//                string filePath = Path.Combine(filepath, filename + ".pdf");
//                if (File.Exists(filePath))
//                {
//                    int counter = 1;
//                    string newFilename = filename + " (" + counter.ToString() + ")";
//                    filePath = Path.Combine(filepath, newFilename + ".pdf");

//                    // Keep incrementing the counter until a unique filename is found
//                    while (File.Exists(filePath))
//                    {
//                        counter++;
//                        newFilename = filename + " (" + counter.ToString() + ")";
//                        filePath = Path.Combine(filepath, newFilename + ".pdf");
//                    }
//                }

//                var document = new HtmlToPdfDocument()
//                {
//                    GlobalSettings = {
//                PaperSize = DinkToPdf.PaperKind.A4,
//                Orientation = pageOrientation == "landscape" ? Orientation.Landscape : Orientation.Portrait,
//                Margins = new MarginSettings { Left = 30, Right = 30 },
//                Out = filePath
//                        },
//                    Objects = {
//                            new ObjectSettings
//                            {
//                              HtmlContent = htmlString,
//                              WebSettings = { DefaultEncoding = "utf-8" }
//                            }
//                        }
//                };


//                // Check if the file already exists


//                // Create a new converter for each PDF generation
//                var converter = new SynchronizedConverter(new PdfTools());

//                byte[] pdf = converter.Convert(document);

//                // Save the PDF with the unique filename
//                //File.WriteAllBytes(filePath, pdf);
//                //converter = null;
//                return "success";

//            }
//            catch (Exception ex)
//            {
//                return "error";
//            }
//        }


//        static void SetPdfPassword(string pdfPath, string password)
//        {
//            // Run pdftk command to set password
//            string pdftkPath = pdfPath; // Replace with the actual path to pdftk executable
//            string outputFilePath = pdfPath;

//            ProcessStartInfo startInfo = new ProcessStartInfo(pdftkPath)
//            {
//                RedirectStandardOutput = true,
//                UseShellExecute = false,
//                CreateNoWindow = true,
//                Arguments = $"{pdfPath} output {outputFilePath} owner_pw {password}"
//            };

//            Process process = new Process
//            {
//                StartInfo = startInfo
//            };

//            process.Start();
//            process.WaitForExit();

//            // Rename the output file to the original filename
//            File.Delete(pdfPath);
//            File.Move(outputFilePath, pdfPath);
//        }

//        private bool ValidatePANNumber(string panNumber)
//        {
//            // Step 1: Check if PAN number is of correct length
//            if (panNumber.Length != 10)
//            {
//                return false;
//            }

//            // Step 2: Check if first five characters are letters
//            for (int i = 0; i < 5; i++)
//            {
//                if (!char.IsLetter(panNumber[i]))
//                {
//                    return false;
//                }
//            }

//            // Step 3: Check if next four characters are digits
//            for (int i = 5; i < 9; i++)
//            {
//                if (!char.IsDigit(panNumber[i]))
//                {
//                    return false;
//                }
//            }

//            // Step 4: Check if last character is a letter
//            if (!char.IsLetter(panNumber[9]))
//            {
//                return false;
//            }

//            // All checks passed, PAN number is valid
//            return true;
//        }


//    }
//}

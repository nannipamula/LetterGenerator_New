using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Template.Models.Interfaces;

public class EmailSender
{
    public void SendEmail(string email, string subject, string filePath, string tmplatePWD = "", string ccMailAddresses = "", string msg = "")
    {

        tmplatePWD = tmplatePWD.Replace("${", "").Replace("}", "");
        string[] rawpwdArr = tmplatePWD.Split("_");

        string senderEmail = "Raviteja143Sou@outlook.com";
        string senderPassword = "Raviteja143@!"; // Replace with the actual password

        MailMessage message = new MailMessage();
        message.Subject = subject;
        string passwordInstructionsHTML = string.Empty;
        if (!string.IsNullOrEmpty(tmplatePWD))
        {
            // Simplified email content to avoid spam triggers
            passwordInstructionsHTML = @"
        <!DOCTYPE html>
        <html>
        <head>
        <style>
          body {
            font-family: Verdana, sans-serif;
          }
          .password-instructions {
            border: 1px solid #ccc;
            padding: 20px;
            margin: 20px auto;
            max-width: 600px;
            background-color: #f5f5f5;
          }
          h2 {
            color: #333;
          }
          p {
            margin: 10px 0;
          }
        </style>
        </head>
        <body>
          <div class=""password-instructions"">
            <h2>Password Instructions</h2>
            <h3>Your Password is: <strong>" + tmplatePWD + @"</strong></h3>
            <p>Please follow the instructions below to enter the password and view your PDF document:</p>";

            if (rawpwdArr.Length > 1)
            {
                passwordInstructionsHTML += @"
            <p>The first four characters of your password depend on your selected criteria. 
            This could be any relevant information, followed by your <strong>" + rawpwdArr[1] + @"</strong> in DDMMYYYY format.</p>";
            }
            else
            {
                passwordInstructionsHTML += @"
            <p>The first four characters of your password depend on your selected criteria. 
            This could be any relevant information, followed by your date in DDMMYYYY format.</p>";
            }

            passwordInstructionsHTML += @"
            <h4>The password is case-sensitive (UPPERCASE). Please do not include any spaces or salutations (if any).</h4>
          </div>
        </body>
        </html>";

        }
        message.Body = passwordInstructionsHTML;
        message.BodyEncoding = Encoding.UTF8;
        message.From = new MailAddress(senderEmail);

        if (!string.IsNullOrEmpty(email))
        {
            message.To.Add(new MailAddress(email.Trim(), ""));
        }
        if (!string.IsNullOrEmpty(ccMailAddresses))
        {
            message.CC.Add(new MailAddress(ccMailAddresses, ""));
        }
        message.Priority = MailPriority.Normal;
        message.IsBodyHtml = true;
        message.Attachments.Add(new Attachment(filePath));

        SmtpClient client = new SmtpClient("smtp.office365.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, senderPassword)
        };

        try
        {
            client.Send(message);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

    }


    //public static bool IsBalanced(string input)
    //{
    //    Stack<char> stack = new Stack<char>();
    //    foreach (char ch in input)
    //    {
    //        if (ch == '(' || ch == '[' || ch == '{')
    //            stack.Push(ch);
    //        else if (ch == ')' && (stack.Count == 0 || stack.Pop() != '('))
    //        {
    //            return false;
    //        }
    //        else if (ch == ']' && (stack.Count == 0 || stack.Pop() != '['))
    //        {
    //            return false;
    //        }
    //        else if (ch == '}' && (stack.Count == 0 || stack.Pop() != '{'))
    //        {
    //            return false;
    //        }
    //    }
    //    return stack.Count == 0;
    //}


    public static void SendEmailWithCC(string SenderName, string SenderId, string SenderPassword, string SmtpServerHost,
                                       string SmtpServerPort, string EnableSsl, string IsBodyHtml, string Recipient,
                                       string emailSubject, string emailBody, string ccMailAddresses)
    {
        MailMessage message = new MailMessage();
        message.Subject = emailSubject;
        message.Body = emailBody;
        message.BodyEncoding = Encoding.UTF8;
        //message.From = new MailAddress(SenderId, SenderName);
        message.From = new MailAddress(SenderId, "");
        foreach (var recipentaddress in Recipient.Split(','))
        {
            message.To.Add(new MailAddress(recipentaddress, ""));
        }
        foreach (var ccMailAddress in ccMailAddresses.Split(','))
        {
            message.CC.Add(new MailAddress(ccMailAddress, ""));
        }
        message.Priority = MailPriority.Normal;
        message.IsBodyHtml = Convert.ToBoolean(IsBodyHtml);
        SmtpClient client = new SmtpClient(SmtpServerHost, int.Parse(SmtpServerPort))
        {
            EnableSsl = Convert.ToBoolean(EnableSsl),
            UseDefaultCredentials = true
        };
        try
        {
            client.Send(message);

        }
        catch (Exception ex)
        {

        }
    }

}
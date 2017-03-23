using System;
using System.Net.Mail;
using System.Web;

namespace VMS.App_Code
{
    public class MailHelper
    {
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recepient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="cc">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        public void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            try
            { 
                // Instantiate a new instance of MailMessage
                MailMessage mMailMessage = new MailMessage();

                // Set the sender address of the mail message
                mMailMessage.From = new MailAddress(from);
                // Set the recepient address of the mail message
                mMailMessage.To.Add(new MailAddress(to));

                // Check if the bcc value is null or an empty string
                if ((bcc != null) && (bcc != string.Empty))
                {
                    // Set the Bcc address of the mail message
                    mMailMessage.Bcc.Add(new MailAddress(bcc));
                }      // Check if the cc value is null or an empty value
                if ((cc != null) && (cc != string.Empty))
                {
                    // Set the CC address of the mail message
                    mMailMessage.CC.Add(new MailAddress(cc));
                }       // Set the subject of the mail message
                mMailMessage.Subject = subject;
                // Set the body of the mail message
                mMailMessage.Body = body;

                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                // added here
                string test = HttpContext.Current.Server.MapPath("~/Content/samsung.png");
                LinkedResource lr = new LinkedResource(test, mediaType: "image/png");
                lr.ContentId = "logo";
                
                // Instantiate a new instance of SmtpClient
                SmtpClient mSmtpClient = new SmtpClient();

                mSmtpClient.Host = "mail..com.bd";
                mSmtpClient.Port = 25;
                mSmtpClient.UseDefaultCredentials = true;
                mSmtpClient.Credentials = new System.Net.NetworkCredential("roger.shubho@.com.bd", "\"RogeR\"1987");
                //mSmtpClient.EnableSsl = true;

                // Send the mail message
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }

        public bool SIFTemplate(string to, string vendorTempId, string username, string password, string cc = "", string bcc = "", string from = "no-reply@.com.bd")//"tauhid.zzaman@.com.bd")
        {
            bool flag = true;
            if ((!string.IsNullOrEmpty(to))
                || (!string.IsNullOrEmpty(vendorTempId))
                || (!string.IsNullOrEmpty(username))
                || (!string.IsNullOrEmpty(password)))
            {
                #region "template"
                string msg =
                "<!DOCTYPE html PUBLIC \" -///W3C///DTD XHTML 1.0 Transitional///EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                "<html lang=\"en\"> " +
                "<head> " +
                "<title>Responsive Email Template</title> " +
                "<meta http-equiv=\"Content-Type\" content=\"text/html charset=UTF-8\" /> " +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /> " +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /> " +
                "<style type=\"text/css\"> " +
                "/* Stop WebKit from changing text sizes */ " +
                "body, table, td, a { " +
                    "-webkit-text-size-adjust: 100%; " +
                    "-ms-text-size-adjust: 100%; " +
                "} " +
                "body { " +
                    "height: 100% !important; " +
                    "margin: 0 !important; " +
                    "padding: 0 !important; " +
                    "width: 100% !important; " +
                "} " +
                "/* Removes spacing between tables in Outlook 2007+ */ " +
                "table, td { " +
                    "mso-table-lspace: 0pt; " +
                    "mso-table-rspace: 0pt; " +
                "}  " +
                "img { " +
                    "border: 0; " +
                    "line-height: 100%; " +
                    "text-decoration: none; " +
                    "-ms-interpolation-mode: bicubic; /* Smoother rendering in IE */ " +
                "} " +
                "table { " +
                    "border-collapse: collapse !important; " +
                "} " +
                "/* iOS Blue Links */ " +
                "a[x-apple-data-detectors] { " +
                    "color: inherit !important; " +
                    "text-decoration: none !important; " +
                    "font-size: inherit !important; " +
                    "font-family: inherit !important; " +
                    "font-weight: inherit !important; " +
                    "line-height: inherit !important; " +
                "} " +
                "/* Table fix for Outlook */ " +
                "table { " +
                    "border-collapse:separate; " +
                "} " +
                ".ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td { " +
                    "line-height: 100%; " +
                "} " +
                ".ExternalClass { " +
                    "width: 100%; " +
                "} " +
                "/* Mobile Styling */ " +
                "@media screen and (max-width: 525px){ " +
                ".wrapper { " +
                    "width: 100% !important; " +
                    "max-width: 100% !important; " +
                "} " +
                ".hide-element { " +
                    "display: none !important; " +
                "} " +
                ".no-padding { " +
                    "padding: 0 !important; " +
                "} " +
                ".img-max { " +
                    "max-width: 100% !important; " +
                    "width: 100% !important; " +
                    "height: auto !important; " +
                "} " +
                ".table-max { " +
                    "width: 100% !important; " +
                "} " +
                ".mobile-btn-container { " +
                    "margin: 0 auto; " +
                    "width: 100% !important; " +
                "} " +
                ".mobile-btn { " +
                    "padding: 15px !important; " +
                    "border: 0 !important; " +
                    "font-size: 16px !important; " +
                    "display: block !important; " +
                "} " +
                "} " +
                "/* iPads (landscape) Styling */ " +
                "@media handheld, all and (device-width: 768px) and (device-height: 1024px) and (orientation : landscape) { " +
                ".wrapper-ipad { " +
                    "max-width: 280px !important; " +
                "} " +
                ".table-max-ipad{ " +
                    "max-width:465px !important; " +
                "} " +
                "} " +

                "/* iPads (portrait) Styling */ " +
                "@media handheld, all and  (device-width: 768px) and (device-height: 1024px) and (orientation : portrait) { " +
                ".wrapper-ipad { " +
                    "max-width: 280px !important; " +
                "} " +
                ".table-max-ipad{ " +
                    "max-width:465px !important; " +
                "} " +
                "} " +
                "</style> " +
                "</head> " +
                "<body style=\"margin: 0 !important; padding: 0 !important;\"> " +
                "<table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> " +
                  "<tr> " +
                    "<td align=\"center\"> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "<table role=\"presentation\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"> " +
                      "<tr> " +
                      "<td align=\"center\" valign=\"top\" width=\"600\"> " +
                      "<![endif]--> " +
                      "<table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\" class=\"wrapper\"> " +
                        "<tr> " +
                          "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                        "</tr> " +
                        "<tr> " +
                          //"<td align=\"center\"><a href=\"#" target=\"_blank\"> <img src=\"" " + "width=\"100%\" style=\"display: block; border:0; \" border=\"0\"> </a> </td> " +
                          "<td align=\"center\"><a href=\"#" target=\"_blank\"> <img src='cid:logo' " + "width=\"100%\" style=\"display: block; border:0; \" border=\"0\"> </a> </td> " +
                        "</tr> " +
                        "<tr> " +
                          "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                        "</tr> " +
                      "</table> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "</td> " +
                      "</tr> " +
                      "</table> " +
                      "<![endif]--> " +
                    "</td> " +
                  "</tr> " +
                  "<tr> " +
                    "<td bgcolor=\"#eff5f1\" align=\"center\" style=\"padding: 0 10px 20px 10px;\"> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "<table role=\"presentation\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"> " +
                      "<tr> " +
                      "<td align=\"center\" valign=\"top\" width=\"600\"> " +
                      "<![endif]--> " +
                      "<table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\" class=\"table-max\"> " +
                        "<tr> " +
                          "<td><table role=\"presentation\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                              "<tr> " +
                                "<td><table role=\"presentation\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                                   "<tr> " +
                                     "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"left\"><h1 style=\"font-family: Helvetica, Arial, sans-serif; font-size: 28px; font-weight:normal; color: #2C3E50; margin:0; " + "mso-line-height-rule:exactly;\">CONGRATULATIONS!</h1></td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"left\" style=\"font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 25px; color: #2C3E50;\">  " +
                                       "<strong style=\"color:green;\">STEP: 1 - Complete</strong><br> " +
                                       "Your Supplier Information Form (SIF) has been accepted.<br> " +
                                       "<br> " +
                                       "<strong style=\"color:gray;\">Your Temporary Supplier ID:&nbsp;</strong>" + vendorTempId + "<br> " +
                                       "<strong style=\"color:gray;\">User Name:&nbsp;</strong>" + username + "</br> " +
                                       "<strong style=\"color:gray;\">Password:&nbsp;</strong>" + password + "</br><br> " +
                                       "</td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"left\" style=\"font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 25px; color: #2C3E50;\">  " +
                                       "<strong style=\"color:brown;\">STEP: 2 - Fill up Supplier Enlistment Form (SEF)</strong><br> " +
                                       "Use the above credentials to login to the website.<br> " +
                                       "</td> " +
                                   "</tr> " +
                                 "</table><br><br></td> " +
                              "</tr> " +
                            "</table></td> " +
                        "</tr> " +
                        "<tr> " +
                        "<td> " +
                            "<h5 style=\"color: red; \">** THIS IS AN AUTOMATED EMAIL. DO NOT REPLY TO THIS EMAIL **</h5> " +
                          "</td> " +
                      "</tr> " +
                        "</table> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "</td> " +
                      "</tr> " +
                      "</table> " +
                      "<![endif]--> " +
                    "</td> " +
                "</table> " +
                "</body> " +
                "</html> ";
                #endregion "template"

                SendMailMessage(from, to, cc, bcc, " Vendor Management System - Step 1 Complete", msg);
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        public bool SEFTemplate(string to, string vendorTempId, string cc = "", string bcc = "", string from = "no-reply@.com.bd")
        {
            bool flag = true;
            if ((!string.IsNullOrEmpty(to))
                || (!string.IsNullOrEmpty(vendorTempId)))
            {
                #region "template"
                string msg =
                    "<!DOCTYPE html PUBLIC \" -///W3C///DTD XHTML 1.0 Transitional///EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                    " <html lang=\"en\"> " +
                    " <head> " +
                    " <title>Responsive Email Template</title> " +
                    " <meta http-equiv=\"Content-Type\" content=\"text/html charset=UTF-8\" /> " +
                    " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /> " +
                    " <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /> " +
                    " <style type=\"text/css\"> " +
                    " /* Stop WebKit from changing text sizes */ " +
                    " body, table, td, a { " +
                        " -webkit-text-size-adjust: 100%; " +
                        " -ms-text-size-adjust: 100%; " +
                    " } " +
                    " body { " +
                        " height: 100% !important; " +
                        " margin: 0 !important; " +
                        " padding: 0 !important; " +
                        " width: 100% !important; " +
                    " } " +
                    " /* Removes spacing between tables in Outlook 2007+ */ " +
                    " table, td { " +
                        " mso-table-lspace: 0pt; " +
                        " mso-table-rspace: 0pt; " +
                    " }  " +
                    " img { " +
                        " border: 0; " +
                        " line-height: 100%; " +
                        " text-decoration: none; " +
                        " -ms-interpolation-mode: bicubic; /* Smoother rendering in IE */ " +
                    " } " +
                    " table { " +
                        " border-collapse: collapse !important; " +
                    " } " +
                    " /* iOS Blue Links */ " +
                    " a[x-apple-data-detectors] { " +
                        " color: inherit !important; " +
                        " text-decoration: none !important; " +
                        " font-size: inherit !important; " +
                        " font-family: inherit !important; " +
                        " font-weight: inherit !important; " +
                        " line-height: inherit !important; " +
                    " } " +
                    " /* Table fix for Outlook */ " +
                    " table { " +
                        " border-collapse:separate; " +
                    " } " +
                    " .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td { " +
                        " line-height: 100%; " +
                    " } " +
                    " .ExternalClass { " +
                        " width: 100%; " +
                    " } " +
                    " /* Mobile Styling */ " +
                    " @media screen and (max-width: 525px){ " +
                    " .wrapper { " +
                        " width: 100% !important; " +
                        " max-width: 100% !important; " +
                    " } " +
                    " .hide-element { " +
                        " display: none !important; " +
                    " } " +
                    " .no-padding { " +
                        " padding: 0 !important; " +
                    " } " +
                    " .img-max { " +
                        " max-width: 100% !important; " +
                        " width: 100% !important; " +
                        " height: auto !important; " +
                    " } " +
                    " .table-max { " +
                        " width: 100% !important; " +
                    " } " +
                    " .mobile-btn-container { " +
                        " margin: 0 auto; " +
                        " width: 100% !important; " +
                    " } " +
                    " .mobile-btn { " +
                        " padding: 15px !important; " +
                        " border: 0 !important; " +
                        " font-size: 16px !important; " +
                        " display: block !important; " +
                    " } " +
                    " } " +
                    " /* iPads (landscape) Styling */ " +
                    " @media handheld, all and (device-width: 768px) and (device-height: 1024px) and (orientation : landscape) { " +
                    " .wrapper-ipad { " +
                        " max-width: 280px !important; " +
                    " } " +
                    " .table-max-ipad{ " +
                        " max-width:465px !important; " +
                    " } " +
                    " } " +

                    " /* iPads (portrait) Styling */ " +
                    " @media handheld, all and  (device-width: 768px) and (device-height: 1024px) and (orientation : portrait) { " +
                    " .wrapper-ipad { " +
                        " max-width: 280px !important; " +
                    " } " +
                    " .table-max-ipad{ " +
                        " max-width:465px !important; " +
                    " } " +
                    " } " +
                    " </style> " +
                    " </head> " +
                    " <body style=\"margin: 0 !important; padding: 0 !important;\"> " +
                    " <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> " +
                        " <tr> " +
                        " <td align=\"center\"> " +
                            " <!--[if (gte mso 9)|(IE)]> " +
                            " <table role=\"presentation\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"> " +
                            " <tr> " +
                            " <td align=\"center\" valign=\"top\" width=\"600\"> " +
                            " <![endif]--> " +
                            " <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\" class=\"wrapper\"> " +
                            " <tr> " +
                                " <td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                            " </tr> " +
                            " <tr> " +
                                " <td align=\"center\"><a href=\"#" title=\"Replace with your logo\" target=\"_blank\"> <img src=\"samsung.png\" alt=\"Replace with your logo\" style=\"display: block; border:0; \" border=\"0\"> </a> </td> " +
                            " </tr> " +
                            " <tr> " +
                                " <td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                            " </tr> " +
                            " </table> " +
                            " <!--[if (gte mso 9)|(IE)]> " +
                            " </td> " +
                            " </tr> " +
                            " </table> " +
                            " <![endif]--> " +
                        " </td> " +
                        " </tr> " +
                        " <tr> " +
                        " <td bgcolor=\"#eff5f1\" align=\"center\" style=\"padding: 0 10px 20px 10px;\"> " +
                            " <!--[if (gte mso 9)|(IE)]> " +
                            " <table role=\"presentation\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"> " +
                            " <tr> " +
                            " <td align=\"center\" valign=\"top\" width=\"600\"> " +
                            " <![endif]--> " +
                            " <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\" class=\"table-max\"> " +
                            " <tr> " +
                                " <td><table role=\"presentation\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                                    " <tr> " +
                                    " <td><table role=\"presentation\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                                        " <tr> " +
                                            " <td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                        " </tr> " +
                                        " <tr> " +
                                            " <td align=\"left\"><h1 style=\"font-family: Helvetica, Arial, sans-serif; font-size: 28px; font-weight:normal; color: #2C3E50; margin:0; mso-line-height-rule:exactly;\">CONGRATULATIONS!</h1></td> " +
                                        " </tr> " +
                                        " <tr> " +
                                            " <td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                        " </tr> " +
                                        " <tr> " +
                                            " <td align=\"left\" style=\"font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 25px; color: #2C3E50;\">  " +
                                            " <strong style=\"color:green;\">STEP: 2 - Complete</strong><br> " +
                                            " Your Supplier Enlistment Form (SEF) has been accepted.<br> " +
                                            " <br> " +
                                            " <strong style=\"color:gray;\">Your Temporary SEF ID:&nbsp;</strong>something<br><br> " +
                                            " </td> " +
                                        " </tr> " +
                                        " <tr> " +
                                            " <td align=\"left\" style=\"font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 25px; color: #2C3E50;\">  " +
                                            " <strong style=\"color:brown;\">STEP: 3 - Audit Date Confirmation </strong><br> " +
                                            " You will receive a email from us with a AUDIT date. </br>  " +
                                            " Please be patient, we will contact you. <br> <br> <br> " +
                                            " Thank you. <br> " +
                                            " </td> " +
                                        " </tr> " +
                                        " </table></td> " +
                                    " </tr> " +
                                    " <tr> " +
                                    " <td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                    " </tr> " +
                                    " </td> " +
                            " </tr> " +
                            " <tr> " +
                                " <td> " +
                                    " <h5 style=\"color:red;\">** THIS IS AN AUTOMATED EMAIL. DO NOT REPLY TO THIS EMAIL **</h5> " +
                                " </td> " +
                            " </tr> " +
                            " </table> " +
                            " <!--[if (gte mso 9)|(IE)]> " +
                            " </td> " +
                            " </tr> " +
                            " </table> " +
                            " <![endif]--> " +
                        " </td> " +
                    " </table> " +
                    " </body> " +
                    " </html> ";
                #endregion "template"

                SendMailMessage(from, to, cc, bcc, " Vendor Management System - Step 2 Complete", msg);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        ///-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        public bool TestEmailTemplate(string to, string vendorTempId, string username, string password, string cc = "", string bcc = "", string from = "no-reply@.com.bd")//"tauhid.zzaman@.com.bd")
        {
            bool flag = true;
            if ((!string.IsNullOrEmpty(to))
                || (!string.IsNullOrEmpty(vendorTempId))
                || (!string.IsNullOrEmpty(username))
                || (!string.IsNullOrEmpty(password)))
            {
                #region "template"
                string msg =
                "<!DOCTYPE html PUBLIC \" -///W3C///DTD XHTML 1.0 Transitional///EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                "<html lang=\"en\"> " +
                "<head> " +
                "<title>Responsive Email Template</title> " +
                "<meta http-equiv=\"Content-Type\" content=\"text/html charset=UTF-8\" /> " +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /> " +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /> " +
                "<style type=\"text/css\"> " +
                "/* Stop WebKit from changing text sizes */ " +
                "body, table, td, a { " +
                    "-webkit-text-size-adjust: 100%; " +
                    "-ms-text-size-adjust: 100%; " +
                "} " +
                "body { " +
                    "height: 100% !important; " +
                    "margin: 0 !important; " +
                    "padding: 0 !important; " +
                    "width: 100% !important; " +
                "} " +
                "/* Removes spacing between tables in Outlook 2007+ */ " +
                "table, td { " +
                    "mso-table-lspace: 0pt; " +
                    "mso-table-rspace: 0pt; " +
                "}  " +
                "img { " +
                    "border: 0; " +
                    "line-height: 100%; " +
                    "text-decoration: none; " +
                    "-ms-interpolation-mode: bicubic; /* Smoother rendering in IE */ " +
                "} " +
                "table { " +
                    "border-collapse: collapse !important; " +
                "} " +
                "/* iOS Blue Links */ " +
                "a[x-apple-data-detectors] { " +
                    "color: inherit !important; " +
                    "text-decoration: none !important; " +
                    "font-size: inherit !important; " +
                    "font-family: inherit !important; " +
                    "font-weight: inherit !important; " +
                    "line-height: inherit !important; " +
                "} " +
                "/* Table fix for Outlook */ " +
                "table { " +
                    "border-collapse:separate; " +
                "} " +
                ".ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td { " +
                    "line-height: 100%; " +
                "} " +
                ".ExternalClass { " +
                    "width: 100%; " +
                "} " +
                "/* Mobile Styling */ " +
                "@media screen and (max-width: 525px){ " +
                ".wrapper { " +
                    "width: 100% !important; " +
                    "max-width: 100% !important; " +
                "} " +
                ".hide-element { " +
                    "display: none !important; " +
                "} " +
                ".no-padding { " +
                    "padding: 0 !important; " +
                "} " +
                ".img-max { " +
                    "max-width: 100% !important; " +
                    "width: 100% !important; " +
                    "height: auto !important; " +
                "} " +
                ".table-max { " +
                    "width: 100% !important; " +
                "} " +
                ".mobile-btn-container { " +
                    "margin: 0 auto; " +
                    "width: 100% !important; " +
                "} " +
                ".mobile-btn { " +
                    "padding: 15px !important; " +
                    "border: 0 !important; " +
                    "font-size: 16px !important; " +
                    "display: block !important; " +
                "} " +
                "} " +
                "/* iPads (landscape) Styling */ " +
                "@media handheld, all and (device-width: 768px) and (device-height: 1024px) and (orientation : landscape) { " +
                ".wrapper-ipad { " +
                    "max-width: 280px !important; " +
                "} " +
                ".table-max-ipad{ " +
                    "max-width:465px !important; " +
                "} " +
                "} " +

                "/* iPads (portrait) Styling */ " +
                "@media handheld, all and  (device-width: 768px) and (device-height: 1024px) and (orientation : portrait) { " +
                ".wrapper-ipad { " +
                    "max-width: 280px !important; " +
                "} " +
                ".table-max-ipad{ " +
                    "max-width:465px !important; " +
                "} " +
                "} " +
                "</style> " +
                "</head> " +
                "<body style=\"margin: 0 !important; padding: 0 !important;\"> " +
                "<table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> " +
                  "<tr> " +
                    "<td align=\"center\"> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "<table role=\"presentation\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"> " +
                      "<tr> " +
                      "<td align=\"center\" valign=\"top\" width=\"600\"> " +
                      "<![endif]--> " +
                      "<table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\" class=\"wrapper\"> " +
                        "<tr> " +
                          "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                        "</tr> " +
                        "<tr> " +
                          //"<td align=\"center\"><a href=\"http://www..com.bd\" target=\"_blank\"> <img src=\"\" " + "width=\"100%\" style=\"display: block; border:0; \" border=\"0\"> </a> </td> " +
                          "<td align=\"center\"><a href=\"http://www..com.bd\" target=\"_blank\"> <img src='cid:logo' " + "width=\"100%\" style=\"display: block; border:0; \" border=\"0\"> </a> </td> " +
                        "</tr> " +
                        "<tr> " +
                          "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                        "</tr> " +
                      "</table> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "</td> " +
                      "</tr> " +
                      "</table> " +
                      "<![endif]--> " +
                    "</td> " +
                  "</tr> " +
                  "<tr> " +
                    "<td bgcolor=\"#eff5f1\" align=\"center\" style=\"padding: 0 10px 20px 10px;\"> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "<table role=\"presentation\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\"> " +
                      "<tr> " +
                      "<td align=\"center\" valign=\"top\" width=\"600\"> " +
                      "<![endif]--> " +
                      "<table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\" class=\"table-max\"> " +
                        "<tr> " +
                          "<td><table role=\"presentation\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                              "<tr> " +
                                "<td><table role=\"presentation\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                                   "<tr> " +
                                     "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"left\"><h1 style=\"font-family: Helvetica, Arial, sans-serif; font-size: 28px; font-weight:normal; color: #2C3E50; margin:0; " + "mso-line-height-rule:exactly;\">CONGRATULATIONS!</h1></td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"center\" height=\"25\" style=\"height:25px; font-size: 0;\">&nbsp;</td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"left\" style=\"font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 25px; color: #2C3E50;\">  " +
                                       "<strong style=\"color:green;\">STEP: 1 - Complete</strong><br> " +
                                       "Your Supplier Information Form (SIF) has been accepted.<br> " +
                                       "<br> " +
                                       "<strong style=\"color:gray;\">Your Temporary Supplier ID:&nbsp;</strong>" + vendorTempId + "<br> " +
                                       "<strong style=\"color:gray;\">User Name:&nbsp;</strong>" + username + "</br> " +
                                       "<strong style=\"color:gray;\">Password:&nbsp;</strong>" + password + "</br><br> " +
                                       "</td> " +
                                   "</tr> " +
                                   "<tr> " +
                                     "<td align=\"left\" style=\"font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 25px; color: #2C3E50;\">  " +
                                       "<strong style=\"color:brown;\">STEP: 2 - Fill up Supplier Enlistment Form (SEF)</strong><br> " +
                                       "Use the above credentials to login to the website.<br> " +
                                       "</td> " +
                                   "</tr> " +
                                 "</table><br><br></td> " +
                              "</tr> " +
                            "</table></td> " +
                        "</tr> " +
                        "<tr> " +
                        "<td> " +
                            "<h5 style=\"color: red; \">** THIS IS AN AUTOMATED EMAIL. DO NOT REPLY TO THIS EMAIL **</h5> " +
                          "</td> " +
                      "</tr> " +
                        "</table> " +
                      "<!--[if (gte mso 9)|(IE)]> " +
                      "</td> " +
                      "</tr> " +
                      "</table> " +
                      "<![endif]--> " +
                    "</td> " +
                "</table> " +
                "</body> " +
                "</html> ";
                #endregion "template"

                SendMailMessage(from, to, cc, bcc, " Vendor Management System - Step 1 Complete", msg);
            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }
}
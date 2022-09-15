using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

//TODO Go to https://ethereal.email/ and create a free ethereal account, and enter the details below
var fromAddress = "buildsharper@gmail.com";
var smtpServer = "";
var username = "";
var password = "";

//Let's prep our email
var email = new MimeMessage();
email.From.Add(MailboxAddress.Parse(fromAddress));
email.To.Add(MailboxAddress.Parse(username));
email.Subject = "BuildSharper.com - Testing Mailkit";

//Build the body with both HTML and TEXT versions
var bodyBuilder = new BodyBuilder();
bodyBuilder.HtmlBody = "<h1>HTML Header</h1><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam nec porta mi. Proin lorem massa, consectetur eget tortor at, ultricies pellentesque ante.</p>";
bodyBuilder.TextBody = "TEXT Header\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam nec porta mi. Proin lorem massa, consectetur eget tortor at, ultricies pellentesque ante.";

//add a sample attachment
var fileBytes = File.ReadAllBytes("sample.pdf");
bodyBuilder.Attachments.Add("sample.pdf", fileBytes, new ContentType("application", "pdf"));

email.Body = bodyBuilder.ToMessageBody();

Console.WriteLine("Press any key to send email.");
Console.ReadKey();
Console.WriteLine("Sending email...");

//let's send the email
using (var smtp = new SmtpClient())
{
    smtp.Connect(smtpServer, 587, SecureSocketOptions.StartTls);
    smtp.Authenticate(username, password);
    smtp.Send(email);
    smtp.Disconnect(true);
}

Console.WriteLine("Sent!");
Console.ReadKey();
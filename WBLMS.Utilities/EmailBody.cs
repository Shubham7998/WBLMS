namespace WBLMS.Utilities
{
    public class EmailBody
    {
        public static string EmailStringBody(string emailId, string emailToken)
        {
            return $@"
                <html>
                    <body>
                        <h2>Password Reset Request</h2>
                        <p>Hello!</p>
                        <p>You have requested to reset your password. To proceed, please click the link below:</p>
                        <p><a href=""http://localhost:4200/reset?email={emailId}&token={emailToken}"">Reset Password</a></p>
                        <p>If you did not make this request, you can safely ignore this email.</p>
                        <p>Thank you!</p>
                    </body>
                </html>";
        }
    }
}
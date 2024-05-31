using WBLMS.DTO;

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

        public static string LeaveRequestEmailBody(string emailId, string managerName, GetLeaveRequestDTO leaveRequestDTO)
        {
            return $@"
            <html>
                <body>
                    <h2>Leave Request Details</h2>
                    <p>Hello {managerName},</p>
                    <p>{leaveRequestDTO.FirstName} {leaveRequestDTO.LastName} has requested leave.</p>
                    <p><strong>Leave Type:</strong> {leaveRequestDTO.LeaveType}</p>
                    <p><strong>Reason:</strong> {leaveRequestDTO.Reason}</p>
                    <p><strong>Start Date:</strong> {leaveRequestDTO.StartDate?.ToString("yyyy-MM-dd") ?? "N/A"}</p>
                    <p><strong>End Date:</strong> {leaveRequestDTO.EndDate?.ToString("yyyy-MM-dd") ?? "N/A"}</p>
                    <p><strong>Number of Leave Days:</strong> {leaveRequestDTO.NumberOfLeaveDays}</p>
                    <p><strong>Request Date:</strong> {leaveRequestDTO.RequestDate?.ToString("yyyy-MM-dd") ?? "N/A"}</p>
                    <p><strong>Status:</strong> {leaveRequestDTO.Status}</p>
                    <p>Please review the request and take the necessary action.</p>
                    <p>Thank you!</p>
                </body>
            </html>";
        }

    }
}
//return $@"
//            <html>
//                <body>
//                    <h2>Leave Request Details</h2>
//                    <p>Hello {managerName},</p>
//                    <p>{leaveRequestDTO.FirstName} {leaveRequestDTO.LastName} has requested leave.</p>
//                    <p> {leaveRequestDTO.Reason}</p>
//                    <p><strong>Start Date:</strong> {leaveRequestDTO.StartDate?.ToString("yyyy-MM-dd") ?? "N/A"}</p>
//                    <p>I'd like to request leave due to {leaveRequestDTO.LeaveType} from {leaveRequestDTO.StartDate} to {leaveRequestDTO.EndDate}</p>
//                    <p><strong>End Date:</strong> {leaveRequestDTO.EndDate?.ToString("yyyy-MM-dd") ?? "N/A"}</p>
//                    <p><strong>Number of Leave Days:</strong> {leaveRequestDTO.NumberOfLeaveDays}</p>
//                    <p><strong>Request Date:</strong> {leaveRequestDTO.RequestDate?.ToString("yyyy-MM-dd") ?? "N/A"}</p>
//                    <p> I'll ensure any pending tasks are taken care of upon my return. If anything urgent comes up, feel free to reach out.</p>
//                    <p>Thanks for understanding.</p>
//                </body>
//            </html>";
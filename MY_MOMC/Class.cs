using System.Collections.Generic;
namespace MY_MOMC
{
    internal class Class
    {
        internal class User
        {
            public int UserId { get; set; }
            public string UserEmailId { get; set; }

            public List<UserMeetingDetails> usermeetingdetails { get; set; }
        }
        internal class UserMeetingDetails
        {
            public int MeetingId { get; set;}
            public string MeetingName { get; set;}
            public string MeetingStartDateTime { get; set; }
            public string MeetingEndDateTime { get;set; }
            public string MeetingDuration { get; set;}
            public List<MeetingAttendeesEmailName> meetingAttendeesEmailNames { get; set; } 
            public List<EmailDetails> emails { get; set; }
        }
        internal class MeetingAttendeesEmailName
        {
            public string AttendeeEmail { get; set; }
            public string AttendeeName { get; set; }
        }
        internal class EmailDetails
        {
            public List<string> To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public List<string> AttachedFiles { get; set; }
            public List<string> DrawboardAttaches { get; set; }

            public int EmailSentCount { get; set; }
        }
    }
}

namespace OpenMindProject.Models.Emails
{
    public class EmailsModel
    {
        public string EmailId { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Subjects { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public long?  Date { get; set; }
    }
}
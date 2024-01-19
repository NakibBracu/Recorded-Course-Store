namespace RCS.Data.EmailUtilities
{
    public class Smtp
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}

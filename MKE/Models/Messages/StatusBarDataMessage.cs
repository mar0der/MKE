using System.Windows;


namespace MKE.Models.Messages
{
    public class StatusBarDataMessage
    {
        public string StatusMessage { get; set; }
        public Point Coordinates { get; set; }
        public StatusBarDataMessage(string statusMessage, Point coordinates) 
        { 
            StatusMessage = statusMessage;
            Coordinates = coordinates;
        }
    }
}

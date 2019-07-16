using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Extensions
{
    public class ArchiveSmsViewModel
    {
        public int Id { get; set; }
        public int SentMessagId { get; set; }
        public string FullNameSender { get; set; }
        public string ContextMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string CreateDateTimeShamsi => CreateDateTime.ToPersianDate();
        public string StateSendMessageToWebservice { get; set; }
        public DateTime? ReceivedDateTime { get; set; }
        public string FullNamePerson { get; set; }
        public string DeliveryStatus { get; set; }
        public string Mobile { get; set; }
        public string RefNumber { get; set; }
        public int SMSCount { get; set; }
        public string GroupName { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string cell { get; set; }
    }
}

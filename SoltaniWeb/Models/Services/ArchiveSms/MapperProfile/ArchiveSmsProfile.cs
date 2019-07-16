using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;

namespace SoltaniWeb.Models.Services.ArchiveSms.MapperProfile
{
    public class ArchiveSmsProfile: Profile
    {
        Cls_SMS.ClsSend send = new Cls_SMS.ClsSend();
        public ArchiveSmsProfile()
        {

            CreateMap<tbl_SentMessagPerson, ArchiveSmsViewModel>()
                .ForMember(x => x.ContextMessage, opt => opt.MapFrom(z => z.SentMessag.ContextMessage))
                .ForMember(x => x.FullNamePerson, opt => opt.MapFrom(x => x.PersonId != null ? (x.Person.Fname ?? "") + " " + (x.Person.Lname ?? "") : x.PersonCellPhone))
                .ForMember(x => x.CreateDateTime, opt => opt.MapFrom(z => z.SentMessag.CreateDateTime))
                .ForMember(x => x.FullNameSender, opt => opt.MapFrom(x => x.SentMessag.User.username))
                .ForMember(x => x.Branch, opt => opt.MapFrom(x => x.SentMessag.User.branches_.branch_name))
                .ForMember(x => x.SentMessagId, opt => opt.MapFrom(x => x.SentMessagId))
                .ForMember(x => x.StateSendMessageToWebservice, opt => opt.MapFrom(x => x.SentMessag.State))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(x => x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone))
                .ForMember(x => x.RefNumber, opt => opt.MapFrom(x => x.SentMessag.RefNumber))
                .ForMember(x => x.DeliveryStatus, opt => opt.MapFrom(x => (x.DeliveryStatus != null && x.SentMessag.State == "CHECK_OK") ? ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), x.DeliveryStatus)).GetDisplayName() : (x.SentMessag.State != "CHECK_OK") ? "خطای ارسال پیام به سرویس" : "نامشخص"))
                .ForMember(x => x.SMSCount, opt => opt.MapFrom(x => send.FindTxtcount(x.SentMessag.ContextMessage)))
                .ForMember(x => x.State, opt => opt.MapFrom(x => x.SentMessag.State))
                .ForMember(x => x.cell, opt => opt.MapFrom(x => x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone))
                ;
        }
    }
}

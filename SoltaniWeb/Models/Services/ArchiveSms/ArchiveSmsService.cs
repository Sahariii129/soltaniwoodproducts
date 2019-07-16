using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using OfficeOpenXml;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.structs.ArchiveSmsVM;

namespace SoltaniWeb.Models.Services.ArchiveSms
{
    public class ArchiveSmsService: IArchiveSmsService
    {
        _4820_soltaniwebContext _context = new _4820_soltaniwebContext();
        private readonly IMapper _mapper;
        Cls_SMS.ClsSend _clsSend = new Cls_SMS.ClsSend();
        public ArchiveSmsService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IEnumerable<ArchiveSmsViewModel> GetAll(int branchId)
        {
            IQueryable<ArchiveSmsViewModel> result;
            result = branchId!=0 ? _context.tbl_SentMessagPerson.Where(x=>x.SentMessag.User.branches_id == branchId).ProjectTo<ArchiveSmsViewModel>() : _context.tbl_SentMessagPerson.ProjectTo<ArchiveSmsViewModel>(_mapper.ConfigurationProvider);
            return result;
        }

        public IEnumerable<ArchiveSmsViewModel> Getpaging(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResultStatus SendSmsWithListPersonId(ArchiveSmsCreateViewModel model)
        {
            var op = new ResultStatus();

            try
            {
                if (model.PersonsId.Any())
                {
                    var person = _context.tbl_person.Where(x => model.PersonsId.Any(y => y == x.id)).ToList();


                    bool isPersian = false;

                    int smsCount = 0;
                    Cls_SMS.ClsSend.FindTxtLanguageAndcount(model.Context, ref isPersian, ref smsCount);
                    var numberOfPersonToSend = 100 / smsCount;
                    int personGroup = person.Count / numberOfPersonToSend;
                    var remainingPerson = person.Count % numberOfPersonToSend;

                    int start = 0;
                    for (int i = 0; i < personGroup; i++)
                    {

                        var lst = person.GetRange(start, numberOfPersonToSend);
                        start = numberOfPersonToSend * (i + 1);
                        var result = _clsSend.SendSMS_Batch(model.Context, lst.Select(x => x.cell).ToArray(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                        var sentMessages = new tbl_sentMessag()
                        {
                            CreateDateTime = DateTime.Now,
                            ContextMessage = model.Context,
                            State = result[0],
                            RefNumber = result[1],
                            UserId = model.UserId
                        };
                        _context.tbl_sentMessag.Add(sentMessages);
                        foreach (var per in lst)
                        {
                            var sentMessagePerson = new tbl_SentMessagPerson()
                            {
                                Person = per,
                                SentMessag = sentMessages,
                            };
                            _context.tbl_SentMessagPerson.Add(sentMessagePerson);
                        }
                    }


                    if (remainingPerson > 0)
                    {
                        var lst = person.GetRange(start, remainingPerson);
                        var result = _clsSend.SendSMS_Batch(model.Context, lst.Select(x => x.cell).ToArray(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                        var sentMessages = new tbl_sentMessag()
                        {
                            CreateDateTime = DateTime.Now,
                            ContextMessage = model.Context,
                            State = result[0],
                            RefNumber = result[1],
                            UserId = model.UserId
                        };
                        _context.tbl_sentMessag.Add(sentMessages);
                        foreach (var per in lst)
                        {
                            var sentMessagePerson = new tbl_SentMessagPerson()
                            {
                                Person = per,
                                SentMessag = sentMessages,
                            };
                            _context.tbl_SentMessagPerson.Add(sentMessagePerson);
                        }
                    }
                    _context.SaveChanges();
                    op.IsSuccessed = true;
                    op.Message = "اطلاعات با موفقیت ثبت گردید";

                }
                else
                {
                    op.Message = "اطلاعات را وارد کنید";
                }

            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در عملیات";
            }
            
            return op;
        }

        public ResultStatus SendSmsWithPersonMobile(string personMobile,string context,int userId)
        {
            var op = new ResultStatus();
            try
            {
                if (!string.IsNullOrEmpty(personMobile) && personMobile != "0" && !string.IsNullOrEmpty(context) && userId!=0)
                {
                    var result = _clsSend.SendSMS_Single(context, personMobile, "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                    var sentMessages = new tbl_sentMessag()
                    {
                        CreateDateTime = DateTime.Now,
                        ContextMessage = context,
                        State = result[0],
                        RefNumber = result[1],
                        UserId = userId
                    };
                    _context.tbl_sentMessag.Add(sentMessages);
                    var person = _context.tbl_person.FirstOrDefault(x => x.cell.Contains(personMobile) ||
                                                                      x.PersonInformationSettings.Count(per => per.PropertyName == PersonInformationSetting.Mobile.ToString() && per.PropertyValue.Contains(personMobile))!=0);
                    _context.tbl_SentMessagPerson.Add(new tbl_SentMessagPerson
                    {
                        Person = person,
                        PersonCellPhone = personMobile,
                        SentMessag = sentMessages
                    });
                    //  lstPersonCell.Add(personCellphone);
                    _context.SaveChanges();
                    op.IsSuccessed = true;
                    op.Message = "اطلاعات با موفقیت ثبت گردید";

                }
                else
                {
                    op.Message = "اطلاعات را وارد کنید";
                }
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در عملیات";
            }

            return op;
        }

        public ResultStatus SendSmsWithFile(IFormFile fileNumber,int userId)
        {
            var op = new ResultStatus();
            try
            {
                if (fileNumber != null)
                {

                    List<PersonCellExecl> perExcel = new List<PersonCellExecl>();

                    using (var stream = new MemoryStream())
                    {
                        fileNumber.CopyTo(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 1; row <= rowCount; row++)
                            {
                                perExcel.Add(new PersonCellExecl() { Cell = worksheet.Cells[row, 1].Value.ToString(), Text = worksheet.Cells[row, 2].Value.ToString() });
                            }
                        }
                    }

                    foreach (var item in perExcel.GroupBy(x => x.Text))
                    {
                        var messExcel = perExcel.FirstOrDefault(x => x.Text == item.Key).Text;
                        var mobilesExcel = perExcel.Where(x => x.Text == item.Key).Select(x => x.Cell).ToList();
                        var result = _clsSend.SendSMS_Batch(messExcel, mobilesExcel.ToArray(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                        var sentMessages = new tbl_sentMessag()
                        {
                            CreateDateTime = DateTime.Now,
                            ContextMessage = messExcel,
                            State = result[0],
                            RefNumber = result[1],
                            UserId = userId
                        };
                        _context.tbl_sentMessag.Add(sentMessages);
                        foreach (var perm in mobilesExcel)
                        {
                            var person = _context.tbl_person.FirstOrDefault(x => x.cell.Contains(perm) ||
                                                                                 x.PersonInformationSettings.Count(per => per.PropertyName == PersonInformationSetting.Mobile.ToString() && per.PropertyValue.Contains(perm)) != 0);
                            var sentMessagePerson = new tbl_SentMessagPerson()
                            {
                                Person = person,
                                PersonCellPhone = perm,
                                SentMessag = sentMessages,
                            };
                            _context.tbl_SentMessagPerson.Add(sentMessagePerson);
                        }
                    }
                    _context.SaveChanges();
                    op.IsSuccessed = true;
                    op.Message = "اطلاعات با موفقیت ثبت گردید";
                }
                else
                {
                    op.Message = "اطلاعات را وارد کنید";
                }
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در عملیات";
            }

            return op;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.structs.PersonVM;
using SoltaniWeb.Models.Services.Person;

namespace SoltaniWeb.Controllers
{
    [SWPAuthorizeIdentityController]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int? id)
        {
            PersonCreateViewModel person = new PersonCreateViewModel();
           
            if (id != null && id != 0)
            {

                person = _personService.FindPerson(id.Value);
                ViewBag.Branches = db.tbl_branches.Select(x => new SelectListItem
                {
                    Text = x.branch_name,
                    Value = x.id.ToString(),
                    Selected = x.id==person.BrancheId
                }).ToList();
            }
            else
            {
                ViewBag.Branches = db.tbl_branches.Select(x => new SelectListItem
                {
                    Text = x.branch_name,
                    Value = x.id.ToString(),
                }).ToList();
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult CreatePersson(PersonCreateViewModel model, string[] personInfos, string[] personAddresses)
        {
            var op = new ResultStatus();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        _personService.Update(model);
                    }
                    else
                    {
                        if (personInfos.Length != 0)
                        {
                            var lstPersonInformation = personInfos.Select(x => new PersonInformationSettingViewModel
                            {
                                Id = int.Parse(x.Split(",")[0]),
                                PropertyName = x.Split(",")[1],
                                PropertyValue = x.Split(",")[2]
                            }).ToList();
                            model.PersonInformationSetting = lstPersonInformation;
                        }

                        if (personAddresses.Length != 0)
                        {
                            var lstPersonAddress = personAddresses.Select(x => new PersonAddressViewModel
                            {
                                Id = int.Parse(x.Split(",")[0]),
                                Address = x.Split(",")[1],
                                PostalCode = x.Split(",")[2],
                                Phone = x.Split(",")[3],
                                Phone2 = x.Split(",")[4],
                                Phone3 = x.Split(",")[5]
                            }).ToList();
                            model.PersonAddresses = lstPersonAddress;
                        }

                        var mobile = model.Cell ??
                                      ((model.PersonInformationSetting!=null && model.PersonInformationSetting.Count!=0)? model.PersonInformationSetting.FirstOrDefault(x => x.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue:"");
                        var perFind = _personService.FindPerson(model.FName, model.LName, mobile, model.Codemelli);
                        if (perFind.IsSuccessed)
                        {
                            op = perFind;
                            op.IsSuccessed = false;
                            return Json(op);
                        }
                        _personService.Create(model);
                        
                    }

                    op.IsSuccessed = true;
                    op.Message = "اطلاعات با موفقیت ثبت گردید";
                    return Json(op);
                }

                op.IsSuccessed = false;
                op.Message = "نام، نام خانوادگی و جنسیت اجباری می باشد";
                return Json(op);
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در عملیات";
                return Json(op);
            }
            //return View(model);
        }

        #region PersonAddress
        [HttpPost]
        public ActionResult EditAddresPerson(int personId, int id, string addres, string postalCode, string phone, string phone2, string phone3, string[] personInfos)
        {
            if (personId == 0)
            {
                List<PersonAddressViewModel> lstPersonAddress = new List<PersonAddressViewModel>();
                if (personInfos.Length==0)
                {
                    lstPersonAddress.Add(new PersonAddressViewModel
                    {
                        Id = 1,
                        PersonId=personId,
                        PostalCode = postalCode,
                        Address = addres,
                        Phone2 = phone2,
                        Phone = phone,
                        Phone3 = phone3

                    });

                }
                else
                {
                    lstPersonAddress = personInfos.Select(x => new PersonAddressViewModel
                    {
                        Id = int.Parse(x.Split(",")[0]),
                        Address = x.Split(",")[1],
                        PostalCode = x.Split(",")[2],
                        Phone = x.Split(",")[3],
                        Phone2 = x.Split(",")[4],
                        Phone3 = x.Split(",")[5]
                    }).ToList();
                    var peradd = lstPersonAddress.FirstOrDefault(x => x.Id == id);
                    if (peradd != null)
                    {
                        peradd.Address = addres;
                        peradd.Phone = phone;
                        peradd.Phone2 = phone2;
                        peradd.Phone3 = phone3;
                        peradd.PostalCode = postalCode;

                    }
                    else
                    {
                        var idAddress = (lstPersonAddress.LastOrDefault()?.Id ?? 0) + 1;
                        lstPersonAddress.Add(new PersonAddressViewModel
                        {
                            Id = idAddress,
                            PostalCode = postalCode,
                            Address = addres,
                            Phone2 = phone2,
                            Phone = phone,
                            Phone3 = phone3

                        });
                    }


                }
                return PartialView("_PersonAddress", lstPersonAddress);
            }
            else
            {
                var address = _personService.FindPersonAddresses(personId);
                var perAddres = address.FirstOrDefault(x => x.Id == id);
                if (perAddres != null)
                {
                    perAddres.Address = addres;
                    perAddres.Phone = phone;
                    perAddres.Phone2 = phone2;
                    perAddres.Phone3 = phone3;
                    perAddres.PostalCode = postalCode;
                    _personService.UpdateAddress(perAddres);
                }
                else
                {
                    var perAddressNew = new PersonAddressViewModel
                    {
                        PersonId = personId,
                        PostalCode = postalCode,
                        Address = addres,
                        Phone2 = phone2,
                        Phone = phone,
                        Phone3 = phone3

                    };
                  var addId=  _personService.AddPersonAddresses(perAddressNew);
                    perAddressNew.Id = addId;
                    address.Add(perAddressNew);
                }

                var result = address;
                return PartialView("_PersonAddress", result);
            }




        }

        [HttpPost]
        public ActionResult DeletAddresPerson(int id, int personId, string[] personInfos)
        {
            if (personId == 0)
            {
                List<PersonAddressViewModel> lstPersonAddress = new List<PersonAddressViewModel>();
                if (personInfos.Length!=0)
                {
                    lstPersonAddress = personInfos.Select(x => new PersonAddressViewModel
                    {
                        Id = int.Parse(x.Split(",")[0]),
                        Address = x.Split(",")[1],
                        PostalCode = x.Split(",")[2],
                        Phone = x.Split(",")[3],
                        Phone2 = x.Split(",")[4],
                        Phone3 = x.Split(",")[5]
                    }).ToList();
                    var peradd = lstPersonAddress.FirstOrDefault(x => x.Id == id);
                    if (peradd != null)
                    {
                        lstPersonAddress.Remove(peradd);

                    }

                }

                return PartialView("_PersonAddress", lstPersonAddress);
            }
            else
            {
                var address = _personService.FindPersonAddresses(personId);
                var perAddres = address.FirstOrDefault(x => x.Id == id);
                if (perAddres != null)
                {
                    address.Remove(perAddres);
                    _personService.RemovePersonAddresses(perAddres);
                }
                return PartialView("_PersonAddress", address);
            }



        }

        #endregion

        #region PersonInformation
        [HttpPost]
        public ActionResult EditPersonInformation(int personId, int id,string propertyName, string propertyValue, string[] personInfos)
        {
            propertyName = propertyName.ToEnum<PersonInformationSetting>().ToString();
            if (personId == 0 )
            {
               
                var lstPersonInformation = new List<PersonInformationSettingViewModel>();
                if (personInfos.Length==0)
                {
                    lstPersonInformation.Add(new PersonInformationSettingViewModel
                    {
                        Id = 1,
                        PersonId=personId,
                        PropertyName=propertyName,
                        PropertyValue=propertyValue

                    });

                }
                else
                {
                    
                    lstPersonInformation = personInfos.Select(x => new PersonInformationSettingViewModel
                    {
                        Id = int.Parse(x.Split(",")[0]),
                        PropertyName= x.Split(",")[1],
                        PropertyValue= x.Split(",")[2]
                    }).ToList();
                    var peradd = lstPersonInformation.FirstOrDefault(x =>x.Id==id);

                    if (peradd != null)
                    {
                        peradd.PropertyName = propertyName;
                        peradd.PropertyValue = propertyValue;

                    }
                    else
                    {
                        var idperIfno = (lstPersonInformation.LastOrDefault()?.Id ?? 0) + 1;
                        lstPersonInformation.Add(new PersonInformationSettingViewModel
                        {
                            Id = idperIfno,
                            PropertyName = propertyName,
                            PropertyValue = propertyValue,
                        });
                    }
                }
               // ViewBag.PersonInformationEdite = lstPersonInformation;
                return PartialView("_PersonInformation", lstPersonInformation);
            }
            else
            {
                var address = _personService.FindPersonInformation(personId);
                var perAddres = address.FirstOrDefault(x => x.Id == id);
                if (perAddres != null)
                {
                    perAddres.PropertyName = propertyName;
                    perAddres.PropertyValue = propertyValue;
                    _personService.UpdateInformationSetting(perAddres);
                }
                else
                {
                    var PersonInfo = new PersonInformationSettingViewModel
                    {
                      PersonId = personId,
                        PropertyValue = propertyValue,
                        PropertyName= propertyName,
                    };
                   var perInforId= _personService.AddPersonInformationSetting(PersonInfo);
                    PersonInfo.Id = perInforId;
                    address.Add(PersonInfo);
                }

                var result = address;
                return PartialView("_PersonInformation", result);
            }




        }

        [HttpPost]
        public ActionResult DeletPersonInformation(int id, int personId,string[] personInfos)
        {
            if (personId == 0)
            {
                List<PersonInformationSettingViewModel> lstPersonInformation = new List<PersonInformationSettingViewModel>();
                if (personInfos.Length!=0)
                {
                    lstPersonInformation = personInfos.Select(x => new PersonInformationSettingViewModel
                    {
                        Id = int.Parse(x.Split(",")[0]),
                        PropertyName = x.Split(",")[1],
                        PropertyValue = x.Split(",")[2]
                    }).ToList();
                    var peradd = lstPersonInformation.FirstOrDefault(x => x.Id == id);
                    if (peradd != null)
                    {
                        lstPersonInformation.Remove(peradd);

                    }
                }
                return PartialView("_PersonInformation", lstPersonInformation);
            }
            else
            {
                var address = _personService.FindPersonInformation(personId);
                var perAddres = address.FirstOrDefault(x => x.Id == id);
                if (perAddres != null)
                {
                    address.Remove(perAddres);
                    _personService.RemovePersonInformationSetting(perAddres);
                }
                return PartialView("_PersonInformation", address);
            }



        }

        #endregion

        public ActionResult Persons_Read([DataSourceRequest]DataSourceRequest request)
        {
           
            var data= _personService.GetAll();
            var dsResultTemps = data.ToDataSourceResult(request);
            return Json(dsResultTemps);
           

        }

        public IActionResult PersonAddres(int personId)
        {
            ViewBag.personId = personId;
            return PartialView();
        }

        public ActionResult PersonAddres_Read([DataSourceRequest] DataSourceRequest request, int personId)
        {
            var result = _personService.FindPersonAddresses(personId);
            
            var dsResultTemp = result.ToDataSourceResult(request);
            return Json(dsResultTemp);
        }
        public IActionResult PersonInformation(int personId)
        {
            ViewBag.personId = personId;
            return PartialView();
        }

        public ActionResult PersonInformation_Read([DataSourceRequest] DataSourceRequest request, int personId)
        {
            var result = _personService.FindPersonInformation(personId);
           

            var dsResultTemp = result.ToDataSourceResult(request);
            return Json(dsResultTemp);
        }
    }
}
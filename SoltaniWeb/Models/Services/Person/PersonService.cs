using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;

using SoltaniWeb.Models.structs.PersonVM;

namespace SoltaniWeb.Models.Services.Person
{
    public class PersonService : IPersonService
    {
        private static bool UpdateDatabase = false;
        private IMemoryCache _cache;
        _4820_soltaniwebContext _context = new _4820_soltaniwebContext();
        private readonly IMapper _mapper;
        public PersonService(IMemoryCache memoryCache, IMapper mapper)
        {
            _cache = memoryCache;
            _mapper = mapper;
        }
        public void Create(PersonCreateViewModel person)
        {
            tbl_person _person = new tbl_person();
            _person.Fname = person.FName;
            _person.Lname = person.LName;
            _person.codemelli = person.Codemelli;
            _person.Pdescription = person.Pdescription;
            _person.tell = person.Tell;
            _person.cell = person.Cell;
            _person.sex = person.Sex;
            _person.prefix = person.Prefix;
            _person.BrancheId = person.BrancheId;
            _person.CreateDatetime=DateTime.Now;
            foreach (var per in person.PersonAddresses)
            {
                _person.PersonAddresses.Add(new tbl_PersonAddress
                {
                    Address = per.Address,
                    Phone = per.Phone,
                    Phone2 = per.Phone2,
                    Phone3 = per.Phone3,
                    PostalCode = per.PostalCode
                });
            }
            foreach (var per in person.PersonInformationSetting)
            {
                _person.PersonInformationSettings.Add(new tbl_PersonInformationSetting()
                {
                    PropertyValue = per.PropertyValue,
                    PropertyName = per.PropertyName
                });
            }
            _context.tbl_person.Add(_person);
            _context.SaveChanges();
           

        }

        public void Destroy(structs.PersonVM.PersonViewModel person)
        {
            throw new NotImplementedException();
        }

        public PersonCreateViewModel FindPerson(int personId)
        {
            if (personId == 0)
            {
                return null;
            }

            var person = _context.tbl_person.Include(x => x.PersonAddresses)
                .Include(x => x.PersonInformationSettings)
                .FirstOrDefault(x => x.id == personId);
            return _mapper.Map<PersonCreateViewModel>(person);

        }
        public ResultStatus FindPerson(string fName, string lName, string mobile, string codeMeli)
        {
            //if (string.IsNullOrEmpty(fName) && string.IsNullOrEmpty(fName))
            //{
            //    var person = _context.tbl_person.FirstOrDefault(x => fName.Contains(fName) &&
            //                                                         lName.Contains(lName)
            //                                                         );
            //}

            //var person = _context.tbl_person.Include(x => x.PersonAddresses)
            //    .Include(x => x.PersonInformationSettings)
            //    .FirstOrDefault(x => x.id == personId);
            //return _mapper.Map<PersonCreateViewModel>(person);
            var op = new ResultStatus();
            if (!string.IsNullOrEmpty(fName) && !string.IsNullOrEmpty(fName))
            {
                var person = _context.tbl_person.FirstOrDefault(x => ((x.Fname ?? "") + " " + (x.Lname ?? "")).Contains((fName ?? "") + " " + (lName ?? "")));
                if (person != null)
                {
                    op.IsSuccessed = true;
                    op.Message = "نام و نام خانوادگی مورد نظر قبلا برای مشتری " + (person.Fname ?? "") + " " + (person.Lname ?? "") +
                                 " ثبت شده است";
                }
            }
            if (!string.IsNullOrEmpty(mobile))
            {
               
                var person = _context.tbl_person.FirstOrDefault(p => p.cell.Contains(mobile) ||
                                                                     p.PersonInformationSettings.FirstOrDefault(x => x.PropertyName == PersonInformationSetting.Mobile.ToString() && x.PropertyValue.Contains(mobile)) != null);
                if (person != null)
                {
                    op.IsSuccessed = true;
                    op.Message = "موبایل مورد نظر قبلا برای مشتری " + (person.Fname ?? "") + " " + (person.Lname ?? "") +
                                 " ثبت شده است";
                }

            }
            if (!string.IsNullOrEmpty(codeMeli))
            {
                var person = _context.tbl_person.FirstOrDefault(p => p.codemelli.Contains(mobile));
                if (person != null)
                {
                    op.IsSuccessed = true;
                    op.Message = "کد ملی مورد نظر قبلا برای مشتری " + (person.Fname ?? "") + " " + (person.Lname ?? "") +
                                 " ثبت شده است";
                }
            }
            return op;

        }
        public IQueryable<PersonViewModel> GetAll()
        {
            IList<tbl_person> cachePerson;
            var result = _context.tbl_person.ProjectTo<PersonViewModel>(_mapper.ConfigurationProvider);
            return result;
           
        }

        public IEnumerable<tbl_person> Read()
        {
            return _context.tbl_person.Include("Groups.Group").Include(x => x.PersonAddresses)
                .Include(x => x.PersonInformationSettings);
            //IEnumerable<tbl_person> cachePerson;

            //// Look for cache key.
            //if (!_cache.TryGetValue(CacheKeys.Person, out cachePerson) || UpdateDatabase || cachePerson == null)
            //{
            //    // Key not in cache, so get data.
            //    try
            //    {
            //        //var model = _mapper.Map<List<PersonViewModel>>(persond);
            //        cachePerson = _context.tbl_person.Include("Groups.Group").Include(x => x.PersonAddresses).Include(x => x.PersonInformationSettings);
            //    }
            //    catch (Exception e)
            //    {
            //    }


            //    // Set cache options.
            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        // Keep in cache for this time, reset time if accessed.
            //        .SetSlidingExpiration(TimeSpan.FromDays(3));

            //    // Save data in cache.
            //    _cache.Set(CacheKeys.Person, cachePerson, cacheEntryOptions);
            //}

            //return cachePerson;
            //return GetAll();
        }
        public List<PersonAddressViewModel> FindPersonAddresses(int personId)
        {
            
            return _context.tbl_PersonAddress.Where(x=>x.PersonId==personId).Select(x=>new PersonAddressViewModel
            {
                Address=x.Address,
                Id=x.Id,
                PersonId=x.PersonId,
                Phone=x.Phone,
                Phone2=x.Phone2,
                Phone3= x.Phone3,
                PostalCode=x.PostalCode
            }).ToList();


        }

        public int AddPersonAddresses(PersonAddressViewModel personAddress)
        {
            var peraddress = _mapper.Map<tbl_PersonAddress>(personAddress);
            _context.tbl_PersonAddress.Add(peraddress);
            _context.SaveChanges();
            return peraddress.Id;
        }

        public void RemovePersonAddresses(PersonAddressViewModel personAddress)
        {
            var perAddress = _mapper.Map<tbl_PersonAddress>(personAddress);
            _context.tbl_PersonAddress.Remove(perAddress);
            _context.SaveChanges();
           
        }

        public int AddPersonInformationSetting(PersonInformationSettingViewModel personInformation)
        {
            var peraddress = _mapper.Map<tbl_PersonInformationSetting>(personInformation);
            _context.tbl_PersonInformationSetting.Add(peraddress);
            _context.SaveChanges();
            return peraddress.Id;
        }

        public void RemovePersonInformationSetting(PersonInformationSettingViewModel personInformation)
        {
            var personInformationSetting = _mapper.Map<tbl_PersonInformationSetting>(personInformation);
            _context.tbl_PersonInformationSetting.Remove(personInformationSetting);
            _context.SaveChanges();
        }

        public List<PersonInformationSettingViewModel> FindPersonInformation(int personId)
        {
            return _context.tbl_PersonInformationSetting.Where(x => x.PersonId == personId).Select(x=>new PersonInformationSettingViewModel
            {
                Id=x.Id,
                PersonId=x.PersonId,
                PropertyName=x.PropertyName,
                PropertyValue=x.PropertyValue
            }).ToList();
        }
        public void Update(PersonCreateViewModel person)
        {
            var per = _context.tbl_person.FirstOrDefault(x => x.id == person.Id);
            if (per != null)
            {
                per.Fname = person.FName;
                per.Lname = person.LName;
                per.cell = person.Cell;
                per.codemelli = person.Codemelli;
                per.tell = person.Tell;
                per.Pdescription = person.Pdescription;
                per.prefix = person.Prefix;
                per.sex = person.Sex;
                per.BrancheId = person.BrancheId;
                per.CreateDatetime = per.CreateDatetime ?? DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void UpdateAddress(PersonAddressViewModel personAddress)
        {
            var person = _context.tbl_PersonAddress.FirstOrDefault(x=>x.Id==personAddress.Id);
            if (person != null)
            {
                person.Address = personAddress.Address;
                person.Phone = personAddress.Phone;
                person.Phone2 = personAddress.Phone2;
                person.Phone3 = personAddress.Phone3;
                person.PostalCode = personAddress.PostalCode;

                _context.SaveChanges();
            }
           
        }
        public void UpdateInformationSetting(PersonInformationSettingViewModel personInformation)
        {
            var person = _context.tbl_PersonInformationSetting.FirstOrDefault(x => x.Id == personInformation.Id);
            if (person != null)
            {
                person.PropertyName = personInformation.PropertyName;
                person.PropertyValue = personInformation.PropertyValue;
                _context.SaveChanges();
            }
           
            
        }
    }
}

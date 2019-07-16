using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs.ArchiveSmsVM;

namespace SoltaniWeb.Models.Services.ArchiveSms
{
    public interface IArchiveSmsService
    {
        IEnumerable<ArchiveSmsViewModel> GetAll(int branchId=0);
        IEnumerable<ArchiveSmsViewModel> Getpaging(int pageNumber,int pageSize);
        ResultStatus SendSmsWithListPersonId(ArchiveSmsCreateViewModel model);
        ResultStatus SendSmsWithPersonMobile(string personMobile, string context, int userId);
        ResultStatus SendSmsWithFile(IFormFile fileNumber, int userId);
    }
}

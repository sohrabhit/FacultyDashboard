using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ISettingRepository
    {
        Task<Setting> IncreaseResource_7(int? valueCount, SienceGroupType? ty, bool IncreaseOrDecress);
        Task<Setting> IncreaseEducation_1(int? valueCount, EducationType? ty, bool IncreaseOrDecress);
        Task<Setting> IncreaseEducation_2(int? valueCount, EducationType? ty, bool IncreaseOrDecress);
        Task<Setting> Research_Count(int? valueCount, bool IncreaseOrDecress);
    }
}

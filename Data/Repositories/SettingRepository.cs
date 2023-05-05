using Common;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository, IScopedDependency
    {
        public SettingRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        public async Task<Setting> IncreaseResource_7(int? valueCount, SienceGroupType? ty, bool IncreaseOrDecress)
        {
            var item = await Table.FirstOrDefaultAsync();
            if (valueCount.HasValue)
            {
                if (IncreaseOrDecress)
                {
                    if (item != null && ty.HasValue)
                    {
                        switch(ty)
                        {
                            case SienceGroupType.L1: {
                                    item.Resource_7_All += valueCount.Value;
                                    item.Resource_7_1 += valueCount.Value;
                                }
                                break;
                            case SienceGroupType.L2:
                                {
                                    item.Resource_7_All += valueCount.Value;
                                    item.Resource_7_2 += valueCount.Value;
                                }
                                break;
                            case SienceGroupType.L3:
                                {
                                    item.Resource_7_All += valueCount.Value;
                                    item.Resource_7_3 += valueCount.Value;
                                }
                                break;
                            case SienceGroupType.L4:
                                {
                                    item.Resource_7_All += valueCount.Value;
                                    item.Resource_7_4 += valueCount.Value;
                                }
                                break;
                        }
                    }
                    //Table.Where(x => x.Resource_7_All.HasValue).ToList().Select(c =>{c.Resource_7_All += valueCount; c.Resource_7_1 += valueCount; return c;}).ToList();
                }
                else
                {
                    if (item != null && ty.HasValue)
                    {
                        switch (ty)
                        {
                            case SienceGroupType.L1:
                                {
                                    if (item.Resource_7_All - valueCount.Value >= 0)
                                        item.Resource_7_All -= valueCount.Value;
                                    if (item.Resource_7_1 - valueCount.Value >= 0)
                                        item.Resource_7_1 -= valueCount.Value;
                                }
                                break;
                            case SienceGroupType.L2:
                                {
                                    if (item.Resource_7_All - valueCount.Value >= 0)
                                        item.Resource_7_All -= valueCount.Value;
                                    if (item.Resource_7_2 - valueCount.Value >= 0)
                                        item.Resource_7_2 -= valueCount.Value;
                                }
                                break;
                            case SienceGroupType.L3:
                                {
                                    if (item.Resource_7_All - valueCount.Value >= 0)
                                        item.Resource_7_All -= valueCount.Value;
                                    if (item.Resource_7_3 - valueCount.Value >= 0)
                                        item.Resource_7_3 -= valueCount.Value;
                                }
                                break;
                            case SienceGroupType.L4:
                                {
                                    if (item.Resource_7_All - valueCount.Value >= 0)
                                        item.Resource_7_All -= valueCount.Value;
                                    if (item.Resource_7_4 - valueCount.Value >= 0)
                                        item.Resource_7_4 -= valueCount.Value;
                                }
                                break;
                        }
                    }
                }
            }
            return item;
        }
        public async Task<Setting> IncreaseEducation_1(int? valueCount, EducationType? ty, bool IncreaseOrDecress)
        {
            var item = await Table.FirstOrDefaultAsync();
            if (valueCount.HasValue)
            {
                if (IncreaseOrDecress)
                {
                    if (item != null && ty.HasValue)
                    {
                        switch (ty)
                        {
                            case EducationType.L1:
                                {
                                    item.Education_1_All += valueCount.Value;
                                    item.Education_1_1 += valueCount.Value;
                                }
                                break;
                            case EducationType.L2:
                                {
                                    item.Education_1_All += valueCount.Value;
                                    item.Education_1_2 += valueCount.Value;
                                }
                                break;
                            case EducationType.L3:
                                {
                                    item.Education_1_All += valueCount.Value;
                                    item.Education_1_3 += valueCount.Value;
                                }
                                break;
                            case EducationType.L4:
                                {
                                    item.Education_1_All += valueCount.Value;
                                    item.Education_1_4 += valueCount.Value;
                                }
                                break;
                        }
                    }
                    //Table.Where(x => x.Education_1_All.HasValue).ToList().Select(c =>{c.Education_1_All += valueCount; c.Education_1_1 += valueCount; return c;}).ToList();
                }
                else
                {
                    if (item != null && ty.HasValue)
                    {
                        switch (ty)
                        {
                            case EducationType.L1:
                                {
                                    if (item.Education_1_All - valueCount.Value >= 0)
                                        item.Education_1_All -= valueCount.Value;
                                    if (item.Education_1_1 - valueCount.Value >= 0)
                                        item.Education_1_1 -= valueCount.Value;
                                }
                                break;
                            case EducationType.L2:
                                {
                                    if (item.Education_1_All - valueCount.Value >= 0)
                                        item.Education_1_All -= valueCount.Value;
                                    if (item.Education_1_2 - valueCount.Value >= 0)
                                        item.Education_1_2 -= valueCount.Value;
                                }
                                break;
                            case EducationType.L3:
                                {
                                    if (item.Education_1_All - valueCount.Value >= 0)
                                        item.Education_1_All -= valueCount.Value;
                                    if (item.Education_1_3 - valueCount.Value >= 0)
                                        item.Education_1_3 -= valueCount.Value;
                                }
                                break;
                            case EducationType.L4:
                                {
                                    if (item.Education_1_All - valueCount.Value >= 0)
                                        item.Education_1_All -= valueCount.Value;
                                    if (item.Education_1_4 - valueCount.Value >= 0)
                                        item.Education_1_4 -= valueCount.Value;
                                }
                                break;
                        }
                    }
                }
            }
            return item;
        }
        public async Task<Setting> IncreaseEducation_2(int? valueCount, EducationType? ty, bool IncreaseOrDecress)
        {
            var item = await Table.FirstOrDefaultAsync();
            if (valueCount.HasValue)
            {
                if (IncreaseOrDecress)
                {
                    if (item != null && ty.HasValue)
                    {
                        switch (ty)
                        {
                            case EducationType.L1:
                                {
                                    item.Education_2_All += valueCount.Value;
                                    item.Education_2_1 += valueCount.Value;
                                }
                                break;
                            case EducationType.L2:
                                {
                                    item.Education_2_All += valueCount.Value;
                                    item.Education_2_2 += valueCount.Value;
                                }
                                break;
                            case EducationType.L3:
                                {
                                    item.Education_2_All += valueCount.Value;
                                    item.Education_2_3 += valueCount.Value;
                                }
                                break;
                            case EducationType.L4:
                                {
                                    item.Education_2_All += valueCount.Value;
                                    item.Education_2_4 += valueCount.Value;
                                }
                                break;
                        }
                    }
                    //Table.Where(x => x.Education_2_All.HasValue).ToList().Select(c =>{c.Education_2_All += valueCount; c.Education_2_1 += valueCount; return c;}).ToList();
                }
                else
                {
                    if (item != null && ty.HasValue)
                    {
                        switch (ty)
                        {
                            case EducationType.L1:
                                {
                                    if (item.Education_2_All - valueCount.Value >= 0)
                                        item.Education_2_All -= valueCount.Value;
                                    if (item.Education_2_1 - valueCount.Value >= 0)
                                        item.Education_2_1 -= valueCount.Value;
                                }
                                break;
                            case EducationType.L2:
                                {
                                    if (item.Education_2_All - valueCount.Value >= 0)
                                        item.Education_2_All -= valueCount.Value;
                                    if (item.Education_2_2 - valueCount.Value >= 0)
                                        item.Education_2_2 -= valueCount.Value;
                                }
                                break;
                            case EducationType.L3:
                                {
                                    if (item.Education_2_All - valueCount.Value >= 0)
                                        item.Education_2_All -= valueCount.Value;
                                    if (item.Education_2_3 - valueCount.Value >= 0)
                                        item.Education_2_3 -= valueCount.Value;
                                }
                                break;
                            case EducationType.L4:
                                {
                                    if (item.Education_2_All - valueCount.Value >= 0)
                                        item.Education_2_All -= valueCount.Value;
                                    if (item.Education_2_4 - valueCount.Value >= 0)
                                        item.Education_2_4 -= valueCount.Value;
                                }
                                break;
                        }
                    }
                }
            }
            return item;
        }
        public async Task<Setting> Research_Count(int? valueCount, bool IncreaseOrDecress)
        {
            var item = await Table.FirstOrDefaultAsync();
            if (valueCount.HasValue)
            {
                if (IncreaseOrDecress)
                {
                    if (item != null)
                    {
                        item.Research_Count += valueCount.Value;
                    }
                    //Table.Where(x => x.Education_2_All.HasValue).ToList().Select(c =>{c.Education_2_All += valueCount; c.Education_2_1 += valueCount; return c;}).ToList();
                }
                else
                {
                    if (item != null)
                    {
                        if (item.Research_Count - valueCount.Value >= 0)
                            item.Research_Count -= valueCount.Value;
                    }
                }
            }
            return item;
        }
    }
}
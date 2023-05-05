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
    public class UserReport_ListViewModel
    {
        public List<UserReport> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class UserReport_Repository : Repository<UserReport>, IUserReport_Repository, IScopedDependency
    {
        public UserReport_Repository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        public async Task<UserReport> GetById(int id)
        {
            return await Table.Include(x => x.RegistereUser).Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<List<UserReport>> GetByUserId(string id)
        {
            return await Table.Include(x => x.RegistereUser).Where(x => x.RegistereUser != null && x.RegistereUser.Id.Equals(id)).ToListAsync();
        }
        public async Task<UserReport_ListViewModel> GetAllDatas(int count, int pageId = 1)
        {
            //IQueryable<Activity_1> result = Table; //----lazy load
            //-------paging---------//
            //int take = 2;
            int skip = (pageId - 1) * count;

            UserReport_ListViewModel list = new UserReport_ListViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling(Table.Count() / (double)count);

            list.Datas = await Table.Include(x => x.RegistereUser).OrderBy(u => u.Id).Skip(skip).Take(count).ToListAsync();
            return list;

        }
    }
}

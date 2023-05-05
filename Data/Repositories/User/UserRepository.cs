using Common;
using Common.Utilities;
using Entities;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class User_ListViewModel
    {
        public List<User> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<User_ListViewModel> GetAllDatas(int count, int pageId = 1)
        {
            User_ListViewModel list = new User_ListViewModel();
            int skip = (pageId - 1) * count;
            skip = skip < 0 ? 0 : skip;

            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling(Table.Count() / (double)count);

            list.Datas = await Table.OrderBy(u => u.Id).Skip(skip).Take(count).ToListAsync();
            return list;

        }
        public async Task<ExcelPackage> ExportAllToExcel(int count, string Title, LanguageType lang, int pageId = 1)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Report";
            ws.Cells["B1"].Value = Title;

            var list = await GetAllDatas(count, pageId);

            ws = GetExcel(list.Datas, ws, lang);
            return pck;
        }
        public async Task<PdfPTable> ExportAllToPDF(int count, LanguageType lang, string rootPath, int pageId = 1)
        {
            var list = await GetAllDatas(count, pageId);
            var table = GetPDF(list.Datas, lang, rootPath);
            return table;
        }
        public async Task<StringBuilder> ExportAllToWord(int count, LanguageType lang, int pageId = 1)
        {
            var list = await GetAllDatas(count, pageId);
            var table = GetWord(list.Datas, lang);
            return table;
        }

public async Task<User> GetById(int id)
        {
            return await Table/*.Include(x => x.RegistereUser)*/.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new Exception("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }

        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            user.SecurityStamp = Guid.NewGuid().ToString("D");// Guid.NewGuid();
            return UpdateAsync(user, cancellationToken);
        }

        //public override void Update(User entity, bool saveNow = true)
        //{
        //    entity.SecurityStamp = Guid.NewGuid();
        //    base.Update(entity, saveNow);
        //}

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
    }
}

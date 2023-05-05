using Entities;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserReport_Repository
    {
        Task<UserReport> GetById(int id);
        Task<UserReport_ListViewModel> GetAllDatas(int count, int pageId);
        Task<List<UserReport>> GetByUserId(string id);
        //Task<PdfPTable> ExportAllToPDF(int count, LanguageType lang, int pageId);
        //Task<StringBuilder> ExportAllToWord(int count, LanguageType lang, int pageId);
    }
}

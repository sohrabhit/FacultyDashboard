using Entities;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IIT_3_Repository
    {
        Task<IT_3> GetById(int id);
        Task<IT_3_ListViewModel> GetAllDatas(int count, int pageId);
        Task<ExcelPackage> ExportAllToExcel(int count, string Title, LanguageType lang, int pageId);
        Task<PdfPTable> ExportAllToPDF(int count, LanguageType lang, string rootPath, int pageId);
        Task<StringBuilder> ExportAllToWord(int count, LanguageType lang, int pageId);
    }
}

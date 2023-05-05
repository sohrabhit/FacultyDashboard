using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using iTextSharp.text.pdf;
using OfficeOpenXml;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User_ListViewModel> GetAllDatas(int count, int pageId);
        Task<User> GetById(int id);
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task<ExcelPackage> ExportAllToExcel(int count, string Title, LanguageType lang, int pageId);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
        Task<PdfPTable> ExportAllToPDF(int count, LanguageType lang, string rootPath, int pageId);
        Task<StringBuilder> ExportAllToWord(int count, LanguageType lang, int pageId);
    }
}
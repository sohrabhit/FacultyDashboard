﻿using Entities;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IResource_5_Repository
    {
        Task<Resource_5> GetById(int id);
        Task<Resource_5_ListViewModel> GetAllDatas(int count, int pageId);
        Task<ExcelPackage> ExportAllToExcel(int count, string Title, LanguageType lang, int pageId);
        Task<PdfPTable> ExportAllToPDF(int count, LanguageType lang, string rootPath, int pageId);
        Task<StringBuilder> ExportAllToWord(int count, LanguageType lang, int pageId);
    }
}
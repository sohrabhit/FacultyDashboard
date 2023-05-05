﻿using Common;
using Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Research_5_ListViewModel
    {
        public List<Research_5> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class Research_5_Repository : Repository<Research_5>, IResearch_5_Repository, IScopedDependency
    {
        public Research_5_Repository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        public async Task<Research_5> GetById(int id)
        {
            return await Table.Include(x => x.RegistereUser).Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<Research_5_ListViewModel> GetAllDatas(int count, int pageId = 1)
        {
            Research_5_ListViewModel list = new Research_5_ListViewModel();
            int skip = (pageId - 1) * count;
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling(Table.Count() / (double)count);

            list.Datas = await Table.Include(x => x.RegistereUser).OrderBy(u => u.Id).Skip(skip).Take(count).ToListAsync();
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
    }
}
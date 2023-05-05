using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Entities;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        PdfPTable GetPDF(List<TEntity> data, LanguageType lang, string rootPath);
        //PdfPTable GetPDF(List<TEntity> data, LanguageType lang);
        StringBuilder GetWord(List<TEntity> data, LanguageType lang);

        //ICollection<TType> Search<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        //ICollection<TProperty> Get<TProperty>(Expression<Func<TEntity, bool>> where, TEntity item, IEnumerable<PropertyInfo> keys/*Expression<Func<TEntity, TProperty>> select*/) where TProperty : class;
        List<TEntity> Search_prevmodel(TEntity searchedmodel);
        List<TEntity> Search(List<Tuple<string, string, object>> SearchParameters, TEntity searchedmodel);

        IEnumerable<ReportWith2Col> GetAmar(Expression<Func<TEntity, bool>> where, string select, LanguageType languageType, int Doctor_id, Report_Type report_Type);
        IEnumerable<ChartWith3ColString> GetChart(Expression<Func<TEntity, bool>> where, string select, LanguageType languageType, int Doctor_id, DateGroupBy_Type? dateGroupBy, Report_ComputType? report_computetype);

        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        void Add(TEntity entity, bool saveNow = true);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        void Attach(TEntity entity);
        void Delete(TEntity entity, bool saveNow = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteAllAsync(CancellationToken cancellationToken, bool saveNow = true);
        void Detach(TEntity entity);
        TEntity GetById(params object[] ids);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        void Update(TEntity entity, bool saveNow = true);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    }
}
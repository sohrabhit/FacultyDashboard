using Common;
using Common.Search;
using Common.Utilities;
using Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class QueryFilterObject
    {
        public InfoAttribute AttributeInfo { get; set; }

        public string PropertyName { get; set; }

        public Type PropertyType { get; set; }

        public object Value { get; set; }

        public string ComparisonOperator { get; set; }

        public string LogicalOperator { get; set; }


    }
    public class QueryFilterObject_v2
    {
        public InfoAttribute AttributeInfo { get; set; }

        public string PropertyName { get; set; }

        public Type PropertyType { get; set; }

        public object Value { get; set; }

        public Operator ComparisonOperator { get; set; }

        public string LogicalOperator { get; set; }


    }
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }


        #region Async Method
        // FindAsync : مقادیر رو بر اساس کلیدها واکشی میکنه
        // CancellationToken : با قطع ارتباط با سرور عملیات کلا کنسل میکنه و باعث نمیشه بیخودی منابع و زمان هدر بره
        public virtual Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken).AsTask();
        }

        // saveNow : اگر خواستیم چند تا مقدار پاس بدیم (صبر کنه همه بیاد بعدا یکجا ثبت بشه) بعد باهم ذخیره کنیم این رو ترو میکنیم
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
            //var entry = DbContext.Entry(entity);
            //if (entry != null)
            //{
            //    if (DbContext.Entry(entity).State == EntityState.Detached)
            //        Entities.Attach(entity);
            //    entry.State = EntityState.Modified;
            //}
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteAllAsync(CancellationToken cancellationToken, bool saveNow = true)
        {
            //Assert.NotNull(entity, nameof(entity));
            Entities.RemoveRange(await Entities.ToListAsync());
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            DbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        //public ICollection<TProperty> Search<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> select) where TProperty : class
        //{
        //    //List<object> f = new List<object>();
        //    var all = Entities.Where(where).Select(select).ToList();

        //    return all;
        //}

        public List<TEntity> Search(List<Tuple<string, string, object>> SearchParameters, TEntity searchedmodel)
        {
            // 1 => FieldName
            // 2 => Operator
            // 3 => Value
            Type type = searchedmodel.GetType();
            foreach (var item in SearchParameters)
            {
                PropertyInfo prop = type.GetProperty(item.Item1);
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    InfoAttribute authAttr = attr as InfoAttribute;
                    if (authAttr != null)
                    {
                        if (authAttr.fieldType == FieldType.Enum)
                            prop.SetValue(searchedmodel, Enum.Parse(authAttr.Enum, item.Item3.ToString())); //prop.SetValue(searchedmodel, Enum.Parse(authAttr.Enum, item.Item3.ToString(), true), null);
                        if (authAttr.fieldType == FieldType.Strings || authAttr.fieldType == FieldType.Multiple || authAttr.fieldType == FieldType.Class)
                            prop.SetValue(searchedmodel, item.Item3.ToString());
                        if (authAttr.fieldType == FieldType.Integerr)
                            prop.SetValue(searchedmodel, Convert.ToInt32(item.Item3));
                        if (authAttr.fieldType == FieldType.Float)
                            prop.SetValue(searchedmodel, Convert.ToDecimal(item.Item3));
                        if (authAttr.fieldType == FieldType.Double)
                            prop.SetValue(searchedmodel, Convert.ToDouble(item.Item3));
                        if (authAttr.fieldType == FieldType.Long)
                            prop.SetValue(searchedmodel, long.Parse(item.Item3.ToString()));
                        if (authAttr.fieldType == FieldType.Bool)
                            prop.SetValue(searchedmodel, Convert.ToBoolean(item.Item3));
                        else if (authAttr.fieldType == FieldType.DateTime)
                        {
                            var sd = item.Item3.ToString().Split("/");
                            prop.SetValue(searchedmodel, new DateTime(int.Parse(sd[2]), int.Parse(sd[0]), int.Parse(sd[1]), 0, 0, 0), null);

                            // چون تاریخ دیتابیس فارسی هست
                            //PersianCalendar p = new PersianCalendar();
                            //DateTime s = Model.Leave_StartDate.Value, e = Model.Leave_EndDate.Value;
                            //Model.Leave_StartDate = p..ToDateTime(s.Year, s.Month, s.Day, 0, 0, 0, 0);
                            //var test = new DateTime(int.Parse(sd[2]), int.Parse(sd[0]), int.Parse(sd[1]), 0, 0, 0);
                            //var test2 = new DateTime(1400, 5, 12, 12, 0, 0);
                            //var con = DateAndTime.GetPersianDateTime(new DateTime(int.Parse(sd[2]), int.Parse(sd[0]), int.Parse(sd[1]), 0, 0, 0));
                            //         var con = new DateTime(1400, 5, 12, 5, 57, 47);
                            //prop.SetValue(searchedmodel, con, null);
                        }
                    }
                }
            }
            var b = searchedmodel;
            PropertyInfo[] props = searchedmodel.GetType().GetProperties(BindingFlags.DeclaredOnly |BindingFlags.Public | BindingFlags.Instance);
            List<QueryFilterObject_v2> Quesries = new List<QueryFilterObject_v2>();
            foreach (var item in props)
            {
                var att = AttributeTefactor(item.Name);
                var propertyValue = item.GetValue(searchedmodel, null);
                if (propertyValue != null && propertyValue.ToString() != "0" && (att.fieldType != FieldType.Class && att.fieldType != FieldType.Collection))
                {
                    //Quesries.Add(new QueryFilterObject()
                    //{
                    //    AttributeInfo = att,
                    //    PropertyName = item.Name,
                    //    PropertyType = item.PropertyType,
                    //    ComparisonOperator = "==",
                    //    Value = propertyValue,//.ToString(),
                    //    LogicalOperator = "and" // and  or
                    //});
                    var op = SearchParameters.Where(x => x.Item1 == item.Name).Select(x => x.Item2).FirstOrDefault();
                    if (!string.IsNullOrEmpty(op))

                        Quesries.Add(new QueryFilterObject_v2()
                        {
                            AttributeInfo = att,
                            PropertyName = item.Name,
                            PropertyType = item.PropertyType,
                            ComparisonOperator = !string.IsNullOrEmpty(op) ? ConvertOps(op/*"=="*/) : new Operator("=", Expression.Equal, Operator.TypesToApply.Both),
                            Value = propertyValue,//.ToString(),
                            LogicalOperator = "and" // and  or
                        });
                }
            }
            //var lambda = SimpleComparison<TEntity>(Quesries);
            var lambda = Searchop<TEntity>(Quesries);
            return Entities.Where(lambda).ToList();
        }
        #region prev
        public List<TEntity> Search_prevmodel(TEntity searchedmodel)
        {
            PropertyInfo[] props = searchedmodel.GetType().GetProperties(BindingFlags.DeclaredOnly |
                                              BindingFlags.Public |
                                              BindingFlags.Instance);
            List<QueryFilterObject> Quesries = new List<QueryFilterObject>();
            foreach (var item in props)
            {
                var att = AttributeTefactor(item.Name);
                var propertyValue = item.GetValue(searchedmodel, null);
                if (propertyValue != null && propertyValue.ToString() != "0" && (att.fieldType != FieldType.Class && att.fieldType != FieldType.Collection))
                {
                    Quesries.Add(new QueryFilterObject()
                    {
                        AttributeInfo = att,
                        PropertyName = item.Name,
                        PropertyType = item.PropertyType,
                        ComparisonOperator = "==",
                        Value = propertyValue,//.ToString(),
                        LogicalOperator = "and" // and  or
                    });
                }
            }
            var lambda = SimpleComparison<TEntity>(Quesries);
            return Entities.Where(lambda).ToList();
        }
        private static Expression ToShortDateConstant(object value)
        {
            object val = null;
            try
            {
                if (value != null)
                {
                    var sd = (DateTime)value;
                    val = new DateTime(sd.Year, sd.Month, sd.Day, 0, 0, 0);// Convert.ChangeType(value, prop.PropertyType);
                }
            }
            catch (Exception)
            { }
            return Expression.Constant(val);
        }
        private static Expression ToStringConstant(object value)
        {
            object val = null;
            try
            {
                if (value != null)
                    val = value.ToString();// Convert.ChangeType(value, prop.PropertyType);
            }
            catch (Exception)
            { }
            return Expression.Constant(val);
        }
        private Expression BuildSimpleBinary(Expression expr, Expression right, string @operator)
        {
            ExpressionType expressionType = ExpressionType.Equal;
            switch (@operator)
            {
                case "eq": expressionType = ExpressionType.Equal; break;
                case "ne": expressionType = ExpressionType.NotEqual; break;
                case "lt": expressionType = ExpressionType.LessThan; break;
                case "gt": expressionType = ExpressionType.GreaterThan; break;
                case "le": expressionType = ExpressionType.LessThanOrEqual; break;
                case "ge": expressionType = ExpressionType.GreaterThanOrEqual; break;
                case "add": expressionType = ExpressionType.Add; break;
                case "sub": expressionType = ExpressionType.Subtract; break;
                case "mul": expressionType = ExpressionType.Multiply; break;
                case "div": expressionType = ExpressionType.Divide; break;
                case "mod": expressionType = ExpressionType.Modulo; break;
                case "and": expressionType = ExpressionType.AndAlso; break;
                case "or": expressionType = ExpressionType.OrElse; break;
                    //default: throw new SnNotSupportedException("Unknown operator: " + @operator);
            }
            return Expression.MakeBinary(expressionType, expr, right);
        }
        public Expression<Func<TSource, bool>> SimpleComparison_main<TSource>(List<QueryFilterObject> queryFilterObjects)
        {
            //initialize the body expression
            // https://stackoverflow.com/questions/33836819/building-a-list-of-expressions-via-expression-trees
            BinaryExpression bodyExpression = null;
            BinaryExpression andExpressionBody = null;
            BinaryExpression orExpressionBody = null;

            //create parameter expression
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "DynamicFilterQuery");

            //list of binary expressions to store either the || or && operators
            List<BinaryExpression> andExpressions = new List<BinaryExpression>();
            List<BinaryExpression> orExpressions = new List<BinaryExpression>();

            foreach (var queryFilterObject in queryFilterObjects)
            {
                //create member property expression
                var property = Expression.Property(parameterExpression, queryFilterObject.PropertyName);

                //create the constant expression value
                var constantExpressionValue = Expression.Constant(queryFilterObject.Value, queryFilterObject.PropertyType);
                // زمان تاریخ عبارت جستجو را 0 می کند
                if (queryFilterObject.PropertyType == typeof(DateTime))
                {
                    var sd = (DateTime)queryFilterObject.Value;
                    constantExpressionValue = Expression.Constant(new DateTime(sd.Year, sd.Month, sd.Day, 0, 0, 0), queryFilterObject.PropertyType);
                }

                //create the binary expression clause based on the comparison operator
                BinaryExpression clause = null;
                if (queryFilterObject.ComparisonOperator == "==")
                {
                    if (queryFilterObject.PropertyType == typeof(DateTime))
                    {
                        var property2 = Expression.Convert(ToShortDateConstant(queryFilterObject.Value), queryFilterObject.PropertyType);
                        clause = Expression.Equal(property2, constantExpressionValue);
                    }
                    else if (queryFilterObject.AttributeInfo.fieldType == FieldType.Multiple)
                    {
                        //var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        //StringExtensions.ExpressionContainss("Source", "Item")
                        MethodInfo method = typeof(ExpressionMethods).GetMethod("ExpressionContainss", new[] { typeof(string), typeof(string) });
                        var SearchedValue = Expression.Constant(queryFilterObject.Value, typeof(string));
                        //var property2 = Expression.Convert(ToStringConstant(queryFilterObject.Value), queryFilterObject.PropertyType);
                        clause = Expression.MakeBinary(ExpressionType.Equal, property/*Expression.Constant("1,2")*/,
                            /*SearchedValue*/constantExpressionValue, false, method);
                        // property => مشکل در مقداردهی این هست

                        //Expression exp = null;
                        //MethodInfo mymethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        //ConstantExpression constant = Expression.Constant(queryFilterObject.Value);
                        //MethodCallExpression result = Expression.Call(property, mymethod, constant);
                        //exp = result;

                        //var SearchedValue = Expression.Convert(ToStringConstant(queryFilterObject.Value), typeof(string));
                        //constantExpressionValue = Expression.Constant(/*SearchedValue*/queryFilterObject.Value, queryFilterObject.PropertyType);
                        //var containsCall = Expression.Call(constantExpressionValue, stringContainsMethod,
                        //    Expression.Constant(queryFilterObject.Value, typeof(string)));
                    }
                    else
                    {
                        //   clause = Expression.Equal(property, constantExpressionValue);
                        // new
                        ////MethodInfo method = typeof(ExpressionMethods).GetMethod("ExpressionContainss", new[] { typeof(string), typeof(string) });
                        ////MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        //var SearchedValue = Expression.Constant(queryFilterObject.Value, typeof(string)); // fh
                        ////clause = Expression.MakeBinary(ExpressionType.Equal, Expression.Constant("jhfhfs")/*property*/, constantExpressionValue, false, method);
                        //clause = Expression.MakeBinary(ExpressionType.Equal, property, constantExpressionValue, false, method);


                        //StringComparison StrComp = StringComparison.CurrentCultureIgnoreCase;
                        //MethodInfo miContain = typeof(ExpressionMethods).GetMethod("ExpressionContainss", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                        //var bEXP = Expression.Call(miContain, source, Expression.Constant(ValToCheck), Expression.Constant(StrComp));

                        MethodCallExpression expression = Expression.Call(
                            typeof(ExpressionMethods).GetMethod("ExpressionContainss",
                                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public), property, constantExpressionValue);

                        //var finalExpression = Expression.Lambda<Func<TSource, bool>>(expression, parameterExpression);
                        //return finalExpression;
                        //   clause = expression as BinaryExpression;

                        //var bEXP = Expression.Call(miContain, property, Expression.Constant(queryFilterObject.Value)/*, Expression.Constant(StrComp)*/);
                        clause = Expression.MakeBinary(ExpressionType.Equal, property, constantExpressionValue, false, expression.Method);
                    }
                    //selectedobj = selectedobj.Where(a => EntityFunctions.TruncateTime(a.DischargeDate) == EntityFunctions.TruncateTime(dt1));
                    //   EntityFunctions.Like()
                }
                else if (queryFilterObject.ComparisonOperator == "!=")
                {
                    clause = Expression.NotEqual(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == ">")
                {
                    clause = Expression.GreaterThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == ">=")
                {
                    clause = Expression.GreaterThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == "<")
                {
                    clause = Expression.LessThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == "<=")
                {
                    clause = Expression.LessThanOrEqual(property, constantExpressionValue);
                }
                //you should validate against a null clause....

                //assign the item either to the relevant logical comparison expression list
                if (queryFilterObject.LogicalOperator == "and" || queryFilterObject.LogicalOperator == "&&")
                {
                    andExpressions.Add(clause);
                }
                else if (queryFilterObject.LogicalOperator == "or" || queryFilterObject.LogicalOperator == "||")
                {
                    orExpressions.Add(clause);
                }
            }

            if (andExpressions.Count > 0)
                andExpressionBody = andExpressions.Aggregate((e, next) => Expression.And(e, next));

            if (orExpressions.Count > 0)
                orExpressionBody = orExpressions.Aggregate((e, next) => Expression.Or(e, next));

            if (andExpressionBody != null && orExpressionBody != null)
                bodyExpression = Expression.OrElse(andExpressionBody, orExpressionBody);

            if (andExpressionBody != null && orExpressionBody == null)
                bodyExpression = andExpressionBody;

            if (andExpressionBody == null && orExpressionBody != null)
                bodyExpression = orExpressionBody;

            if (bodyExpression == null)
                throw new Exception("Null Expression.");

            var finalExpression = Expression.Lambda<Func<TSource, bool>>(bodyExpression, parameterExpression);

            return finalExpression;

        }
        public Expression<Func<TSource, bool>> SimpleComparison<TSource>(List<QueryFilterObject> queryFilterObjects)
        {
            //initialize the body expression
            // https://stackoverflow.com/questions/33836819/building-a-list-of-expressions-via-expression-trees
            Expression bodyExpression = null;
            Expression andExpressionBody = null;
            Expression orExpressionBody = null;

            //create parameter expression
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "DynamicFilterQuery");

            //list of binary expressions to store either the || or && operators
            List<Expression> andExpressions = new List<Expression>();
            List<Expression> orExpressions = new List<Expression>();

            foreach (var queryFilterObject in queryFilterObjects)
            {
                //create member property expression
                var property = Expression.Property(parameterExpression, queryFilterObject.PropertyName);

                //create the constant expression value
                var constantExpressionValue = Expression.Constant(queryFilterObject.Value, queryFilterObject.PropertyType);
                // زمان تاریخ عبارت جستجو را 0 می کند
                if (queryFilterObject.PropertyType == typeof(DateTime))
                {
                    var sd = (DateTime)queryFilterObject.Value;
                    constantExpressionValue = Expression.Constant(new DateTime(sd.Year, sd.Month, sd.Day, 0, 0, 0), queryFilterObject.PropertyType);
                }

                //create the binary expression clause based on the comparison operator
                Expression clause = null;
                if (queryFilterObject.ComparisonOperator == "==")
                {
                    if (queryFilterObject.PropertyType == typeof(DateTime))
                    {
                        var property2 = Expression.Convert(ToShortDateConstant(queryFilterObject.Value), queryFilterObject.PropertyType);
                        clause = Expression.Equal(property2, constantExpressionValue);
                    }
                    else if (queryFilterObject.AttributeInfo.fieldType == FieldType.Multiple)
                    {
                        //var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        //StringExtensions.ExpressionContainss("Source", "Item")
                        MethodInfo method = typeof(ExpressionMethods).GetMethod("ExpressionContainss", new[] { typeof(string), typeof(string) });
                        var SearchedValue = Expression.Constant(queryFilterObject.Value, typeof(string));
                        //var property2 = Expression.Convert(ToStringConstant(queryFilterObject.Value), queryFilterObject.PropertyType);
                        clause = Expression.MakeBinary(ExpressionType.Equal, property/*Expression.Constant("1,2")*/,
                            /*SearchedValue*/constantExpressionValue, false, method);
                        // property => مشکل در مقداردهی این هست

                        //Expression exp = null;
                        //MethodInfo mymethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        //ConstantExpression constant = Expression.Constant(queryFilterObject.Value);
                        //MethodCallExpression result = Expression.Call(property, mymethod, constant);
                        //exp = result;

                        //var SearchedValue = Expression.Convert(ToStringConstant(queryFilterObject.Value), typeof(string));
                        //constantExpressionValue = Expression.Constant(/*SearchedValue*/queryFilterObject.Value, queryFilterObject.PropertyType);
                        //var containsCall = Expression.Call(constantExpressionValue, stringContainsMethod,
                        //    Expression.Constant(queryFilterObject.Value, typeof(string)));
                    }
                    else
                    {
                        //clause = Expression.Equal(property, constantExpressionValue);
                        // new
                        //MethodInfo method = typeof(ExpressionMethods).GetMethod("ExpressionContainss", new[] { typeof(string), typeof(string) });
                        ////MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        //var SearchedValue = Expression.Constant(queryFilterObject.Value, typeof(string)); // fh
                        ////clause = Expression.MakeBinary(ExpressionType.Equal, Expression.Constant("jhfhfs")/*property*/, constantExpressionValue, false, method);
                        //clause = Expression.MakeBinary(ExpressionType.Equal, property, constantExpressionValue, false, method);


                        //StringComparison StrComp = StringComparison.CurrentCultureIgnoreCase;
                        //MethodInfo miContain = typeof(ExpressionMethods).GetMethod("ExpressionContainss", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                        //var bEXP = Expression.Call(miContain, source, Expression.Constant(ValToCheck), Expression.Constant(StrComp));
                        var func = typeof(ExpressionMethods).GetMethod("ExpressionContainss"/*, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public*/);
                        clause = Expression.Call(null, func, property, constantExpressionValue);
                        //clause = expression;// as BinaryExpression;

                        //var bEXP = Expression.Call(miContain, property, Expression.Constant(queryFilterObject.Value)/*, Expression.Constant(StrComp)*/);
                        // clause = Expression.MakeBinary(ExpressionType.Equal, property, constantExpressionValue, false, expression.Method);
                    }
                    //selectedobj = selectedobj.Where(a => EntityFunctions.TruncateTime(a.DischargeDate) == EntityFunctions.TruncateTime(dt1));
                    //   EntityFunctions.Like()
                }
                else if (queryFilterObject.ComparisonOperator == "!=")
                {
                    clause = Expression.NotEqual(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == ">")
                {
                    clause = Expression.GreaterThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == ">=")
                {
                    clause = Expression.GreaterThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == "<")
                {
                    clause = Expression.LessThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.ComparisonOperator == "<=")
                {
                    clause = Expression.LessThanOrEqual(property, constantExpressionValue);
                }
                //you should validate against a null clause....

                //assign the item either to the relevant logical comparison expression list
                if (queryFilterObject.LogicalOperator == "and" || queryFilterObject.LogicalOperator == "&&")
                {
                    andExpressions.Add(clause);
                }
                else if (queryFilterObject.LogicalOperator == "or" || queryFilterObject.LogicalOperator == "||")
                {
                    orExpressions.Add(clause);
                }
            }

            if (andExpressions.Count > 0)
                andExpressionBody = andExpressions.Aggregate((e, next) => Expression.And(e, next));

            if (orExpressions.Count > 0)
                orExpressionBody = orExpressions.Aggregate((e, next) => Expression.Or(e, next));

            if (andExpressionBody != null && orExpressionBody != null)
                bodyExpression = Expression.OrElse(andExpressionBody, orExpressionBody);

            if (andExpressionBody != null && orExpressionBody == null)
                bodyExpression = andExpressionBody;

            if (andExpressionBody == null && orExpressionBody != null)
                bodyExpression = orExpressionBody;

            if (bodyExpression == null)
                throw new Exception("Null Expression.");

            var finalExpression = Expression.Lambda<Func<TSource, bool>>(bodyExpression, parameterExpression);

            return finalExpression;

        }
        public Operator ConvertOps(string value)
        {
            Operator op = new Operator("=", Expression.Equal, Operator.TypesToApply.Both);
            switch (value)
            {
                //case "bw" => Between
                //case "nbw" => NotBetween
                case "eq": op = new Operator("=", Expression.Equal, Operator.TypesToApply.Both); break;
                case "ne": op = new Operator("<>", Expression.NotEqual, Operator.TypesToApply.Both); break;
                case "lt": op = new Operator("<=", Expression.LessThanOrEqual, Operator.TypesToApply.Numeric); break;
                case "gt": op = new Operator(">=", Expression.GreaterThanOrEqual, Operator.TypesToApply.Numeric); break;
                case "sw":
                    op = new Operator("شروع با", (expression, expression1) => Expression.Call(expression,
                 typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), expression1), Operator.TypesToApply.String); break;
                case "ct":
                    op = new Operator("شامل باشد", (expression, expression1) => Expression.Call(expression,
                 typeof(string).GetMethod("Contains"), expression1), Operator.TypesToApply.String); break;
                case "nct":
                    op = new Operator("شامل نباشد", (expression, expression1) =>
                    {
                        var contain = Expression.Call(expression, typeof(string).GetMethod("Contains"), expression1);
                        return Expression.Not(contain);
                    }, Operator.TypesToApply.String); break;
                case "fw":
                    op = new Operator("خاتمه با", (expression, expression1) =>
                 Expression.Call(expression, typeof(string).GetMethod("EndsWith"), expression1), Operator.TypesToApply.String); break;
                case "null": op = new Operator("تهی", (expression, expression1) => Expression.Call(expression, typeof(string).GetMethod("IsNullOrWhiteSpace"), null), Operator.TypesToApply.String); break;
                case "nn":
                    op = new Operator("تهی نباشد", (expression, expression1) =>
                    {
                        var contain = Expression.Call(expression, typeof(string).GetMethod("IsNullOrWhiteSpace"), null);
                        return Expression.Not(contain);
                    }, Operator.TypesToApply.String); break;
            }
            return op;
        }
        #endregion
        private Expression<Func<TSource, bool>> Searchop<TSource>(List<QueryFilterObject_v2> data/*System.Linq.Expressions.Expression<Func<Patient,bool>> predic, Type comparetype, string searchValue*/)
        {
            System.Linq.Expressions.Expression expression = null;
            var isFirst = true;
            ParameterExpression pe = Expression.Parameter(typeof(TSource), "lambdaExParameter");

            var searchAndor = new List<AndOr>();
            searchAndor.Add(new AndOr("And", "و", Expression.AndAlso));
            
            for (int i = 0; i < data.Count; i++)
            {
                Expression right, left;
                ExpressionExtensions.CreateLeftAndRightExpression<TSource>(data[i].PropertyName, data[i].PropertyType, data[i].AttributeInfo
                    , data[i].Value != null ? data[i].Value.ToString() : "", pe, out left, out right);

                var ex = ExpressionExtensions.AddOperatorExpression(data[i].ComparisonOperator.Func, left, right);
                expression = ExpressionExtensions.JoinExpressions(isFirst, searchAndor[0].Func, expression, ex);
                isFirst = false;
            }
            var finalExpression = Expression.Lambda<Func<TSource, bool>>(expression, pe);

            return finalExpression;
        }
        //private static Expression<Func<object, bool>> GetValidationExpression(Type type)
        //{
        //    //throw exception for non-nullable types (strings are nullable, but is a reference type and thus have to be called out separately)
        //    if (type != typeof(string) && !(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)))
        //        throw new Exception("Non-nullable types not supported.");

        //    //strings can't be blank, numbers can't be 0, and dates can't be minvalue
        //    if (type == typeof(string)) return t => !string.IsNullOrWhiteSpace((string)t);
        //    if (type == typeof(int?)) return t => t != null && (int)t >= 0;
        //    if (type == typeof(decimal?)) return t => t != null && (decimal)t >= decimal.Zero;
        //    if (type == typeof(DateTime?)) return t => t != null && (DateTime?)t != DateTime.MinValue;

        //    //everything else just can't be null
        //    return t => t != null;
        //}
        public static object GetPropertyValue(object T, string PropName)
        {
            return T.GetType().GetProperty(PropName) == null ? null : T.GetType().GetProperty(PropName).GetValue(T, null);
        }

        public IEnumerable<ReportWith2Col> GetAmar(Expression<Func<TEntity, bool>> where, string select, LanguageType languageType
            , int Doctor_id, Report_Type report_Type)// where TProperty : class
        {
            List<ReportWith2Col> f = new List<ReportWith2Col>();
            var all = Entities.Where(where).Select(Helper.DynamicSelectGenerator<TEntity>(select));//.ToList();
            if (select.Contains(","))
            {
                var s = select.Split(",");
                foreach (var item in s)
                {
                    var att = AttributeTefactor(item);
                    if (att.fieldType == FieldType.Integerr)
                    {
                        f = all.Where(x => x.GetType().GetProperty(item).GetValue(x) != null)
                          .GroupBy(x => new { typ = x.GetType().GetProperty(item).GetValue(x).ToString() })
                          .Select(y => new ReportWith2Col()
                          {
                              Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA, // y.Key.typ,//.GetDisplayName()
                              Col2 = (((float)all.Sum(x => (int?)x.GetType().GetProperty(item).GetValue(x)) / all.Count())).ToString()
                          }).ToList();
                    }
                }
            }
            else
            {
                var att = AttributeTefactor(select);
                if (att.fieldType == FieldType.Integerr)
                {
                    f = all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                      .GroupBy(x => new { typ = x.GetType().GetProperty(select).GetValue(x).ToString() })
                      .Select(y => new ReportWith2Col()
                      {
                          Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA, // y.Key.typ,//.GetDisplayName()
                          Col2 = (((float)all.Sum(x => (int?)x.GetType().GetProperty(select).GetValue(x)) / all.Count())).ToString()
                      }).ToList();
                }
            }
            return f;
        }
        public ExcelWorksheet GetExcel(List<TEntity> data, ExcelWorksheet ws, LanguageType lang)// where TProperty : class
        {
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            string name = "";
            int row = 3;
            for (int i = 0; i < props.Length; i++)// (PropertyInfo prop in props)
            {
                object[] attrs = props[i].GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    InfoAttribute authAttr = attr as InfoAttribute;
                    if (authAttr != null)
                    {
                        if (!string.IsNullOrEmpty(authAttr.DisplayName_EN) && lang == LanguageType.EN)
                            name = authAttr.DisplayName_EN;
                        else if (!string.IsNullOrEmpty(authAttr.DisplayName_FA) && lang == LanguageType.FA)
                            name = authAttr.DisplayName_FA;
                    }
                }
                ws.Cells[row, i + 1].Value = name;
            }
            row = 4;
            for (int i = 0; i < data.Count; i++)// rowitem in data)
            {
                var allitem = data[i].GetType().GetProperties();
                for (int j = 0; j < allitem.Length; j++)
                {
                    var att = AttributeTefactor(allitem[j].Name);
                    if (att.fieldType == FieldType.Strings || att.fieldType == FieldType.Integerr || att.fieldType == FieldType.Double || att.fieldType == FieldType.Float || att.fieldType == FieldType.Long || att.fieldType == FieldType.Bool)
                    {
                        ws.Cells[i + row, (j + 1)].Value = GetPropertyValue(data[i], allitem[j].Name) ?? "";
                    }
                    else if (att.fieldType == FieldType.Enum)
                    {
                        ws.Cells[i + row, (j + 1)].Value = GetPropertyValue(data[i], allitem[j].Name) != null ? GetPropertyValue(data[i], allitem[j].Name).ToString().GetDisplayName(att.Enum) : "";
                    }
                    //else if (att.fieldType == FieldType.Multiple)
                    //{

                    //}
                    else if (att.fieldType == FieldType.DateTime)
                    {
                        DateTime d;
                        var dt = GetPropertyValue(data[i], allitem[j].Name);
                        if (dt != null && DateTime.TryParse(dt.ToString(), out d))
                        {
                            if (lang == LanguageType.FA)
                                ws.Cells[i + row, (j + 1)].Value = DateAndTime.ConvertToLongPersian((DateTime)dt);
                            else
                                ws.Cells[i + row, (j + 1)].Value = dt.ToString();
                        }
                    }
                }
            }
            return ws;
        }
        public StringBuilder GetWord(List<TEntity> data, LanguageType lang)// where TProperty : class
        {
            StringBuilder sbDocumentBody = new StringBuilder();
            sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            string name = "";
            sbDocumentBody.Append("<tr><td>");
            sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

            //    // Add Column Headers dynamically from datatable  
            sbDocumentBody.Append("<tr>");
            for (int i = 0; i < props.Length; i++)// (PropertyInfo prop in props)
            {
                if (!props[i].Name.Contains("_FA")/* && props[i].GetType() == typeof(DateTime)*/)
                {
                    object[] attrs = props[i].GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        InfoAttribute authAttr = attr as InfoAttribute;
                        if (authAttr != null)
                        {
                            if (!string.IsNullOrEmpty(authAttr.DisplayName_EN) && lang == LanguageType.EN)
                                name = authAttr.DisplayName_EN;
                            else if (!string.IsNullOrEmpty(authAttr.DisplayName_FA) && lang == LanguageType.FA)
                                name = authAttr.DisplayName_FA;
                        }
                        sbDocumentBody.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">"
                            + name + "</td>");
                        //ws.Cells[row, i + 1].Value = name;
                    }
                }
            }
            sbDocumentBody.Append("</tr>");

            //    // Add Data Rows dynamically from datatable  
            for (int i = 0; i < data.Count; i++)// rowitem in data)
            {
                sbDocumentBody.Append("<tr>");
                var allitem = data[i].GetType().GetProperties();
                for (int j = 0; j < allitem.Length; j++)
                {
                    if (!allitem[j].Name.Contains("_FA")/* && props[i].GetType() == typeof(DateTime)*/)
                    {
                        var att = AttributeTefactor(allitem[j].Name);
                        if (att.fieldType == FieldType.Strings || att.fieldType == FieldType.Integerr || att.fieldType == FieldType.Double || att.fieldType == FieldType.Float || att.fieldType == FieldType.Long || att.fieldType == FieldType.Bool)
                            sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + GetPropertyValue(data[i], allitem[j].Name) ?? "" + "</td>");
                        else if (att.fieldType == FieldType.Enum)
                            sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + GetPropertyValue(data[i], allitem[j].Name) != null ? GetPropertyValue(data[i], allitem[j].Name).ToString().GetDisplayName(att.Enum) : "" + "</td>");
                        else if (att.fieldType == FieldType.DateTime)
                        {
                            DateTime d;
                            var dt = GetPropertyValue(data[i], allitem[j].Name);
                            if (dt != null && DateTime.TryParse(dt.ToString(), out d))
                            {
                                if (lang == LanguageType.FA)
                                    sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + DateAndTime.ConvertToLongPersian((DateTime)dt) + "</td>");
                                else
                                    sbDocumentBody.Append("<td class=\"Content\"style=\"border: 1px solid gray;\">" + dt.ToString() + "</td>");
                            }
                        }
                        //else if (att.fieldType == FieldType.Multiple)
                        //{

                        //}
                    }
                }
                sbDocumentBody.Append("</tr>");
            }
            sbDocumentBody.Append("</table>");
            sbDocumentBody.Append("</td></tr></table>");
            return sbDocumentBody;
        }
        public PdfPTable GetPDF(List<TEntity> data, LanguageType lang, string rootPath)// where TProperty : class
        {
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            //Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
            //Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);
          
            //var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\assets\\fonts\\yekan-400.ttf";
            var fontPath = rootPath + @"\assets\fonts\tahoma.ttf";
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var matnFont = new Font(baseFont, 8, Font.NORMAL, BaseColor.BLACK);
            var HeaderFont = new Font(baseFont, 10, Font.BOLD, BaseColor.BLACK);

            var count = props.Count(x => !x.Name.Contains("_FA"));
            count--;
            List<float> colcount = new List<float>();
            for (int i = 0; i < count; i++)
                colcount.Add(2F);

            //float[] columnDefinitionSize = { 2F, 2F, 2F, 2F, 2F, 2F };
            PdfPTable table;
            PdfPCell cell;
            table = new PdfPTable(colcount.ToArray())
            {
                WidthPercentage = 100
            };
            cell = new PdfPCell
            {
                BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
            };
            string name = "";
            for (int i = 0; i < props.Length; i++)// (PropertyInfo prop in props)
            {
                if (!props[i].Name.Contains("_FA")/* && props[i].GetType() == typeof(DateTime)*/)
                {
                    object[] attrs = props[i].GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        InfoAttribute authAttr = attr as InfoAttribute;
                        if (authAttr != null)
                        {
                            if (!string.IsNullOrEmpty(authAttr.DisplayName_EN) && lang == LanguageType.EN)
                                name = authAttr.DisplayName_EN;
                            else if (!string.IsNullOrEmpty(authAttr.DisplayName_FA) && lang == LanguageType.FA)
                                name = authAttr.DisplayName_FA;
                        }
                        PdfPCell cell1 = new PdfPCell(new Phrase(name, HeaderFont)) { RunDirection = PdfWriter.RUN_DIRECTION_RTL };
                        table.AddCell(cell1);
                    }
                    //ws.Cells[row, i + 1].Value = name;
                }
            }
            //table.HeaderRows = 1;

            for (int i = 0; i < data.Count; i++)// rowitem in data)
            {
                var allitem = data[i].GetType().GetProperties();
                for (int j = 0; j < allitem.Length; j++)
                {
                    if (!allitem[j].Name.Contains("_FA")/* && props[i].GetType() == typeof(DateTime)*/)
                    {
                        var att = AttributeTefactor(allitem[j].Name);
                        if (att.fieldType == FieldType.Strings || att.fieldType == FieldType.Integerr || att.fieldType == FieldType.Double || att.fieldType == FieldType.Float || att.fieldType == FieldType.Long || att.fieldType == FieldType.Bool)
                        {
                            PdfPCell cell1 = new PdfPCell(new Phrase(GetPropertyValue(data[i], allitem[j].Name).ToString() ?? "", matnFont)) { RunDirection = PdfWriter.RUN_DIRECTION_RTL };
                            table.AddCell(cell1);
                        }
                        else if (att.fieldType == FieldType.Enum)
                        {
                            PdfPCell cell1 = new PdfPCell(new Phrase(GetPropertyValue(data[i], allitem[j].Name) != null ? GetPropertyValue(data[i], allitem[j].Name).ToString().GetDisplayName(att.Enum) : "", matnFont)) { RunDirection = PdfWriter.RUN_DIRECTION_RTL };
                            table.AddCell(cell1);
                        }
                        else if (att.fieldType == FieldType.DateTime)
                        {
                            DateTime d;
                            var dt = GetPropertyValue(data[i], allitem[j].Name);
                            if (dt != null && DateTime.TryParse(dt.ToString(), out d))
                            {
                                if (lang == LanguageType.FA)
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase(DateAndTime.ConvertToLongPersian((DateTime)dt) ?? "", matnFont)) { RunDirection = PdfWriter.RUN_DIRECTION_RTL };
                                    table.AddCell(cell1);
                                }
                                else
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase(dt.ToString(), matnFont)) { RunDirection = PdfWriter.RUN_DIRECTION_RTL };
                                    table.AddCell(cell1);
                                }
                            }
                        }
                        //else if (att.fieldType == FieldType.Multiple)
                        //{

                        //}
                    }
                }
            }
            //foreach (var rec in data.Datas)
            //{
            //    table.AddCell(new Phrase(rec.Id.ToString(), font2));
            //    table.AddCell(new Phrase(rec.Release.Value.ToString(), font2));
            //    table.AddCell(new Phrase(rec.PatientCount.ToString(), font2));
            //    table.AddCell(new Phrase(rec.StartDate_FA.Value.ToString(), font2));
            //}
            return table;
        }
        public IEnumerable<ChartWith3ColString> GetChart(Expression<Func<TEntity, bool>> where, string select, LanguageType languageType
        , int Doctor_id, DateGroupBy_Type? dateGroupBy, Report_ComputType? report_computetype/*, string CustomIntigerDateField*/)// where TProperty : class
        {
            List<ChartWith3ColString> f = new List<ChartWith3ColString>();
            var all = Entities.Where(where).Select(Helper.DynamicSelectGenerator<TEntity>(select));//.ToList();
            if (select.Contains(","))
            {
                var s = select.Split(",");
                foreach (var item in s)
                {
                    var att = AttributeTefactor(item);
                    if (att.fieldType == FieldType.Integerr)
                    {
                        if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Avg)
                        {
                            f.Add(new ChartWith3ColString()
                            {
                                Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                                Col2 = (((float)all.Sum(x => (int?)x.GetType().GetProperty(item).GetValue(x)) / all.Count())).ToString(),
                                ChartType = "line"
                            });
                        }
                        else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Sum)
                        {
                            f.Add(new ChartWith3ColString()
                            {
                                Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                                Col2 = (all.Sum(x => (int?)x.GetType().GetProperty(item).GetValue(x))).ToString(),
                                ChartType = "line"
                            });
                        }
                        else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Count)
                        {
                            f.Add(new ChartWith3ColString()
                            {
                                Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                                Col2 = (all.Count(x => x.GetType().GetProperty(item).GetValue(x) != null)).ToString(),
                                ChartType = "line"
                            });
                        }
                        else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Percent)
                        {
                            f.Add(new ChartWith3ColString()
                            {
                                Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                                Col2 = (all.Count(x => x.GetType().GetProperty(item).GetValue(x) != null) * 100 / all.Count()).ToString(),
                                ChartType = "line"
                            });
                        }
                    }
                    else if (att.fieldType == FieldType.Enum)
                    {
                        if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Percent)
                        {
                            f.AddRange(all.Where(x => x.GetType().GetProperty(item).GetValue(x) != null)
                          .GroupBy(x => new { typ = x.GetType().GetProperty(item).GetValue(x).ToString() })
                          .Select(y => new ChartWith3ColString()
                          {
                              Col1 = y.Key.typ.GetDisplayName(att.Enum),//.GetDisplayName()
                              Col2 = (y.Count() * 100 / all.Count()).ToString(),
                              ChartType = "percentage"
                          }).ToList());
                        }
                        else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Avg)
                        {
                            f.AddRange(all.Where(x => x.GetType().GetProperty(item).GetValue(x) != null)
                          .GroupBy(x => new { typ = x.GetType().GetProperty(item).GetValue(x).ToString() })
                          .Select(y => new ChartWith3ColString()
                          {
                              Col1 = y.Key.typ.GetDisplayName(att.Enum),//.GetDisplayName()
                              Col2 = (y.Sum(x => y.Count()) / all.Count()).ToString(),
                              ChartType = "percentage"
                          }).ToList());
                        }
                        else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Count)
                        {
                            f.AddRange(all.Where(x => x.GetType().GetProperty(item).GetValue(x) != null)
                          .GroupBy(x => new { typ = x.GetType().GetProperty(item).GetValue(x).ToString() })
                          .Select(y => new ChartWith3ColString()
                          {
                              Col1 = y.Key.typ.GetDisplayName(att.Enum),//.GetDisplayName()
                              Col2 = (y.Count()).ToString(),
                              ChartType = "percentage"
                          }).ToList());
                        }
                    }
                    else if (att.fieldType == FieldType.DateTime)
                    {
                        if (dateGroupBy.Value == DateGroupBy_Type.Year)
                        {
                            f.AddRange(all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                              .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(select).GetValue(x)).Year })
                              .Select(y => new ChartWith3ColString()
                              {
                                  Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                                  Col2 = (y.Count() * 100 / all.Count()).ToString(),
                                  ChartType = "date",
                                  //Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString(),
                                  //Date = y.Min(k => ((DateTime)k.GetType().GetProperty(select).GetValue(k))),
                                  Col6 = "en"
                              }).ToList());
                        }
                        else if (dateGroupBy.Value == DateGroupBy_Type.Month)
                        {
                            f.AddRange(all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                              .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(select).GetValue(x)).Month })
                              .Select(y => new ChartWith3ColString()
                              {
                                  Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                                  Col2 = (y.Count() * 100 / all.Count()).ToString(),
                                  ChartType = "date",
                                  //Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString()
                                  //Date = y.Min(k => ((DateTime)k.GetType().GetProperty(select).GetValue(k))),
                                  Col6 = "en"
                              }).ToList());
                        }
                        else if (dateGroupBy.Value == DateGroupBy_Type.Day)
                        {
                            f.AddRange(all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                              .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(select).GetValue(x))/*.Day*/ })
                              .Select(y => new ChartWith3ColString()
                              {
                                  Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                                  Col2 = (y.Count() * 100 / all.Count()).ToString(),
                                  ChartType = "date",
                                  //Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString()
                                  //Date = y.Min(k => ((DateTime)k.GetType().GetProperty(select).GetValue(k))),
                                  Col6 = "en"
                              }).ToList());
                        }
                    }
                }
            }
            else
            {
                var att = AttributeTefactor(select);
                if (att.fieldType == FieldType.Integerr)
                {
                    // سفارشی
                    //if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Custome)
                    //{
                    //    f.AddRange(all.Where(x => x.GetType().GetProperty(CustomIntigerDateField).GetValue(x) != null)
                    //     .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(CustomIntigerDateField).GetValue(x)).Month })
                    //     .Select(y => new ChartWith3ColString()
                    //     {
                    //         Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                    //         Col2 = (y.Sum(x=>x.)).ToString(),
                    //         ChartType = "date",
                    //         Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString()
                    //     }).ToList());
                    //}
                    // سفارشی
                    if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Avg)
                    {
                        f.Add(new ChartWith3ColString()
                        {
                            Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                            Col2 = (((float)all.Sum(x => (int?)x.GetType().GetProperty(select).GetValue(x)) / all.Count())).ToString(),
                            ChartType = "line"
                        });
                    }
                    else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Sum)
                    {
                        f.Add(new ChartWith3ColString()
                        {
                            Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                            Col2 = (all.Sum(x => (int?)x.GetType().GetProperty(select).GetValue(x))).ToString(),
                            ChartType = "line"
                        });
                    }
                    else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Count)
                    {
                        f.Add(new ChartWith3ColString()
                        {
                            Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                            Col2 = (all.Count(x => x.GetType().GetProperty(select).GetValue(x) != null)).ToString(),
                            ChartType = "line"
                        });
                    }
                    else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Percent)
                    {
                        f.Add(new ChartWith3ColString()
                        {
                            Col1 = languageType == LanguageType.EN ? att.DisplayName_EN : att.DisplayName_FA,
                            Col2 = (all.Count(x => x.GetType().GetProperty(select).GetValue(x) != null) * 100 / all.Count()).ToString(),
                            ChartType = "line"
                        });
                    }
                }
                else if (att.fieldType == FieldType.Enum)
                {
                    if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Percent)
                    {
                        f = all//.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                      .GroupBy(x => new { typ = x.GetType().GetProperty(select).GetValue(x).ToString() })
                      .Select(y => new ChartWith3ColString()
                      {
                          Col1 = y.Key.typ.GetDisplayName(att.Enum),
                          Col2 = (y.Count() * 100 / all.Count()).ToString(),
                          ChartType = "percentage"
                      }).ToList();
                    }
                    else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Avg)
                    {
                        f = all//.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                      .GroupBy(x => new { typ = x.GetType().GetProperty(select).GetValue(x).ToString() })
                      .Select(y => new ChartWith3ColString()
                      {
                          Col1 = y.Key.typ.GetDisplayName(att.Enum),
                          Col2 = (y.Sum(x=>y.Count()) / all.Count()).ToString(),
                          ChartType = "percentage"
                      }).ToList();
                    }
                    else if (report_computetype.HasValue && report_computetype.Value == Report_ComputType.Count)
                    {
                        f = all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                      .GroupBy(x => new { typ = x.GetType().GetProperty(select).GetValue(x).ToString() })
                      .Select(y => new ChartWith3ColString()
                      {
                          Col1 = y.Key.typ.GetDisplayName(att.Enum),
                          Col2 = (y.Count()).ToString(),
                          ChartType = "percentage"
                      }).ToList();
                    }
                }
                else if (att.fieldType == FieldType.DateTime)
                {
                    if (dateGroupBy.Value == DateGroupBy_Type.Year)
                    {
                        f.AddRange(all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                          .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(select).GetValue(x)).Year })
                          .Select(y => new ChartWith3ColString()
                          {
                              Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                              Col2 = (y.Count() * 100 / all.Count()).ToString(),
                              ChartType = "date",
                              //Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString()
                              //Date = y.Min(k => ((DateTime)k.GetType().GetProperty(select).GetValue(k))),
                              Col6 = "en"
                          }).ToList());
                    }
                    else if (dateGroupBy.Value == DateGroupBy_Type.Month)
                    {
                        f.AddRange(all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                          .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(select).GetValue(x)).Month })
                          .Select(y => new ChartWith3ColString()
                          {
                              Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                              Col2 = (y.Count() * 100 / all.Count()).ToString(),
                              ChartType = "date",
                              //Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString()
                              //Date = y.Min(k => ((DateTime)k.GetType().GetProperty(select).GetValue(k))),
                              Col6 = "en"
                          }).ToList());
                    }
                    else if (dateGroupBy.Value == DateGroupBy_Type.Day)
                    {
                        f.AddRange(all.Where(x => x.GetType().GetProperty(select).GetValue(x) != null)
                          .GroupBy(x => new { typ = ((DateTime)x.GetType().GetProperty(select).GetValue(x))/*.Day*/ })
                          .Select(y => new ChartWith3ColString()
                          {
                              Col1 = y.Key.typ.ToString(),//.GetDisplayName(),//EnumSelector.GetEnum_WithText_Display(First_Fields.Item4, y.Key.typ),
                              Col2 = (y.Count() * 100 / all.Count()).ToString(),
                              ChartType = "date",
                              //Col3 = all.Select(x => x.GetType().GetProperty(select).GetValue(x)).FirstOrDefault().ToString()
                              //Date = y.Min(k => ((DateTime)k.GetType().GetProperty(select).GetValue(k))),
                              Col6 = "en"
                          }).ToList());
                    }
                }
            }
            return f;
        }
        public InfoAttribute AttributeTefactor(string FieldName)
        {
            PropertyInfo[] props = typeof(TEntity/*Entities.Demographic*/).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    InfoAttribute authAttr = attr as InfoAttribute;
                    if (authAttr != null && prop.Name.Trim().Equals(FieldName))
                    {
                        return authAttr;
                        //string propName = prop.Name;
                        //test += authAttr.fieldType;
                        //var fieldType = authAttr.fieldType;
                        //if (authAttr.Enum != null)
                        //    test += authAttr.Enum.FullName;
                        //var theEnum = authAttr.Enum;
                        //if (!string.IsNullOrEmpty(authAttr.DisplayName))
                        //    test += authAttr.DisplayName;

                        //var DisplayName = authAttr.DisplayName;
                    }
                }
            }
            return new InfoAttribute(FieldType.Bool, null, "", "");// test;
        }
        #region Explicit Loading
        // برای واکشی فیلدای Collection
        // یعنی جداولی که بصورت چندگانه مرتبط شدن رو واکشی کن
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            // اگر از نوع AsNoTracking
            // بود اول باید Attach کنیم
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
        // برای واکشی فیلدای Collection نیستند
        // یعنی جداولی که بصورت یکانه مرتبط شدن رو واکشی کن
        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion
    }
}

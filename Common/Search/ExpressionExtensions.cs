﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Common.Search
{
    public static class ExpressionExtensions
    {
        public static List<T> CreateQuery<T>(Expression whereCallExpression, IQueryable entities)
        {
            return entities.Provider.CreateQuery<T>(whereCallExpression).ToList();
        }

        public static MethodCallExpression CreateWhereCall<T>(Expression condition, ParameterExpression pe, IQueryable entities
            , Type entitytype)
        {
            var whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { /*entities.ElementType*/entitytype },
                entities.Expression,
                Expression.Lambda<Func<T, bool>>(condition, new[] { pe }));
            return whereCallExpression;
        }

        public static void CreateLeftAndRightExpression<T>(string propertyName, Type type, InfoAttribute att, string searchValue,
            ParameterExpression pe, out Expression left, out Expression right)
        {
            var typeOfNullable = type;
            typeOfNullable = typeOfNullable.IsNullableType() ? typeOfNullable.GetTypeOfNullable() : typeOfNullable;
            left = null;


            var typeMethodInfos = typeOfNullable.GetMethods();
            var parseMethodInfo = typeMethodInfos.FirstOrDefault(a => a.Name == "Parse" && a.GetParameters().Count() == 1);

            var propertyInfos = typeof(T).GetProperties();
            if (propertyName.Contains("."))
            {
                left = CreateComplexTypeExpression(propertyName, propertyInfos, pe);
            }
            else
            {
                var propertyInfo = propertyInfos.FirstOrDefault(a => a.Name == propertyName);
                if (propertyInfo != null)
                {
                    left = Expression.Property(pe, propertyInfo);
                }
            }

            if (left != null) left = Expression.Convert(left, typeOfNullable);

            if (parseMethodInfo != null)
            {
                // Error :Exception has been thrown by the target of an invocation.
                var invoke = parseMethodInfo.Invoke(searchValue, new object[] { searchValue });
                right = Expression.Constant(invoke, typeOfNullable);


            }
            else
            {
                if(att.fieldType == FieldType.DateTime)
                {
                    var sd = (DateTime)(object)searchValue;
                    right = Expression.Constant(new DateTime(sd.Year, sd.Month, sd.Day, 0, 0, 0), typeof(DateTime));

                    // گرفتن تاریخ بدون زمان
                    left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
                    var methodInfo = typeof(DbFunctions).GetMethod("TruncateTime", new[] { left.Type });
                    left = Expression.Call(methodInfo, left);
                }
                else if (att.fieldType == FieldType.Enum)
                {
                    right = Expression.Constant(Enum.ToObject(att.Enum, Enum.Parse(att.Enum, searchValue)));
                }
                else
                {
                    //type is string
                    right = Expression.Constant(searchValue.ToLower());
                    var methods = typeof(string).GetMethods();
                    var firstOrDefault = methods.FirstOrDefault(a => a.Name == "ToLower" && !a.GetParameters().Any());
                    if (firstOrDefault != null)
                        left = Expression.Call(left, firstOrDefault);
                }
            }
        }

        public static Expression CreateComplexTypeExpression(string searchFilter, IEnumerable<PropertyInfo> propertyInfos, Expression pe)
        {
            Expression ex = null;
            var infos = searchFilter.Split('.');
            var enumerable = propertyInfos.ToList();
            for (var index = 0; index < infos.Length - 1; index++)
            {
                var propertyInfo = infos[index];
                var nextPropertyInfo = infos[index + 1];
                if (propertyInfos == null) continue;
                var propertyInfo2 = enumerable.FirstOrDefault(a => a.Name == propertyInfo);
                if (propertyInfo2 == null) continue;
                var val = Expression.Property(pe, propertyInfo2);
                var propertyInfos3 = propertyInfo2.PropertyType.GetProperties();
                var propertyInfo3 = propertyInfos3.FirstOrDefault(a => a.Name == nextPropertyInfo);
                if (propertyInfo3 != null) ex = Expression.Property(val, propertyInfo3);
            }

            return ex;
        }
        public static Expression AddOperatorExpression(Func<Expression, Expression, Expression> func, Expression left, Expression right)
        {
            return func.Invoke(left, right);
        }
        public static Expression JoinExpressions(bool isFirst, Func<Expression, Expression, Expression> func, Expression expression, Expression ex)
        {
            if (!isFirst)
            {
                return func.Invoke(expression, ex);
            }

            expression = ex;
            return expression;
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

    }
}
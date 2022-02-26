using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GeneralFilter
{
    public class Filter<TEntity> where TEntity : IComparable<TEntity>
    {
        private Expression filterExpression;
        private static ParameterExpression parameter = Expression.Parameter(typeof(TEntity), nameof(parameter));

        private static List<EntityInfo> entitiesInfo = new List<EntityInfo>();

        public Filter()
        {
            Reset();
        }

        public void Reset()
        {
            filterExpression = Expression.Constant(true);
        }

        public IEnumerable<TEntity> ApplyFilter(IEnumerable<TEntity> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Func<TEntity, bool> selector = Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameter).Compile();
            return source.Where(selector);
        }

        private void And(Expression condition)
        {
            filterExpression = Expression.And(filterExpression, condition);
        }

        public void AndEqual<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            BinaryExpression equal = MakeOperation(Expression.Equal, propertyName, value);
            And(equal);
        }

        private static BinaryExpression MakeOperation<TProperty>(Func<Expression, Expression, BinaryExpression> operation,
                                                                 string propertyName, TProperty value)
        {
            ConstantExpression constant = GetConstantExpression(value);

            MemberExpression property;
            BinaryExpression operationExpression;
            if (!string.IsNullOrEmpty(propertyName))
            {
                property = GetPropertyExpression(propertyName);
                operationExpression = operation(property, constant);
            }
            else
            {
                operationExpression = operation(parameter, constant);
            }

            return operationExpression;
        }

        public void AndEqual(TEntity value)
        {
            BinaryExpression equal = MakeOperation(Expression.Equal, value);
            And(equal);
        }

        private static BinaryExpression MakeOperation(Func<Expression, Expression, BinaryExpression> operation, TEntity value)
        {
            return MakeOperation(operation, null, value);
        }

        private static MemberExpression GetPropertyExpression(string propertyName)
        {
            Type type = typeof(TEntity);
            if (parameter == null)
                parameter = Expression.Parameter(type, nameof(parameter));

            MemberInfo property = type.GetProperty(propertyName);
            return Expression.MakeMemberAccess(parameter, property);
        }

        private static ConstantExpression GetConstantExpression<TProperty>(TProperty value)
        {
            return Expression.Constant(value, typeof(TProperty));
        }

        public void AndGreaterThan<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            BinaryExpression greaterThan = GreaterThan(propertyName, value);
            And(greaterThan);
        }

        private static BinaryExpression GreaterThan<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TProperty));
            Expression compare = CallMethod(propertyName, method, value);

            return Expression.GreaterThan(compare, GetConstantExpression(0));
        }

        private static MethodInfo GetMethod(string methodName, Type type)
        {
            return type.GetMethod(methodName, new[] { type });
        }

        private static MethodCallExpression CallMethod<TProperty>(string propertyName, MethodInfo method, TProperty value)
        {
            ConstantExpression constant = GetConstantExpression(value);

            MemberExpression property;
            MethodCallExpression called;
            if (!string.IsNullOrEmpty(propertyName))
            {
                property = GetPropertyExpression(propertyName);
                called = Expression.Call(property, method, constant);
            }
            else
            {
                called = Expression.Call(parameter, method, constant);
            }

            return called;
        }

        public void AndGreaterThan(TEntity value)
        {
            BinaryExpression greaterThan = GreaterThan(value);
            And(greaterThan);
        }

        private static BinaryExpression GreaterThan(TEntity value)
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TEntity));
            Expression compare = CallMethod(method, value);

            return Expression.GreaterThan(compare, GetConstantExpression(0));
        }

        private static Expression CallMethod<TValue>(MethodInfo method, TValue value)
            where TValue : IComparable<TValue>
        {
            return CallMethod(null, method, value);
        }

        public void AndLessThan<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            BinaryExpression lessThan = LessThan(propertyName, value);
            And(lessThan);
        }

        private BinaryExpression LessThan<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TProperty));
            Expression compare = CallMethod(propertyName, method, value);

            return Expression.LessThan(compare, GetConstantExpression(0));
        }

        public void AndLessThan(TEntity value)
        {
            BinaryExpression lessThan = LessThan(value);
            And(lessThan);
        }

        private static BinaryExpression LessThan(TEntity value)
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TEntity));
            Expression compare = CallMethod(method, value);

            return Expression.LessThan(compare, GetConstantExpression(0));
        }

        public void AndGreaterThanOrEqual<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TProperty));
            Expression compare = CallMethod(propertyName, method, value);
            BinaryExpression greaterThanOrEqual = Expression.GreaterThanOrEqual(compare, Expression.Constant(0));

            And(greaterThanOrEqual);
        }

        public void AndGreaterThanOrEqual(TEntity value)
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TEntity));
            Expression compare = CallMethod(method, value);
            BinaryExpression greaterThanOrEqual = Expression.GreaterThanOrEqual(compare, Expression.Constant(0));

            And(greaterThanOrEqual);
        }

        public void AndLessThanOrEqual<TProperty>(string propertyName, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TProperty));
            Expression compare = CallMethod(propertyName, method, value);
            BinaryExpression lessThanOrEqual = Expression.LessThanOrEqual(compare, Expression.Constant(0));

            And(lessThanOrEqual);
        }

        public void AndLessThanOrEqual(TEntity value)
        {
            MethodInfo method = GetMethod(nameof(value.CompareTo), typeof(TEntity));
            Expression compare = CallMethod(method, value);
            BinaryExpression lessThanOrEqual = Expression.LessThanOrEqual(compare, Expression.Constant(0));

            And(lessThanOrEqual);
        }

        public void AndBetween<TProperty>(string propertyName, TProperty least, TProperty greatest)
            where TProperty : IComparable<TProperty>
        {
            BinaryExpression lessThan = LessThan(propertyName, greatest);
            BinaryExpression greaterThan = GreaterThan(propertyName, least);
            BinaryExpression between = Expression.And(lessThan, greaterThan);

            And(between);
        }

        public void AndBetween(TEntity least, TEntity greatest)
        {
            BinaryExpression lessThan = LessThan(greatest);
            BinaryExpression greaterThan = GreaterThan(least);
            BinaryExpression between = Expression.And(lessThan, greaterThan);

            And(between);
        }

        [OnlyStringUsed]
        public void AndContains(string propertyName, string value)
        {
            MethodInfo method = GetMethod(nameof(propertyName.Contains), typeof(string));
            Expression contains = CallMethod(propertyName, method, value);

            And(contains);
        }

        [OnlyStringUsed]
        public void AndContains(string value)
        {
            MethodInfo method = GetMethod(nameof(value.Contains), typeof(string));
            Expression contains = CallMethod(method, value);

            And(contains);
        }

        [OnlyStringUsed]
        public void AndMatchesPattern(string propertyName, string pattern)
        {
            const int parametersCount = 2;
            MethodInfo method = GetMethod(nameof(Regex.Match), typeof(Regex), parametersCount, BindingFlags.Public | BindingFlags.Static);

            Expression match = CallMethod(method, propertyName, pattern);
            Expression success = Expression.Property(match, nameof(Group.Success));

            And(success);
        }

        private static MethodInfo GetMethod(string methodName, Type type, int parametersCount, BindingFlags flags)
        {
            MethodInfo[] methods = type.GetMethods(flags);
            return methods.Single(n => n.Name == methodName && n.GetParameters().Count() == parametersCount);
        }

        private static MethodCallExpression CallMethod<TProperty>(MethodInfo method, string propertyName, TProperty value)
        {
            ConstantExpression constant = GetConstantExpression(value);

            MemberExpression property;
            MethodCallExpression called;
            if (!string.IsNullOrEmpty(propertyName))
            {
                property = GetPropertyExpression(propertyName);
                called = Expression.Call(method, property, constant);
            }
            else
            {
                called = Expression.Call(method, parameter, constant);
            }


            return called;
        }

        [OnlyStringUsed]
        public void AndMatchesPattern(string pattern)
        {
            const int parametersCount = 2;
            MethodInfo method = GetMethod(nameof(Regex.Match), typeof(Regex), parametersCount, BindingFlags.Public | BindingFlags.Static);

            Expression match = CallMethod(pattern, method);
            Expression success = Expression.Property(match, nameof(Group.Success));

            And(success);
        }

        private static MethodCallExpression CallMethod(string value, MethodInfo method)
        {
            return CallMethod(method, null, value);
        }

        public IEnumerable<TEntity> ApplySort(IEnumerable<TEntity> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!entitiesInfo.Any())
                return source;

            IEnumerable<TEntity> sorted = source;
            foreach (EntityInfo entityInfo in entitiesInfo)
            {
                //!!!
                MethodCallExpression sortedExpression = CallSortedMethod(sorted, entityInfo);
                LambdaExpression sortedLambda = Expression.Lambda(sortedExpression);

                Func<IOrderedEnumerable<TEntity>> sort = (Func<IOrderedEnumerable<TEntity>>)sortedLambda.Compile();
                sorted = sort();
            }

            entitiesInfo.Clear();
            return sorted;
        }

        public void SortByAsc<TProperty>(string propertyName)
        {
            string methodName = entitiesInfo.Any() ? nameof(Enumerable.ThenBy) : nameof(Enumerable.OrderBy);

            entitiesInfo.Add(new EntityInfo(propertyName, typeof(TProperty), methodName));
        }

        private static MethodCallExpression CallSortedMethod(IEnumerable<TEntity> source, EntityInfo entityInfo)
        {
            ConstantExpression sourceExpression = Expression.Constant(source);
            MethodInfo method = GetSortedMethod(entityInfo.SortedMethodName, entityInfo.PropertyType);
            LambdaExpression selector = GetSelectorExpression(entityInfo.PropertyName);

            return Expression.Call(method, sourceExpression, selector);
        }

        private static MethodInfo GetSortedMethod(string methodName, Type propertyType = null)
        {
            const int parametersCount = 2;
            MethodInfo method = GetMethod(methodName, typeof(Enumerable), parametersCount, BindingFlags.Public | BindingFlags.Static); ;

            MethodInfo sortedMethod;
            if (propertyType != null)
                sortedMethod = method.MakeGenericMethod(typeof(TEntity), propertyType);
            else
                sortedMethod = method.MakeGenericMethod(typeof(TEntity));

            return sortedMethod;
        }

        private static LambdaExpression GetSelectorExpression(string propertyName = null)
        {
            LambdaExpression selector;
            if (!string.IsNullOrEmpty(propertyName))
            {
                Expression property = Expression.Property(parameter, propertyName);
                selector = Expression.Lambda(property, parameter);
            }
            else
            {
                selector = Expression.Lambda(parameter);
            }

            return selector;
        }

        public void SortByAsc(string propertyName, Type propertyType)
        {
            string methodName = entitiesInfo.Any() ? nameof(Enumerable.ThenBy) : nameof(Enumerable.OrderBy);

            entitiesInfo.Add(new EntityInfo(propertyName, propertyType, methodName));
        }

        public void SortByDesc<TProperty>(string propertyName)
        {
            string methodName = entitiesInfo.Any() ? nameof(Enumerable.ThenByDescending) : nameof(Enumerable.OrderByDescending);

            entitiesInfo.Add(new EntityInfo(propertyName, typeof(TProperty), methodName));
        }

        public void SortByDesc(string propertyName, Type propertyType)
        {
            string methodName = entitiesInfo.Any() ? nameof(Enumerable.ThenByDescending) : nameof(Enumerable.OrderByDescending);

            entitiesInfo.Add(new EntityInfo(propertyName, propertyType, methodName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.PagedList
{
    public static class QueryOrederBLL
    {
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;
            return o.ToString();
        }
        public static IQueryable<T> SetQueryableOrder<T>(this IQueryable<T> query, string sort, string order)
        {
            if (string.IsNullOrEmpty(sort))
                throw new Exception("必须指定排序字段!");

            PropertyInfo sortProperty = typeof(T).GetProperty(sort, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (sortProperty == null)
                throw new Exception("查询对象中不存在排序字段" + sort + "！");

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression body = param;
            if (Nullable.GetUnderlyingType(body.Type) != null)
                body = Expression.Property(body, "Value");
            body = Expression.MakeMemberAccess(body, sortProperty);
            LambdaExpression keySelectorLambda = Expression.Lambda(body, param);

            if (string.IsNullOrEmpty(order))
                order = "ASC";
            string queryMethod = order.ToUpper() == "DESC" ? "OrderByDescending" : "OrderBy";
            query = query.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), queryMethod,
                                                               new Type[] { typeof(T), body.Type },
                                                               query.Expression,
                                                               Expression.Quote(keySelectorLambda)));
            return query;
        }

        public class DGroupBy<T> : IGrouping<object[], T>
        {
            private List<T> _innerlist = new List<T>();

            private object[] _key;

            public DGroupBy(object[] key) { _key = key; }

            public object[] Key
            {
                get { return _key; }
            }

            public void Add(T value)
            {
                _innerlist.Add(value);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return this._innerlist.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this._innerlist.GetEnumerator();
            }
        }

        public static IEnumerable<IGrouping<object[], T>> DynamicGroupBy<T>(this IEnumerable<T> data, string[] keys)
        {
            List<DGroupBy<T>> list = new List<DGroupBy<T>>();
            foreach (var item in data.Select(x => new { 
                k = keys.Select(y => x.GetType().GetProperty(y, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(x, null)).ToArray(),
                v = x
            }))
            {
                DGroupBy<T> existing = list.SingleOrDefault(x => x.Key.Zip(item.k, (a, b) => a.Equals(b)).All(y => y));
                if (existing == null)
                {
                    existing = new DGroupBy<T>(item.k);
                    list.Add(existing);
                }
                existing.Add(item.v);
            }
            return list;
        }

        public static Expression<Func<T, bool>> GetLamda<T>(string value,string Property)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            MemberExpression member = Expression.PropertyOrField(param,Property);
            Expression body = Expression.Equal(member, Expression.Constant(value, member.Type));
            LambdaExpression keySelectorLambda = Expression.Lambda(body, param);
            Expression<Func<T, bool>> where = Expression.Lambda<Func<T, bool>>(body, param);
            return where;
        }
    }
}

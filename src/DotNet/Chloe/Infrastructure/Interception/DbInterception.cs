﻿using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chloe.Infrastructure.Interception
{
    public static class DbInterception
    {
        static List<IDbCommandInterceptor> _interceptors = new List<IDbCommandInterceptor>();
        static readonly object _lockObject = new object();
        public static void Add(IDbCommandInterceptor interceptor)
        {
            Utils.CheckNull(interceptor);

            lock (_lockObject)
            {
                var newList = _interceptors.ToList();
                newList.Add(interceptor);
                newList.TrimExcess();
                _interceptors = newList;
            }
        }
        public static void Remove(IDbCommandInterceptor interceptor)
        {
            Utils.CheckNull(interceptor);

            lock (_lockObject)
            {
                var newList = _interceptors.ToList();
                newList.Remove(interceptor);
                newList.TrimExcess();
                _interceptors = newList;
            }
        }

        public static IEnumerator<IDbCommandInterceptor> GetEnumerator()
        {
            return _interceptors.GetEnumerator();
        }
    }
}

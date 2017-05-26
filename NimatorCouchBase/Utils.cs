using System;
using System.Collections.Generic;
using System.Linq;

namespace NimatorCouchBase
{
    public static class Utils
    {
        public static string GetAllExceptionMessages(this Exception pException)
        {
            if (pException == null)
            {
                return string.Empty;
            }           
            var messages = pException.FromHierarchy(pEx => pEx.InnerException).Select(pEx => pEx.Message);
            var allExceptionMessages = string.Join(Environment.NewLine, messages);
            return allExceptionMessages;
        }

        private static IEnumerable<TSource> FromHierarchy<TSource>(this TSource pSource,Func<TSource, TSource> pNextItem, Func<TSource, bool> pCanContinue)
        {
            for (var current = pSource; pCanContinue(current); current = pNextItem(current))
            {
                yield return current;
            }
        }

        private static IEnumerable<TSource> FromHierarchy<TSource>(this TSource pSource,Func<TSource, TSource> pNextItem) where TSource : class
        {
            return FromHierarchy(pSource, pNextItem, pS => pS != null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ProperServices.Common.Extensions
{
    public static class Iterators
    {
        /// <summary>
        /// Converts an IEnumerable to an array.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="iterator"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> iterator, Converter<TInput, TOutput> converter)
        {
            List<TInput> list = new List<TInput>(iterator);
            return Array.ConvertAll<TInput, TOutput>(list.ToArray(), converter);
        }
    }
}

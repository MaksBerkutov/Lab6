using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Module.Logic
{
    class Task1
    {
        public static async Task<IEnumerable<T>> SortedAsync<T>(IEnumerable<T> input)
        {
            return await Task.Run(() =>
            {
                input.ToList().Sort();
                return input;
            });
           
        }
        public static async Task<IEnumerable<T>>ClearCopyItemAsync<T>(IEnumerable<T> input)
        {
            return await Task.Run(() =>
            { 
                return input.ToHashSet().ToList();
            });
        }
        public static async Task<int>FindItemAsync<T>(IEnumerable<T> input,T Find)
        {
            return await Task.Run(() =>
            {
                return input.ToList().FindIndex(p=>p.Equals(Find));
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cx.Compiler.Extensions
{

    public static class IEnumerableExtensions 
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => 
            self.Select((item, index) => (item, index));
    }
}
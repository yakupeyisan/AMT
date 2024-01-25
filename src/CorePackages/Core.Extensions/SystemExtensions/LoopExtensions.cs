using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.SystemExtensions
{
    public static class LoopExtensions
    {
        public static void ForEachTry<T>(this IList<T> list, Action<T> action,Action<Exception>? tryAction=null)
        {
            foreach (var item in list)
                try
                {
                    action(item);
                }
                catch (Exception e) {
                    if(tryAction!= null) tryAction(e);
                }
        }
    }
}

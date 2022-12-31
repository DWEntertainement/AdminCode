using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminCode.Services
{
    public static class ExtensionMethods
    {
        public static void Replace<T>(this List<T> List, T oldItem, T newItem)
        {
            var oldItemIndex = List.IndexOf(oldItem);
            List[oldItemIndex] = newItem;
            
        }
    }
}

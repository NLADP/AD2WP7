using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace AD2
{
    public static class ObservableExtensions
    {
        internal static void Merge<T>(this ObservableCollection<T> collection, T[] items)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            for (int i = 0; i < items.Length; i++)
            {
                if (collection.Contains(items[i])) continue;

                int k = i;
                Deployment.Current.Dispatcher.BeginInvoke(() => collection.Insert(k, items[k]));
            }
        }
    }
}

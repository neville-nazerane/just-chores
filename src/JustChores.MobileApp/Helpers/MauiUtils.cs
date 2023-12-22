using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Helpers
{
    public static class MauiUtils
    {

        public static T GetResource<T>(string key)
        {
            if (App.Current.Resources.TryGetValue(key, out var resource))
                return (T)resource;

            throw new KeyNotFoundException(key);
        }

    }
}

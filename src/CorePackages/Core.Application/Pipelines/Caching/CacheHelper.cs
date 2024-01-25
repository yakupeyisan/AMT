using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching
{
    internal static class CacheHelper
    {
        //lock
        private static readonly object _lock = new();
        private static Dictionary<string, Dictionary<string,CachableKeyParam>> _cacheKeys = new();
        public static void Add(CachableKeyParam cachableKey)
        {
            lock (_lock)
            {
                if (_cacheKeys.ContainsKey(cachableKey.Key) == false)
                {
                    _cacheKeys.Add(cachableKey.Key, new());
                }
                if (_cacheKeys[cachableKey.Key].ContainsKey(cachableKey.GetKey())) return;
                _cacheKeys[cachableKey.Key].Add(cachableKey.GetKey(), cachableKey);
            }
        }
        public static List<CachableKeyParam> GetKeys(string key)
        {
            lock (_lock)
            {
                if (_cacheKeys.ContainsKey(key))
                {
                    return _cacheKeys[key].Values.ToList();
                }
                return new();
            }
        }
        public static void Remove(CachableKeyParam cachableKey,string key)
        {
            lock (_lock)
            {
                if (_cacheKeys.ContainsKey(cachableKey.Key))
                {
                    _cacheKeys[cachableKey.Key].Remove(key);
                }
                if (_cacheKeys[cachableKey.Key].Count == 0)
                {
                    _cacheKeys.Remove(cachableKey.Key);
                }
            }
        }
    }
}

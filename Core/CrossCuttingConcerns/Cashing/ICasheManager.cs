using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Cashing
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key); 
        void Add(string key, object value, int duration);
        // cashe de varsa cashe de yoksa veritabanından getiririz

        bool IsAdd(string key); 
        void Remove(string key);
        void RemoveByPattern(string pattern);


    }
}

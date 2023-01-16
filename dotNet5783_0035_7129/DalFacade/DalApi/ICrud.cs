using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;

public interface ICrud <T> where T : struct
{
    
   int Add(T? t);
   
   bool Update( T? t);
   
   bool Delete(int id);
    
   T GetByID(int id);
    
   T? GetByCondition(Func<T?, bool>? func);
    
    IEnumerable<T?> GetAll(Func<T?,bool>? func=null);


}

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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
   int Add(T? t);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
   bool Update( T? t);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
   bool Delete(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
   T PrintByID(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="func"></param>
    /// <returns></returns>
   T? PrintByCondition(Func<T?, bool>? func);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<T?> PrintAll(Func<T?,bool>? func=null); 


}

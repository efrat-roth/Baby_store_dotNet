using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;

public interface ICrud <T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
   int Add(T t);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
   bool Update(ref T t);
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
    /// <returns></returns>
    IEnumerable<T> PrintAll(); 

}

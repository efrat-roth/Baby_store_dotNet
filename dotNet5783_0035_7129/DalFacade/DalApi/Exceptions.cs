using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;
[Serializable]
/// <summary>
/// Throwing the error problem. There is no id as requested.
/// </summary>
/// <returns></returns>
public class IdDoesNotExist : Exception
{
   public IdDoesNotExist(string message):base(message) { }
}
    /// <summary>
    /// Throwing the error problem. There is another is as requested.
    /// </summary>
    /// <returns></returns>
    public class IdAlreadyExist: Exception
{
    public  IdAlreadyExist(string message) : base(message) { }
}
/// <summary>
/// Throwing the error problem.The list is empty.
/// </summary>
/// <returns></returns>
public class ListIsEmpty : Exception
{
    public ListIsEmpty(string message) : base(message) { }
}
   


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;
[Serializable]

public class Exceptions: Exception
{
    /// <summary>
    /// Throwing the error problem. There is no id as requested.
    /// </summary>
    /// <returns></returns>
    public   Exceptions(string message):base(message)
    {
        Console.WriteLine ("There is no id as you asked for");
    }
    /// <summary>
    /// Throwing the error problem. There is another is as requested.
    /// </summary>
    /// <returns></returns>
    public Exceptions() : base()
    {
        
    }
    /// <summary>
    /// Throwing the error problem.The list is empty.
    /// </summary>
    /// <returns></returns>
    public Exceptions(string message,Exception inner) : base(message, inner)
    {
        Console.WriteLine("There is another id as you gave");

    }
    /// <summary>
    /// Throwing the error problem.The date is invalid.
    /// </summary>
    /// <returns></returns>
   

}

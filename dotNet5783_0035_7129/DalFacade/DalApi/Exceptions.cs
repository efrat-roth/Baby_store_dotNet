using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;

public class EXception
{
    /// <summary>
    /// Throwing the error problem. There is no id as requested.
    /// </summary>
    /// <returns></returns>
    public string throwNotId()
    {
        return "There is no id as you asked for";
    }
    /// <summary>
    /// Throwing the error problem. There is another is as requested.
    /// </summary>
    /// <returns></returns>
    public string throwMoretId()
    {
        return "There is another id as you gave";
    }
    /// <summary>
    /// Throwing the error problem.The list is empty.
    /// </summary>
    /// <returns></returns>
    public string throwNotList()
    {
        return "There is another id as you gave";

    }
    /// <summary>
    /// Throwing the error problem.The date is invalid.
    /// </summary>
    /// <returns></returns>
    public string throwNoDate()
    {
        return "The date is invalid";

    }

}

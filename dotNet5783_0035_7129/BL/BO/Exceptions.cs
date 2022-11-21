using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// Throwing an error problem. There is no ID as requested.
/// </summary>
[Serializable]
public class IdDoesNotExistException : Exception
{
    public IdDoesNotExistException(string message = "The item is not in the database") : base(message) { }
    
    public IdDoesNotExistException():base(){}  
    override public string ToString() => $@"
     The item is not in the database";
}

/// <summary>
/// Throwing the error problem. There is another is as requested.
/// </summary>
[Serializable]
public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string message = "The ID is exist already") : base(message) { }
    override public string ToString() => $@"
     The ID is exist already";

}
/// <summary>
/// Throwing the error problem.The list is empty.
/// </summary>
/// <returns></returns>
[Serializable]
public class ListIsEmptyException : Exception
{
    public ListIsEmptyException(string message = "The list is Empty") : base(message) { }
    override public string ToString() => $@"
     The list is Empty";

}
/// <summary>
/// Invalid variable input
/// </summary>
[Serializable]
public class InvalidVariableException : Exception
{
    public InvalidVariableException(string message = "The input is invalid") : base(message) { }
    override public string ToString() => $@"
     The input is invalid";
}
public class CanNotDOActionException : Exception
{
    public CanNotDOActionException(string message = "Can't do the action") : base(message) { }
    override public string ToString() => $@"
     Can't do the action";

}



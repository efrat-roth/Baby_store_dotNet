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
    public override string Message => "The item is not in the database";
    
    override public string ToString() => Message;
}

/// <summary>
/// Throwing the error problem. There is another is as requested.
/// </summary>
[Serializable]
public class IdAlreadyExistException : Exception
{
    public override string Message => "The ID is exist already";
    override public string ToString() => Message;

}
/// <summary>
/// Throwing the error problem.The list is empty.
/// </summary>
/// <returns></returns>
[Serializable]
public class ListIsEmptyException : Exception
{
    public override string Message => "The list is Empty";
    override public string ToString() => Message;


}
/// <summary>
/// Invalid variable input
/// </summary>
[Serializable]
public class InvalidVariableException : Exception
{
    public override string Message => "The input is invalid";   
    override public string ToString() => Message;

}
/// <summary>
/// The action invalid
/// </summary>
public class CanNotDOActionException : Exception
{
    public override string Message => "Can't do the action";
    override public string ToString() => Message;
}

/// <summary>
/// Throwing an error problem. There is a nullable object
/// </summary>
[Serializable]
public class ObgectNullableException : Exception
{
    public override string Message => "An item is nullable";

    override public string ToString() => Message;
}

/// <summary>
/// Catch inner of add exception
/// </summary>
public class FailedAdd : Exception
{
    public FailedAdd(Exception inner) : base("Add failed ", inner) { }
    override public string ToString() => @$"{Message} - {this.InnerException}";
}
/// <summary>
/// Catch inner of delete exception
/// </summary>
public class FailedDelete : Exception
{
    public FailedDelete(Exception inner) : base("Delete failed", inner) { }
    override public string ToString() => @$"{Message} - {this.InnerException}";
}
/// <summary>
/// Catch inner of get exception
/// </summary>
public class FailedGet : Exception
{
    public FailedGet(Exception inner) : base("Get failed", inner) { }
    override public string ToString() => @$"{Message} - {this.InnerException}";
}
/// <summary>
/// Catch inner of update exception
/// </summary>
public class FailedUpdate : Exception
{
    public FailedUpdate(Exception inner) : base("Update failed", inner) { }
    override public string ToString()=> @$"{Message} - {this.InnerException}";

}
/// <summary>
/// Catch inner of get exception
/// </summary>
public class FailedGetAll: Exception
{
    public FailedGetAll(Exception inner) : base("Get all failed", inner) { }
    override public string ToString() => @$"{Message} - {InnerException}";
}

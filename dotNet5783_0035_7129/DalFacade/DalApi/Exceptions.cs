using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;
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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class NodeDateStatus
{
    /// <summary>
    /// The date of creating order
    /// </summary>
    public DateTime? Date { get; set; }
    /// <summary>
    /// The status of Order
    /// </summary>
    public string? status { get; set; }
    /// <summary>
    /// print details of the class
    /// </summary>
    /// <returns></returns>string
    public override string ToString()
    {
        return Tools.ToStringProperty(this);
    }

}

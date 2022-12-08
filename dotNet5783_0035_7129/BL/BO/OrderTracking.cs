using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// The id of the order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// What is the status of the order
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// The list of status and created date of order
    /// </summary>
    public  IEnumerable<NodeDateStatus?>? ListDateStatus { get; set; }
    /// <summary>
    /// An information about status of order
    /// </summary>
    /// <returns></returns>string
    public override string ToString() => $@"
       Order ID={ID},
       Status of order: {Status}
       List of dates ans status: {string.Join('\n', ListDateStatus)},
";
}

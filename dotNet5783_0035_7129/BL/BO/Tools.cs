﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public static class Tools
{
    /// <summary>
    /// To string for all classes
    /// </summary>
    /// <typeparam name="T"></typeparam>Generic Variable
    /// <param name="t"></param>The item that its details are return
    /// <returns></returns>The details of t
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t!.GetType().GetProperties())
        {
            str += "\n" + item.Name+": ";
            if (item.GetValue(t,null)is IEnumerable<object>)
            {
                IEnumerable<object?>? list = (IEnumerable<object?>?)item.GetValue(t,null);
                string s=string.Join(" ", list??throw new ObgectNullableException());
                str += s;
            }
            else
            {
                str += item.GetValue(t, null);
            }

        }
        return str+"\n";
    }

}

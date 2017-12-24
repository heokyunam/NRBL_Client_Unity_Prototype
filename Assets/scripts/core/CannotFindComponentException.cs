using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CannotFindComponentException : Exception
{
    public CannotFindComponentException(string message) : base("component error : " + message)
    {

    }
}

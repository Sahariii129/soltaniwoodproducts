using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class MessageBox
{
    public static string Show(string Text, Location location, Type_me type, Modal modal)
    {
        return "MessageBox('" + Text + "', '" + type + "', '" + (modal.ToString() == "WithModal" ? true : false) + "','" + location + "'); ";
    }
}

public enum Location
{
    top,
    topCenter,
    topLeft,
    topRight,
    center,
    centerLeft,
    centerRight,
    bottom,
    bottomCenter,
    bottomLeft,
    bottomRight
}

public enum Type_me
{
    alert,
    information,
    error,
    warning,
    notification,
    success
}

public enum Modal
{
    WithModal = 1,
    WithoutModal = 0
}
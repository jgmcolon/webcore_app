using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using webcore_app.Core.Interfaces;

namespace webcore_app.Core.Common
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}

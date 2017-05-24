using System;
using System.Reflection;
using NUnitLite;

namespace test_chargeio
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args);
        }
    }
}

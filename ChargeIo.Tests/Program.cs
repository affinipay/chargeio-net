﻿using System;
using System.Reflection;
using NUnitLite;

namespace ChargeIO.Tests
{
    internal class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args);
        }
    }
}

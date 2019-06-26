using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Concurrent;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MyTargetForNLogConsoleApp
{
    [Target("mySecond")]
    class MySecondTarget:TargetWithLayout
    {
        public string fileName;


    [RequiredParameter]
        public string Host { get; set; }

        public MySecondTarget()
        {

        }
    }
}

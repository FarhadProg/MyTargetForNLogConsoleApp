using System;
using System.Threading;
using NLog;

namespace MyTargetForNLogConsoleApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                int numberOfThreads = 20;
                for (int i = 0; i < numberOfThreads; i++)
                    ThreadPool.QueueUserWorkItem(JobForAThread);
                Thread.Sleep(3000);
                Console.WriteLine("Логирование {0} потоков завершено", numberOfThreads);
                Console.ReadLine();
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
 
            }

        }

        static void JobForAThread(object state)
        {
            try
            {
                GenerateLog();
                Console.WriteLine("Выполнение внутри потока {0} из пула ", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("В потоке " + Thread.CurrentThread.ManagedThreadId.ToString() +
                    " произошла ошибка:" + ex.Message);
            }

        }

        static void GenerateLog()
        {
            try
            {

                Logger log = LogManager.GetCurrentClassLogger();
                log.Trace("trace message");
                log.Debug("debug message");
                log.Info("info message");
                log.Warn("warn message");
                log.Error("error message");
                log.Fatal("fatal message");
            }

            catch(Exception ex)
            {
                throw ex;

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MyTargetForNLogConsoleApp
{
    [Target("MyFirst")]
    public sealed class MyTarget : TargetWithContext
    {
        private static readonly string BASE_LOG_DIR_NAME;
        private static readonly string BASE_FILE_NAME;
        private static readonly string FILE_EXTENSHION;
        public string FullPath { get; private set; }
        public string ThreadId { get; private set; }
        public string ShortFileName { get; private set; }

        [RequiredParameter]
        public string fileName { get; private set; }

        public MyTarget()
        {
            try
            {
                IncludeEventProperties = true;
                ThreadId = "0";
                fileName = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("В конструктре класса произошла ошибка " + ex.Message);
            }
        }
        static MyTarget()
        {

            try
            {
                BASE_LOG_DIR_NAME = @"C:\LogsTMP\";
                BASE_FILE_NAME = "Log_";
                FILE_EXTENSHION = ".log";
                if (!Directory.Exists(BASE_LOG_DIR_NAME))
                {
                    Directory.CreateDirectory(BASE_LOG_DIR_NAME);
                }
            }
            catch (DriveNotFoundException ex)
            {
                Console.WriteLine("В статическом конструктре класса произошла ошибка " + ex.Message);

            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("В статическом конструктре класса произошла ошибка" + ex.Message);

            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine("В статическом конструктре класса произошла ошибка " + ex.Message);

            }
            catch (IOException ex)
            {
                Console.WriteLine("В статическом конструктре класса произошла ошибка " + ex.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("В статическом конструктре класса произошла ошибка " + ex.Message);
            }

        }


        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                string logMessage = RenderLogEvent(Layout, logEvent);
                IDictionary<string, object> logProperties = GetAllProperties(logEvent);
                ThreadId = logProperties["@threadId"].ToString();
                SetShortFileName();
                FullPath = Path.Combine(BASE_LOG_DIR_NAME, ShortFileName);
                using (StreamWriter sw = new StreamWriter(FullPath, true))
                {
                    sw.WriteLine(logMessage);

                }
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message);
            }
            catch (DriveNotFoundException ex)
            {
                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message);

            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message);

            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message);

            }
            catch (IOException ex)
            {
                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message);
            }

        }

        private void SetShortFileName()
        {
            ShortFileName = BASE_FILE_NAME + ThreadId + FILE_EXTENSHION;
        }

    }
}

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
        public string ThreadId { get; private set; }
        public string BaseLogDirName { get; private set; }
        public string ShortFileName { get; private set; }

        public string FileExtension { get; private set; }

        [RequiredParameter]
        public string fileName { get; private set; }

        public MyTarget()
        {
            this.IncludeEventProperties = true;
            this.ThreadId = "0";
            this.BaseLogDirName = @"c:\LogsTMP\";
            this.ShortFileName = "Log_";
            this.FileExtension = ".log";
            this.fileName = "";

        }


        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                string logMessage = this.RenderLogEvent(this.Layout, logEvent);
                IDictionary<string, object> logProperties = this.GetAllProperties(logEvent);
                WriteLogMessageToFile(logMessage, logProperties);
            }
            catch(Exception ex)
            {

                Console.WriteLine("При записи сообщения в лог произошла ошибка:" + ex.Message );
            }

        }

        private void WriteLogMessageToFile(string message, IDictionary<string, object> properties)
        {
            

            try
            {
                this.ThreadId = properties["@threadId"].ToString();
                this.fileName = this.BaseLogDirName + this.ShortFileName + this.ThreadId + this.FileExtension;
               
                if (!Directory.Exists(this.BaseLogDirName))
                {
                    Directory.CreateDirectory(this.BaseLogDirName);
                }

                using (StreamWriter sw = new StreamWriter(this.fileName, true))
                {
                    sw.WriteLine(message);

                }
            }
            catch (DriveNotFoundException ex)
            {
                throw ex;

            }
           // По идее DirectoryNotFoundException не нужно , так как мы если директории нет,
           //мы создаем её. 
            catch (DirectoryNotFoundException ex) 
            {
                throw ex;

            }
            catch(PathTooLongException ex)
            {
                throw ex;

            }
            catch(IOException ex)
            {
                throw ex;

            }
            catch(Exception ex)
            {
                throw ex;

            }


        }
    }
}

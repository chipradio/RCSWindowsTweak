using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsTweak
{
    class Logging
    {
        static public void LogErrorMessage(string strErrMsg)
        {
            FileStream stream = null;
            try
            {
                var logFile = Application.StartupPath;

                if (!logFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    logFile += Path.DirectorySeparatorChar;
                }

                logFile += @"Errors\\";

                if (!Directory.Exists(logFile))
                {
                    Directory.CreateDirectory(logFile);
                }

                logFile += string.Format("{0:yyyMMdd}errors.log", DateTime.Now);

                stream = new FileStream(logFile, System.IO.FileMode.Append);
                using (var writer = new StreamWriter(stream))
                {
                    stream = null;
                    string errorMsg = DateTime.Now.ToString("MM/dd/yy hh:mm:ss") + strErrMsg;
                    writer.WriteLine(errorMsg);
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        static public void LogDebugMessage(string strDebugMsg)
        {
            FileStream stream = null;
            try
            {
                var logFile = Application.StartupPath;

                if (!logFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    logFile += Path.DirectorySeparatorChar;
                }

                logFile += @"Debug\\";

                if (!Directory.Exists(logFile))
                {
                    Directory.CreateDirectory(logFile);
                }

                logFile += string.Format("{0:yyyMMdd}debug.log", DateTime.Now);

                stream = new FileStream(logFile, System.IO.FileMode.Append);
                using (var writer = new StreamWriter(stream))
                {
                    stream = null;
                    string errorMsg = DateTime.Now.ToString("MM/dd/yy hh:mm:ss") + strDebugMsg;
                    writer.WriteLine(errorMsg);
                }
            }
            catch (System.Exception ex)
            {
                LogErrorMessage(" Error LogDebugMessage.\r\nMessage: " + ex.Message + "\r\nInnerException: " + ex.InnerException);
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        static public void LogFileMessage(string strDebugMsg)
        {
            FileStream stream = null;
            try
            {
                var logFile = Application.StartupPath;

                if (!logFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    logFile += Path.DirectorySeparatorChar;
                }

                logFile += @"Debug\\";

                if (!Directory.Exists(logFile))
                {
                    Directory.CreateDirectory(logFile);
                }

                logFile += string.Format("{0:yyyMMdd}files.log", DateTime.Now);

                stream = new FileStream(logFile, System.IO.FileMode.Append);
                using (var writer = new StreamWriter(stream))
                {
                    stream = null;
                    string errorMsg = DateTime.Now.ToString("MM/dd/yy hh:mm:ss") + strDebugMsg;
                    writer.WriteLine(errorMsg);
                }
            }
            catch (System.Exception ex)
            {
                LogErrorMessage(" Error LogDebugMessage.\r\nMessage: " + ex.Message + "\r\nInnerException: " + ex.InnerException);
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        static public void LogSuccessMessage(string strErrMsg)
        {
            FileStream stream = null;
            try
            {
                var logFile = Application.StartupPath;

                if (!logFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    logFile += Path.DirectorySeparatorChar;
                }

                logFile += @"Success\\";

                if (!Directory.Exists(logFile))
                {
                    Directory.CreateDirectory(logFile);
                }

                logFile += string.Format("{0:yyyMMdd}success.log", DateTime.Now);

                stream = new FileStream(logFile, System.IO.FileMode.Append);
                using (var writer = new StreamWriter(stream))
                {
                    stream = null;
                    string errorMsg = DateTime.Now.ToString("MM/dd/yy hh:mm:ss") + strErrMsg;
                    writer.WriteLine(errorMsg);
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        static public void PerformCleanupOfOldFiles(int nDaysToKeep)
        {
            try
            {

                string debugFile = System.Windows.Forms.Application.StartupPath;
                if (!debugFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    debugFile += Path.DirectorySeparatorChar;
                }

                string[] logFiles = Directory.GetFiles(debugFile, "*.log");

                //Time to go through all the files
                foreach (string fileName in logFiles)
                {
                    var fiInfo = new FileInfo(fileName);

                    LogDebugMessage("Perform Cleanup, Checking " + fiInfo.Name + " with last write time of " + Convert.ToString(fiInfo.LastWriteTime));

                    if (fiInfo.LastWriteTime < (DateTime.Now - new TimeSpan(nDaysToKeep, 0, 0, 0)))
                    {
                        fiInfo.Delete();
                    }

                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex.Message);
            }

        }
    }
}

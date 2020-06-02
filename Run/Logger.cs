using System.IO;

namespace EvolutionSimulator.Run
{
    static class Logger
    {
        private static StreamWriter logFile;
        public static void OpenLogFile(string path)
        {
            logFile = new StreamWriter(path, false);
        }
        public static void Log(string logInfo)
        {
            logFile.WriteLine(logInfo);
        }
        public static void CloseLogFile()
        {
            logFile.Close();
        }
    }
}

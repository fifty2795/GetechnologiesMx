using Examen.API.Utilidades.Interfaces;
using Examen.API.Utilidades.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Examen.API.Utilidades.Utilidades
{
    public class LogService : ILogService
    {
        private readonly LogSettings _logSettings;

        public LogService(IOptions<LogSettings> logSettings)
        {
            _logSettings = logSettings.Value;
        }

        public void LogError(string message, Exception? ex = null)
        {
            Log("ERROR", message, ex);
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        private void Log(string level, string message, Exception? ex = null)
        {
            var logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            string logPath = _logSettings.LogFilePath + "\\" + _logSettings.LogFileName;

            if (ex != null)
            {
                logMessage += Environment.NewLine + $"Exception: {ex.Message}{Environment.NewLine}StackTrace: {ex.StackTrace}";
            }

            try
            {
                if (File.Exists(logPath) && new FileInfo(logPath).Length >= _logSettings.TamanioMaximo)
                {
                    RotateLogs();
                }

                File.AppendAllText(logPath, logMessage + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en el log: {e.Message}");
            }
        }

        private void RotateLogs()
        {
            try
            {
                string logPath = _logSettings.LogFilePath + "\\" + _logSettings.LogFileName;
                var backupFilePath = Path.Combine(_logSettings.LogFilePath, $"Log_{DateTime.UtcNow:yyyyMMddHHmmss}.txt");
                
                File.Move(logPath, backupFilePath);
                
                var files = Directory.GetFiles(_logSettings.LogFilePath, "Log_*.txt")
                      .OrderByDescending(f => File.GetCreationTime(f))
                      .Skip(_logSettings.MaximoArchivosLog)
                      .ToList();


                foreach (var file in files)
                {
                    File.Delete(file);
                }

                // Crear un nuevo archivo de log vacío
                File.Create(logPath).Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la rotación de logs: {e.Message}");
            }
        }
    }
}

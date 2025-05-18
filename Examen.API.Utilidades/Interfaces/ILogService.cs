﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Utilidades.Interfaces
{
    public interface ILogService
    {
        void LogError(string message, Exception? ex = null);

        void LogInfo(string message);

        void LogWarning(string message);

    }
}

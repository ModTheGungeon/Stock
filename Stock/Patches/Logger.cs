using System;
using MonoMod;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Stock.Patches {
    [MonoModPatch("global::UnityEngine.Logger")]
    public class LoggerPatch {
        public static ETGMod.Logger UnityLogger = new ETGMod.Logger("Unity");

        /// <summary>
        /// Converts a UnityEngine.LogType to an ETGMod.Logger.LogLevel
        /// </summary>
        /// <returns>The ETGMod LogLevel</returns>
        /// <param name="type">The Unity LogType</param>
        private ETGMod.Logger.LogLevel _LogTypeToLogLevel(LogType type) {
            switch (type) {
            case LogType.Log: return ETGMod.Logger.LogLevel.Info;
            case LogType.Assert: return ETGMod.Logger.LogLevel.Error;
            case LogType.Error: return ETGMod.Logger.LogLevel.Error;
            case LogType.Exception: return ETGMod.Logger.LogLevel.Error;
            case LogType.Warning: return ETGMod.Logger.LogLevel.Warn;
            default: return ETGMod.Logger.LogLevel.Debug;
            }
        }

        private string _FormatMessage(object message, string tag = null, Object context = null) {
            if (tag != null && context != null) return $"[tag: {tag}, context: {context}] {message}";
            if (tag != null && context == null) return $"[context: {context}] {message}";
            if (tag == null && context != null) return $"[tag: {tag}] {message}";
            return message.ToString();
        }

        public bool IsLogTypeAllowed(LogType logType) {
            return UnityLogger.LogLevelEnabled(_LogTypeToLogLevel(logType));
        }

        public void Log(string tag, object message, Object context) {
            UnityLogger.Info(_FormatMessage(message, tag: tag, context: context));
        }

        public void Log(string tag, object message) {
            UnityLogger.Info(_FormatMessage(message, tag: tag));
        }

        public void Log(object message) {
            UnityLogger.Info(_FormatMessage(message));
        }

        public void Log(LogType logType, string tag, object message, Object context) {
            UnityLogger.Log(_LogTypeToLogLevel(logType), _FormatMessage(message, tag: tag, context: context));
        }

        public void Log(LogType logType, string tag, object message) {
            UnityLogger.Log(_LogTypeToLogLevel(logType), _FormatMessage(message, tag: tag));
        }

        public void Log(LogType logType, object message, Object context) {
            UnityLogger.Log(_LogTypeToLogLevel(logType), _FormatMessage(message, context: context));
        }

        public void Log(LogType logType, object message) {
            UnityLogger.Log(_LogTypeToLogLevel(logType), _FormatMessage(message));
        }

        public void LogError(string tag, object message, Object context) {
            UnityLogger.Error(_FormatMessage(message, tag: tag, context: context));
        }

        public void LogError(string tag, object message) {
            UnityLogger.Error(_FormatMessage(message, tag: tag));
        }

        public void LogException(Exception exception) {
            UnityLogger.Error(_FormatMessage(exception));
        }

        public void LogException(Exception exception, Object context) {
            UnityLogger.Error(_FormatMessage(exception, context: context));
        }

        public void LogWarning(string tag, object message) {
            UnityLogger.Warn(_FormatMessage(message, tag: tag));
        }

        public void LogWarning(string tag, object message, Object context) {
            UnityLogger.Warn(_FormatMessage(message, tag: tag, context: context));
        }
    }
}
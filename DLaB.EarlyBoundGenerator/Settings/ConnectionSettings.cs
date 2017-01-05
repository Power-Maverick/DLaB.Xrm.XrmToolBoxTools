﻿using System;
using System.IO;
using McTools.Xrm.Connection;
using XrmToolBox.Extensibility;

namespace DLaB.EarlyBoundGenerator.Settings
{
    [Serializable]
    public class ConnectionSettings
    {
        public string SettingsPath { get; set; }

        public string FullSettingsPath => Path.IsPathRooted(SettingsPath) ? SettingsPath : Path.GetFullPath(Path.Combine(Paths.PluginsPath, SettingsPath));

        public string SettingsDirectoryName => Path.GetDirectoryName(FullSettingsPath);

        public void Save(ConnectionDetail connectionDetail)
        {
            SettingsManager.Instance.Save(typeof(EarlyBoundGeneratorPlugin), this, connectionDetail.ConnectionName);
        }

        public static ConnectionSettings GetDefault()
        {
            return new ConnectionSettings
            {
                SettingsPath = Path.GetFullPath(Path.Combine(Paths.PluginsPath, "DLaB.EarlyBoundGenerator.Settings.xml"))
            };
        }

        public static ConnectionSettings GetForConnection(ConnectionDetail connectionDetail)
        {
            ConnectionSettings localSettings;
            // ReSharper disable once UnusedVariable
            var loadedSuccessfully = SettingsManager.Instance.TryLoad(typeof(EarlyBoundGeneratorPlugin), out localSettings, connectionDetail?.ConnectionName) ||
                                     SettingsManager.Instance.TryLoad(typeof(EarlyBoundGeneratorPlugin), out localSettings);
            return localSettings ?? GetDefault();
        }
    }
}
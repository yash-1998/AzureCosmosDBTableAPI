using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableApiSamples
{
    using Microsoft.Extensions.Configuration;
    public class AppSettings
    {
        public String StorageConnectionString { get; set; }
        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("Settings.json").Build();
            AppSettings appSettings = configurationRoot.Get<AppSettings>();
            return appSettings;
        }
    }
}

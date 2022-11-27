﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typedown.Universal.Interfaces;

namespace Typedown.Universal.Models.UploadConfigModels
{
    public class PowerShellModel : ConfigModel
    {
        public static string DefaultScript = "# Upload file and return URL\nfunction Upload-Image($FilePath)\n{    \n    return $FilePath\n}";

        public string Script { get; set; } = DefaultScript;

        public override async Task<string> Upload(IServiceProvider serviceProvider, string filePath)
        {
            return await Task.Run(() =>
            {
                var powerShell = serviceProvider.GetService<IPowerShellService>();
                var result = powerShell.Invoke(Script, "Upload-Image", filePath);
                return result.First();
            });
        }
    }
}
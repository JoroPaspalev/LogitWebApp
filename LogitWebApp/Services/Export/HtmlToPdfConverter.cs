namespace WHMS.Services.Common
{
    using LogitWebApp.Services.Export;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class HtmlToPdfConverter : IHtmlToPdfConverter
    {
        public byte[] Convert(string basePath, string htmlCode, string formatType = "A4", string orientationType = "portrait")
        {
            var inputFileName = $"input_{Guid.NewGuid()}.html";
            var outputFileName = $"output_{Guid.NewGuid()}.pdf";
            File.WriteAllText($"{basePath}/{inputFileName}", htmlCode, encoding: Encoding.UTF8);
            var startInfo = new ProcessStartInfo("phantomjs.exe")
            {
                WorkingDirectory = basePath,
                Arguments = $"rasterize.js \"{inputFileName}\" \"{outputFileName}\" \"{formatType}\" \"{orientationType.ToLower()}\"",
                UseShellExecute = true,
            };

            var process = new Process { StartInfo = startInfo };
            process.Start();

            process.WaitForExit();

            var bytes = File.ReadAllBytes($"{basePath}/{outputFileName}");

            File.Delete($"{basePath}/{inputFileName}");
            File.Delete($"{basePath}/{outputFileName}");

            return bytes;
        }
    }
}

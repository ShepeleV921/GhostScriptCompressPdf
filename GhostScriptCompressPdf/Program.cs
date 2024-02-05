using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using Ghostscript.NET.Processor;

using Ghostscript.NET.Viewer;
using System;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace GhostScriptCompressPdf
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (!Directory.Exists("PDF"))
                Directory.CreateDirectory("PDF");

            if (!Directory.Exists("NewCompressPDF"))
                Directory.CreateDirectory("NewCompressPDF");

            string path = "PDF\\";
            string[] files = Directory.GetFiles(path);


            Console.WriteLine($"кол-во всего файлов: {files.Length}");
            for (int i = 0; i < files.Length; i++)
            {
                // Путь к исполняемому файлу Ghostscript (может отличаться в зависимости от вашей установки)
                string ghostscriptPath = @"C:\Program Files\gs\gs10.02.1\bin\gswin64c.exe";

                // Путь к исходному файлу PDF
                string inputPdfPath = files[i];

                // Путь к выходному файлу PDF (куда будет сохранен уменьшенный файл)
                FileInfo fileInfo = new FileInfo(files[i]);
                string outputFile = $"NewCompressPDF\\{fileInfo.Name}";


                // Задаем параметры для Ghostscript
                string ghostscriptArgs = $"-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/ebook -dNOPAUSE -dQUIET -dBATCH -sOutputFile=\"{outputFile}\" \"{inputPdfPath}\"";

                // Создаем процесс для запуска Ghostscript
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ghostscriptPath,
                    Arguments = ghostscriptArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // Ждем завершения процесса
                    process.WaitForExit();

                    // Выводим результат выполнения (можно изменить на ваш вариант обработки)
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    Console.WriteLine("Output: " + output);
                    Console.WriteLine("Error: " + error);
                }


            }
        }
    }

}


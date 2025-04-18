﻿using Anno1800LogoModGenerator;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Compression;
using System.Text;
using System.Text;



namespace Anno1800LogoModGenerator.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IJSRuntime JS { get; set; }




        protected override void OnInitialized()
        {
        }

        private async Task UploadFile(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles(int.MaxValue))
            {
                
            }
        }

        public async Task DownloadModAsync()
        {
            byte[] zipBytes = CreateModZip();
            string base64 = Convert.ToBase64String(zipBytes);

            await JS.InvokeVoidAsync("window.downloadFileFromBase64", "logo_mod.zip", base64);
        }

        public byte[] CreateModZip()
        {
            using var memoryStream = new MemoryStream();

            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                // Add modinfo.json
                var modInfoEntry = archive.CreateEntry("modinfo.json");
                using (var writer = new StreamWriter(modInfoEntry.Open(), Encoding.UTF8))
                {
                    string json = """
                {
                  "Version":"1.0",
                  "ModID":"AutoGenerated_Personal_Logos",
                  "IncompatibleIds":null,
                  "ModDependencies":null,
                  "Category":{
                    "Chinese":null,
                    "English":"Personal",
                    "French":null,
                    "German":null,
                    "Italian":null,
                    "Japanese":null,
                    "Korean":null,
                    "Polish":null,
                    "Russian":null,
                    "Spanish":null,
                    "Taiwanese":null,
                    "Czech":null
                  },
                  "ModName":{
                    "Chinese":null,
                    "English":"Personal Logos",
                    "French":null,
                    "German":null,
                    "Italian":null,
                    "Japanese":null,
                    "Korean":null,
                    "Polish":null,
                    "Russian":null,
                    "Spanish":null,
                    "Taiwanese":null,
                    "Czech":null
                  },
                  "KnownIssues":null,
                  "DLCDependencies":null,
                  "CreatorName":"Anno 1800 Logo Mod Generator",
                  "CreatorContact":null,
                  "Description":{
                    "Chinese":null,
                    "English":"Personal player logos. Mod generated by a tool.",
                    "French":null,
                    "German":null,
                    "Italian":null,
                    "Japanese":null,
                    "Korean":null,
                    "Polish":null,
                    "Russian":null,
                    "Spanish":null,
                    "Taiwanese":null,
                    "Czech":null
                  }
                }
                """;
                    writer.Write(json);
                }

                var assetsText = @$"
<ModOps>
  <ModOp GUID='92' Type=""addNextSibling""> 	
  	";
                for (int i = 0; i < Logos.Count; i++)
                {
                    var guid = StartingGuid + i;
                    assetsText += @$"
  	<Asset>
	      <Template>PlayerLogo</Template>
	      <Values>
	          <Standard>
	              <GUID>{guid}</GUID>
	              <Name>Logo_{guid}</Name>
	              <IconFilename>data/modgraphics/ui/logos/large/Logo_{guid}.png</IconFilename>
	          </Standard>
	          <PlayerLogo>
	              <DefaultLogo>data/modgraphics/ui/logos/large/Logo_{guid}.png</DefaultLogo>
	              <MiniLogo>data/modgraphics/ui/logos/small/Logo_{guid}.png</MiniLogo>
	          </PlayerLogo>
	          <Locked>
	              <DefaultLockedState>0</DefaultLockedState>
	          </Locked>
	      </Values>
    </Asset>
    ";
                }

                assetsText += @$"
  </ModOp>
  <ModOp Type=""addPrevSibling"" GUID='500769' Path=""/Values/CreateGameScene/Logos/Item[Logo='501722']"">
  	";
                for (int i = 0; i < Logos.Count; i++)
                {
                    assetsText += @$"
      <Item>
          <Logo>{StartingGuid + i}</Logo>
      </Item>
          ";
                }
                assetsText += @$"
  </ModOp>
</ModOps>



";

                var entry = archive.CreateEntry("data/config/export/main/asset/assets.xml");
                using (var assetsWriter = new StreamWriter(entry.Open()))
                {
                    assetsWriter.Write(assetsText);
                }

                for (int i = 0; i < Logos.Count; i++)
                {
                    var guid = StartingGuid + i;
                    AddImageFile(archive, $"data/modgraphics/ui/logos/large/Logo_{guid}.png", Logos[i].Data);
                    AddImageFile(archive, $"data/modgraphics/ui/logos/small/Logo_{guid}.png", Logos[i].DataSmall);
                }
            }

            memoryStream.Position = 0;
            return memoryStream.ToArray();
        }

        private static void AddImageFile(ZipArchive archive, string path, byte[] imageBytes)
        {
            var entry = archive.CreateEntry(path);
            using var entryStream = entry.Open();
            entryStream.Write(imageBytes, 0, imageBytes.Length);


            var test = ExecuteCmd(@$"annotex.exe ""{path.Split("/").Last()}.png"" -l=1", Directory.GetCurrentDirectory());
        }


        private static string ConvertPngToDds(string file, string outputFolder)
        {
            return ExecuteCmd(@$"annotex ""{file}.png"" -l={levels}", outputFolder);
        }

        private static string ExecuteCmd(string command, string? workingDirectory = null)
        {
            // Create a new process to run the command
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",          // Use cmd.exe to run command-line commands
                Arguments = "/C " + command,   // '/C' tells cmd.exe to execute the command and then terminate
                RedirectStandardOutput = true, // Redirect output to be able to read it
                UseShellExecute = false,      // Must be false to redirect output
                CreateNoWindow = true,        // Do not create a new window for the command
                WorkingDirectory = workingDirectory // Set working directory (optional)
            };

            // Start the process and read the output
            using (Process? process = Process.Start(processStartInfo))
            {
                using (StreamReader? reader = process?.StandardOutput)
                {
                    return reader?.ReadToEnd() ?? "";
                }
            }
        }

    }

    public static class ImageExtensions
    {
        public static byte[] SaveImageToByteArray(this Image<Rgba32> image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, new PngEncoder()); // You can use any encoder you want
                return ms.ToArray();
            }
        }

    }
}

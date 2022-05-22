using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace OOI.ModelCompiler.ASPDNETUserInterface.Pages
{
    public class CreateFileModel : PageModel
    {
        private IHostingEnvironment Environment;
        public CreateFileModel(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        

        public void OnPostUpload(List<IFormFile> postedFiles)
        {
            

            string InputPath = Path.Combine(this.Environment.WebRootPath, "Temp/InputFiles");
            if (!Directory.Exists(InputPath))
            {
                Directory.CreateDirectory(InputPath);
            }

            string OutputPath = Path.Combine(this.Environment.WebRootPath, "Temp/OutputFiles");
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }

            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(InputPath, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
            }
        }
    }
}
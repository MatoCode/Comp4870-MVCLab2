using Microsoft.AspNetCore.Mvc;

namespace TextFilesMVC;

public class FileController : Controller
{
    private readonly ILogger<FileController> _logger;
    private readonly IWebHostEnvironment _env; // get host environment in order to be able to access other folders in the project

    public FileController(ILogger<FileController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        // _logger.LogInformation(_env.ContentRootPath); // log the path to the root of the project
        var path = Path.Combine(_env.ContentRootPath, "TextFiles"); // combine the path to the root of the project with the path to the file
        _logger.LogInformation(path); // log the path to the file

        var files = Directory.GetFiles(path); // get all files in the directory
        string[] filenames = new List<string>(files).ToArray(); // create an array of strings with the same length as the number of files
        int i = 0;
        foreach (var file in files) {
            filenames[i] = Path.GetFileNameWithoutExtension(file); // get the name of the file and add it to the array
            i++;
        }
        ViewBag.Path = _env.ContentRootPath; // pass the path to the view
        ViewBag.Files = filenames; // pass the array to the view
        return View();
    }

    public IActionResult Display(string id) {
        string searchPattern = "*" + id + "*"; // create a search pattern for the file
        var filePath = Directory.GetFiles(Path.Combine(_env.ContentRootPath, "TextFiles"), searchPattern); // get the path to the file
        var content = System.IO.File.ReadAllText(filePath[0]); // read the file
        ViewBag.Content = content; // pass the content to the view
        return View();
    }


}

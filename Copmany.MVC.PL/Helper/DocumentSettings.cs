namespace Copmany.MVC.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            // string folderPath = "F:\\ASP\\EFCore\\Copmany.MVC\\Copmany.MVC.PL\\wwwroot\\files\\" + folderName;
            // var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"\wwwroot\files\" ,folderName);
            var fileName = $"{Guid.NewGuid()}{file.Name}";  

            var filePath = Path.Combine(folderPath, fileName);
           using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"\wwwroot\files", folderName , fileName);
            if (File.Exists(filePath)) 
                File.Delete(filePath);  
        }
    }
}

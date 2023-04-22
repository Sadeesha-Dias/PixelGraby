using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PixelGraby.Pages
{
   
    public class HistoryModel : PageModel
    {
        public string FilePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
       
        public List<HistoryModel> Videos { get; set; }

        public void OnGet()
        {
            var videoDirectory = "C:\\Users\\User\\OneDrive\\Desktop\\TestV";
            var videoFiles = Directory.GetFiles(videoDirectory);

            Videos = new List<HistoryModel>();

            foreach (var videoFile in videoFiles)
            {
                var videoModel = new HistoryModel();
                videoModel.FilePath = videoFile;
                videoModel.FileName = Path.GetFileName(videoFile);
                videoModel.FileSize = GetFileSize(videoFile);

                var thumbnailPath = GetThumbnailPath(videoFile);
                if (System.IO.File.Exists(thumbnailPath))
                {
                    videoModel.ThumbnailPath = thumbnailPath;
                }
                else
                {
                    //videoModel.ThumbnailPath = "/images/default-thumbnail.png";
                    videoModel.ThumbnailPath = "C:\\Users\\User\\OneDrive\\Desktop\\TestV\\default-thumbnail.png";
                }

                Videos.Add(videoModel);
            }
        }

        private string GetThumbnailPath(string videoFile)
        {
            // Code to generate or retrieve the thumbnail for the video
            // and save it to the same directory as the video file.
            // This assumes that the thumbnail has the same name as the
            // video file, with the extension ".jpg" instead of ".mp4".
            var thumbnailFileName = Path.GetFileNameWithoutExtension(videoFile) + ".png";
            return Path.Combine(Path.GetDirectoryName(videoFile), thumbnailFileName);
        }

        private string GetFileSize(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var sizeInBytes = fileInfo.Length;

            if (sizeInBytes < 1024)
            {
                return sizeInBytes + " bytes";
            }
            else if (sizeInBytes < 1024 * 1024)
            {
                return (sizeInBytes / 1024) + " KB";
            }
            else
            {
                return (sizeInBytes / (1024 * 1024)) + " MB";
            }
        }
    }

}

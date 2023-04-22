using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Headers;

namespace PixelGraby.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [TempData]
        public string Message { get; set; }

        //[BindProperty]
        //public bool UrlError { get; set; }


        public async Task<IActionResult> OnPost(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("video/mp4"));

                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    var fileName = Path.GetFileName(url);
                    var filePath = Path.Combine(@"C:\Users\User\OneDrive\Desktop\TestV", fileName);

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await stream.CopyToAsync(fileStream);
                        }

                        TempData["Message"] = "Video downloaded successfully!";
                        return RedirectToPage();
                    }
                }
            }
        }
    }
}
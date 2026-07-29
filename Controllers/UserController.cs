using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadForm_Project.Data;
using UploadForm_Project.Models;
    
namespace UploadForm_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.UserProfiles.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProfile model)
        {
            if (model.UploadFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".zip" };

                var extension = Path.GetExtension(model.UploadFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("UploadFile", "Only JPG, JPEG, PNG and ZIP files are allowed.");
                }

                if (model.UploadFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UploadFile", "Maximum file size is 5 MB.");
                }

                if (ModelState.IsValid)
                {
                    using var memoryStream = new MemoryStream();

                    await model.UploadFile.CopyToAsync(memoryStream);

                    model.FileData = memoryStream.ToArray();
                    model.FileName = model.UploadFile.FileName;
                    model.ContentType = model.UploadFile.ContentType;
                }
            }

            if (!ModelState.IsValid)
                return View(model);


            _context.UserProfiles.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);

            if (user == null || user.FileData == null)
                return NotFound();

            return File(user.FileData, user.ContentType, user.FileName);
        }

        public async Task<IActionResult> Image(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);

            if (user == null || user.FileData == null)
                return NotFound();

            return File(user.FileData, user.ContentType);
        }
    }
}
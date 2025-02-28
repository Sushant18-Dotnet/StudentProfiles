
using Microsoft.AspNetCore.Mvc;
using StudentProfile.Data;
using StudentProfile.Models;

namespace StudentProfile.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _db;
        public StudentsController(StudentDbContext db)
        {
            _db = db; 
        }


        [HttpGet]
        
        public IActionResult index()
        {
            var students = _db.Students.ToList();
            return View(students);
        }

        [HttpGet]
        [Route("Student/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Student/Create")]
        public async Task<IActionResult> Create(StudentsViewModel viewmodel)
        {
            if (viewmodel.ProfileImages != null && viewmodel.ProfileImages.Length > 0)
            {
                // Ensure directory exists
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Generate a unique file name to avoid overwriting
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(viewmodel.ProfileImages.FileName);

                // Define the path to save the image
                var filePath = Path.Combine(directoryPath, uniqueFileName);

                // Save the file to the specified path asynchronously
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewmodel.ProfileImages.CopyToAsync(stream);  
                }

                
                var student = new Student
                {
                    Name = viewmodel.Name,
                    Email = viewmodel.Email,
                    phone = viewmodel.phone,
                    ProfileImage = uniqueFileName,  
                    subscribed = viewmodel.subscribed
                };

               
                _db.Add(student);
                await _db.SaveChangesAsync();  
            }

           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            var student = _db.Students.Find(id);


            _db.SaveChanges();

            return View(student);


        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student model)
        {
           
                var student = await _db.Students.FindAsync(model.id);

                if (student != null)
                {
                    // Update basic properties
                    student.Name = model.Name;
                    student.Email = model.Email;
                    student.phone = model.phone;

                    // If a new profile image is uploaded, handle it
                    if (model.ProfileImages != null && model.ProfileImages.Length > 0)
                    {
                        // Define the path to save the image
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                        // Ensure directory exists
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Generate a unique file name to avoid overwriting
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfileImages.FileName);

                        // Define the file path to save the image
                        var filePath = Path.Combine(directoryPath, uniqueFileName);

                        // Save the file asynchronously
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ProfileImages.CopyToAsync(stream);
                        }

                        // Update ProfileImage field with the new file name
                        student.ProfileImage = uniqueFileName;
                    }

                    // Save changes to the database
                    _db.Update(student);
                    await _db.SaveChangesAsync();
                }

                return RedirectToAction("Index");



           


        }



        [HttpGet]

        public IActionResult Delete(Guid id)
        {

            var student = _db.Students.Find(id);

            _db.SaveChanges();

            return View(student);

        }

        [HttpPost]
        
        public IActionResult Delete(Student stud)
        {

          

            var student = _db.Students.Remove(stud);


            _db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}

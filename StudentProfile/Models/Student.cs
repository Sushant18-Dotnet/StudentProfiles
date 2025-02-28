using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProfile.Models
{
    public class Student
    {


        public Guid id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }


        public string phone { get; set; }
        
        [NotMapped]

        public IFormFile ProfileImages { get; set; }
        public string ProfileImage { get; set; }

        public bool subscribed { get; set; }
    }
}

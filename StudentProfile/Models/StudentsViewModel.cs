namespace StudentProfile.Models
{
    public class StudentsViewModel
    {

        public string Name { get; set; }

        public string Email { get; set; }


        public string phone { get; set; }



        public IFormFile ProfileImages { get; set; }

        public bool subscribed { get; set; }
    }
}

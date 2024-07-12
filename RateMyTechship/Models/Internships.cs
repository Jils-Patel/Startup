using System.ComponentModel;

namespace RateMyTechship.Models
{
    public class Internships
    {
        public int ID { get; set; }
        [DisplayName("Company")]
        public string CompanyName { get; set; }
        [DisplayName("Position")]
        public string Role { get; set; }
        public string Location { get; set; }
        [DisplayName("Application Link")]
        public string ApplicationLink { get; set; }
        public string Term { get; set; }
        [DisplayName("Application Deadline")]
        public DateTime ApplicationDeadline { get; set; }
    }
}

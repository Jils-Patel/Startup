using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyTechship.Models
{
    public class Review
    {
        public int ID { get; set; }
        [DisplayName("Date Posted")]
        public string? CreationDate { get ; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Internship Period")]
        public string Duration { get; set; }
        public string Position { get; set; }
        [DisplayName("Rating (1 - 5)")]
        public int Rating { get; set; }
        [DisplayName("Work Environment")]
        public string WorkCulture { get; set; }
        [DisplayName("Learning Oppurtunities")]
        public string LearningOpportunities { get; set; }
        [DisplayName("Networking Oppurtunities")]
        public string NetworkingOpportunities { get; set; }
        [DisplayName("Mentorship")]
        public string Workload { get; set; }
        [DisplayName("Review")]
        public string InternshipReview { get; set; }
        public int Likes { get; set; }
        public int Dislike { get; set; }
        public bool HasLiked { get; set; }
        public bool HasDisliked { get; set; }
        public List<string> LikedByUserIds { get; set; } = new List<string>();
        public List<string> DislikedByUserIds { get; set; } = new List<string>();
        public string? AuthorId { get; set; }
        public Review()
        {

        }
    }
}

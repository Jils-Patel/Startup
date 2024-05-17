using System.ComponentModel;

namespace RateMyTechship.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Major { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        [DisplayName("Image")]
        public string BackgroundImage { get; set; }
        public Blog()
        {

        }
    }
}

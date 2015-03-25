
namespace ODataOpenIssuesDashboard.Models
{
    public class StackQuestion
    {
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public long CreationDate { get; set; }
        public string Link { get; set; }
        public int AnswerCount { get; set; }
    }
}
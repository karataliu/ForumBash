namespace ForumBash.Service
{
    public class BashModel
    {
        public string CurrentView { get; set; }

    }

    public class BashModelHome : BashModel
    {
        public string Status { get; set; }
    }
}
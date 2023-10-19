namespace GameDeveloperEntity.Concrete
{
    public class Queue
    {
        public int Id { get; set; }
        public int HomeUserId { get; set; }
        public int AwayUserId { get; set; }
        public int WinnerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int Active { get; set; }
    }
}

namespace MusicApp.Models
{
    public class Musician
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Instrument { get; set; }

        public List<RecordMember> RecordMembers { get; set; }
    }
}

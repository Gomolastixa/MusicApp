namespace MusicApp.Models
{
    public class RecordMember
    {
        public int Id { get; set; }
        public int MusicRecordId { get; set; }
        public MusicRecord MusicRecord { get; set; }



        public int MusicianId { get; set; }
        public Musician Musician { get; set; } 
    }
}

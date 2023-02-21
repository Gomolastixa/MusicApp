namespace MusicApp.Models
{
    public class RecordMember
    {
        public int Id { get; set; }

        public int MusicRecordId { get; set; }
        public virtual MusicRecord MusicRecord { get; set; }


        public int MusicianId { get; set; }
        public virtual Musician Musician { get; set; } 
    }
}

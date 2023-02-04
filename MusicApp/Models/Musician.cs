using System.ComponentModel;

namespace MusicApp.Models
{
    public class Musician
    {
        public int Id { get; set; }
        [DisplayName("Musician")]
        public string FullName { get; set; }
        public string Instrument { get; set; }

        public virtual ICollection<RecordMember> RecordMembers { get; set; }
    }
}

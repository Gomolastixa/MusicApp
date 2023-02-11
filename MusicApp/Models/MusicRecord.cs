using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class MusicRecord
    {
     
        public int Id { get; set; }
        [DisplayName ("Title")]
        public string Name { get; set; }
        
        public string Artist { get; set; }
        [DisplayName ("Released In")]
        public int Year { get; set; }
        public string? Genre { get; set; }


       public virtual ICollection<RecordMember> RecordMembers { get; set; }
    }
}

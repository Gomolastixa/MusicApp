using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class MusicRecord
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name="Καλλιτεχνης")]
        public string Artist { get; set; }
        public int Year { get; set; }
        public string? Genre { get; set; }


        public MusicRecord()
        {

        }

    }
}

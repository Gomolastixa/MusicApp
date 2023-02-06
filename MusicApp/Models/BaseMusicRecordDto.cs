namespace MusicApp.Models
{
    public abstract class BaseMusicRecordDto
    {
        public string Name { get; set; }

        public string Artist { get; set; }
        public int Year { get; set; }
        public string? Genre { get; set; }
    }
}

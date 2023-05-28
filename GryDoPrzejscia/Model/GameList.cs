using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GryDoPrzejscia.Model
{
    public class GameList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Launcher { get; set; }
       
        [Column("Gram")]
        [DefaultValue(false)]
        public bool isPlayed { get; set; }

        [Column("Przeszedłem")]
        [DefaultValue(false)]
        public bool isFinished { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Learning.Models
{
    public class Odgovor
    {
        [Key]
        public int Id { get; set; }
        public bool JeLiTacno { get; set; }

        public int PitanjeId { get; set; }
        public Pitanje Pitanje { get; set; }

        public int KvizId { get; set; }
        public Kviz Kviz { get; set; }
    }
}

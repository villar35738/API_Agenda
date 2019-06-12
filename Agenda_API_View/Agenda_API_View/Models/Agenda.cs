using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_API_View.Models
{
    public class Agenda
    {
        [Key]
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }
}

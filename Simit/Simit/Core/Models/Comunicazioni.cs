using System;

namespace Simit.Core.Models
{
    public class Comunicazioni
    {
        public int      Id       { get; set; }
        public DateTime Data     { get; set; }
        public string   Titolo   { get; set; }
        public string   Dettagli { get; set; }
    }
}

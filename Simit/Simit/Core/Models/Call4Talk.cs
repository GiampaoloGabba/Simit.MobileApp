namespace Simit.Core.Models
{
    public partial class Call4Talk
    {
        public int    Id          { get; set; }
        public int    Episodio    { get; set; }
        public string Titolo      { get; set; }
        public string Relatore    { get; set; }
        public string Descrizione { get; set; }
        public string UrlVideo    { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace Simit.Core.Models
{
    public partial class Programma : INotifyPropertyChanged
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Giorno")]
        public long Giorno { get; set; }

        [JsonProperty("Data")]
        public DateTime Data { get; set; }

        [JsonProperty("Ora")]
        public string Ora { get; set; }

        [JsonProperty("InfoAgg")]
        public string InfoAgg { get; set; }

        [JsonProperty("Sala")]
        public string Sala { get; set; }

        [JsonProperty("Titolo")]
        public string Titolo { get; set; }

        [JsonProperty("SottoTitolo")]
        public string SottoTitolo { get; set; }

        [JsonProperty("Colore")]
        public string Colore { get; set; }

        [JsonProperty("Scheda")]
        public List<Scheda> Scheda { get; set; }

        [JsonProperty("Ore")]
        public List<Ore> Ore { get; set; }

        [JsonIgnore]
        public string ColoreHex
        {
            get
            {
                switch (Colore)
                {
                    case "Blu":     return "#0062A4";
                    case "Giallo":  return "#FDB924";
                    case "Grigio":  return "#ADB6B1";
                    case "Rosso":   return "#EE3F3D";
                    case "Verde":   return "#50B749";
                    case "Azzurro": return "#9BC0E9";
                    case "Viola":   return "#B18ABF";
                    default:        return "#FFFFFF";
                }
            }
        }

        [JsonIgnore]
        public bool HasDati => Colore != "Bianco" || !string.IsNullOrEmpty(Sala);

        [JsonIgnore]
        public bool HasScheda => Scheda.Any() || Ore.Any();

        [JsonIgnore]
        public bool HasReminder => Settings.Current.GetEventId($"event_2024_{Id}") != string.Empty;

        [JsonIgnore]
        public bool HasReminderForced { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class Ore
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Orario")]
        public string Orario { get; set; }

        [JsonProperty("Descrizione")]
        public string Descrizione { get; set; }

        [JsonProperty("Relatore")]
        public string Relatore { get; set; }

        [JsonProperty("ProgrammaId")]
        public long ProgrammaId { get; set; }
    }

    public partial class Scheda
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Titolo")]
        public string Titolo { get; set; }

        [JsonProperty("SottoTitolo")]
        public string SottoTitolo { get; set; }

        [JsonProperty("SottoTitoloBold")]
        public string SottoTitoloBold { get; set; }

        [JsonProperty("Sponsor")]
        public string Sponsor { get; set; }

        [JsonProperty("ProgrammaId")]
        public long ProgrammaId { get; set; }
    }
}

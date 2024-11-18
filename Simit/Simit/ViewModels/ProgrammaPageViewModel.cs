using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Fusillade;
using Plugin.Calendars.Abstractions;
using Prism;
using Prism.Navigation;
using Simit.Core;
using Simit.Core.Api;
using Simit.Core.Models;
using Simit.Views.Popups;
using Xamarin.Forms;

namespace Simit.ViewModels
{
	public class ProgrammaPageViewModel : ViewModelBase, IActiveAware
	{
		private readonly SimitApiManager _simitApiManager;
		private readonly List<ProgrammaGroup> _programmaCache = new List<ProgrammaGroup>();

		public ObservableCollection<Programma> Programma1 { get; set; }
		public ObservableCollection<Programma> Programma2 { get; set; }
		public ObservableCollection<Programma> Programma3 { get; set; }
		public ObservableCollection<Programma> Programma4 { get; set; }

		public bool IsActive { get; set; }
		public ICommand DettagliCmd    { get; set; }
		public ICommand AddReminderCmd { get; set; }
		public ICommand FacultyCmd { get; set; }

        public event EventHandler IsActiveChanged;

		private bool _isBusyAction;

		public ProgrammaPageViewModel(INavigationService navigationService, SimitApiManager simitApiManager)
			: base(navigationService)
		{
			_simitApiManager = simitApiManager;
			Title            = "Programma";
			ShowRefresh = true;

			DettagliCmd = new Command<Programma>(async (programma) =>
			{
				if (!_isBusyAction)
				{
                    _isBusyAction = true;
					var navPar = new NavigationParameters {{"programma", programma}};
					await NavigationService.NavigateAsync(nameof(DettagliPopupPage), navPar);
                    _isBusyAction = false;
				}
			});
			
			
            FacultyCmd = new Command<Programma>(async (programma) =>
			{
				if (!_isBusyAction)
				{
                    _isBusyAction = true;
					await NavigationService.NavigateAsync(nameof(RelatoriPopupPage));
                    _isBusyAction = false;
				}
			});

			AddReminderCmd = new Command<Programma>(async (programma) =>
			{
				var ora       = programma.Ora.Split('-');

				if (ora.Length < 2)
				{
					App.DisplayAlert("Attenzione","Impossibile salvare l'evento corrente nel calendario");
					return;
				}

				var oraInizio = (programma.Data.ToString("yyyy-MM-dd") + " " + ora[0].Replace(".",":")).ToDateTime();
				var oraFine   = (programma.Data.ToString("yyyy-MM-dd") + " " + ora[1].Replace(".",":")).ToDateTime();

				var evento = new Plugin.Calendars.Abstractions.CalendarEvent
				{
					Description = programma.SottoTitolo.ToNotNull(),
					Location    = "SIMIT 2024 - " + programma.InfoAgg,
					AllDay      = false,
					Name        = programma.Titolo,
					Reminders = new List<CalendarEventReminder>
					{
						new CalendarEventReminder
						{
							Method     = CalendarReminderMethod.Default,
							TimeBefore = TimeSpan.FromMinutes(20)
						}
					}
				};

				if (oraInizio != null && oraFine != null)
				{
					evento.Start = oraInizio.Value;
					evento.End = oraFine.Value;
				}

				if (await ReminderService.AddReminderAsync("event_" + programma.Id, evento))
				{
					programma.HasReminderForced = true;
				}

			});
        }

		private void OnIsActiveChanged()
		{
			if (IsActive)
			{
				ShowGilead = true;

				if (Programma1 == null)
					GetProgramma();
			}
		}


        protected override async Task RefreshPage()
        {
            await Task.Delay(150);
            Programma1.Clear();
            Programma2.Clear();
            Programma3.Clear();
            Programma4.Clear();
            await Task.Delay(150);
            await GetProgrammaAsync(false);
        }

        private void GetProgramma()
		{
			Task.Run(async () =>
			{
                await GetProgrammaAsync();
			});
		}

        private async Task GetProgrammaAsync(bool loadFromCache = true)
		{
            var prgRequest = await _simitApiManager.GetProgramma(Priority.UserInitiated, loadFromCache);
            var programma = prgRequest.Content;

            if (programma == null || programma.Count == 0)
            {
                prgRequest = await _simitApiManager.GetProgramma(Priority.UserInitiated);
                programma = prgRequest.Content ?? new List<Programma>();
            }

            foreach (var progr in programma)
            {
                progr.HasReminderForced = progr.HasReminder;
            }

            var cont = 0;

            foreach (var gruppo in programma.GroupBy(x => x.Data))
            {
                cont++;
                switch (cont)
                {
                    case 1:
                        Programma1 = new ObservableCollection<Programma>(gruppo.ToList());
                        break;
                    case 2:
                        Programma2 = new ObservableCollection<Programma>(gruppo.ToList());
                        break;
                    case 3:
                        Programma3 = new ObservableCollection<Programma>(gruppo.ToList());
                        break;
                    default:
                        Programma4 = new ObservableCollection<Programma>(gruppo.ToList());
                        break;
                }
            }
		}
	}

	public class ProgrammaGroup : INotifyPropertyChanged {

		public int      Giorno { get; set; }
		public DateTime Data   { get; set; }

		public bool HasFilters => false;

		public ObservableCollection<Programma> Programmi { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
	}
}

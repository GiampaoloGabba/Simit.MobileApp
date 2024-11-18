using System;
using System.Threading.Tasks;
using Plugin.Calendars.Abstractions;
using Plugin.Calendars;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Simit.Core
{
    public static class ReminderService
    {
        public static async Task<bool> HasReminderAsync(string id)
        {
            if (!Settings.Current.HasSetReminder)
                return false;
            
            var ready = await CheckPermissionsGetCalendarAsync(false);
            if (!ready)
                return false;

            var externalId = Settings.Current.GetEventId(id);
            if (string.IsNullOrWhiteSpace(externalId))
                return false;

            try
            {
                var calEvent = await CrossCalendars.Current.GetEventByIdAsync(externalId);
                return calEvent != null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Event has an Id, but doesn't exist, removing" + ex);
                Settings.Current.RemoveReminderId(id);

            }
            return false;
        }

        public static async Task<bool> AddReminderAsync(string id, CalendarEvent calEvent)
        {
            var ready = await CheckPermissionsGetCalendarAsync();
            if (!ready)
                return false;

            try
            {
                var conferenceCal = await GetOrCreateConferenceCalendarAsync();
                await CrossCalendars.Current.AddOrUpdateEventAsync(conferenceCal, calEvent);

                Settings.Current.SaveReminderId(id, calEvent.ExternalID);

                if(!Settings.Current.HasSetReminder)
                {
                    App.DisplayAlert("Calendario creato", $"E' stato creato il calendario {conferenceCal.Name} nel tuo dispositivo in cui verrano salvati i reminder selezionati all'interno dell'app.");
                }
                Settings.Current.HasSetReminder = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Unable to create event: " + ex);
                App.DisplayAlert("Creazione evento", $"Impossibile creare l'evento nel calendario. Controlla la tua app calendario e riprova");
                return false;
            }
            return true;
        }

        public static async Task<bool> RemoveReminderAsync(string id)
        {
            var ready = await CheckPermissionsGetCalendarAsync();
            if (!ready)
                return false;

            try
            {
                var conferenceCal = await GetOrCreateConferenceCalendarAsync();
                var externalId = Settings.Current.GetEventId(id);
                var calEvent = await CrossCalendars.Current.GetEventByIdAsync(externalId);
                await CrossCalendars.Current.DeleteEventAsync(conferenceCal, calEvent);
                Settings.Current.RemoveReminderId(id);
                Settings.Current.HasSetReminder = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Unable to delete event: " + ex);
                App.DisplayAlert("Eliminazione evento", $"Impossibile eliminare l'evento dal calendario. Controlla la tua app calendario e riprova");
                return false;
            }
            return true;
        }

        static async Task<bool> CheckPermissionsGetCalendarAsync(bool alert = true)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();
            if (status != PermissionStatus.Granted)
            {
                var request = await Permissions.RequestAsync<Permissions.CalendarWrite>();
                if (request != PermissionStatus.Granted)
                {
                    if (alert)
                    {
                        App.DisplayAlert("Permessi calendario", $"I permessi di scrittura nel calendario sono stati negati. Accedi alle impostazioni del telefono e attivali");
                    }

                    return false;
                }
            }

            if (Device.RuntimePlatform == Device.Android)
            {
	            status = await Permissions.CheckStatusAsync<Permissions.CalendarRead>();
	            if (status != PermissionStatus.Granted)
	            {
		            var request = await Permissions.RequestAsync<Permissions.CalendarRead>();
		            if (request != PermissionStatus.Granted)
		            {
			            if (alert)
			            {
				            App.DisplayAlert("Permessi calendario", $"I permessi di lettura nel calendario sono stati negati. Accedi alle impostazioni del telefono e attivali");
			            }

			            return false;
		            }
	            }
            }

            var currentCalendar = await GetOrCreateConferenceCalendarAsync();

            if (currentCalendar == null)
            {
                if (alert)
                {
                    App.DisplayAlert("Calendario non trovato", $"Il calendario della conferenza non è stato trovato. Controlla la tua app calendario e riprova");
                }
                return false;
            }

            return true;
        }


        static async Task<Calendar> GetOrCreateConferenceCalendarAsync()
        {
            
            var id = Settings.Current.ConferenceCalendarId;
            if (!string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    var calendar = await CrossCalendars.Current.GetCalendarByIdAsync(id);
                    if(calendar != null)
                        return calendar;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Unable to get calendar.. odd as we created it already: " + ex);

                }

            }

            //try to find conference app if already uninstalled for some reason
            try
            {
	            var calendars = await CrossCalendars.Current.GetCalendarsAsync();
	            foreach(var calendar in calendars)
	            {
		            //find first calendar we can add stuff to
		            if(calendar.CanEditEvents && calendar.Name == "SIMIT2024")
		            {
			            Settings.Current.ConferenceCalendarId = calendar.ExternalID;
			            return calendar;
		            }
	            }
            }
            catch(Exception ex)
            {
	            Debug.WriteLine("Unable to get calendars.. " + ex);
            }

            var conferenceCalendar = new Calendar {Color = "#7635EB", Name = "SIMIT2024"};

            try
            {
                await CrossCalendars.Current.AddOrUpdateCalendarAsync(conferenceCalendar);
                Settings.Current.ConferenceCalendarId = conferenceCalendar.ExternalID;
                return conferenceCalendar;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Unable to create calendar.. " + ex);
            }

            return null;
        }


    }
}

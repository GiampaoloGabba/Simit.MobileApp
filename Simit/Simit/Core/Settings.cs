// Helpers/Settings.cs

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace Simit.Core
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters.
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        static Settings settings;

        /// <summary>
        /// Gets or sets the current settings. This should always be used
        /// </summary>
        /// <value>The current.</value>
        public static Settings Current => settings ?? (settings = new Settings());

        public void SaveReminderId(string id, string calId) => Preferences.Set(GetReminderId(id), calId);

        string GetReminderId(string id) => "reminder_" + id;

        public string GetEventId(string id) => Preferences.Get(GetReminderId(id), string.Empty);

        public void RemoveReminderId(string id) => Preferences.Set(GetReminderId(id), string.Empty);

        const string HasSetReminderKey = "set_a_reminder_2024";
        static readonly bool HasSetReminderDefault = false;

        public bool HasSetReminder
        {
            get => Preferences.Get(HasSetReminderKey, HasSetReminderDefault);
            set => Preferences.Set(HasSetReminderKey, value);
        }

        const string ConferenceCalendarIdKey = "conference_calendar_2024";
        static readonly string ConferenceCalendarIdDefault = string.Empty;
        public string ConferenceCalendarId
        {
            get => Preferences.Get(ConferenceCalendarIdKey, ConferenceCalendarIdDefault);
            set => Preferences.Set(ConferenceCalendarIdKey, value);
        }

        const string PushNotificationsEnabledKey = "push_enabled";
        static readonly bool PushNotificationsEnabledDefault = false;

        public bool PushNotificationsEnabled
        {
            get => Preferences.Get(PushNotificationsEnabledKey, PushNotificationsEnabledDefault);
            set { Preferences.Set(PushNotificationsEnabledKey, value); OnPropertyChanged(); }
        }

        const string PushRegisteredKey = "push_registered";
        static readonly bool PushRegisteredDefault = false;

        public bool PushRegistered
        {
            get => Preferences.Get(PushRegisteredKey, PushRegisteredDefault);
            set => Preferences.Set(PushRegisteredKey, value);
        }

        public string Email
        {
            get => Preferences.Get("email", string.Empty);
            set => Preferences.Set("email", value);
        }

        public string Token
        {
            get => Preferences.Get("token", string.Empty);
            set => Preferences.Set("token", value);
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}

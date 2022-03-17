using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace smart_potato.Core
{
    internal class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event CollectionChangeEventHandler? CollectionUpdated;

        protected void OnCollectionUpdated(CollectionChangeEventArgs e)
        {
            CollectionUpdated?.Invoke(this, e);
        }

        public event EventHandler<NotificationPushedEventArgs>? NotificationPushed;

        protected void OnNotificationPushed(NotificationPushedEventArgs args)
        {
            NotificationPushed?.Invoke(this, args);
        }
    }

    internal class NotificationPushedEventArgs : EventArgs
    {
        public string Message { get; set; } = "";
        public NotificationLevel Level { get; set; }
    }

    internal enum NotificationLevel
    {
        ERROR,
        WARNING,
        CONFIRMATION,
        NEUTRAL,
    }
}



using System;
using System.ComponentModel;

namespace Org.Feeder.Tests.Extension
{
    public static class NotifyPropertyChangedExtensions
    {
        public static bool IsPropertyChangedFired(
          this INotifyPropertyChanged notifyPropertyChanged,
          Action action, string propertyName)
        {
            var fired = false;
            notifyPropertyChanged.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    fired = true;
                }
            };

            action();

            return fired;
        }
    }
}

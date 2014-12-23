using Gizeta.KCV.ManualCounter.Models;
using Gizeta.KCV.ManualCounter.Utils;
using Livet;
using Livet.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gizeta.KCV.ManualCounter.ViewModels
{
    public class CounterViewModel : NotificationObject
    {
        private readonly static CounterViewModel instance = new CounterViewModel();

        private CounterViewModel()
        {
            this.FrequencyValueCollection = new Dictionary<Frequency, string>();
            this.FrequencyValueCollection.Add(Frequency.None, Frequency.None.GetDescription());
            this.FrequencyValueCollection.Add(Frequency.Day, Frequency.Day.GetDescription());
            this.FrequencyValueCollection.Add(Frequency.Week, Frequency.Week.GetDescription());
            this.FrequencyValueCollection.Add(Frequency.Month, Frequency.Month.GetDescription());
            this.FrequencyValueCollection.Add(Frequency.Year, Frequency.Year.GetDescription());
        }

        public static CounterViewModel Instance
        {
            get { return instance; }
        }

        public Dictionary<Frequency, string> FrequencyValueCollection { get; set; }

        private ObservableCollection<Counter> counters;
        public ObservableCollection<Counter> Counters
        {
            get { return counters; }
            set
            {
                if (counters != value)
                {
                    counters = value;
                    PluginSettings.Current.Counters = counters.Where(x => x != null).ToList();
                    PluginSettings.Current.Save();

                    this.RaisePropertyChanged();
                }
            }
        }

        public double CounterWidth
        {
            get { return PluginSettings.Current.CounterWidth; }
        }

        public double CounterHeight
        {
            get { return PluginSettings.Current.CounterHeight; }
        }

        public void Initialize()
        {
            counters = new ObservableCollection<Counter>(PluginSettings.Current.Counters);
            this.UpdateCounter();
            counters.Add(null);
        }

        public ViewModelCommand AddCounter
        {
            get
            {
                return new ViewModelCommand(() =>
                {
                    this.Counters.Insert(counters.Count - 1, new Counter { IsEditing = true });
                    PluginSettings.Current.Counters = counters.Where(x => x != null).ToList();
                    PluginSettings.Current.Save();
                });
            }
        }

        public ListenerCommand<Counter> EditCounter
        {
            get { return new ListenerCommand<Counter>(counter => counter.IsEditing = true); }
        }

        public ListenerCommand<Counter> ResetCounter
        {
            get { return new ListenerCommand<Counter>(counter => counter.Reset()); }
        }

        public ListenerCommand<Counter> RemoveCounter
        {
            get
            {
                return new ListenerCommand<Counter>(counter =>
                {
                    this.Counters.Remove(counter);
                    PluginSettings.Current.Counters = counters.Where(x => x != null).ToList();
                    PluginSettings.Current.Save();
                });
            }
        }

        public ListenerCommand<Counter> CounterIncrease
        {
            get { return new ListenerCommand<Counter>(counter => counter.Increase(true)); }
        }

        public ListenerCommand<Counter> QuitCounterEditing
        {
            get { return new ListenerCommand<Counter>(counter => counter.IsEditing = false); }
        }

        public void UpdateCounter()
        {
            DateTime now = DateTime.Now;
            foreach (var c in counters)
            {
                if (c == null) continue;
                switch (c.ResetFrequency)
                {
                    case Frequency.Day:
                        if (now - c.ResetDate >= TimeSpan.FromDays(1))
                            c.Reset();
                        break;
                    case Frequency.Week:
                        if (now - c.ResetDate >= TimeSpan.FromDays(7))
                            c.Reset();
                        break;
                    case Frequency.Month:
                        if (now - c.ResetDate >= TimeSpan.FromDays(DateTime.DaysInMonth(c.ResetDate.Year, c.ResetDate.Month)))
                            c.Reset();
                        break;
                    case Frequency.Year:
                        if (now - c.ResetDate >= TimeSpan.FromDays(DateTime.IsLeapYear(c.ResetDate.Year) ? 366 : 365))
                            c.Reset();
                        break;
                }
            }
        }
    }
}

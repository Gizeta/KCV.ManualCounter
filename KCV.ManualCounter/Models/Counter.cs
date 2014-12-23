using Livet;
using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Linq.Expressions;

namespace Gizeta.KCV.ManualCounter.Models
{
    public enum Frequency
    {
        [Description("无")]
        None,

        [Description("每天")]
        Day,

        [Description("每周")]
        Week,

        [Description("每月")]
        Month,

        [Description("每年")]
        Year
    }

    [Serializable]
    public class Counter : NotificationObject
    {
        public Counter()
        {
            setResetDate();
        }
        
        private string content = "";
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private Frequency resetFrequency = Frequency.None;
        public Frequency ResetFrequency
        {
            get { return resetFrequency; }
            set
            {
                if (resetFrequency != value)
                {
                    resetFrequency = value;
                    this.RaisePropertyChanged();

                    setResetDate();
                }
            }
        }

        private int currentValue = 0;
        public int CurrentValue
        {
            get { return currentValue; }
            set
            {
                if (currentValue != value && currentValue >= 0)
                {
                    currentValue = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged("Progress");
                    this.RaisePropertyChanged("ProgressBarWidth");
                    this.RaisePropertyChanged("ProgressText");
                    this.RaisePropertyChanged("ProgressPercentText");
                }
            }
        }

        private int totalValue = 0;
        public int TotalValue
        {
            get { return totalValue; }
            set
            {
                if (totalValue != value && totalValue >= 0)
                {
                    totalValue = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged("Progress");
                    this.RaisePropertyChanged("ProgressBarWidth");
                    this.RaisePropertyChanged("ProgressText");
                    this.RaisePropertyChanged("ProgressPercentText");
                }
            }
        }

        private int stepValue = 1;
        public int StepValue
        {
            get { return stepValue; }
            set
            {
                if (stepValue != value && stepValue != 0)
                {
                    stepValue = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private DateTime resetDate = DateTime.Now;
        public DateTime ResetDate
        {
            get { return resetDate; }
            set
            {
                if (resetDate != value)
                {
                    resetDate = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private bool resetAlongWithQuests = true;
        public bool ResetAlongWithQuests
        {
            get { return resetAlongWithQuests; }
            set
            {
                if (resetAlongWithQuests != value)
                {
                    resetAlongWithQuests = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private bool isEditing = false;
        [XmlIgnore]
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                if (isEditing != value)
                {
                    isEditing = value;
                    this.RaisePropertyChanged();

                    if(value)
                        PluginSettings.Current.Save();
                }
            }
        }

        private string stepText = "";
        public string StepText
        {
            get { return stepText.Length == 0 ? string.Format("{0}{1}", stepValue >= 0 ? "+" : "-", stepValue) : stepText; }
            set
            {
                if (stepText != value)
                {
                    stepText = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public double Progress
        {
            get { return totalValue == 0 ? 0 : currentValue * 1.0 / totalValue; }
        }

        [XmlIgnore]
        public double ProgressBarWidth // interal for release
        {
            get { return PluginSettings.Current.CounterWidth * this.Progress; }
        }

        [XmlIgnore]
        public string ProgressText
        {
            get { return string.Format("{0} / {1}", currentValue, totalValue == 0 ? "∞" : totalValue.ToString()); }
        }

        [XmlIgnore]
        public string ProgressPercentText
        {
            get { return totalValue == 0 ? "" : string.Format("{0:P}", this.Progress); }
        }

        public void Increase(bool autoReset = false)
        {
            if (totalValue == 0)
            {
                this.CurrentValue++;
                return;
            }
            if (currentValue >= totalValue)
            {
                if (autoReset)
                    this.Reset();
            }
            else
            {
                this.CurrentValue++;
            }

            PluginSettings.Current.Save();
        }

        public void Reset()
        {
            this.CurrentValue = 0;
            setResetDate();

            PluginSettings.Current.Save();
        }

        private void setResetDate()
        {
            DateTime now = DateTime.Now;
            switch (resetFrequency)
            {
                case Frequency.Day:
                    if (resetAlongWithQuests)
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Hour > 2 ? now.Day : now.Day - 1, 3, 0, 0, 0);
                    else
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
                    break;
                case Frequency.Week:
                    if (resetAlongWithQuests)
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Hour > 2 ? now.Day : now.Day - 1, 3, 0, 0, 0) - TimeSpan.FromDays(getDayOfWeek(now) - 1);
                    else
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0) - TimeSpan.FromDays(getDayOfWeek(now) - 1);
                    break;
                case Frequency.Month:
                    if (resetAlongWithQuests)
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Hour > 2 ? now.Day : now.Day - 1, 3, 0, 0, 0) - TimeSpan.FromDays(now.Day - 1);
                    else
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0) - TimeSpan.FromDays(now.Day - 1);
                    break;
                case Frequency.Year:
                    if (resetAlongWithQuests)
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Hour > 2 ? now.Day : now.Day - 1, 3, 0, 0, 0) - TimeSpan.FromDays(now.DayOfYear - 1);
                    else
                        this.ResetDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0) - TimeSpan.FromDays(now.DayOfYear - 1);
                    break;
            }
        }

        private int getDayOfWeek(DateTime time)
        {
            switch(time.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
                default:
                    return 1;
            }
        }
    }
}

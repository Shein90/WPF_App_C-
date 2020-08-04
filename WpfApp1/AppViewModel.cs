using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UCIT_Diplom
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ActiveTask> ActiveTasks { get; set; } = new ObservableCollection<ActiveTask>();
        
        [JsonIgnore]
        public WorkAnalysis Analysis { get; set; } = new WorkAnalysis();

        [JsonIgnore]
        public string TaskText { get; set; }
        [JsonIgnore]
        public string StartWorkTimeText { get => Analysis.StartWork == default(DateTime) ? "-" : Analysis.StartWork.ToString("T"); }
        [JsonIgnore]
        public string EndWorkTimeText { get => Analysis.EndWork == default(DateTime) ? "-" : Analysis.EndWork.ToString("T"); }
        [JsonIgnore]
        public string NumberOfCompletedTasks { get; set; } = "-";
        [JsonIgnore]
        public string Result { get => Analysis.Result; }

        public Day Mon  { get; set; } = new Day();
        public Day Tues { get; set; } = new Day();
        public Day Wed  { get; set; } = new Day();
        public Day Thur { get; set; } = new Day();
        public Day Fri  { get; set; } = new Day();

        // Конструктор
        public AppViewModel()
        {
            ActiveTask.owner = ActiveTasks;
        }

        #region Commands
        private RelayCommand addTask;
        [JsonIgnore]
        public RelayCommand AddTask
        {
            get
            {
                return addTask ??
                    (addTask = new RelayCommand((obj) =>
                    {
                        if (TaskText != string.Empty)
                        {
                            ActiveTasks.Insert(0, new ActiveTask(TaskText));
                            TaskText = string.Empty;
                            OnPropertyChanged("TaskText");
                            OnPropertyChanged("ActiveTasks");
                        }
                    }));
            }
        }
        private RelayCommand clearTask;
        [JsonIgnore]
        public RelayCommand ClearTask
        {
            get
            {
                return clearTask ??
                    (clearTask = new RelayCommand((obj) =>
                    {
                        ActiveTasks.Clear();
                        OnPropertyChanged("ActiveTasks");
                    }));
            }
        }

        private RelayCommand startWork;
        [JsonIgnore]
        public RelayCommand StartWork
        {
            get
            {
                return startWork ??
                    (startWork = new RelayCommand((obj) =>
                    {
                        Analysis.StartWork = DateTime.Now;
                        OnPropertyChanged("StartWorkTimeText");
                    }));
            }
        }
        private RelayCommand endWork;
        [JsonIgnore]
        public RelayCommand EndWork
        {
            get
            {
                return endWork ??
                    (endWork = new RelayCommand((obj) =>
                    {
                        if (Analysis.StartWork.Date == DateTime.Today)
                        {
                            Analysis.EndWork = DateTime.Now;
                            NumberOfCompletedTasks = ActiveTask.completedTasks.ToString();
                            Analysis.Calculate(ActiveTasks.Count(o => o.Active == false), Convert.ToInt32(NumberOfCompletedTasks));
                            OnPropertyChanged("EndWorkTimeText");
                            OnPropertyChanged("NumberOfCompletedTasks");
                            OnPropertyChanged("Result");
                        }
                    }));
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}

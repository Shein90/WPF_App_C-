using System;
using System.Collections.ObjectModel;
using System.Timers;

namespace UCIT_Diplom
{
    public class ActiveTask
    {    
        public static ObservableCollection<ActiveTask> owner;
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public string Text { get; set; }
        static public int completedTasks = 0;

        public ActiveTask(string text)
        {
            this.Text = text;
            Date = DateTime.Now;
            Active = false;
        }

        private RelayCommand activationTask;
        public RelayCommand ActivationTask
        {
            get
            {
                return activationTask ?? // возвращает левый операнд если не равен null 
                    (activationTask = new RelayCommand((obj) =>
                    {
                        this.Active = true;
                        owner.Remove(this);
                        owner.Add(this);
                        completedTasks++;
                    }));
            }
        }
    }
}
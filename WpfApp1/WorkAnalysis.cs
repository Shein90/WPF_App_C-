using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UCIT_Diplom
{
    public class WorkAnalysis
    {
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public int NumberOfCompletedTasks { get; set; }
        public string Result { get; set; } = "-";
        
                        // кол-во актив. задач  // количество завершенных
        public void Calculate(int countTask, int completedTask)
        {
            
            TimeSpan dif = EndWork - StartWork;

            if (dif.Hours >= 0)
            {
                Result = string.Format("Ваша продуктивность : {0:f0} %", (((double)completedTask / (double)(countTask + completedTask)) * 100));
            }
            else
            {
                Result = "Данных для анализа недостаточно!";
            }
            
        }
    }
}

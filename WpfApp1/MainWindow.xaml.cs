using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace UCIT_Diplom
{
    public partial class MainWindow : Window
    {
        private AppViewModel viewModel;
       
        public MainWindow()
        {
            InitializeComponent();
            AppViewModel readModel;
            if (File.Exists("config2.json"))
            {
                string json = File.ReadAllText("config2.json");
                readModel = JsonConvert.DeserializeObject<AppViewModel>(json);
            }
            else
            {
                readModel = null;
            }

            if (readModel != null)
                viewModel = readModel;
            else
                viewModel = new AppViewModel();


            this.DataContext = viewModel;
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string json = JsonConvert.SerializeObject(viewModel);
            File.WriteAllText("config2.json", json);
        }
    }
}

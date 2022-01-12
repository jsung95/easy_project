using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.ViewModel
{
    public class ViewModelBase
    {
        
        public class Notifier : INotifyPropertyChanged
        {
            private static readonly ILog log = LogManager.GetLogger(typeof(App));
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                log.Info("OnPropertyChanged(string) invoked.");
                try
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this,
                        new PropertyChangedEventArgs(propertyName));
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message);
                }
                
            }
        }
    }
}

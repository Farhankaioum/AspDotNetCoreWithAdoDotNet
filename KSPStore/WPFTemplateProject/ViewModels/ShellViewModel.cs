using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WPFTemplateProject.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            ActivateItemAsync(IoC.Get<DemoViewModel>(), new CancellationToken());
        }
    }
}

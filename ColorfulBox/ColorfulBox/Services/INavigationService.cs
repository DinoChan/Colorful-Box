using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorfulBox.Services
{
    public interface INavigationService
    {
        event EventHandler<bool> IsNavigatingChanged;

        event EventHandler Navigated;

        bool CanGoBack { get; }

        bool IsNavigating { get; }

        Task NavigateToHomeAsync();

        Task NavigateToSettingsAsync();

        Task NavigateToPage<TPage>();

        Task NavigateToPage<TPage>(object parameter);

        Task GoBackAsync();
    }
}

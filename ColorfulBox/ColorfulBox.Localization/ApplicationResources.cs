using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;

namespace ColorfulBox.Localization
{
    public class ApplicationResources : INotifyPropertyChanged
    {
        public static ApplicationResources Current { get; private set; }

        public ApplicationResources()
        {
            Labels = new Labels();
            if (string.IsNullOrWhiteSpace(ApplicationLanguages.PrimaryLanguageOverride) == false)
                Language = ApplicationLanguages.PrimaryLanguageOverride;

            Current = this;
        }

        public Labels Labels { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private string _language;

        /// <summary>
        /// 获取或设置 Language 的值
        /// </summary>
        public string Language
        {
            get { return _language; }
            set
            {
                if (_language == value)
                    return;

                _language = value;
                Labels.Culture = new System.Globalization.CultureInfo(_language);
                ApplicationLanguages.PrimaryLanguageOverride = _language;
                OnPropertyChanged("");
            }
        }
    }
}


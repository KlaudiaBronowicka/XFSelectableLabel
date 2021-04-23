using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFSelectableLabel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private string _selectedText;
        public string SelectedText
        {
            get => _selectedText;
            set
            {
                _selectedText = value;
                OnPropertyChanged();
            }
        }

        public ICommand TextSelectedCommand => new Command<string>((s) => TextSelected(s));

        public MainViewModel()
        {
        }

        private void TextSelected(string text)
        {
            SelectedText = text;
        }
    }
}

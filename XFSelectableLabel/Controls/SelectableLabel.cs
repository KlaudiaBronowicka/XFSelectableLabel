using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFSelectableLabel.Controls
{
    public class SelectableLabel : View
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SelectableLabel), default(string));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SelectableLabel), Color.Black);
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(SelectableLabel), FontAttributes.None);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(SelectableLabel), -1.0);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty OnTextSelectedCommandProperty =
            BindableProperty.Create(nameof(OnTextSelectedCommand), typeof(ICommand), typeof(SelectableLabel), null);

        public ICommand OnTextSelectedCommand
        {
            get { return (ICommand)GetValue(OnTextSelectedCommandProperty); }
            set { SetValue(OnTextSelectedCommandProperty, value); }
        }

        public static void Execute(ICommand command, object parameter)
        {
            if (command == null) return;
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

        public Command<string> OnTextSelected => new Command<string>((s) => Execute(OnTextSelectedCommand, s));
    }
}
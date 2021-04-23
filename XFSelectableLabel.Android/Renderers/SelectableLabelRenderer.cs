using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using XFSelectableLabel.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SelectableLabel), typeof(XFSelectableLabel.Droid.Renderers.SelectableLabelRenderer))]

namespace XFSelectableLabel.Droid.Renderers
{
    public class SelectableLabelRenderer : ViewRenderer<SelectableLabel, TextView>
    {
        TextView textView;

        public SelectableLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SelectableLabel> e)
        {
            base.OnElementChanged(e);

            var label = (SelectableLabel)Element;

            if (label == null) return;

            if (Control == null)
            {
                textView = new TextView(this.Context);
            }

            textView.Enabled = true;
            textView.SetTextIsSelectable(true);
            textView.DefaultFocusHighlightEnabled = true;

            textView.CustomSelectionActionModeCallback = new CustomSelectionActionModeCallback(TextSelected);

            textView.Text = label.Text;

            textView.SetTextColor(label.TextColor.ToAndroid());

            switch (label.FontAttributes)
            {
                case FontAttributes.None:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    break;
                case FontAttributes.Bold:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    break;
                case FontAttributes.Italic:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Italic);
                    break;
                default:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    break;
            }

            textView.TextSize = (float)label.FontSize;
            SetNativeControl(textView);
        }

        private void TextSelected()
        {
            var word = textView.Text[textView.SelectionStart..textView.SelectionEnd];

            Element.OnTextSelected.Execute(word);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            textView.Enabled = false;
            textView.Enabled = true;
        }

        private class CustomSelectionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {
            private Action _selectionCallback;

            public CustomSelectionActionModeCallback(Action selectionCallback)
            {
                _selectionCallback = selectionCallback;
            }

            public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

            public bool OnCreateActionMode(ActionMode mode, IMenu menu) => true;

            public void OnDestroyActionMode(ActionMode mode) { }

            public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
            {
                menu?.Clear();
                _selectionCallback?.Invoke();

                return false;
            }
        }
    }
}


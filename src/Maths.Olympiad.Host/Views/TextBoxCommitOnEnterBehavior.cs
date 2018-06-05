using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Maths.Olympiad.Host.Views
{
    public class TextBoxCommitOnEnterBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoUpdateSource(e.Source);
            }
        }
        void DoUpdateSource(object source)
        {
            if (!(source is TextBox elt))
            {
                return;
            }

            BindingExpression binding = BindingOperations.GetBindingExpression(elt, TextBox.TextProperty);

            binding?.UpdateSource();
        }
    }
}
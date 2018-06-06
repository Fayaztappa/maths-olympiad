using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Maths.Olympiad.Host.Views
{
    /// <summary>
    /// Interaction logic for QuestionView.xaml
    /// </summary>
    public partial class QuestionView : UserControl
    {
        public QuestionView()
        {
            InitializeComponent();
            IsVisibleChanged += OnIsVisibleChanged;
            DataContextChanged += OnIsVisibleChanged;
            Loaded += OnLoaded;

            SetFocus();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SetFocus();
        }

        private void SetFocus()
        {
            FocusManager.SetFocusedElement(this, txtTotal);
            txtTotal.Focus();
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            SetFocus();
        }
    }
}

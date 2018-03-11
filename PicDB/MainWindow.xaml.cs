using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mw;
        public MainWindow()
        {
            GlobalInformation gi = GlobalInformation.InitializeInstance("Pictures");
            mw = new MainWindowViewModel();
            DataContext = mw;
        }

        private void CopyrightNotice_KeyUp(object sender, KeyEventArgs e)
        {
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = mw.CurrentPicture.IPTC.CopyrightNotices;

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear 
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list 
            resultStack.Children.Clear();

            // Add the result 
            foreach (var obj in data)
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work 
                    addItem(obj, border);
                }
            }
        }

        private void CopyrightNoticeAutoComplete_LostFocus(object sender, RoutedEventArgs e)
        {
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            if(!CopyrightNoticeAutoComplete.IsKeyboardFocusWithin) border.Visibility = Visibility.Collapsed;
            
        }

        private void addItem(string text, Border border)
        {
            TextBlock block = new TextBlock();

            // Add the text 
            block.Text = text;

            // A little style... 
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events 
            block.MouseLeftButtonUp += (sender, e) =>
            {
                CopyrightNoticeLabel.Text = (sender as TextBlock).Text;
                border.Visibility = Visibility.Collapsed;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel 
            resultStack.Children.Add(block);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> TextBoxes = new List<TextBox>();
            TextBoxes.Add(KeywordsLabel);
            TextBoxes.Add(ByLineLabel);
            TextBoxes.Add(HeadlineLabel);
            TextBoxes.Add(CaptionLabel);
            TextBoxes.Add(CopyrightNoticeLabel);

            foreach(TextBox tb in TextBoxes)
            {
                BindingExpression bn = tb.GetBindingExpression(TextBox.TextProperty);
                bn.UpdateSource();
            }

            mw.CurrentPictureChanged();
        }

        private void SearchTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                mw.SearchPictures(SearchText.Text);
            }
        }

        private void SearchTextKeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                mw.SearchPictures(null);
            }
        }
    }
}

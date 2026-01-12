using LGCNS.iPharmMES.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Board.UserControls
{
    public partial class SelectRoutePopup : iPharmMESChildWindow
    {
        public SelectRoutePopup()
        {
            InitializeComponent();
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = this.DataContext as SelectRoutePopupViewModel;
            if (e.Key == Key.Enter)
            {
                viewModel.SearchCommand.Execute(null);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

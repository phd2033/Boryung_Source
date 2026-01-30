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
using System.Windows.Threading;
using ShopFloorUI;

namespace 보령
{
    public partial class 설비액션기록 : iPharmMESChildWindow
    {
        public 설비액션기록()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            (this.LayoutRoot.DataContext as 설비액션기록ViewModel).LoadedCommandAsync.Execute(null);
        }
    }
}

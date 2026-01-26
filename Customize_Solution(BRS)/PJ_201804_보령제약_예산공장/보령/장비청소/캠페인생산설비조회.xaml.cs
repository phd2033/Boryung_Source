using ShopFloorUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace 보령
{
    public partial class 캠페인생산설비조회 : ShopFloorCustomWindow
    {
        public 캠페인생산설비조회()
        {
            InitializeComponent();
            this.DataContext = new 캠페인생산설비조회ViewModel();
        }

        public override string TableTypeName
        {
            get { return "TABLE,캠페인생산설비조회"; }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        
    }
}


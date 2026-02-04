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
    public partial class 멸균물품유효기간확인 : ShopFloorCustomWindow
    {
        public 멸균물품유효기간확인()
        {
            InitializeComponent();
        }

        public override string TableTypeName
        {
            get { return "TABLE,멸균물품유효기간확인"; }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}


﻿using ShopFloorUI;
using System.Windows.Controls;
using System.Linq;
using LGCNS.EZMES.ControlsLib;
using System.Collections.Generic;
using LGCNS.iPharmMES.Common;
using System.Windows.Media;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows;
using System.ComponentModel;

namespace 보령
{
    [Description("두께측정")]
    public partial class 두께측정 : ShopFloorCustomWindow
    {
        public 두께측정()
        {
            InitializeComponent();
        }

        public override string TableTypeName
        {
            get { return "TABLE,두께측정"; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

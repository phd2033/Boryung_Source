﻿using System;
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
using ShopFloorUI;
using System.ComponentModel;
using System.Windows.Data;

namespace 보령
{
    [Description("무균공정사용Nozzle확인")]
    public partial class 무균공정사용Nozzle확인 : ShopFloorCustomWindow
    {
        public 무균공정사용Nozzle확인()
        {
            InitializeComponent();
        }
        public override string TableTypeName
        {
            get { return "TABLE,무균공정사용Nozzle확인"; }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
        }
    }
}

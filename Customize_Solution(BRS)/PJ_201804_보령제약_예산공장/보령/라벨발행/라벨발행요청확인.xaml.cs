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
using System.Windows.Navigation;
using ShopFloorUI;

namespace 보령
{
    public partial class 라벨발행요청확인 : ShopFloorCustomWindow
    {
        public 라벨발행요청확인()
        {
            InitializeComponent();
        }
        public override string TableTypeName
        {
            get { return "TABLE,라벨발행요청확인"; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
        }        
    }
}

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
using ShopFloorUI;

namespace 보령
{
    public partial class 원료의약품라벨발행 : ShopFloorCustomWindow
    {
        public 원료의약품라벨발행()
        {
            InitializeComponent();
        }

        public override string TableTypeName
        {
            get { return "TABLE,원료의약품라벨발행"; }
        }
        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {
            if (Phase != null)
            {
                if (await Phase.SessionCheck() != enumInstructionRegistErrorType.Ok)
                    DialogResult = false;
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void txtVesselId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (this.LayoutRoot.DataContext as 원료의약품라벨발행ViewModel).PrintStandbyCommandAsync.Execute(this.txtVesselId.Text);
            }
        }
    }
}

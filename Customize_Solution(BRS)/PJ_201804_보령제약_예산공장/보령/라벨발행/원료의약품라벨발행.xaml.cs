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
        private void MainDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.MainDataGrid.SelectedItem != null)
            {
                BR_BRS_SEL_EquipmentStatus_PROC_OPSG.OUTDATA tar = this.MainDataGrid.SelectedItem as BR_BRS_SEL_EquipmentStatus_PROC_OPSG.OUTDATA;
                tar.SELFLAG = !tar.SELFLAG;

                this.MainDataGrid.SelectedItem = null;
                this.MainDataGrid.Refresh();
            }
        }
    }
}

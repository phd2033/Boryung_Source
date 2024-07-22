using LGCNS.iPharmMES.Common;
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
    public partial class 간섭상황기록 : ShopFloorCustomWindow
    {
        public override string TableTypeName
        {
            get { return "TABLE,간섭상황기록"; }
        }
        public 간섭상황기록()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
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

        private void Module_Loaded(object sender, RoutedEventArgs e)
        {
            cbobModule.Items.Add("1 Module");
            cbobModule.Items.Add("2 Module");
            cbobModule.Items.Add("3 Module");
            cbobModule.Items.Add("4 Module");
            cbobModule.Items.Add("5 Module");
        }

    }
}

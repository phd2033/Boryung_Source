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
using LGCNS.iPharmMES.Common;


namespace 보령
{
    public partial class 설비액션목록 : ShopFloorCustomWindow
    {
        public 설비액션목록()
        {
            InitializeComponent();
        }

        public override string TableTypeName
        {
            get { return "TABLE,설비액션목록"; }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private async void txtSelEqptId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //(this.LayoutRoot.DataContext as 설비액션목록ViewModel).AddEqptListCommandAsync.Execute(txtSelEqptId.Text.ToUpper());
                // 비동기 방식으로 호출되서 아래 로직으로 변경. 위의 로직으로 할 경우 excute 순서가 뒤바껴서 값을 정상적으로 반영하지 못함.
                var viewModel = this.LayoutRoot.DataContext as 설비액션목록ViewModel;
                await viewModel.AddEqptGridList(txtSelEqptId.Text.ToUpper());

                txtSelEqptId.Text = string.Empty;

                //await (this.LayoutRoot.DataContext as 설비액션목록ViewModel).AddEqptGridList(txtSelEqptId.Text.ToUpper());
            }
        }
        private void GridRequestOutList_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            if ((sender as C1.Silverlight.DataGrid.C1DataGrid).SelectedItem != null)
                (this.LayoutRoot.DataContext as 설비액션목록ViewModel).RemoveEqptCommandAsync.Execute(null);
        }

        private async void btnSelectEqpt_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtSelEqptId.Text != null)
            {
                //(this.LayoutRoot.DataContext as 설비액션목록ViewModel).AddEqptListCommandAsync.Execute(txtSelEqptId.Text.ToUpper());
                // 비동기 방식으로 호출되서 아래 로직으로 변경. 위의 로직으로 할 경우 excute 순서가 뒤바껴서 값을 정상적으로 반영하지 못함.
                var viewModel = this.LayoutRoot.DataContext as 설비액션목록ViewModel;
                await viewModel.AddEqptGridList(txtSelEqptId.Text.ToUpper());
            }
        }
    }
}

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
using System.Windows.Data;

namespace 보령
{
    public partial class 설비액션기록 : iPharmMESChildWindow
    {
        private 설비액션목록ViewModel eqptList;

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
        private async void cmbList_DropDownOpened(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo == null) return;

            var vm = this.DataContext as 설비액션기록ViewModel; 
            var rowData = combo.DataContext as BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATA;

            if (combo.ItemsSource != null && combo.ItemsSource.Cast<object>().Any()) return;

            try
            {
                vm.BR_PHR_SEL_EQPACOMBOList.INDATAs.Clear();
                vm.BR_PHR_SEL_EQPACOMBOList.OUTDATAs.Clear();
                vm.BR_PHR_SEL_EQPACOMBOList.INDATAs.Add(new BR_PHR_SEL_EQPACOMBOList.INDATA
                {
                    BIZRULEID = rowData.BIZRULE,
                    LANGID = "ko-KR"
                });

                if (await vm.BR_PHR_SEL_EQPACOMBOList.Execute())
                {
                    var resultList = vm.BR_PHR_SEL_EQPACOMBOList.OUTDATAs
                                       .Select(x => x.DISPLAYVALUE).ToList();
                    combo.ItemsSource = resultList;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo == null) return;

            var rowData = combo.DataContext as BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATA;
            if (rowData == null) return;

            if (combo.SelectedItem != null)
            {
                rowData.EQPAINFO = combo.SelectedItem.ToString();
            }
        }
        private void dtpTime_DateTimeChanged(object sender, EventArgs e)
        {
            var dtp = sender as C1.Silverlight.DateTimeEditors.C1DateTimePicker;
            if (dtp == null || dtp.DateTime == null) return;

            var rowData = dtp.DataContext as BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATA;
            if (rowData == null) return;

            rowData.EQPAINFO = dtp.DateTime.Value.ToString("yyyy-MM-dd");
        }


    }
    public class DataTypeToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rowData = value as BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATA;
            if (rowData == null || parameter == null) return Visibility.Collapsed;

            string target = parameter.ToString();

            string eqpaType = rowData.EQPATYPE != null ? rowData.EQPATYPE.ToString().Trim() : "";

            if (eqpaType.Equals(target, StringComparison.OrdinalIgnoreCase))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class StateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = value as string;
            if (state == "Y")
            {
                return new SolidColorBrush(Color.FromArgb(255, 100, 220, 100)); 
            }
            return new SolidColorBrush(Color.FromArgb(255, 211, 211, 211)); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

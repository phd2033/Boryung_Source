using LGCNS.iPharmMES.Common;
using System;
using System.Windows.Input;
using System.Linq;
using System.Text;
using C1.Silverlight.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace Board
{
    public partial class OperationEquipmentCampain : UserControl

    {
        public OperationEquipmentCampain()
        {
            InitializeComponent();
            this.DataContext = new OperationEquipmentCampainViewModel();

            this.CampainOperationEquipmentGrid.SelectionChanged += (s, e) =>
            {
                var selectedItem = this.CampainOperationEquipmentGrid.SelectedItem;

                if (selectedItem != null)
                {
                    this.SlideUpAnimation.Stop();
                    this.SlideUpAnimation.Begin();

                    var vm = this.DataContext as OperationEquipmentCampainViewModel;

                    if (vm != null)
                    {
                        vm.ShowHistoryCommand.Execute(selectedItem);
                        this.CampainHistory.ItemsSource = null;
                        this.CampainHistory.ItemsSource = vm.BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY.OUTDATAs;
                    }
                }
            };
        }
        private async void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = this.DataContext as OperationEquipmentCampainViewModel;
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                var groupItem = textBox?.DataContext as GroupList;
                var groupName = textBox.Text;

                if (!string.IsNullOrWhiteSpace(groupName))
                {
                    groupName = groupName.Trim();

                    if (!string.IsNullOrWhiteSpace(groupName))
                    {
                        var bizRule = new BR_BRS_REG_SIMPLE_CLEAN_GROUP();

                        string operationMode;
                        if( groupItem != null && groupItem.IsEditing && groupItem.OriginalGroupName != "")
                        {
                            operationMode = "UPD";
                            bizRule.INDATAs.Add(new BR_BRS_REG_SIMPLE_CLEAN_GROUP.INDATA()
                            {
                                GROUPNAME = groupItem.OriginalGroupName,
                                NEW_GROUPNAME = groupName,
                                INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                MODE = operationMode
                            });
                        }
                        else
                        {
                            operationMode = "INS";
                            bizRule.INDATAs.Add(new BR_BRS_REG_SIMPLE_CLEAN_GROUP.INDATA()
                            {
                                GROUPNAME = groupName,
                                INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                MODE = operationMode
                            });
                        }

                        try
                        {
                            if (!await bizRule.Execute())
                            {
                                throw new Exception();
                            }
                            if (viewModel != null)
                            {
                                viewModel.LoadedCommand.Execute(null);
                                viewModel.GroupedRoutes = new ObservableCollection<RouteItem>();

                                if (this.RouteTreeView != null)
                                {
                                    this.RouteTreeView.ItemsSource = null;

                                    this.RouteTreeView.ItemsSource = viewModel.GroupedRoutes;
                                }
                                viewModel.LoadedCommand.Execute(null);
                            }
                            if (groupItem != null)
                            {
                                groupItem.OriginalGroupName = groupName;
                                groupItem.IsEditing = false;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("그룹 이름을 입력해주세요");
                }
            }
        }

        private void BtnCloseHistory_Click(object sender, RoutedEventArgs e)
        {
            this.SlideDownAnimation.Begin();
            CampainOperationEquipmentGrid.SelectedItem = null;
        }

    }
}

using C1.Silverlight.Excel;
using LGCNS.iPharmMES.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using Board.UserControls;
using System.Collections.ObjectModel;


namespace Board
{
    public class RouteItem 
    {

        public string GroupName { get; set; }
        public string OdName { get; set; }
        public ObservableCollection<RouteItem> Children { get; set; } = new ObservableCollection<RouteItem>();

    }

    public class GroupList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _originalGroupName;
        public string OriginalGroupName
        {
            get { return _originalGroupName; }
        }
        private string _groupName;
        public string GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                if (_groupName != value)
                {
                    if (string.IsNullOrEmpty(_originalGroupName))
                    {
                        _originalGroupName = value;
                    }
                    _groupName = value;
                    OnPropertyChanged("GroupName");
                }
            }
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get
            {
                return _isEditing;
            }
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    OnPropertyChanged("IsEditing");
                    OnPropertyChanged("ReadOnlyVisibility");
                    OnPropertyChanged("EditVisibility");
                }
            }
        }
        public Visibility ReadOnlyVisibility
        {
            get
            {
                return IsEditing ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        public Visibility EditVisibility
        {
            get
            {
                return IsEditing ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }

    public class OperationEquipmentCampainViewModel : ViewModelBase
    {
        #region property
        private OperationEquipmentCampain _mainWnd;

        private string _SelectedEquipmentCode;
        public string SelectedEquipmentCode
        {
            get { return _SelectedEquipmentCode; }
            set
            {
                _SelectedEquipmentCode = value;
                NotifyPropertyChanged();
            }
        }

        private string _ODNAME;
        public string ODNAME
        {
            get { return _ODNAME; }
            set
            {
                _ODNAME = value;
                NotifyPropertyChanged();
            }
        }

        private string _GROUPNAME;
        public string GROUPNAME
        {
            get { return _GROUPNAME; }
            set
            {
                _GROUPNAME = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<RouteItem> _groupedRoutes;
        public ObservableCollection<RouteItem> GroupedRoutes
        {
            get { return _groupedRoutes; }
            set
            {
                _groupedRoutes = value;
                OnPropertyChanged("GroupedRoutes");
            }
        }
        private ObservableCollection<GroupList> _Groupeds;
        public ObservableCollection<GroupList> Groupeds
        {
            get { return _Groupeds; }
            set
            {
                _Groupeds = value;
                OnPropertyChanged(nameof(Groupeds));
            }
        }
        private string _DHT_CNT;
        public string DHT_CNT
        {
            get { return _DHT_CNT; }
            set
            {
                _DHT_CNT = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Data

        private BR_PHR_SEL_MaterialClass_TreeView_New _BR_PHR_SEL_MaterialClass_TreeView_New;

        private BR_BRS_SEL_SIMPLE_CLEAN_GROUP _BR_BRS_SEL_SIMPLE_CLEAN_GROUP;
        public BR_BRS_SEL_SIMPLE_CLEAN_GROUP BR_BRS_SEL_SIMPLE_CLEAN_GROUP
        {
            get { return _BR_BRS_SEL_SIMPLE_CLEAN_GROUP; }
            set
            {
                _BR_BRS_SEL_SIMPLE_CLEAN_GROUP = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_SIMPLE_CLEAN_ROUTE _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE;
        public BR_BRS_SEL_SIMPLE_CLEAN_ROUTE BR_BRS_SEL_SIMPLE_CLEAN_ROUTE
        {
            get { return _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE; }
            set
            {
                _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_SIMPLE_CLEAN_EQPT _BR_BRS_SEL_SIMPLE_CLEAN_EQPT;
        public BR_BRS_SEL_SIMPLE_CLEAN_EQPT BR_BRS_SEL_SIMPLE_CLEAN_EQPT
        {
            get { return _BR_BRS_SEL_SIMPLE_CLEAN_EQPT; }
            set
            {
                _BR_BRS_SEL_SIMPLE_CLEAN_EQPT = value;
                NotifyPropertyChanged();
            }
        }
        
        private BR_BRS_REG_SIMPLE_CLEAN_GROUP _BR_BRS_REG_SIMPLE_CLEAN_GROUP;
        public BR_BRS_REG_SIMPLE_CLEAN_GROUP BR_BRS_REG_SIMPLE_CLEAN_GROUP
        {
            get { return _BR_BRS_REG_SIMPLE_CLEAN_GROUP; }
            set
            {
                _BR_BRS_REG_SIMPLE_CLEAN_GROUP = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Command

        public ICommand LoadedCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["LoadedCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["LoadedCommand"] = false;
                            CommandCanExecutes["LoadedCommand"] = false;

                            ///

                            //if (arg == null || !(arg is OperationEquipmentCampain))
                            //    return;
                            _mainWnd = arg as OperationEquipmentCampain;

                            _BR_BRS_SEL_SIMPLE_CLEAN_GROUP.OUTDATAs.Clear();

                            if (!await _BR_BRS_SEL_SIMPLE_CLEAN_GROUP.Execute()) throw new Exception();
                            var groupList = _BR_BRS_SEL_SIMPLE_CLEAN_GROUP.OUTDATAs
                                            .Select(item => new GroupList
                                            {
                                                GroupName = item.GROUPNAME
                                            });
                            Groupeds = new ObservableCollection<GroupList>(groupList);

                            CommandResults["LoadedCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["LoadedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadedCommand") ?
                        CommandCanExecutes["LoadedCommand"] : (CommandCanExecutes["LoadedCommand"] = true);
                });
            }
        }

        public ICommand BtnSearchCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["BtnSearchCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["BtnSearchCommand"] = false;
                            CommandCanExecutes["BtnSearchCommand"] = false;

                            BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.INDATAs.Clear();
                            BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.OUTDATAs.Clear();

                            BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.INDATAs.Add(new BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.INDATA()
                            {
                                MTRLID = SelectedEquipmentCode,
                                ODNAME = ODNAME,
                                ISDETAILED = true
                            });

                            if (!await BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.Execute()) throw new Exception();

                            var sourceData = BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.OUTDATAs;

                            if(sourceData != null)
                            {
                                var groupedData = sourceData.GroupBy(item => item.GROUPNAME).Select(group => new RouteItem
                                {
                                    GroupName = group.Key,
                                    OdName = group.Key,
                                    Children = new ObservableCollection<RouteItem>(
                                    group.Select(child => new RouteItem
                                    {
                                        GroupName = child.GROUPNAME,
                                        OdName = child.ODNAME
                                    })
                                  )
                                });
                                this.GroupedRoutes = new ObservableCollection<RouteItem>(groupedData);

                            }
                            CommandResults["BtnSearchCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["BtnSearchCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["BtnSearchCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("BtnSearchCommand") ?
                        CommandCanExecutes["BtnSearchCommand"] : (CommandCanExecutes["BtnSearchCommand"] = true);
                });
            }
        }

        public ICommand GroupDetailSearchCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    string groupName = arg as string;
                    if (!string.IsNullOrEmpty(groupName))
                    {
                        using (await AwaitableLocks["GroupDetailSearchCommand"].EnterAsync())
                        {
                            try
                            {
                                IsBusy = true;

                                CommandResults["GroupDetailSearchCommand"] = false;
                                CommandCanExecutes["GroupDetailSearchCommand"] = false;

                                ///
                                _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.INDATAs.Clear();
                                _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.OUTDATAs.Clear();

                                var guid = Groupeds.FirstOrDefault(item => item.GroupName == groupName);

                                if (guid != null)
                                {
                                    _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.INDATAs.Add(new BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.INDATA()
                                    {
                                        GROUPNAME = guid.GroupName ?? "",
                                        ISDETAILED = false
                                    });

                                    if (!await _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.Execute()) throw new Exception();

                                    var sourceData = _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.OUTDATAs;

                                    if (sourceData != null)
                                    {
                                        var groupedData = sourceData
                                            .GroupBy(item => item.GROUPNAME)
                                            .Select(group => new RouteItem
                                            {
                                                GroupName = group.Key,
                                                OdName = group.Key,
                                                Children = new ObservableCollection<RouteItem>(
                                                    group.Select(child => new RouteItem
                                                    {
                                                        GroupName = child.GROUPNAME,
                                                        OdName = child.ODNAME
                                                    })
                                                )
                                            });
                                        this.GroupedRoutes = new ObservableCollection<RouteItem>(groupedData);
                                    }

                                    var groupGuid = _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.OUTDATAs.FirstOrDefault();

                                    if(groupGuid != null)
                                    {
                                        _BR_BRS_SEL_SIMPLE_CLEAN_EQPT.INDATAs.Add(new BR_BRS_SEL_SIMPLE_CLEAN_EQPT.INDATA()
                                        {
                                            GROUPGUID = groupGuid.GROUPGUID
                                        });

                                        if (!await _BR_BRS_SEL_SIMPLE_CLEAN_EQPT.Execute()) throw new Exception();
                                    }
                                    else { _BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs.Clear(); }
                                }

                                CommandResults["GroupDetailSearchCommand"] = true;
                            }
                            catch (Exception ex)
                            {
                                CommandResults["GroupDetailSearchCommand"] = false;
                                OnException(ex.Message, ex);
                            }
                            finally
                            {
                                CommandCanExecutes["GroupDetailSearchCommand"] = true;
                                IsBusy = false;
                            }
                        }
                    }

                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("GroupDetailSearchCommand") ?
                        CommandCanExecutes["GroupDetailSearchCommand"] : (CommandCanExecutes["GroupDetailSearchCommand"] = true);
                });
            }
        }

        public ICommand RouteManagementCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    string groupName = arg as string;
                    if (groupName == null) return;
                    using (await AwaitableLocks["RouteManagementCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["RouteManagementCommand"] = false;
                            CommandCanExecutes["RouteManagementCommand"] = false;

                            SelectRoutePopup popup = new SelectRoutePopup();

                            popup.Closed += (s, e) =>
                            {
                                if (popup.DialogResult.GetValueOrDefault())
                                {

                                }
                            };

                            popup.Show();


                            CommandResults["RouteManagementCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RouteManagementCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RouteManagementCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RouteManagementCommand") ?
                        CommandCanExecutes["RouteManagementCommand"] : (CommandCanExecutes["RouteManagementCommand"] = true);
                });
            }
        }

        public ICommand EquipmentSearchCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    string GroupName = arg as string;
                    using (await AwaitableLocks["EquipmentSearchCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["EquipmentSearchCommand"] = false;
                            CommandCanExecutes["EquipmentSearchCommand"] = false;

                            ///
                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT.INDATAs.Clear();
                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs.Clear();

                            var groupGuid = BR_BRS_SEL_SIMPLE_CLEAN_ROUTE.OUTDATAs.FirstOrDefault();

                            if (groupGuid != null)
                            {
                                BR_BRS_SEL_SIMPLE_CLEAN_EQPT.INDATAs.Add(new BR_BRS_SEL_SIMPLE_CLEAN_EQPT.INDATA()
                                {
                                    GROUPGUID = groupGuid.GROUPGUID
                                });

                                if (!await BR_BRS_SEL_SIMPLE_CLEAN_EQPT.Execute()) throw new Exception();
                            }
                            else { BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs.Clear(); }

                            CommandResults["EquipmentSearchCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["EquipmentSearchCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["EquipmentSearchCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("EquipmentSearchCommand") ?
                        CommandCanExecutes["EquipmentSearchCommand"] : (CommandCanExecutes["EquipmentSearchCommand"] = true);
                });
            }
        }

        public ICommand CreateGroupCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["CreateGroupCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["CreateGroupCommand"] = false;
                            CommandCanExecutes["CreateGroupCommand"] = false;

                            var authHelper = new iPharmAuthCommandHelper();

                            authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "SM_SystemInfo_UI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("캠페인 그룹을 추가합니다."),
                                string.Format("캠페인 그룹 추가"),
                                false,
                                "SM_SystemInfo_UI",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            var newItem = new GroupList
                            {
                                GroupName = "",
                                IsEditing = true
                            };

                            Groupeds.Insert(0, newItem);

                            CommandResults["CreateGroupCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["CreateGroupCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["CreateGroupCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    bool canExecute = CommandCanExecutes.ContainsKey("CreateGroupCommand") ?
                        CommandCanExecutes["CreateGroupCommand"] : (CommandCanExecutes["CreateGroupCommand"] = true);
                    if (this.HasItemEditMode)
                    {
                        return false;
                    }
                    return canExecute;
                });
            }
        }

        public ICommand SaveGroupCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    string GroupName = arg as string;
                    using (await AwaitableLocks["SaveGroupCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["SaveGroupCommand"] = false;
                            CommandCanExecutes["SaveGroupCommand"] = false;

                            var newItem = new GroupList
                            {
                                GroupName = " ",
                                IsEditing = true
                            };

                            Groupeds.Insert(0, newItem);

                            CommandResults["SaveGroupCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SaveGroupCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["SaveGroupCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SaveGroupCommand") ?
                        CommandCanExecutes["SaveGroupCommand"] : (CommandCanExecutes["SaveGroupCommand"] = true);
                });
            }
        }
        public ICommand DeleteGroupCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    string GroupName = arg as string;
                    using (await AwaitableLocks["DeleteGroupCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            var authHelper = new iPharmAuthCommandHelper();

                            CommandResults["DeleteGroupCommand"] = false;
                            CommandCanExecutes["DeleteGroupCommand"] = false;

                            _BR_BRS_REG_SIMPLE_CLEAN_GROUP.INDATAs.Clear();

                            _BR_BRS_REG_SIMPLE_CLEAN_GROUP.INDATAs.Add(new BR_BRS_REG_SIMPLE_CLEAN_GROUP.INDATA
                            {
                                GROUPNAME = GroupName,
                                MODE = "DEL"
                            });

                            authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "SM_SystemInfo_UI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("캠페인 그룹을 삭제합니다."),
                                string.Format("캠페인 그룹 삭제"),
                                false,
                                "SM_SystemInfo_UI",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            await _BR_BRS_REG_SIMPLE_CLEAN_GROUP.Execute();
                            LoadedCommand.Execute(null);

                            CommandResults["DeleteGroupCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["DeleteGroupCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["DeleteGroupCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("DeleteGroupCommand") ?
                        CommandCanExecutes["DeleteGroupCommand"] : (CommandCanExecutes["DeleteGroupCommand"] = true);
                });
            }
        }
        public ICommand ChangeGroupNameCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    GroupList selectedGroup = arg as GroupList;
                    if (selectedGroup == null) return;
                    using (await AwaitableLocks["ChangeGroupNameCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            var authHelper = new iPharmAuthCommandHelper();

                            CommandResults["ChangeGroupNameCommand"] = false;
                            CommandCanExecutes["ChangeGroupNameCommand"] = false;

                                authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "SM_SystemInfo_UI");

                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    string.Format("캠페인 그룹 이름을 변경합니다."),
                                    string.Format("캠페인 그룹 이름 변경"),
                                    false,
                                    "SM_SystemInfo_UI",
                                    "", null, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                                selectedGroup.IsEditing = true;

                            CommandResults["ChangeGroupNameCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ChangeGroupNameCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ChangeGroupNameCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    bool canExecute = CommandCanExecutes.ContainsKey("ChangeGroupNameCommand") ?
                        CommandCanExecutes["ChangeGroupNameCommand"] : (CommandCanExecutes["ChangeGroupNameCommand"] = true);
                    if (this.HasItemEditMode)
                    {
                        return false;
                    }
                    return canExecute;
                });
            }
        }


        #endregion

        public OperationEquipmentCampainViewModel()
        {
            GroupedRoutes = new ObservableCollection<RouteItem>();
            Groupeds = new ObservableCollection<GroupList>();
            _BR_PHR_SEL_MaterialClass_TreeView_New = new BR_PHR_SEL_MaterialClass_TreeView_New();
            _BR_BRS_SEL_SIMPLE_CLEAN_GROUP = new BR_BRS_SEL_SIMPLE_CLEAN_GROUP();
            _BR_BRS_SEL_SIMPLE_CLEAN_ROUTE = new BR_BRS_SEL_SIMPLE_CLEAN_ROUTE();
            _BR_BRS_SEL_SIMPLE_CLEAN_EQPT = new BR_BRS_SEL_SIMPLE_CLEAN_EQPT();
            _BR_BRS_REG_SIMPLE_CLEAN_GROUP = new BR_BRS_REG_SIMPLE_CLEAN_GROUP();
        }
        public bool HasItemEditMode
        {
            get
            {
                return Groupeds.Any(item => item.IsEditing == true);
            }
        }

    }
}

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
        public string OdId { get; set; }
        //public string OdVer { get; set; }
        public string RouteGuid { get; set; }
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
            set
            {
                if (_originalGroupName != value)
                {
                        _originalGroupName = value;
                        OnPropertyChanged("OriginalGroupName");
                }
            }
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

        private string _GROUPGUID;
        public string GROUPGUID
        {
            get { return _GROUPGUID; }
            set
            {
                _GROUPGUID = value;
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

        private string _COMMENTS;
        public string COMMENTS
        {
            get { return _COMMENTS; }
            set
            {
                _COMMENTS = value;
                NotifyPropertyChanged();
            }
        }

        private string _CHK;
        public string CHK
        {
            get { return _CHK; }
            set
            {
                _CHK = value;
                OnPropertyChanged("CHK");
            }
        }

        private void EqptItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SIMPLE_CLEAN_CNT" || e.PropertyName == "DHT_CNT" || e.PropertyName == "ISUSE")
            {
                CheckFieldDataChangedLogic(sender);
            }
        }

        public event EventHandler RequestShowEqptPopup;

        #endregion

        #region BizRule

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

        private BR_BRS_REG_SIMPLE_CLEAN_ROUTE _BR_BRS_REG_SIMPLE_CLEAN_ROUTE;
        public BR_BRS_REG_SIMPLE_CLEAN_ROUTE BR_BRS_REG_SIMPLE_CLEAN_ROUTE
        {
            get { return _BR_BRS_REG_SIMPLE_CLEAN_ROUTE; }
            set
            {
                _BR_BRS_REG_SIMPLE_CLEAN_ROUTE = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK;
        public BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK
        {
            get { return _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK; }
            set
            {
                _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_REG_SIMPLE_CLEAN_EQPT _BR_BRS_REG_SIMPLE_CLEAN_EQPT;
        public BR_BRS_REG_SIMPLE_CLEAN_EQPT BR_BRS_REG_SIMPLE_CLEAN_EQPT
        {
            get { return _BR_BRS_REG_SIMPLE_CLEAN_EQPT; }
            set
            {
                _BR_BRS_REG_SIMPLE_CLEAN_EQPT = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY;
        public BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY
        {
            get { return _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY; }
            set
            {
                _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY = value;
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
                                    OdId = group.Key,
                                    OdName = group.Key,
                                    RouteGuid = group.Key,
                                    Children = new ObservableCollection<RouteItem>(
                                    group.Select(child => new RouteItem
                                    {
                                        GroupName = child.GROUPNAME,
                                        OdId = child.ODID,
                                        OdName = child.ODNAME,
                                        RouteGuid = child.ROUTEGUID,
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
                                _BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATAs.Clear();
                                _BR_BRS_REG_SIMPLE_CLEAN_ROUTE.OUTDATAs.Clear();

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
                                        var groupedData = sourceData.GroupBy(item => item.GROUPNAME).Select(group => new RouteItem
                                        {
                                            GroupName = group.Key,
                                            OdId = group.Key,
                                            OdName = group.Key,
                                            RouteGuid = group.Key,
                                            Children = new ObservableCollection<RouteItem>(
                                            group.Select(child => new RouteItem
                                            {
                                                GroupName = child.GROUPNAME,
                                                OdId = child.ODID,
                                                OdName = child.ODNAME,
                                                RouteGuid = child.ROUTEGUID,
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

                                        if (BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs != null)
                                        {
                                            foreach (var item in BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs)
                                            {
                                                item.PropertyChanged -= EqptItem_PropertyChanged;
                                                item.PropertyChanged += EqptItem_PropertyChanged;
                                            }
                                        }
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

                            var popupViewModel = new SelectRoutePopupViewModel(groupName);

                            SelectRoutePopup popup = new SelectRoutePopup();

                            popup.DataContext = popupViewModel;

                            popup.Closed += (s, e) =>
                            {
                                GroupDetailSearchCommand.Execute(groupName);
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

                                if(BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs != null)
                                {
                                    foreach(var item in BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs)
                                    {
                                        item.PropertyChanged -= EqptItem_PropertyChanged;
                                        item.PropertyChanged += EqptItem_PropertyChanged;
                                    }
                                }
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
        public ICommand DeleteRouteCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    var selectedRoute = arg as RouteItem;
                    using (await AwaitableLocks["DeleteRouteCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            var authHelper = new iPharmAuthCommandHelper();

                            CommandResults["DeleteRouteCommand"] = false;
                            CommandCanExecutes["DeleteRouteCommand"] = false;

                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATAs.Clear();
                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.OUTDATAs.Clear();


                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATAs.Add(new BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATA
                            {
                                ROUTEGUID = selectedRoute.RouteGuid,
                                GROUPNAME = selectedRoute.GroupName,
                                MODE = "DEL"
                            });

                            authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "SM_SystemInfo_UI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("캠페인 라우트를 삭제합니다."),
                                string.Format("캠페인 라우트 삭제"),
                                false,
                                "SM_SystemInfo_UI",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            await BR_BRS_REG_SIMPLE_CLEAN_ROUTE.Execute();
                            GroupDetailSearchCommand.Execute(selectedRoute.GroupName);

                            CommandResults["DeleteRouteCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["DeleteRouteCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["DeleteRouteCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("DeleteRouteCommand") ?
                        CommandCanExecutes["DeleteRouteCommand"] : (CommandCanExecutes["DeleteRouteCommand"] = true);
                });
            }
        }
        public ICommand RowEditCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RowEditCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["RowEditCommand"] = false;
                            CommandCanExecutes["RowEditCommand"] = false;

                            IsBusy = true;

                            var temp = _mainWnd.CampainOperationEquipmentGrid.SelectedItem as BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATA;
                            if (temp.DHT_CNT != "0" || temp.SIMPLE_CLEAN_CNT != "0" || temp.EQPTID != null)
                            {
                                temp.CHK = "Y";
                            }

                            var groupGuid = BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs.FirstOrDefault(outdat => outdat.GROUPGUID != null)?.GROUPGUID;

                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK.INDATAs.Clear();
                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK.OUTDATAs.Clear();

                            if (temp.EQPTID != null && _mainWnd.CampainOperationEquipmentGrid.Columns[1].IsReadOnly == false)
                            {
                                BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK.INDATAs.Add(new BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK.INDATA
                                {
                                    EQPTID = temp.EQPTID,
                                    GROUPGUID = groupGuid
                                });

                                await BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK.Execute();

                                foreach (var outdata in BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK.OUTDATAs)
                                {
                                    if (outdata.EQPTNAME != null)
                                    {
                                        temp.GROUPGUID = groupGuid;
                                        temp.EQPTID = temp.EQPTID.ToUpper();
                                        temp.EQPTNAME = outdata.EQPTNAME;
                                        temp.VERSION = "1.000000";
                                        temp.SIMPLE_CLEAN_CNT = "0";
                                        temp.DHT_CNT = "0";
                                        temp.INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID;

                                        _mainWnd.CampainOperationEquipmentGrid.Columns[1].IsReadOnly = true;
                                        _mainWnd.CampainOperationEquipmentGrid.Columns[2].IsReadOnly = true;
                                    }
                                }
                            }
                      
                            IsBusy = false;

                            CommandResults["RowEditCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RowEditCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RowEditCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RowEditCommand") ?
                        CommandCanExecutes["RowEditCommand"] : (CommandCanExecutes["RowEditCommand"] = true);
                });
            }
        }
        public ICommand RowAddCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RowAddCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["RowAddCommand"] = false;
                            CommandCanExecutes["RowAddCommand"] = false;

                            IsBusy = true;

                            var temp = _mainWnd.CampainOperationEquipmentGrid.SelectedItem as BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATA;
                            temp.CHK = "Y";
                            temp.ISUSE = "Y";
                            _mainWnd.CampainOperationEquipmentGrid.Columns[2].IsReadOnly = true;

                            RequestShowEqptPopup?.Invoke(this, EventArgs.Empty);

                            IsBusy = false;

                            CommandResults["RowAddCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RowAddCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RowAddCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RowAddCommand") ?
                        CommandCanExecutes["RowAddCommand"] : (CommandCanExecutes["RowAddCommand"] = true);
                });
            }
        }
        public ICommand EquipmentSaveCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["EquipmentSaveCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            var authHelper = new iPharmAuthCommandHelper();

                            CommandResults["EquipmentSaveCommand"] = false;
                            CommandCanExecutes["EquipmentSaveCommand"] = false;

                            var checkFlag = false;

                            string groupName = null;

                            foreach (var item in BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATAs)
                            {
                                if (groupName == null)
                                {
                                    groupName = item.GROUPNAME;
                                }
                                if(item.CHK == "Y" && item != null)
                                {
                                    checkFlag = true;
                                    BR_BRS_REG_SIMPLE_CLEAN_EQPT.INDATAs.Add(new BR_BRS_REG_SIMPLE_CLEAN_EQPT.INDATA
                                    {   
                                        GROUPGUID = item.GROUPGUID,
                                        EQPTID = item.EQPTID,
                                        EQPTNAME = item.EQPTNAME,
                                        VERSION = item.VERSION,
                                        SIMPLE_CLEAN_CNT = Convert.ToInt16(item.SIMPLE_CLEAN_CNT),
                                        DHT_CNT = Convert.ToInt16(item.DHT_CNT),
                                        COMMENTS = item.COMMENTS,
                                        INSUSER = item.INSUSER,
                                        ISUSE = item.ISUSE,
                                        MODE = item.RowEditSec
                                    });
                                }           
                            }
                            if (checkFlag)
                            {
                                authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "SM_SystemInfo_UI");
                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    string.Format("캠페인 라우트 설비 정보를 저장합니다."),
                                    string.Format("캠페인 라우트 설비 저장"),
                                    false,
                                    "SM_SystemInfo_UI",
                                    "", null, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                                await BR_BRS_REG_SIMPLE_CLEAN_EQPT.Execute();
                            }

                            GroupDetailSearchCommand.Execute(groupName);

                            CommandResults["EquipmentSaveCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["EquipmentSaveCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["EquipmentSaveCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("EquipmentSaveCommand") ?
                        CommandCanExecutes["EquipmentSaveCommand"] : (CommandCanExecutes["EquipmentSaveCommand"] = true);
                });
            }
        }
        public ICommand ShowHistoryCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    var selectedData = arg as BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATA;
                    if (selectedData == null) return;
                    using (await AwaitableLocks["ShowHistoryCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ShowHistoryCommand"] = false;
                            CommandCanExecutes["ShowHistoryCommand"] = false;

                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY.INDATAs.Clear();
                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY.OUTDATAs.Clear();

                            BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY.INDATAs.Add(new BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY.INDATA
                            {
                                GROUPGUID = selectedData.GROUPGUID,
                                EQPTID = selectedData.EQPTID
                            });

                            if (await BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY.Execute())
                            {
                            }

                            CommandResults["ShowHistoryCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ShowHistoryCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ShowHistoryCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ShowHistoryCommand") ?
                        CommandCanExecutes["ShowHistoryCommand"] : (CommandCanExecutes["ShowHistoryCommand"] = true);
                });
            }
        }
        private void CheckFieldDataChangedLogic(object changedItem)
        {
            var temp = changedItem as BR_BRS_SEL_SIMPLE_CLEAN_EQPT.OUTDATA;

            if (temp != null)
            {
                try
                {
                    temp.CHK = "Y";
                }
                catch (Exception ex)
                {
                    OnException(ex.Message, ex);
                }
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
            _BR_BRS_REG_SIMPLE_CLEAN_ROUTE = new BR_BRS_REG_SIMPLE_CLEAN_ROUTE();
            _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK = new BR_BRS_SEL_SIMPLE_CLEAN_EQPT_CHECK();
            _BR_BRS_REG_SIMPLE_CLEAN_EQPT = new BR_BRS_REG_SIMPLE_CLEAN_EQPT();
            _BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY = new BR_BRS_SEL_SIMPLE_CLEAN_EQPT_HISTORY();
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

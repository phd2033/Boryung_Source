using LGCNS.iPharmMES.Common;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Board.UserControls
{
    public class MaterialGroupViewModel
    {
        public string MTRLNAME { get; set; } // 상위 항목 이름
        public string MTRLID { get; set; }
        public List<OperationItemViewModel> OperationDefinitions { get; set; } // 하위 항목 컬렉션
    }

    public class OperationItemViewModel : ViewModelBase
    {
        private readonly BR_PHR_SEL_OperationDefinition_MaterialList_CPY.OUTDATA _operationData;
        private bool _isSelected;

        public OperationItemViewModel(BR_PHR_SEL_OperationDefinition_MaterialList_CPY.OUTDATA data)
        {
            _operationData = data;
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MTODSTAT => _operationData.MTODSTAT;
        public string ODNAME => _operationData.ODNAME;
        public string STATCOLOR => _operationData.STATCOLOR;
        public string ODID => _operationData.ODID;
        public decimal VERSION => (decimal)_operationData.VERSION;
        public string MTRLID => _operationData.MTRLID;
        public string MTRLNAME => _operationData.MTRLNAME;
    }

    public class SelectRoutePopupViewModel : ViewModelBase
    {
        #region property
        private string _MTRLNAME;
        public string MTRLNAME
        {
            get { return _MTRLNAME; }
            set
            {
                _MTRLNAME = value;
                NotifyPropertyChanged();
            }
        }
        private string _MaterialName;
        public string MaterialName
        {
            get { return _MaterialName; }
            set
            {
                _MaterialName = value;
                NotifyPropertyChanged();
            }
        }

        private string _GroupName;
        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                _GroupName = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _IsSelected;
        public Boolean IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                NotifyPropertyChanged();
            }
        }

        public SelectRoutePopupViewModel(string groupName)
        {
            GroupName = groupName;
            _BR_PHR_SEL_MaterialClass_TreeView_New = new BR_PHR_SEL_MaterialClass_TreeView_New();
            _BR_PHR_SEL_OperationDefinition_MaterialList_CPY = new BR_PHR_SEL_OperationDefinition_MaterialList_CPY();
            _BR_BRS_REG_SIMPLE_CLEAN_ROUTE = new BR_BRS_REG_SIMPLE_CLEAN_ROUTE();
        }


        private ObservableCollection<MaterialGroupViewModel> _groupedMaterials;
        public ObservableCollection<MaterialGroupViewModel> GroupedMaterials
        {
            get { return _groupedMaterials; }
            set
            {
                _groupedMaterials = value;
                OnPropertyChanged("GroupedMaterials");
            }
        }

        //public SelectRoutePopupViewModel()
        //{
        //    _BR_PHR_SEL_MaterialClass_TreeView_New = new BR_PHR_SEL_MaterialClass_TreeView_New();
        //    _BR_PHR_SEL_OperationDefinition_MaterialList_CPY = new BR_PHR_SEL_OperationDefinition_MaterialList_CPY();
        //}

        #endregion
        #region bizrule
        private BR_PHR_SEL_MaterialClass_TreeView_New _BR_PHR_SEL_MaterialClass_TreeView_New;
        public BR_PHR_SEL_MaterialClass_TreeView_New BR_PHR_SEL_MaterialClass_TreeView_New
        {
            get { return _BR_PHR_SEL_MaterialClass_TreeView_New; }
            set
            {
                _BR_PHR_SEL_MaterialClass_TreeView_New = value;
                NotifyPropertyChanged();
            }
        }
        private BR_PHR_SEL_OperationDefinition_MaterialList_CPY _BR_PHR_SEL_OperationDefinition_MaterialList_CPY;
        public BR_PHR_SEL_OperationDefinition_MaterialList_CPY BR_PHR_SEL_OperationDefinition_MaterialList_CPY
        {
            get { return _BR_PHR_SEL_OperationDefinition_MaterialList_CPY; }
            set
            {
                _BR_PHR_SEL_OperationDefinition_MaterialList_CPY = value;
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
        #endregion
        #region command
        public ICommand LoadedCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["LoadedCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["LoadedCommandAsync"] = false;
                            CommandCanExecutes["LoadedCommandAsync"] = false;

                            BR_PHR_SEL_MaterialClass_TreeView_New.INDATAs.Clear();
                            BR_PHR_SEL_MaterialClass_TreeView_New.OUTDATAs.Clear();

                            BR_PHR_SEL_MaterialClass_TreeView_New.INDATAs.Add(new BR_PHR_SEL_MaterialClass_TreeView_New.INDATA()
                            {
                                ISUSE = "Y"
                            });

                            if (!await BR_PHR_SEL_MaterialClass_TreeView_New.Execute()) throw new Exception();


                            CommandResults["LoadedCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["LoadedCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadedCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
               {
                   return CommandCanExecutes.ContainsKey("LoadedCommandAsync") ?
                       CommandCanExecutes["LoadedCommandAsync"] : (CommandCanExecutes["LoadedCommandAsync"] = true);
               });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    string mtclidString = arg as string;
                    //if (!string.IsNullOrEmpty(mtclidString))
                    //{
                        using (await AwaitableLocks["SearchCommand"].EnterAsync())
                        {
                            try
                            {
                                IsBusy = true;

                                CommandResults["SearchCommand"] = false;
                                CommandCanExecutes["SearchCommand"] = false;

                                _BR_PHR_SEL_OperationDefinition_MaterialList_CPY.INDATAs.Clear();
                                _BR_PHR_SEL_OperationDefinition_MaterialList_CPY.OUTDATAs.Clear();


                            var authHelper = new iPharmAuthCommandHelper();

                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATAs.Clear();
                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.OUTDATAs.Clear();

                            BR_PHR_SEL_OperationDefinition_MaterialList_CPY.INDATAs.Add(new BR_PHR_SEL_OperationDefinition_MaterialList_CPY.INDATA()
                                {
                                    FILTERTYPE_NAME = "C",
                                    MTRLNAME = _MaterialName,
                                    MTCLID = mtclidString
                                });

                                if (!await BR_PHR_SEL_OperationDefinition_MaterialList_CPY.Execute()) throw new Exception();

                                var sourceData = _BR_PHR_SEL_OperationDefinition_MaterialList_CPY.OUTDATAs;

                                if(sourceData != null)
                                {
                                    var groupedData = sourceData
                                        .GroupBy(item => new { item.MTRLNAME, item.MTRLID })
                                        .Select(group => new MaterialGroupViewModel
                                        {
                                            MTRLNAME = group.Key.MTRLNAME,
                                            MTRLID = group.Key.MTRLID,
                                            //OperationDefinitions = group.ToList()
                                            OperationDefinitions = group.Select(outData => new OperationItemViewModel(outData)).ToList()
                                        });

                                    this.GroupedMaterials = new ObservableCollection<MaterialGroupViewModel>(groupedData);
                                }

                                CommandResults["SearchCommand"] = true;
                            }
                            catch (Exception ex)
                            {
                                CommandResults["SearchCommand"] = false;
                                OnException(ex.Message, ex);
                            }
                            finally
                            {
                                CommandCanExecutes["SearchCommand"] = true;

                                IsBusy = false;

                            }
                        }
                    //}

                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SearchCommand") ?
                        CommandCanExecutes["SearchCommand"] : (CommandCanExecutes["SearchCommand"] = true);
                });
            }
        }

        public ICommand AddRouteCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["AddRouteCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["AddRouteCommand"] = false;
                            CommandCanExecutes["AddRouteCommand"] = false;

                            var selectedOperations = GroupedMaterials.SelectMany(group => group.OperationDefinitions).Where(itemViewModel => itemViewModel.IsSelected).ToList();

                            var authHelper = new iPharmAuthCommandHelper();

                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATAs.Clear();
                            BR_BRS_REG_SIMPLE_CLEAN_ROUTE.OUTDATAs.Clear();

                            authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "SM_SystemInfo_UI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("캠페인 라우트를 등록합니다."),
                                string.Format("캠페인 라우트 등록"),
                                false,
                                "SM_SystemInfo_UI",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            foreach (var item in selectedOperations)
                            {
                                BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATAs.Add(new BR_BRS_REG_SIMPLE_CLEAN_ROUTE.INDATA
                                {
                                    MTRLID = item.MTRLID,
                                    MTRLNAME = item.MTRLNAME,
                                    MTODSTAT = item.MTODSTAT,
                                    ODID = item.ODID,
                                    ODNAME = item.ODNAME,
                                    ODVER = item.VERSION,
                                    GROUPNAME = GroupName,
                                    INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                    MODE = "INS"

                                });
                            }
                            if (await BR_BRS_REG_SIMPLE_CLEAN_ROUTE.Execute())
                            {
                                MessageBox.Show("라우트가 추가되었습니다.");
                            };

                            CommandResults["AddRouteCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["AddRouteCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["AddRouteCommand"] = true;
                            IsBusy = false;

                        }
                    }
                    //}

                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("AddRouteCommand") ?
                        CommandCanExecutes["AddRouteCommand"] : (CommandCanExecutes["AddRouteCommand"] = true);
                });
            }
        }

        #endregion
    }
}


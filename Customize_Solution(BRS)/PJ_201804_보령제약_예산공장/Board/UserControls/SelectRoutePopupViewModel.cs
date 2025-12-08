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
        public List<BR_PHR_SEL_OperationDefinition_MaterialList_CPY.OUTDATA> OperationDefinitions { get; set; } // 하위 항목 컬렉션
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

        private string _GroupName2;
        public string GroupName2
        {
            get { return _GroupName2; }
            set
            {
                _GroupName2 = value;
                NotifyPropertyChanged();
            }
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

        public SelectRoutePopupViewModel()
        {
            _BR_PHR_SEL_MaterialClass_TreeView_New = new BR_PHR_SEL_MaterialClass_TreeView_New();
            _BR_PHR_SEL_OperationDefinition_MaterialList_CPY = new BR_PHR_SEL_OperationDefinition_MaterialList_CPY();
        }

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

                            _BR_PHR_SEL_MaterialClass_TreeView_New.INDATAs.Clear();
                            _BR_PHR_SEL_MaterialClass_TreeView_New.OUTDATAs.Clear();

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
                                            OperationDefinitions = group.ToList()
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

        #endregion
    }
}


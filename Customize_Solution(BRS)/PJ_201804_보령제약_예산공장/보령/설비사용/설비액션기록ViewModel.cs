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
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
using System.Collections.ObjectModel;
using System.Linq;
using C1.Silverlight.Data;
using System.Text;
using System.Collections.Generic;
using 보령.UserControls;

namespace 보령
{
    //[ShopFloorCustomHidden]
    public class 설비액션기록ViewModel : ViewModelBase
    {
        #region [Property]
        public 설비액션기록ViewModel(ObservableCollection<설비액션목록ViewModel.EQPTInfo> data)
        {
            _BR_PHR_SEL_System_Printer = new BR_PHR_SEL_System_Printer();
            _BR_PHR_SEL_Equipment_GetInfo = new BR_PHR_SEL_Equipment_GetInfo();
            _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus = new BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus();
            _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID = new BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID();
            _BR_PHR_SEL_EquipmentClass_Parent = new 보령.BR_PHR_SEL_EquipmentClass_Parent();
            _BR_PHR_SEL_EQPACOMBOList = new BR_PHR_SEL_EQPACOMBOList();
            _BR_PHR_SEL_EquipmentClassAction_Multi = new BR_PHR_SEL_EquipmentClassAction_Multi();
            _BR_PHR_UPD_EquipmentAction_Multi = new BR_PHR_UPD_EquipmentAction_Multi();

            _EqptList = new ObservableCollection<설비액션목록ViewModel.EQPTInfo>();
            _ActionList = new ObservableCollection<ActionContainer>();

            EqptList = data;
        }

        설비액션기록 _mainWnd;    
   
        private BR_PHR_SEL_System_Printer.OUTDATA _selectedPrint;

        public string curPrintName
        {
            get
            {
                if (_selectedPrint != null)
                    return _selectedPrint.PRINTERNAME;
                else
                    return "N/A";
            }
        }

        private string _EqptId;
        public string EqptId
        {
            get { return _EqptId; }
            set
            {
                _EqptId = value;
                OnPropertyChanged("EqptId");
            }
        }

        private string _ACTVAL;
        public string ACTVAL
        {
            get { return _ACTVAL; }
            set
            {
                _ACTVAL = value;
                OnPropertyChanged("ACTVAL");
            }
        }

        private string _EQPAINFO;
        public string EQPAINFO
        {
            get { return _EQPAINFO; }
            set
            {
                _EQPAINFO = value;
                OnPropertyChanged("EQPAINFO");
            }
        }

        private DateTime? _ACTVAL_DT;
        public DateTime? ACTVAL_DT
        {
            get
            {
                if (string.IsNullOrEmpty(this.ACTVAL)) return null;
                DateTime dt;

                if (DateTime.TryParse(this.ACTVAL, out dt))
                {
                    return dt;
                }

                return null;
            }
            set
            {
                _ACTVAL_DT = value;

                if (value != null && value.HasValue)
                {
                    this.ACTVAL = value.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    this.ACTVAL = string.Empty;
                }

                OnPropertyChanged("ACTVAL_DT");
                OnPropertyChanged("ACTVAL");
            }
        }

        private bool _btnRecordEnable;
        public bool btnRecordEnable
        {
            get { return _btnRecordEnable; }
            set
            {
                _btnRecordEnable = value;
                OnPropertyChanged("btnRecordEnable");
            }
        }
        
        private ObservableCollection<설비액션목록ViewModel.EQPTInfo> _EqptList;
        public ObservableCollection<설비액션목록ViewModel.EQPTInfo> EqptList
        {
            get { return _EqptList; }
            set
            {
                _EqptList = value;
                OnPropertyChanged("EqptList");
            }
        }

        private ObservableCollection<ActionContainer> _ActionList;
        public ObservableCollection<ActionContainer> ActionList
        {
            get { return _ActionList; }
            set
            {
                _ActionList = value;
                OnPropertyChanged("ActionList");
            }
        }

        private ActionContainer _selectedAction;
        public ActionContainer selectedAction
        {
            get { return _selectedAction; }
            set
            {
                _selectedAction = value;
                OnPropertyChanged("selectedAction");
            }
        }


        private string _EQPATYPE;
        public string EQPATYPE
        {
            get { return _EQPATYPE; }
            set
            {
                _EQPATYPE = value;
                OnPropertyChanged("EqptId");
            }
        }

        private List<string> _LIST_DATA;
        public List<string> LIST_DATA
        {
            get { return _LIST_DATA; }
            set
            {
                _LIST_DATA = value;
                OnPropertyChanged("LIST_DATA");
            }
        }

        private string _EQPTNAME;
        public string EQPTNAME
        {
            get { return _EQPTNAME; }
            set
            {
                _EQPTNAME = value;
                OnPropertyChanged("EQPTNAME");
            }
        }

        private string _EQPTID;
        public string EQPTID
        {
            get { return _EQPTID; }
            set
            {
                _EQPTID = value;
                OnPropertyChanged("EQPTID");
            }
        }

        private string _EQACSTATE;
        public string EQACSTATE  
        {
            get { return _EQACSTATE; }
            set
            {
                _EQACSTATE = value;
                OnPropertyChanged("EQACSTATE");
            }
        }

        #endregion

        #region [BizRule]
        /// <summary>
        /// 작업장 프린터 조회
        /// </summary>
        private BR_PHR_SEL_System_Printer _BR_PHR_SEL_System_Printer;
        
        /// <summary>
        /// 설비 정보 조회
        /// </summary>
        BR_PHR_SEL_Equipment_GetInfo _BR_PHR_SEL_Equipment_GetInfo;
        public BR_PHR_SEL_Equipment_GetInfo BR_PHR_SEL_Equipment_GetInfo
        {
            get { return _BR_PHR_SEL_Equipment_GetInfo; }
            set
            {
                _BR_PHR_SEL_Equipment_GetInfo = value;
            }
        }

        BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus;
        public BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus
        {
            get { return _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus; }
            set
            {
                _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus = value;
            }
        }

        BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID;
        public BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID
        {
            get { return _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID; }
            set
            {
                _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID = value;
            }
        }

        BR_PHR_SEL_EquipmentClass_Parent _BR_PHR_SEL_EquipmentClass_Parent;
        public BR_PHR_SEL_EquipmentClass_Parent BR_PHR_SEL_EquipmentClass_Parent
        {
            get { return _BR_PHR_SEL_EquipmentClass_Parent; }
            set
            {
                _BR_PHR_SEL_EquipmentClass_Parent = value;
            }
        }

        BR_PHR_SEL_EQPACOMBOList _BR_PHR_SEL_EQPACOMBOList;
        public BR_PHR_SEL_EQPACOMBOList BR_PHR_SEL_EQPACOMBOList
        {
            get { return _BR_PHR_SEL_EQPACOMBOList; }
            set
            {
                _BR_PHR_SEL_EQPACOMBOList = value;
            }
        }

        BR_PHR_SEL_EquipmentClassAction_Multi _BR_PHR_SEL_EquipmentClassAction_Multi;
        public BR_PHR_SEL_EquipmentClassAction_Multi BR_PHR_SEL_EquipmentClassAction_Multi
        {
            get { return _BR_PHR_SEL_EquipmentClassAction_Multi; }
            set
            {
                _BR_PHR_SEL_EquipmentClassAction_Multi = value;
            }
        }


        BR_PHR_UPD_EquipmentAction_Multi _BR_PHR_UPD_EquipmentAction_Multi;
        public BR_PHR_UPD_EquipmentAction_Multi BR_PHR_UPD_EquipmentAction_Multi
        {
            get { return _BR_PHR_UPD_EquipmentAction_Multi; }
            set
            {
                _BR_PHR_UPD_EquipmentAction_Multi = value;
            }
        }
        
        #endregion

        #region [Command]
        public ICommand LoadedCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    try
                    {
                        CommandCanExecutes["LoadedCommandAsync"] = false;
                        CommandResults["LoadedCommandAsync"] = false;

                        ///
                        IsBusy = true;

                        if (arg != null && arg is 설비액션기록)
                        {
                            _mainWnd = arg as 설비액션기록;

                            // 프린터 설정
                            _BR_PHR_SEL_System_Printer.INDATAs.Add(new BR_PHR_SEL_System_Printer.INDATA
                            {
                                LANGID = AuthRepositoryViewModel.Instance.LangID,
                                ROOMID = AuthRepositoryViewModel.Instance.RoomID,
                                IPADDRESS = Common.ClientIP
                            });
                            if (await _BR_PHR_SEL_System_Printer.Execute() && _BR_PHR_SEL_System_Printer.OUTDATAs.Count > 0)
                            {
                                _selectedPrint = _BR_PHR_SEL_System_Printer.OUTDATAs[0];
                                OnPropertyChanged("curPrintName");
                            }

                            if(_selectedPrint == null)
                            {
                                OnMessage("프린터 설정이 되지 않았습니다. 프린터를 설정해주세요");
                            }

                            //입력된 설비군의 공통 부모 설비 클래스 조회
                            _BR_PHR_SEL_EquipmentClass_Parent.INDATAs.Clear();
                            _BR_PHR_SEL_EquipmentClass_Parent.OUTDATAs.Clear();

                            foreach(var item in EqptList)
                            {
                                _BR_PHR_SEL_EquipmentClass_Parent.INDATAs.Add(new BR_PHR_SEL_EquipmentClass_Parent.INDATA
                                {
                                    EQPTID = item.EQPTID
                                });
                            }

                            if (await BR_PHR_SEL_EquipmentClass_Parent.Execute() && BR_PHR_SEL_EquipmentClass_Parent.OUTDATAs.Count > 0)
                            {
                                //액션 리스트 조회
                                _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.INDATAs.Clear();
                                _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.OUTDATAs.Clear();

                                _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.INDATAs.Add(new BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.INDATA
                                {
                                    EQCLID = BR_PHR_SEL_EquipmentClass_Parent.OUTDATAs[0].EQCLID,
                                    EQCLIUSE = "Y"
                                });
                                // 액션 리스트 조회
                                if(await _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.Execute() && _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.OUTDATAs.Count > 0)
                                {
                                    foreach (var item in _BR_PHR_SEL_EquipmentClassAction_Parent_ActionStatus.OUTDATAs)
                                    {
                                        ActionList.Add(new ActionContainer
                                        {
                                            EqclName = item.EQCLNAME,
                                            EqacName = item.EQACNAME,
                                            EqacId = item.EQACID,
                                            EqacDesc = item.EQACDESC,
                                            EqacIuse = item.EQACIUSE

                                        });
                                    }

                                    OnPropertyChanged("ActionList");
                                }
                                else
                                {
                                    IsBusy = false;
                                    OnMessage("조회 된 액션 목록이 없습니다");
                                    return;
                                }
                            }
                            else
                            {
                                IsBusy = false;
                                OnMessage("설비군이 조회되지 않았습니다.");
                                return;
                            }
                        }

                        IsBusy = false;
                        CommandResults["LoadedCommandAsync"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["LoadedCommandAsync"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                        CommandCanExecutes["LoadedCommandAsync"] = true;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadedCommandAsync") ?
                        CommandCanExecutes["LoadedCommandAsync"] : (CommandCanExecutes["LoadedCommandAsync"] = true);
                });
            }
        }

        public ICommand ActionSelectCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ActionSelectCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ActionSelectCommand"] = false;
                            CommandCanExecutes["ActionSelectCommand"] = false;

                            string eqptid = string.Empty;

                            if (arg != null)
                            {
                                eqptid = arg as string;
                            }

                            // 액션 파라미터 조회
                            _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.INDATAs.Clear();
                            _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.STATUS_OUTDATAs.Clear();
                            _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATAs.Clear();

                            _BR_PHR_SEL_EquipmentClassAction_Multi.INDATAs.Clear();
                            _BR_PHR_SEL_EquipmentClassAction_Multi.OUTDATAs.Clear();



                            _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.INDATAs.Add(new BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.INDATA
                            {
                                EQCLID = BR_PHR_SEL_EquipmentClass_Parent.OUTDATAs[0].EQCLID,
                                EQACID = selectedAction.EqacId
                            });

                            foreach (var eqpt in EqptList)
                            {
                                _BR_PHR_SEL_EquipmentClassAction_Multi.INDATAs.Add(new BR_PHR_SEL_EquipmentClassAction_Multi.INDATA
                                {
                                    LANGID = "ko-KR",
                                    EQPTID = eqpt.EQPTID,
                                    ISSYSACTVISIBLE = "Y",
                                    EQACID = selectedAction.EqacId
                                });
                            }

                            await _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.Execute();

                            foreach (var item in BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATAs)
                            {
                                if (item.ISREQUIRED == "Y" && !item.EQPANAME.StartsWith("*"))
                                {
                                    item.EQPANAME = "*" + item.EQPANAME;
                                }
                            }

                            await _BR_PHR_SEL_EquipmentClassAction_Multi.Execute();


                            CommandResults["ActionSelectCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ActionSelectCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ActionSelectCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ActionSelectCommand") ?
                        CommandCanExecutes["ActionSelectCommand"] : (CommandCanExecutes["ActionSelectCommand"] = true);
                });
            }
        }
        
        public ICommand ComfirmCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["SelectionChangedCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            _BR_PHR_UPD_EquipmentAction_Multi.INDATAs.Clear();
                            _BR_PHR_UPD_EquipmentAction_Multi.STATUSDATAs.Clear();
                            _BR_PHR_UPD_EquipmentAction_Multi.PARAMDATAs.Clear();

                            var authHelper = new iPharmAuthCommandHelper();

                            authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                "설비액션기록수행",
                                "설비액션기록수행",
                                false,
                                "OM_ProductionOrder_SUI",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            foreach (var eqpt in EqptList)
                            {
                                var matchingActions = _BR_PHR_SEL_EquipmentClassAction_Multi.OUTDATAs.Where(item => item.EQPTID == eqpt.EQPTID);

                                foreach (var item in matchingActions)
                                {
                                    _BR_PHR_UPD_EquipmentAction_Multi.INDATAs.Add(new BR_PHR_UPD_EquipmentAction_Multi.INDATA
                                    {
                                        EQPTID = eqpt.EQPTID,
                                        LANGID = "ko-KR",
                                        USER = AuthRepositoryViewModel.Instance.ConfirmedGuid,
                                        EQACID = item.EQACID,
                                    });
                                }

                                foreach (var item in _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.STATUS_OUTDATAs)
                                {
                                    _BR_PHR_UPD_EquipmentAction_Multi.STATUSDATAs.Add(new BR_PHR_UPD_EquipmentAction_Multi.STATUSDATA
                                    {
                                        EQPTID = eqpt.EQPTID,
                                        EQACID = item.EQACID,
                                        EQSTID = item.EQSTID
                                    });
                                }

                                foreach(var item in _BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID.PARAM_OUTDATAs)
                                {
                                    var paVal = item.EQPAINFO;
                                    _BR_PHR_UPD_EquipmentAction_Multi.PARAMDATAs.Add(new BR_PHR_UPD_EquipmentAction_Multi.PARAMDATA
                                    {
                                        EQPTID = eqpt.EQPTID,
                                        EQSTID = item.EQSTID,
                                        EQPAID = item.EQPAID,
                                        PAVAL = paVal
                                    });
                                }
                            }

                            if (await _BR_PHR_UPD_EquipmentAction_Multi.Execute())
                            {
                                OnMessage("실행 완료되었습니다.");
                            }



                                CommandResults["SelectionChangedCommand"] = false;
                            CommandCanExecutes["SelectionChangedCommand"] = false;


                            IsBusy = false;
                            CommandResults["SelectionChangedCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SelectionChangedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["SelectionChangedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SelectionChangedCommand") ?
                        CommandCanExecutes["SelectionChangedCommand"] : (CommandCanExecutes["SelectionChangedCommand"] = true);
                });
            }
        }

        public ICommand ChangePrintCommand
        {
            get
            {
                return new CommandBase(arg =>
                {
                    try
                    {
                        IsBusy = true;

                        CommandResults["ChangePrintCommand"] = false;
                        CommandCanExecutes["ChangePrintCommand"] = false;

                        ///
                        SelectPrinterPopup popup = new SelectPrinterPopup();

                        popup.Closed += (s, e) =>
                        {
                            if (popup.DialogResult.GetValueOrDefault())
                            {
                                if (popup.SourceGrid.SelectedItem != null && popup.SourceGrid.SelectedItem is BR_PHR_SEL_System_Printer.OUTDATA)
                                {
                                    _selectedPrint = popup.SourceGrid.SelectedItem as BR_PHR_SEL_System_Printer.OUTDATA;
                                    OnPropertyChanged("curPrintName");
                                }
                            }
                        };

                        popup.Show();
                        ///
                        CommandResults["ChangePrintCommand"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["ChangePrintCommand"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        CommandCanExecutes["ChangePrintCommand"] = true;

                        IsBusy = false;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ChangePrintCommand") ?
                        CommandCanExecutes["ChangePrintCommand"] : (CommandCanExecutes["ChangePrintCommand"] = true);
                });
            }
        } // 프린터 변경

        #endregion

        #region [etc]

        public class EqptContainer : WIPContainer
        {
            private string _Eqptid;
            public string Eqptid
            {
                get { return this._Eqptid; }
                set
                {
                    this._Eqptid = value;
                    this.OnPropertyChanged("EqptId");
                }
            }
            private string _Eqclid;
            public string Eqclid
            {
                get { return this._Eqclid; }
                set
                {
                    this._Eqclid = value;
                    this.OnPropertyChanged("Eqclid");
                }
            }
            private string _EqclName;
            public string EqclName
            {
                get { return this._EqclName; }
                set
                {
                    this._EqclName = value;
                    this.OnPropertyChanged("EqclName");
                }
            }
        }


        public class ActionContainer : WIPContainer
        {
            private string _EqclName;
            public string EqclName
            {
                get { return this._EqclName; }
                set
                {
                    this._EqclName = value;
                    this.OnPropertyChanged("EqclName");
                }
            }
            private string _EqacId;
            public string EqacId
            {
                get { return this._EqacId; }
                set
                {
                    this._EqacId = value;
                    this.OnPropertyChanged("EqacId");
                }
            }
            private string _EqacName;
            public string EqacName
            {
                get { return this._EqacName; }
                set
                {
                    this._EqacName = value;
                    this.OnPropertyChanged("EqacName");
                }
            }
            private string _EqacDesc;
            public string EqacDesc
            {
                get { return this._EqacDesc; }
                set
                {
                    this._EqacDesc = value;
                    this.OnPropertyChanged("EqacDesc");
                }
            }
            private string _EqacIuse;
            public string EqacIuse
            {
                get { return this._EqacIuse; }
                set
                {
                    this._EqacIuse = value;
                    this.OnPropertyChanged("EqacIuse");
                }
            }
            
        }
        #endregion
    }
}

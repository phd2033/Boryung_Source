using C1.Silverlight.Data;
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using 보령.UserControls;

namespace 보령
{
    //[ShopFloorCustomHidden]
    public class 설비액션기록ViewModel : ViewModelBase
    {
        #region [Property]
        public 설비액션기록ViewModel()
        {
            _BR_PHR_SEL_System_Printer = new BR_PHR_SEL_System_Printer();
            _BR_PHR_SEL_Equipment_GetInfo = new BR_PHR_SEL_Equipment_GetInfo();
            _BR_PHR_SEL_EquipmentClassAction_IncludeStatus = new BR_PHR_SEL_EquipmentClassAction_IncludeStatus();
            _BR_PHR_SEL_EquipmentActionStatusWithParameter = new 보령.BR_PHR_SEL_EquipmentActionStatusWithParameter();

            _EqptList = new ObservableCollection<EqptContainer>();
            _ActionList = new ObservableCollection<ActionContainer>();
            _selectedAction = new BR_PHR_SEL_EquipmentClassAction_IncludeStatus();

        }

        설비액션기록 _mainWnd;
   
        private BR_PHR_SEL_System_Printer.OUTDATA _selectedPrint;
        private BR_PHR_SEL_EquipmentClassAction_IncludeStatus _selectedAction;

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
        
        private ObservableCollection<EqptContainer> _EqptList;
        public ObservableCollection<EqptContainer> EqptList
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

        BR_PHR_SEL_EquipmentClassAction_IncludeStatus _BR_PHR_SEL_EquipmentClassAction_IncludeStatus;
        public BR_PHR_SEL_EquipmentClassAction_IncludeStatus BR_PHR_SEL_EquipmentClassAction_IncludeStatus
        {
            get { return _BR_PHR_SEL_EquipmentClassAction_IncludeStatus; }
            set
            {
                _BR_PHR_SEL_EquipmentClassAction_IncludeStatus = value;
            }
        }

        BR_PHR_SEL_EquipmentActionStatusWithParameter _BR_PHR_SEL_EquipmentActionStatusWithParameter;
        public BR_PHR_SEL_EquipmentActionStatusWithParameter BR_PHR_SEL_EquipmentActionStatusWithParameter
        {
            get { return _BR_PHR_SEL_EquipmentActionStatusWithParameter; }
            set
            {
                _BR_PHR_SEL_EquipmentActionStatusWithParameter = value;
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
                            
                            _mainWnd.txtEqptId.Focus();
                        }
                        ///

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
        public ICommand EqptScanCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["EqptScanCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["EqptScanCommand"] = false;
                            CommandCanExecutes["EqptScanCommand"] = false;

                            string eqptid = string.Empty;

                            if (arg != null)
                            {
                                eqptid = arg as string;
                            }
                                
                            // 설비코드 없음
                            if (string.IsNullOrWhiteSpace(eqptid)) return;
                            //설비코드 확인
                            EqptCheck(eqptid);
                            //설비 액션 조회
                            ActionCheck(eqptid);
                            //설비 파라미터 조회
                            ParameterCheck(eqptid);

                            CommandResults["EqptScanCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["BarcodeChangedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["BarcodeChangedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("BarcodeChangedCommand") ?
                        CommandCanExecutes["BarcodeChangedCommand"] : (CommandCanExecutes["BarcodeChangedCommand"] = true);
                });
            }
        }


        public ICommand SelectionChangedCommand
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

                            CommandResults["SelectionChangedCommand"] = false;
                            CommandCanExecutes["SelectionChangedCommand"] = false;

                            //string eqptid = string.Empty;

                            //if (arg != null)
                            //{
                            //    eqptid = arg as string;
                            //}

                            //// 설비코드 없음
                            //if (string.IsNullOrWhiteSpace(eqptid)) return;

                            ////설비 파라미터 조회
                            //ParameterCheck(eqptid);

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

        public ICommand ComfirmCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    try
                    {
                        CommandCanExecutes["ComfirmCommandAsync"] = false;
                        CommandResults["ComfirmCommandAsync"] = false;

                        ///
                        IsBusy = true;

                        if (_EqptList.Count > 0)
                        {
                            var authHelper = new iPharmAuthCommandHelper();

                            authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("반제품무게측정"),
                                string.Format("반제품무게측정"),
                                false,
                                "OM_ProductionOrder_SUI",
                                _mainWnd.CurrentOrderInfo.EquipmentID, _mainWnd.CurrentOrderInfo.RecipeID, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            DataSet ds = new DataSet();
                            DataTable dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            //2023.01.03 김호연 원료별 칭량을 하면 2개 이상의 배치가 동시에 기록되므로 EBR 확인할때 오더로 구분해야함
                            dt.Columns.Add(new DataColumn("오더번호"));
                            //-------------------------------------------------------------------------------------------------------
                            dt.Columns.Add(new DataColumn("용기번호"));
                            dt.Columns.Add(new DataColumn("저울번호"));
                            dt.Columns.Add(new DataColumn("총무게"));
                            dt.Columns.Add(new DataColumn("용기무게"));
                            dt.Columns.Add(new DataColumn("내용물무게"));

                            foreach (var item in _EqptList)
                            {
                                var row = dt.NewRow();

                                //2023.01.03 김호연 원료별 칭량을 하면 2개 이상의 배치가 동시에 기록되므로 EBR 확인할때 오더로 구분해야함
                                row["오더번호"] = item.PoId != null ? item.PoId : "";
                                //-------------------------------------------------------------------------------------------------------
                                //row["용기번호"] = item.EqptId != null ? item.EqptId : "";
                                row["저울번호"] = item.Eqptid != null ? item.Eqptid : "";
                                row["총무게"] = item != null ? item.GrossWeight.ToString("F" + item.Precision) : "";
                                row["용기무게"] = item != null ? item.TareWeight.ToString("F" + item.Precision) : "";
                                row["내용물무게"] = item != null ? item.NetWeight.ToString("F" + item.Precision) : "";

                                dt.Rows.Add(row);
                            }

                            var xml = BizActorRuleBase.CreateXMLStream(ds);
                            var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                            _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                            _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                            if (result != enumInstructionRegistErrorType.Ok)
                            {
                                throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                            }

                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                        }
                        else
                            OnMessage("조회된 데이터가 없습니다.");
                        ///

                        CommandResults["ComfirmCommandAsync"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["ComfirmCommandAsync"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                        CommandCanExecutes["ComfirmCommandAsync"] = true;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ComfirmCommandAsync") ?
                        CommandCanExecutes["ComfirmCommandAsync"] : (CommandCanExecutes["ComfirmCommandAsync"] = true);
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
                        IsBusy = false;
                        CommandCanExecutes["ChangePrintCommand"] = true;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ChangePrintCommand") ?
                        CommandCanExecutes["ChangePrintCommand"] : (CommandCanExecutes["ChangePrintCommand"] = true);
                });
            }
        }

        #endregion

        #region [etc]
        /// <summary>
        /// 설비 리스트 추가
        /// </summary>
        /// <param name="eqptid"></param>
        private async void EqptCheck(string eqptid)
        {
            _BR_PHR_SEL_Equipment_GetInfo.INDATAs.Clear();
            _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs.Clear();
            _BR_PHR_SEL_Equipment_GetInfo.INDATAs.Add(new BR_PHR_SEL_Equipment_GetInfo.INDATA
            {
                LANGID = AuthRepositoryViewModel.Instance.LangID,
                EQPTID = eqptid
            });

            await _BR_PHR_SEL_Equipment_GetInfo.Execute();
            if (_BR_PHR_SEL_Equipment_GetInfo.OUTDATAs.Count == 0)
            {
                OnMessage("설비코드가 등록되어있지 않습니다.");
                return;
            }

            //다른 설비코드가 있을 때
            if (EqptList.Count > 0)
            {
                // 동일 설비군이 아니면 오류
                if (_EqptList[0].Eqclid != _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs[0].EQCLID)
                {
                    OnMessage(string.Format("동일한 설비군의 설비들만 추가 가능합니다.\r\n 현재 등록 가능 설비군 {0} \r\n 스캔한 설비 설비군 {1}", _EqptList[0].EqclName, _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs[0].EQCLNAME));
                    return;
                }
                else
                {
                    //동일 설비군일 경우 리스트 추가
                    EqptList.Add(new EqptContainer
                    {
                        Eqptid = eqptid,
                        Eqclid = _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs[0].EQCLID,
                        EqclName = _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs[0].EQCLNAME
                    });

                    OnPropertyChanged("EqptList");
                }
            }
            else
            {
                //처음 설비코드 조회
                _EqptList.Add(new EqptContainer
                {
                    Eqptid = eqptid,
                    Eqclid = _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs[0].EQCLID,
                    EqclName = _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs[0].EQCLNAME
                });
            }
        }

        /// <summary>
        /// 설비 액션 리스트 조회
        /// </summary>
        /// <param name="eqptid"></param>
        private async void ActionCheck(string eqptid)
        {

            _BR_PHR_SEL_EquipmentClassAction_IncludeStatus.INDATAs.Clear();
            _BR_PHR_SEL_EquipmentClassAction_IncludeStatus.OUTDATAs.Clear();

            _BR_PHR_SEL_EquipmentClassAction_IncludeStatus.INDATAs.Add(new BR_PHR_SEL_EquipmentClassAction_IncludeStatus.INDATA
            {
                LANGID = AuthRepositoryViewModel.Instance.LangID,
                EQPTID = eqptid,
                ISSYSACTVISIBLE = "N"
            });
            // 액션 리스트 조회
            await _BR_PHR_SEL_EquipmentClassAction_IncludeStatus.Execute();

            if (_BR_PHR_SEL_EquipmentClassAction_IncludeStatus.OUTDATAs.Count > 0)
            {
                foreach (var item in _BR_PHR_SEL_EquipmentClassAction_IncludeStatus.OUTDATAs)
                {
                    ActionList.Add(new ActionContainer
                    {
                        Eqptid = item.EQPTID,
                        EqacId = item.EQACID,
                        EqacName = item.EQACNAME,
                        EqacState = item.EQACSTATE == "Y" ? "●" : "○",
                        IsReadOnly = item.EQACSTATE == "Y" ? false : true,
                        cellColoer = item.EQACSTATE == "Y" ? "Blue" : "Gray"
                    });
                }

                OnPropertyChanged("ActionList");
            }
            else
            {
                OnMessage("조회 된 액션 목록이 없습니다");
                return;
            }
        }

        private async void ParameterCheck(string eqptid)
        {
            _BR_PHR_SEL_EquipmentActionStatusWithParameter.INDATAs.Clear();
            _BR_PHR_SEL_EquipmentActionStatusWithParameter.STATUS_OUTDATAs.Clear();
            _BR_PHR_SEL_EquipmentActionStatusWithParameter.PARAM_OUTDATAs.Clear();

            _selectedAction = _mainWnd.ActionDataGrid.SelectedItem as BR_PHR_SEL_EquipmentClassAction_IncludeStatus;

            if (_selectedAction == null) return;

            _BR_PHR_SEL_EquipmentActionStatusWithParameter.INDATAs.Add(new BR_PHR_SEL_EquipmentActionStatusWithParameter.INDATA
            {
                EQPTID = eqptid,
                EQACID = _selectedAction.OUTDATAs[0].EQACID
            });

            await _BR_PHR_SEL_EquipmentActionStatusWithParameter.Execute();


            OnPropertyChanged("BR_PHR_SEL_EquipmentActionStatusWithParameter");
        }

        private bool CheckEqptId(string Id)
        {
            foreach (EqptContainer item in _EqptList)
            {
                if (Id == item.Eqptid)
                    return false;
            }
            return true;
        }
        private void InitializeData()
        {
            EqptId = "";
            _mainWnd.txtEqptId.Focus();
        }

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
            private string _EqacState;
            public string EqacState
            {
                get { return this._EqacState; }
                set
                {
                    this._EqacState = value;
                    this.OnPropertyChanged("EqacState");
                }
            }
            private bool _IsReadOnly;
            public bool IsReadOnly
            {
                get { return this._IsReadOnly; }
                set
                {
                    this._IsReadOnly = value;
                    this.OnPropertyChanged("IsReadOnly");
                }
            }

            private string _cellColoer;
            public string cellColoer
            {
                get { return this._cellColoer; }
                set
                {
                    this._cellColoer = value;
                    this.OnPropertyChanged("cellColoer");
                }
            }
        }
        #endregion
    }
}

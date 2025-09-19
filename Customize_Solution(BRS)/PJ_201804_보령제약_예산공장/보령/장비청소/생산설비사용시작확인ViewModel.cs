using System;
using System.Windows.Input;
using LGCNS.iPharmMES.Common;
using C1.Silverlight.Data;
using ShopFloorUI;
using System.Collections.Generic;

namespace 보령
{
    public class 생산설비사용시작확인ViewModel : ViewModelBase
    {
        #region [Property]
        private 생산설비사용시작확인 _mainWnd;
        List<InstructionModel> _inputValues;

        public 생산설비사용시작확인ViewModel()
        {
            _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM = new BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM();
        }
        #endregion
        #region [BizRule]
        
        private BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM;
        public BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM
        {
            get { return _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM; }
            set
            {
                _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM = value;
                NotifyPropertyChanged();
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
                    using (await AwaitableLocks["LoadedCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["LoadedCommandAsync"] = false;
                            CommandCanExecutes["LoadedCommandAsync"] = false;
                            
                            if (arg == null || !(arg is 생산설비사용시작확인)) return;
                            else
                            {
                                _mainWnd = arg as 생산설비사용시작확인;
                               
                                _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.INDATAs.Clear();
                                _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.OUTDATAs.Clear();
                                
                                _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.INDATAs.Add(new BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                                });
                                
                                if (await BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.Execute() == false)
                                    throw BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.Exception;
                            }
                            IsBusy = false;
                            CommandResults["LoadedCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            CommandResults["LoadedCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadedCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadedCommandAsync") ?
                        CommandCanExecutes["LoadedCommandAsync"] : (CommandCanExecutes["LoadedCommandAsync"] = true);
                });
            }
        }
        public ICommand ConfirmCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ConfirmCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["ConfirmCommandAsync"] = false;
                            CommandCanExecutes["ConfirmCommandAsync"] = false;
                            
                            IsBusy = true;

                            // 전자서명(기록값 변경)
                            var authHelper = new iPharmAuthCommandHelper();

                            if (_mainWnd.CurrentInstruction.Raw.INSERTEDYN.Equals("Y") && _mainWnd.Phase.CurrentPhase.STATE.Equals("COMP"))
                            {
                                authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    string.Format("기록값을 변경합니다."),
                                    string.Format("기록값 변경"),
                                    true,
                                    "OM_ProductionOrder_SUI",
                                    "", _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                            }
                            
                            // XML 변환
                            var ds = new DataSet();
                            var dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            dt.Columns.Add(new DataColumn("설비코드"));
                            dt.Columns.Add(new DataColumn("설비명"));
                            dt.Columns.Add(new DataColumn("생산시작"));
                            
                            foreach (var item in _BR_BRS_SEL_EquipmentStatus_PROC_EQSTONDTTM.OUTDATAs)
                            {
                                var row = dt.NewRow();

                                row["설비코드"] = item.EQPTID ?? "";
                                row["설비명"] = item.EQPTNAME ?? "";
                                row["생산시작"] = item.EQSTONDTTM ?? "";

                                dt.Rows.Add(row);
                            }

                            var xml = BizActorRuleBase.CreateXMLStream(ds);
                            var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                            _mainWnd.CurrentInstruction.Raw.ACTVAL = "TABLE,생산설비사용시작확인";
                            _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                            if (result != enumInstructionRegistErrorType.Ok)
                            {
                                throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                            }

                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                            IsBusy = false;
                            ///
                            CommandResults["ConfirmCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            CommandResults["ConfirmCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ConfirmCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ConfirmCommandAsync") ?
                        CommandCanExecutes["ConfirmCommandAsync"] : (CommandCanExecutes["ConfirmCommandAsync"] = true);
                });
            }
        }
        #endregion
    }
}
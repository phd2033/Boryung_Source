using C1.Silverlight.Data;
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using 보령.UserControls;
using LGCNS.iPharmMES.Recipe.Common;

namespace 보령
{
    //[ShopFloorCustomHidden]
    public class 원료의약품라벨발행ViewModel : ViewModelBase
    {
        #region [Property]
        public 원료의약품라벨발행ViewModel()
        {
            _BR_PHR_SEL_System_Printer = new BR_PHR_SEL_System_Printer();
            _BR_BRS_SEL_VESSEL_Info = new BR_BRS_SEL_VESSEL_Info();
            _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance = new BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance();
            _BR_BRS_REG_PRINT_DrugSubstanceLabel = new BR_BRS_REG_PRINT_DrugSubstanceLabel();
            _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY = new BR_BRS_SEL_DrugSubstance_LABEL_HISTORY();
            _IBCList = new ObservableCollection<IBCInfo>();
        }

        원료의약품라벨발행 _mainWnd;
        List<InstructionModel> _inputValues;
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

        private string _VesselId;
        public string VesselId
        {
            get { return _VesselId; }
            set
            {
                _VesselId = value;
                OnPropertyChanged("VesselId");
            }
        }

        private bool _RePrintFlag;
        public bool RePrintFlag
        {
            get { return _RePrintFlag; }
            set
            {
                _RePrintFlag = value;
                OnPropertyChanged("RePrintFlag");
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

        private ObservableCollection<IBCInfo> _IBCList;
        public ObservableCollection<IBCInfo> IBCList
        {
            get { return _IBCList; }
            set
            {
                _IBCList = value;
                OnPropertyChanged("IBCList");
            }
        }
        #endregion

        #region [BizRule]
        /// <summary>
        /// 작업장 프린터 조회
        /// </summary>
        private BR_PHR_SEL_System_Printer _BR_PHR_SEL_System_Printer;
        /// <summary>
        /// 용기정보조회
        /// </summary>
        private BR_BRS_SEL_VESSEL_Info _BR_BRS_SEL_VESSEL_Info;
        /// <summary>
        /// 반제품 조회
        /// </summary>
        private BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance;
        public BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance
        {
            get { return _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance; }
            set
            {
                _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance = value;
                OnPropertyChanged("BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance");
            }
        }
        /// <summary>
        /// 라벨 발행
        /// </summary>
        private BR_BRS_REG_PRINT_DrugSubstanceLabel _BR_BRS_REG_PRINT_DrugSubstanceLabel;
        /// <summary>
        /// 원료의약품 라벨 발행 이력 조회
        /// </summary>
        private BR_BRS_SEL_DrugSubstance_LABEL_HISTORY _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY;
        public BR_BRS_SEL_DrugSubstance_LABEL_HISTORY BR_BRS_SEL_DrugSubstance_LABEL_HISTORY
        {
            get { return _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY; }
            set
            {
                _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY = value;
                OnPropertyChanged("BR_BRS_SEL_DrugSubstance_LABEL_HISTORY");
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

                        if (arg != null && arg is 원료의약품라벨발행)
                        {
                            _mainWnd = arg as 원료의약품라벨발행;
                            btnRecordEnable = false;
                            VesselId = "";
                            
                            //히스토리 이력 불러와서 UI에 보여주기
                            _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.INDATAs.Clear();
                            _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.OUTDATAs.Clear();

                            _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.INDATAs.Add(new BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.INDATA
                            {
                                POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                            });

                            if (await _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.Execute() == false) throw _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.Exception;

                            _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.INDATAs.Clear();
                            _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.OUTDATAs.Clear();

                            _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.INDATAs.Add(new BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.INDATA
                            {
                                POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                            });

                            if (await _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.Execute() == false) throw _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.Exception;
                            
                            ///_BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance BizRule 통해 발행 횟수 확인. 0보다 크면 발행한 이력 있음.
                            if (_BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.OUTDATAs.Count > 0)
                            {
                                if (_mainWnd.CurrentInstruction.Raw.ACTVAL == _mainWnd.TableTypeName && _mainWnd.CurrentInstruction.Raw.NOTE != null)
                                {
                                    // 버튼세팅
                                    OnMessage("해당 지시문을 통해 라벨을 출력한 이력이 있습니다.\n해당 지시문으로는 원료의약품 라벨을 더이상 발행할 수 없습니다.");
                                    btnRecordEnable = false;
                                }
                                else
                                {
                                    _inputValues = InstructionModel.GetParameterSender(_mainWnd.CurrentInstruction, _mainWnd.Instructions);
                                    //레퍼런스 있을 때(재발행)
                                    if (_inputValues.Count > 0)
                                    {
                                        // _inputValues[0].Raw.REF_IRTGUID or _inputValues[0].Raw.ASM_CLS 이게 있는지 유무로 갈려고 했으나 라벨발행요청확인 UI에서 기록없음으로 기록하게 된다면 발행하지 못하도록 막아야하기 때문에 아래와 같이 Validation 진행.
                                        // 기록없음으로 진행했거나 아직 라벨발행 승인을 하지 않았다면 기록 못하도록 막음
                                        if (_inputValues[0].Raw.NOTE == null)
                                        {
                                            OnMessage("※QA 승인 필요※");
                                            return;
                                        }
                                        else if (_inputValues[0].Raw.NOTE != null)
                                        {
                                            DataSet ds = new DataSet();
                                            DataTable dt = new DataTable();
                                            var bytearray = _inputValues[0].Raw.NOTE;
                                            string xml = System.Text.Encoding.UTF8.GetString(bytearray, 0, bytearray.Length);

                                            ds.ReadXmlFromString(xml);
                                            dt = ds.Tables["DATA"];

                                            foreach (var row in dt.Rows)
                                            {
                                                if (row["확인자ID"].Equals("N/A"))
                                                {
                                                    OnMessage("※QA 승인 필요※");
                                                    return;
                                                }
                                                else
                                                {
                                                    btnRecordEnable = true;
                                                    RePrintFlag = true;
                                                }
                                            }
                                        }
                                    }
                                    //초기 발행
                                    else
                                    {
                                        if (_BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.OUTDATAs.Where(o => o.PRINTCNT < 1).Count() > 0)
                                        {
                                            btnRecordEnable = true;
                                            RePrintFlag = false;
                                        }
                                        else
                                        {
                                            OnMessage("※QA 승인 필요※\n원료의약품 라벨을 발행한 이력이 있습니다");
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                OnMessage("※생성된 반제품이 없습니다※");
                                return;
                            }
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
                            else
                                OnMessage("연결된 프린트가 없습니다.");
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

        public ICommand PrintStandbyCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["PrintStandbyCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandCanExecutes["PrintStandbyCommandAsync"] = false;
                            CommandResults["PrintStandbyCommandAsync"] = false;

                            VesselId = arg as string;
                            VesselId = VesselId.ToUpper();
                            OnPropertyChanged("IBCList");

                            var standby = _BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.OUTDATAs.Where(o => o.VESSELID == VesselId).FirstOrDefault();
                            if (standby != null)
                            {
                                if (CheckVesselId(VesselId))
                                {
                                    IBCList.Add(new IBCInfo
                                    {
                                        PoId = _mainWnd.CurrentOrder.ProductionOrderID,
                                        MsubLotId = standby.MSUBLOTID,
                                        VesselId = standby.VESSELID,
                                        NetWeight = Convert.ToString(standby.MSUBLOTQTY) + " " + standby.NOTATION,
                                        TareWeight = Convert.ToString(standby.TAREWEIGHT) + " " + standby.NOTATION,
                                        GrossWeight = Convert.ToString(standby.TAREWEIGHT+ standby.MSUBLOTQTY) + " " + standby.NOTATION
                                    });

                                    VesselId = "";
                                }
                                else
                                {
                                    OnMessage("※중복 스캔※\n라벨 출력할 리스트에 반제품 정보가 있습니다");
                                    VesselId = "";
                                }
                            }
                            else
                            {
                                OnMessage("※조회된 반제품 정보가 없습니다※");
                                VesselId = "";
                            }
                          
                            CommandResults["PrintStandbyCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandCanExecutes["PrintStandbyCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["PrintStandbyCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("PrintStandbyCommandAsync") ?
                        CommandCanExecutes["PrintStandbyCommandAsync"] : (CommandCanExecutes["PrintStandbyCommandAsync"] = true);
                });
            }
        }
 
        public ICommand NoRecordConfirmCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    try
                    {
                        CommandCanExecutes["NoRecordConfirmCommand"] = false;
                        CommandResults["NoRecordConfirmCommand"] = false;

                        ///
                        IsBusy = true;
                        bool FirstNoRecord = false; //2025.08.05 김도연 처음 기록없음 버튼을 통해 지시문을 기록할 경우 코멘트가 남도록 하는 Flag

                        var authHelper = new iPharmAuthCommandHelper();
                        if (_mainWnd.CurrentInstruction.Raw.INSERTEDYN.Equals("Y") && _mainWnd.Phase.CurrentPhase.STATE.Equals("COMP")) // 값 수정
                        {
                            // 전자서명 요청
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
                        else
                        {
                            // 전자서명 요청
                            authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("원료의약품라벨발행 UI 기록없음"),
                                string.Format("원료의약품라벨발행 UI 기록없음"),
                                true,
                                "OM_ProductionOrder_SUI",
                                "", _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }
                            FirstNoRecord = true;
                        }

                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("DATA");
                        ds.Tables.Add(dt);

                        dt.Columns.Add(new DataColumn("오더번호"));
                        dt.Columns.Add(new DataColumn("용기번호"));
                        dt.Columns.Add(new DataColumn("원료바코드"));
                        dt.Columns.Add(new DataColumn("출력시간"));
                        dt.Columns.Add(new DataColumn("출력횟수"));

                        var row = dt.NewRow();

                        row["오더번호"] = "N/A";
                        row["용기번호"] = "N/A";
                        row["원료바코드"] = "N/A";
                        row["출력시간"] = "N/A";
                        row["출력횟수"] = "N/A";

                        dt.Rows.Add(row);
                        
                        var xml = BizActorRuleBase.CreateXMLStream(ds);
                        var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                        _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                        _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                        var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                        if (result != enumInstructionRegistErrorType.Ok)
                        {
                            throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                        }

                        //2025.08.05 김도연 처음 기록없음 버튼을 통해 지시문을 기록할 경우 코멘트가 남도록 함
                        if (FirstNoRecord)
                        {

                            var bizrule = new BR_PHR_REG_InstructionComment();

                            bizrule.IN_Comments.Add(
                                new BR_PHR_REG_InstructionComment.IN_Comment
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    COMMENTTYPE = "CM008",
                                    COMMENT = AuthRepositoryViewModel.GetCommentByFunctionCode("OM_ProductionOrder_SUI"),
                                    INSUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_SUI")
                                }
                                );
                            bizrule.IN_IntructionResults.Add(
                                new BR_PHR_REG_InstructionComment.IN_IntructionResult
                                {
                                    RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                    ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                    IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                    IRTRSTGUID = _mainWnd.CurrentInstruction.Raw.IRTRSTGUID,
                                    IRTSEQ = (int)_mainWnd.CurrentInstruction.Raw.IRTSEQ
                                }
                                );

                            await bizrule.Execute();
                        }

                        if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                        else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);
                        
                        ///
                        CommandResults["NoRecordConfirmCommand"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["NoRecordConfirmCommand"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                        CommandCanExecutes["NoRecordConfirmCommand"] = true;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("NoRecordConfirmCommand") ?
                        CommandCanExecutes["NoRecordConfirmCommand"] : (CommandCanExecutes["NoRecordConfirmCommand"] = true);
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

                        ///현재 생성된 반제품 수랑 구하는 BizRule이랑 IBCLIST에 있는 거랑 갯수 비교
                        ///만약 적거나 많으면 에러메세지 return 
                        ///동일하면 라벨 발행 진행
                        if (_IBCList.Count > 0)
                        {
                            //초기 발행이면 반제품 라벨을 전부 출력하여야하기 때문에 Validation 추가
                            if (!RePrintFlag)
                            {
                                if(_BR_BRS_SEL_ProductionOrderOutputSubLot_DrugSubstance.OUTDATAs.Count != _IBCList.Count)
                                {
                                    OnMessage("※원료의약품이 담긴 용기 스캔 누락※\n라벨 출력 전에 해당 공정에서 생성된 모든 원료의약품 용기가 스캔되었는지 확인해주시기 바랍니다");
                                    return;
                                }
                            }

                            if (curPrintName == "N/A")
                            {
                                OnMessage("※연결된 프린트가 없습니다※\n프린트를 연결해주시기 바랍니다");
                                return;
                            }

                            var authHelper = new iPharmAuthCommandHelper();
                            if (_mainWnd.CurrentInstruction.Raw.INSERTEDYN.Equals("Y") && _mainWnd.Phase.CurrentPhase.STATE.Equals("COMP")) // 값 수정
                            {
                                // 전자서명 요청
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
                            }else
                            {
                                authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    string.Format("원료의약품라벨발행"),
                                    string.Format("원료의약품라벨발행"),
                                    false,
                                    "OM_ProductionOrder_SUI",
                                    _mainWnd.CurrentOrderInfo.EquipmentID, _mainWnd.CurrentOrderInfo.RecipeID, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                            }
                            
                            // 라벨 출력하는 BizRule 돌리고 라벨 출력
                            // 히스토리 다시 불러와서 지시문에 기록
                            _BR_BRS_REG_PRINT_DrugSubstanceLabel.INDATAs.Clear();

                            foreach (var item in _IBCList)
                            {
                                _BR_BRS_REG_PRINT_DrugSubstanceLabel.INDATAs.Add(new BR_BRS_REG_PRINT_DrugSubstanceLabel.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    MSUBLOTID = item.MsubLotId,
                                    VESSELID = item.VesselId,
                                    USERID = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_SUI"),
                                    ROOMNO = AuthRepositoryViewModel.Instance.RoomID,
                                    PRINTNAME = curPrintName
                                });
                            }
                            
                            if (await _BR_BRS_REG_PRINT_DrugSubstanceLabel.Execute() == true)
                            {
                                _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.INDATAs.Clear();
                                _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.OUTDATAs.Clear();

                                _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.INDATAs.Add(new BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                                });

                                if (await _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.Execute() == false)
                                {
                                    throw _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.Exception;
                                }
                            }
                            else
                            {
                                throw _BR_BRS_REG_PRINT_DrugSubstanceLabel.Exception;
                            }

                            DataSet ds = new DataSet();
                            DataTable dt = new DataTable("DATA");
                            ds.Tables.Add(dt);
                            
                            dt.Columns.Add(new DataColumn("오더번호"));
                            dt.Columns.Add(new DataColumn("용기번호"));
                            dt.Columns.Add(new DataColumn("원료바코드"));
                            dt.Columns.Add(new DataColumn("출력시간"));
                            dt.Columns.Add(new DataColumn("출력횟수"));

                            foreach (var item in _BR_BRS_SEL_DrugSubstance_LABEL_HISTORY.OUTDATAs)
                            {
                                var row = dt.NewRow();
                                
                                row["오더번호"] = item.POID != null ? item.POID : "";
                                row["용기번호"] = item.VESSELID != null ? item.VESSELID : "";
                                row["원료바코드"] = item.MSUBLOTBCD != null ? item.MSUBLOTBCD : "";
                                row["출력시간"] = item.INSDTTM != null ? string.Format("{0:yyyy-MM-dd HH:mm:ss}", item.INSDTTM) : "";
                                row["출력횟수"] = item.PRINTCNT != null ? Convert.ToString(item.PRINTCNT) : "";

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
                            OnMessage("※원료의약품 라벨을 출력할 데이터가 없습니다※");
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
        private bool CheckVesselId(string Id)
        {
            foreach (IBCInfo item in _IBCList)
            {
                if (Id == item.VesselId)
                    return false;
            }
            return true;
        }
        #endregion


        #region [User Define]
        public class IBCInfo : ViewModelBase
        {
            private string _PoId;
            public string PoId
            {
                get { return _PoId; }
                set
                {
                    _PoId = value;
                    OnPropertyChanged("PoId");
                }
            }

            private string _MsubLotId;
            public string MsubLotId
            {
                get { return _MsubLotId; }
                set
                {
                    _MsubLotId = value;
                    OnPropertyChanged("MsubLotId");
                }
            }

            private string _VesselId;
            public string VesselId
            {
                get { return _VesselId; }
                set
                {
                    _VesselId = value;
                    OnPropertyChanged("VesselId");
                }
            }

            private string _NetWeight;
            public string NetWeight
            {
                get { return _NetWeight; }
                set
                {
                    _NetWeight = value;
                    OnPropertyChanged("NetWeight");
                }
            }
            private string _TareWeight;
            public string TareWeight
            {
                get { return _TareWeight; }
                set
                {
                    _TareWeight = value;
                    OnPropertyChanged("TareWeight");
                }
            }
            private string _GrossWeight;
            public string GrossWeight
            {
                get { return _GrossWeight; }
                set
                {
                    _GrossWeight = value;
                    OnPropertyChanged("GrossWeight");
                }
            }
        }
        #endregion
    }
}

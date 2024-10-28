using C1.Silverlight.Data;
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using LGCNS.iPharmMES.Recipe.Common;

namespace 보령
{
    public class 간섭상황조회ViewModel : ViewModelBase
    {
        #region [Property]
        public 간섭상황조회ViewModel()
        {
            _BR_PHR_SEL_CommonCode = new BR_PHR_SEL_CommonCode();
            _BR_BRS_SEL_INTERFER_SITUATION_SUM = new BR_BRS_SEL_INTERFER_SITUATION_SUM();
            _ListInterfer = new ObservableCollection<InterferSituation>();
        }

        간섭상황조회 _mainWnd;

        //간섭상황조회List
        private ObservableCollection<InterferSituation> _ListInterfer;
        public ObservableCollection<InterferSituation> ListInterfer
        {
            get { return _ListInterfer; }
            set
            {
                _ListInterfer = value;
                OnPropertyChanged("ListInterfer");
            }
        }
        private bool _OVER_FLAG;
        public bool OVER_FLAG
        {
            get { return _OVER_FLAG; }
            set
            {
                _OVER_FLAG = value;
                OnPropertyChanged("OVER_FLAG");
            }
        }
        #endregion

        #region [Bizrule]
        private BR_PHR_SEL_CommonCode _BR_PHR_SEL_CommonCode;
        public BR_PHR_SEL_CommonCode BR_PHR_SEL_CommonCode
        {
            get { return _BR_PHR_SEL_CommonCode; }
            set
            {
                _BR_PHR_SEL_CommonCode = value;
                OnPropertyChanged("BR_PHR_SEL_CommonCode");
            }
        }
        private BR_BRS_SEL_INTERFER_SITUATION_SUM _BR_BRS_SEL_INTERFER_SITUATION_SUM;
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
                            IsBusy = true;

                            CommandResults["LoadedCommandAsync"] = false;
                            CommandCanExecutes["LoadedCommandAsync"] = false;

                            if (arg != null && arg is 간섭상황조회)
                            {
                                _mainWnd = arg as 간섭상황조회;

                                OVER_FLAG = false;

                                _BR_BRS_SEL_INTERFER_SITUATION_SUM.INDATAs.Clear();
                                _BR_BRS_SEL_INTERFER_SITUATION_SUM.OUTDATAs.Clear();
                                _BR_BRS_SEL_INTERFER_SITUATION_SUM.INDATAs.Add(new BR_BRS_SEL_INTERFER_SITUATION_SUM.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID
                                });

                                if (await _BR_BRS_SEL_INTERFER_SITUATION_SUM.Execute() == false)
                                {
                                    throw _BR_BRS_SEL_INTERFER_SITUATION_SUM.Exception;
                                }

                                _BR_PHR_SEL_CommonCode.INDATAs.Clear();
                                _BR_PHR_SEL_CommonCode.OUTDATAs.Clear();
                                _BR_PHR_SEL_CommonCode.INDATAs.Add(new BR_PHR_SEL_CommonCode.INDATA
                                {
                                    LANGID = AuthRepositoryViewModel.Instance.LangID,
                                    CMCDTYPE = "BRS_INTERFER_SITUATION"
                                });
                                if (await _BR_PHR_SEL_CommonCode.Execute() == false)
                                {
                                    throw _BR_PHR_SEL_CommonCode.Exception;
                                }
                                foreach (var check in _BR_BRS_SEL_INTERFER_SITUATION_SUM.OUTDATAs)
                                {
                                    if (Convert.ToDecimal(_BR_PHR_SEL_CommonCode.OUTDATAs[Convert.ToInt32(check.SEQ) - 1].ATTRIBUTE2) < Convert.ToDecimal(check.FREQUENCY))
                                    {
                                        OVER_FLAG = true;
                                    }
                                    _ListInterfer.Add(new InterferSituation
                                    {
                                        SEQ = Convert.ToDecimal(check.SEQ),
                                        SITUATION = check.CONTENTS,
                                        GUBUN = check.GUBUN,
                                        SUMNO = Convert.ToDecimal(check.FREQUENCY),
                                        OVER_COLOR = Convert.ToDecimal(_BR_PHR_SEL_CommonCode.OUTDATAs[Convert.ToInt32(check.SEQ) - 1].ATTRIBUTE2) < Convert.ToDecimal(check.FREQUENCY) ? "Yellow" : "Wheat"
                                    });
                                }
                            }
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
                            IsBusy = true;

                            CommandResults["ConfirmCommandAsync"] = false;
                            CommandCanExecutes["ConfirmCommandAsync"] = false;

                            var authHelper = new iPharmAuthCommandHelper();

                            if (_mainWnd.CurrentInstruction.Raw.INSERTEDYN.Equals("Y") && _mainWnd.Phase.CurrentPhase.STATE.Equals("COMP")) // 값 수정
                            {
                                // 전자서명 요청
                                authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");
                                enumRoleType inspectorRole = enumRoleType.ROLE001;
                                if (await authHelper.ClickAsync(
                                        Common.enumCertificationType.Function,
                                        Common.enumAccessType.Create,
                                        string.Format("기록값을 변경합니다."),
                                        string.Format("기록값 변경"),
                                        true,
                                        "OM_ProductionOrder_SUI",
                                        "", _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID, this._mainWnd.CurrentInstruction.Raw.DVTPASSYN == "Y" ? enumRoleType.ROLE001.ToString() : inspectorRole.ToString()) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                                _mainWnd.CurrentInstruction.Raw.INSTCOMMENT = AuthRepositoryViewModel.GetCommentByFunctionCode("OM_ProductionOrder_SUI");
                                _mainWnd.CurrentInstruction.Raw.INSTCOMMENTUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_SUI");
                            }
                            else
                            {
                                // 전자서명 후 BR 실행
                                authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");
                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    string.Format("간섭상황기록"),
                                    string.Format("간섭상황기록"),
                                    false,
                                    "OM_ProductionOrder_SUI",
                                    _mainWnd.CurrentOrderInfo.EquipmentID, _mainWnd.CurrentOrderInfo.RecipeID, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                            }

                            if (OVER_FLAG)
                            {
                                authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "OM_ProductionOrder_Deviation");

                                enumRoleType inspectorRole = enumRoleType.ROLE001;
                                if (await authHelper.ClickAsync(
                                        Common.enumCertificationType.Role,
                                        Common.enumAccessType.Create,
                                        "기준을 초과하였습니다. 일탈번호 작성 필요.",
                                        "일탈번호 작성",
                                        true,
                                        "OM_ProductionOrder_Deviation",
                                        "",
                                        this._mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                        this._mainWnd.CurrentInstruction.Raw.DVTPASSYN == "Y" ? enumRoleType.ROLE001.ToString() : inspectorRole.ToString()) == false)
                                {
                                    return;
                                }

                                _mainWnd.CurrentInstruction.Raw.DVTFCYN = "Y";
                                _mainWnd.CurrentInstruction.Raw.DVTCONFIRMUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Deviation");
                                _mainWnd.CurrentInstruction.Raw.DVTYN = null;
                                //_mainWnd.CurrentInstruction.Raw.DVTCOMMENT = AuthRepositoryViewModel.GetCommentByFunctionCode("OM_ProductionOrder_Deviation");
                                //comment = AuthRepositoryViewModel.GetCommentByFunctionCode("OM_ProductionOrder_Deviation");
                            }

                            DataSet ds = new DataSet();
                            DataTable dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            dt.Columns.Add(new DataColumn("NO"));
                            dt.Columns.Add(new DataColumn("간섭내용"));
                            dt.Columns.Add(new DataColumn("간섭구분"));
                            dt.Columns.Add(new DataColumn("간섭횟수"));

                            var row = dt.NewRow();
                            foreach (var rowdata in ListInterfer)
                            {
                                row = dt.NewRow();
                                row["NO"] = rowdata.SEQ;
                                row["간섭내용"] = rowdata.SITUATION;
                                row["간섭구분"] = rowdata.GUBUN;
                                row["간섭횟수"] = rowdata.SUMNO;

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
                /*
   
                            if (OVER_FLAG)
                            {
                                var bizrule = new BR_PHR_REG_InstructionComment();

                                bizrule.IN_Comments.Add(
                                    new BR_PHR_REG_InstructionComment.IN_Comment
                                    {
                                        POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                        OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                        COMMENTTYPE = "CM008",
                                        COMMENT = comment
                                    });
                                bizrule.IN_IntructionResults.Add(
                                    new BR_PHR_REG_InstructionComment.IN_IntructionResult
                                    {
                                        RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                        ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                        IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                        IRTRSTGUID = _mainWnd.CurrentInstruction.Raw.IRTRSTGUID,
                                        IRTSEQ = (int)_mainWnd.CurrentInstruction.Raw.IRTSEQ
                                    });
                                await bizrule.Execute();
                            }
                     */


                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);
                            
                            IsBusy = false;

                            CommandResults["ConfirmCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ConfirmCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ConfirmCommandAsync"] = true;

                            IsBusy = false;
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
        #region User Define
        public class InterferSituation : ViewModelBase
        {
            private decimal _SEQ;
            public decimal SEQ
            {
                get { return _SEQ; }
                set
                {
                    _SEQ = value;
                    OnPropertyChanged("SEQ");
                }
            }
            private string _SITUATION;
            public string SITUATION
            {
                get { return _SITUATION; }
                set
                {
                    _SITUATION = value;
                    OnPropertyChanged("SITUATION");
                }
            }
            private string _GUBUN;
            public string GUBUN
            {
                get { return _GUBUN; }
                set
                {
                    _GUBUN = value;
                    OnPropertyChanged("GUBUN");
                }
            }
            private Decimal _SUMNO;
            public Decimal SUMNO
            {
                get { return _SUMNO; }
                set
                {
                    _SUMNO = value;
                    OnPropertyChanged("SUMNO");
                }
            }
            private String _OVER_COLOR;
            public String OVER_COLOR
            {
                get { return _OVER_COLOR; }
                set
                {
                    _OVER_COLOR = value;
                    OnPropertyChanged("OVER_COLOR");
                }
            }
        }
        #endregion

    }
}
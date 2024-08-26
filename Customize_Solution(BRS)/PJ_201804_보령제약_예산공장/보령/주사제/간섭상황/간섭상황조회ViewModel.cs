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

namespace 보령
{
    public class 간섭상황조회ViewModel : ViewModelBase
    {
        #region [Property]
        public 간섭상황조회ViewModel()
        {
            _BR_PHR_SEL_CommonCode = new BR_PHR_SEL_CommonCode();
            _BR_BRS_REG_INTERFER_SITUATION = new BR_BRS_REG_INTERFER_SITUATION();
            _BR_BRS_SEL_INTERFER_SITUATION = new BR_BRS_SEL_INTERFER_SITUATION();
            _ListInterfer = new ObservableCollection<InterferSituation>();
        }

        간섭상황조회 _mainWnd;

        private decimal _NO;
        public decimal NO
        {
            get { return _NO; }
            set
            {
                _NO = value;
                OnPropertyChanged("NO");
            }
        }
        private string _CONTENTS;
        public string CONTENTS
        {
            get { return _CONTENTS; }
            set
            {
                _CONTENTS = value;
                OnPropertyChanged("CONTENTS");
            }
        }
        private string _DIVISION;
        public string DIVISION
        {
            get { return _DIVISION; }
            set
            {
                _DIVISION = value;
                OnPropertyChanged("DIVISION");
            }
        }
        private string _MODULE;
        public string MODULE
        {
            get { return _MODULE; }
            set
            {
                _MODULE = value;
                OnPropertyChanged("MODULE");
            }
        }
        private int _DISPOSAL;
        public int DISPOSAL
        {
            get { return _DISPOSAL; }
            set
            {
                _DISPOSAL = value;
                OnPropertyChanged("DISPOSAL");
            }
        }
        private DateTime _STRTDTTM;
        public DateTime STRTDTTM
        {
            get { return _STRTDTTM; }
            set
            {
                _STRTDTTM = value;
                OnPropertyChanged("STRTDTTM");
            }
        }
        private DateTime _ENDDTTM;
        public DateTime ENDDTTM
        {
            get { return _ENDDTTM; }
            set
            {
                _ENDDTTM = value;
                OnPropertyChanged("ENDDTTM");
            }
        }
        private string _NOTE;
        public string NOTE
        {
            get { return _NOTE; }
            set
            {
                _NOTE = value;
                OnPropertyChanged("NOTE");
            }
        }
        private bool _RE_FLAG;
        public bool RE_FLAG
        {
            get { return _RE_FLAG; }
            set
            {
                _RE_FLAG = value;
                OnPropertyChanged("RE_FLAG");
            }
        }
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

        private BR_PHR_SEL_CommonCode.OUTDATA _CboCommon;
        public BR_PHR_SEL_CommonCode.OUTDATA CboCommon
        {
            get { return _CboCommon; }
            set
            {
                _CboCommon = value;
                OnPropertyChanged("CboCommon");
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

        private BR_BRS_REG_INTERFER_SITUATION _BR_BRS_REG_INTERFER_SITUATION;
        private BR_BRS_SEL_INTERFER_SITUATION _BR_BRS_SEL_INTERFER_SITUATION;
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
                            RE_FLAG = false;

                            CommandResults["LoadedCommandAsync"] = false;
                            CommandCanExecutes["LoadedCommandAsync"] = false;

                            if (arg != null && arg is 간섭상황조회)
                            {
                                _mainWnd = arg as 간섭상황조회;

                                STRTDTTM = (await AuthRepositoryViewModel.GetDBDateTimeNow()).AddHours(-1);
                                ENDDTTM = await AuthRepositoryViewModel.GetDBDateTimeNow();

                                _BR_PHR_SEL_CommonCode.INDATAs.Clear();
                                _BR_PHR_SEL_CommonCode.OUTDATAs.Clear();
                                _BR_PHR_SEL_CommonCode.INDATAs.Add(new BR_PHR_SEL_CommonCode.INDATA
                                {
                                    LANGID = AuthRepositoryViewModel.Instance.LangID,
                                    CMCDTYPE = "BRS_INTERFER_SITUATION"
                                });

                                //COMBOItems = SetComboBox(_COMBOItems);

                                if (await _BR_PHR_SEL_CommonCode.Execute() == false)
                                {
                                    throw _BR_PHR_SEL_CommonCode.Exception;
                                }
                                /*
                                foreach (var item in _BR_PHR_SEL_CommonCode.OUTDATAs)
                                {
                                }
                                */
                            }
                            // 이전 기록 조회
                            if (_mainWnd.CurrentInstruction.Raw.ACTVAL == _mainWnd.TableTypeName && _mainWnd.CurrentInstruction.Raw.NOTE != null)
                            {
                                
                                    
                                DataSet ds = new DataSet();
                                DataTable dt = new DataTable();
                                var bytearray = _mainWnd.CurrentInstruction.Raw.NOTE;
                                string xml = Encoding.UTF8.GetString(bytearray, 0, bytearray.Length);

                                ds.ReadXmlFromString(xml);
                                if (ds.Tables[0].TableName == "DATA")
                                {
                                    dt = ds.Tables[0];

                                    foreach (var info in dt.Rows)
                                    {
                                        _BR_BRS_SEL_INTERFER_SITUATION.INDATAs.Add(new BR_BRS_SEL_INTERFER_SITUATION.INDATA
                                        {
                                            POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                            OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                            RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                            ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                            IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                            IRTSEQ = Convert.ToInt32(_mainWnd.CurrentInstruction.Raw.IRTSEQ),
                                            SEQ = Convert.ToDecimal(info["NO"]),
                                            CONTENTS = info["간섭내용"].ToString(),
                                            GUBUN = info["간섭구분"].ToString(),
                                            MODULE = info["Module"].ToString(),
                                            DISPOSEQTY = Convert.ToDecimal(info["폐기수량"]),
                                            STDTTM = Convert.ToDateTime(String.Format("{0:yyyy-MM-dd HH:mm:ss}", info["발생시각"].ToString())),
                                            EDDTTM = Convert.ToDateTime(String.Format("{0:yyyy-MM-dd HH:mm:ss}", info["완료시각"].ToString())),
                                            COMMENT = info["비고"].ToString(),
                                            INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                        });
                                    }
                                    if (await _BR_BRS_SEL_INTERFER_SITUATION.Execute())
                                    {
                                        foreach(var outdata in _BR_BRS_SEL_INTERFER_SITUATION.OUTDATAs)
                                        {
                                            _ListInterfer.Add(new InterferSituation
                                            {
                                                CHK = false,
                                                INTERFERGUID = outdata.INTERFERGUID,
                                                SEQ = Convert.ToDecimal(outdata.SEQ),
                                                SITUATION = outdata.CONTENTS,
                                                GUBUN = outdata.GUBUN,
                                                MODULENO = outdata.MODULE,
                                                DISPOSEQTY = Convert.ToDecimal(outdata.DISPOSEQTY),
                                                STDTTM = String.Format("{0:yyyy-MM-dd HH:mm:ss}", outdata.STDTTM.ToString()),
                                                EDDTTM = String.Format("{0:yyyy-MM-dd HH:mm:ss}", outdata.EDDTTM.ToString()),
                                                COMMENT = outdata.COMMENT
                                            });
                                        }
                                    }
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
        public ICommand AddInterferCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["AddInterferCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["AddInterferCommandAsync"] = false;
                            CommandCanExecutes["AddInterferCommandAsync"] = false;
                            ///
                            ListInterfer.Add(new InterferSituation
                            {
                                CHK = false,
                                INTERFERGUID = "",
                                SEQ = NO,
                                SITUATION = CONTENTS,
                                GUBUN = DIVISION,
                                MODULENO = MODULE,
                                DISPOSEQTY = DISPOSAL,
                                STDTTM = String.Format("{0:yyyy-MM-dd HH:mm:ss}", STRTDTTM),
                                EDDTTM = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ENDDTTM),
                                COMMENT = NOTE

                            });
                            foreach(var i in ListInterfer)
                            {

                            }

                            ///

                            CommandResults["AddInterferCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["AddInterferCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["AddInterferCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("AddInterferCommandAsync") ?
                        CommandCanExecutes["AddInterferCommandAsync"] : (CommandCanExecutes["AddInterferCommandAsync"] = true);
                });
            }
        }

        public ICommand SectionChangedCmbCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["SectionChangedCmbCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["SectionChangedCmbCommand"] = false;
                            CommandCanExecutes["SectionChangedCmbCommand"] = false;

                            ///
                            if (arg != null && arg is BR_PHR_SEL_CommonCode.OUTDATA)
                            {
                                CboCommon = arg as BR_PHR_SEL_CommonCode.OUTDATA;
                                DIVISION = CboCommon.ATTRIBUTE1;
                                NO = Convert.ToDecimal(CboCommon.CMCODE);
                                CONTENTS = CboCommon.CMCDNAME;
                            }
                            ///

                            CommandResults["SectionChangedCmbCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SectionChangedCmbCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["SectionChangedCmbCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SectionChangedCmbCommand") ?
                        CommandCanExecutes["SectionChangedCmbCommand"] : (CommandCanExecutes["SectionChangedCmbCommand"] = true);
                });
            }
        }
        public ICommand RowDeleteCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RowDeleteCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["RowDeleteCommand"] = false;
                            CommandCanExecutes["RowDeleteCommand"] = false;

                            ///

                            var elements = (from data in _ListInterfer
                                            where data.CHK == false
                                            select data).ToList();

                            if(_mainWnd.CurrentInstruction.Raw.ACTVAL == _mainWnd.TableTypeName && _mainWnd.CurrentInstruction.Raw.NOTE != null)
                            {
                                var del_elements = (from data in _ListInterfer
                                                    where data.CHK == true
                                                    select data).ToList();

                                foreach (var del_data in del_elements)
                                {

                                }
                            }
                            
                            _ListInterfer.Clear();
                            
                            foreach (var data in elements)
                            {
                                _ListInterfer.Add(data);
                            }

                            CommandResults["RowDeleteCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RowDeleteCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RowDeleteCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RowDeleteCommand") ?
                        CommandCanExecutes["RowDeleteCommand"] : (CommandCanExecutes["RowDeleteCommand"] = true);
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

                                if (await authHelper.ClickAsync(
                                        Common.enumCertificationType.Function,
                                        Common.enumAccessType.Create,
                                        string.Format("기록값을 변경합니다."),
                                        string.Format("기록값 변경"),
                                        false,
                                        "OM_ProductionOrder_SUI",
                                        "", _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                            }

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

                            _BR_BRS_REG_INTERFER_SITUATION.INDATAs.Clear();
                            foreach (var item in ListInterfer)
                            {
                                _BR_BRS_REG_INTERFER_SITUATION.INDATAs.Add(new BR_BRS_REG_INTERFER_SITUATION.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                    ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                    IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                    IRTSEQ = Convert.ToInt32(_mainWnd.CurrentInstruction.Raw.IRTSEQ),
                                    INTERFERGUID = "",
                                    SEQ = item.SEQ,
                                    CONTENTS = item.SITUATION,
                                    GUBUN = item.GUBUN,
                                    MODULE = item.MODULENO,
                                    DISPOSEQTY = item.DISPOSEQTY,
                                    STDTTM = Convert.ToDateTime(item.STDTTM),
                                    EDDTTM = Convert.ToDateTime(item.EDDTTM),
                                    COMMENT = item.COMMENT,
                                    INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                });
                            }
                            if (await _BR_BRS_REG_INTERFER_SITUATION.Execute())
                            {
                                DataSet ds = new DataSet();
                                DataTable dt = new DataTable("DATA");
                                ds.Tables.Add(dt);

                                dt.Columns.Add(new DataColumn("NO"));
                                dt.Columns.Add(new DataColumn("간섭내용"));
                                dt.Columns.Add(new DataColumn("간섭구분"));
                                dt.Columns.Add(new DataColumn("Module"));
                                dt.Columns.Add(new DataColumn("폐기수량"));
                                dt.Columns.Add(new DataColumn("발생시각"));
                                dt.Columns.Add(new DataColumn("완료시각"));
                                dt.Columns.Add(new DataColumn("비고"));

                                var row = dt.NewRow();
                                foreach (var rowdata in ListInterfer)
                                {
                                    row = dt.NewRow();
                                    row["NO"] = rowdata.SEQ;
                                    row["간섭내용"] = rowdata.SITUATION;
                                    row["간섭구분"] = rowdata.GUBUN;
                                    row["Module"] = rowdata.MODULENO;
                                    row["폐기수량"] = rowdata.DISPOSEQTY;
                                    row["발생시각"] = rowdata.STDTTM;
                                    row["완료시각"] = rowdata.EDDTTM;
                                    row["비고"] = rowdata.COMMENT != null? rowdata.COMMENT : "";

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
            private bool _CHK;
            public bool CHK
            {
                get { return _CHK; }
                set
                {
                    _CHK = value;
                    OnPropertyChanged("CHK");
                }
            }
            private string _INTERFERGUID;
            public string INTERFERGUID
            {
                get { return _INTERFERGUID; }
                set
                {
                    _INTERFERGUID = value;
                    OnPropertyChanged("INTERFERGUID");
                }
            }
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
            private string _MODULENO;
            public string MODULENO
            {
                get { return _MODULENO; }
                set
                {
                    _MODULENO = value;
                    OnPropertyChanged("MODULENO");
                }
            }
            private decimal _DISPOSEQTY;
            public decimal DISPOSEQTY
            {
                get { return _DISPOSEQTY; }
                set
                {
                    _DISPOSEQTY = value;
                    OnPropertyChanged("DISPOSEQTY");
                }
            }
            private string _STDTTM;
            public string STDTTM
            {
                get { return _STDTTM; }
                set
                {
                    _STDTTM = value;
                    OnPropertyChanged("STDTTM");
                }
            }
            private string _EDDTTM;
            public string EDDTTM
            {
                get { return _EDDTTM; }
                set
                {
                    _EDDTTM = value;
                    OnPropertyChanged("EDDTTM");
                }
            }
            private string _COMMENT;
            public string COMMENT
            {
                get { return _COMMENT; }
                set
                {
                    _COMMENT = value;
                    OnPropertyChanged("COMMENT");
                }
            }
        }
        #endregion

    }
}

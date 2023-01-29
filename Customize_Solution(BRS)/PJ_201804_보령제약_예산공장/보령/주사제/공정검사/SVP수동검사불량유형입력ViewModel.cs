using LGCNS.iPharmMES.Common;
using Order;
using System;
using System.Windows.Input;
using System.Linq;
using ShopFloorUI;
using System.Text;
using C1.Silverlight.Data;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using C1.Silverlight.Imaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using LGCNS.iPharmMES.Recipe.Common;
using System.Threading.Tasks;

namespace 보령
{
    public class SVP수동검사불량유형입력ViewModel : ViewModelBase
    {
        #region Properties
        SVP수동검사불량유형입력 _mainWnd;

        private RejectionDetail.OUTDATACollection _RejectionDetails;
        public RejectionDetail.OUTDATACollection RejectionDetails
        {
            get { return _RejectionDetails; }
            set
            {
                _RejectionDetails = value;
                OnPropertyChanged("RejectionDetails");
            }
        }

        private string _INSUSER;
        public string INSUSER
        {
            get { return _INSUSER; }
            set
            {
                _INSUSER = value;
                OnPropertyChanged("INSUSER");
            }
        }
        private string _Date;
        public string Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                OnPropertyChanged("Date");
            }
        }
        private decimal _RejectQTY;
        public decimal RejectQTY
        {
            get { return _RejectQTY; }
            set
            {
                _RejectQTY = value;
                OnPropertyChanged("RejectQTY");
            }
        }
        private decimal _Reject_No1;
        public decimal Reject_No1
        {
            get { return _Reject_No1; }
            set
            {
                _Reject_No1 = value;
                OnPropertyChanged("Reject_No1");
            }
        }
        private decimal _Reject_No2;
        public decimal Reject_No2
        {
            get { return _Reject_No2; }
            set
            {
                _Reject_No2 = value;
                OnPropertyChanged("Reject_No2");
            }
        }
        private decimal _Reject_No3;
        public decimal Reject_No3
        {
            get { return _Reject_No3; }
            set
            {
                _Reject_No3 = value;
                OnPropertyChanged("Reject_No3");
            }
        }
        private decimal _Reject_No4;
        public decimal Reject_No4
        {
            get { return _Reject_No4; }
            set
            {
                _Reject_No4 = value;
                OnPropertyChanged("Reject_No4");
            }
        }
        private decimal _Reject_No5;
        public decimal Reject_No5
        {
            get { return _Reject_No5; }
            set
            {
                _Reject_No5 = value;
                OnPropertyChanged("Reject_No5");
            }
        }
        private decimal _Reject_No6;
        public decimal Reject_No6
        {
            get { return _Reject_No6; }
            set
            {
                _Reject_No6 = value;
                OnPropertyChanged("Reject_No6");
            }
        }
        private decimal _Reject_No7;
        public decimal Reject_No7
        {
            get { return _Reject_No7; }
            set
            {
                _Reject_No7 = value;
                OnPropertyChanged("Reject_No7");
            }
        }
        private decimal _Reject_No8;
        public decimal Reject_No8
        {
            get { return _Reject_No8; }
            set
            {
                _Reject_No8 = value;
                OnPropertyChanged("Reject_No8");
            }
        }
        private decimal _Reject_No9;
        public decimal Reject_No9
        {
            get { return _Reject_No9; }
            set
            {
                _Reject_No9 = value;
                OnPropertyChanged("Reject_No9");
            }
        }
        private decimal _Reject_No10;
        public decimal Reject_No10
        {
            get { return _Reject_No10; }
            set
            {
                _Reject_No10 = value;
                OnPropertyChanged("Reject_No10");
            }
        }
        private decimal _Reject_No11;
        public decimal Reject_No11
        {
            get { return _Reject_No11; }
            set
            {
                _Reject_No11 = value;
                OnPropertyChanged("Reject_No11");
            }
        }
        private decimal _Reject_No12;
        public decimal Reject_No12
        {
            get { return _Reject_No12; }
            set
            {
                _Reject_No12 = value;
                OnPropertyChanged("Reject_No12");
            }
        }
        private decimal _Reject_No13;
        public decimal Reject_No13
        {
            get { return _Reject_No13; }
            set
            {
                _Reject_No13 = value;
                OnPropertyChanged("Reject_No13");
            }
        }
        private decimal _Reject_No14;
        public decimal Reject_No14
        {
            get { return _Reject_No14; }
            set
            {
                _Reject_No14 = value;
                OnPropertyChanged("Reject_No14");
            }
        }
        private decimal _Reject_No15;
        public decimal Reject_No15
        {
            get { return _Reject_No15; }
            set
            {
                _Reject_No15 = value;
                OnPropertyChanged("Reject_No15");
            }
        }
        private decimal _Reject_No16;
        public decimal Reject_No16
        {
            get { return _Reject_No16; }
            set
            {
                _Reject_No16 = value;
                OnPropertyChanged("Reject_No16");
            }
        }
        private decimal _Reject_No17;
        public decimal Reject_No17
        {
            get { return _Reject_No17; }
            set
            {
                _Reject_No17 = value;
                OnPropertyChanged("Reject_No17");
            }
        }
        private decimal _Reject_No18;
        public decimal Reject_No18
        {
            get { return _Reject_No18; }
            set
            {
                _Reject_No18 = value;
                OnPropertyChanged("Reject_No18");
            }
        }
        private decimal _Reject_No19;
        public decimal Reject_No19
        {
            get { return _Reject_No19; }
            set
            {
                _Reject_No19 = value;
                OnPropertyChanged("Reject_No19");
            }
        }
        private decimal _Reject_No20;
        public decimal Reject_No20
        {
            get { return _Reject_No20; }
            set
            {
                _Reject_No20 = value;
                OnPropertyChanged("Reject_No20");
            }
        }
        private decimal _Reject_No21;
        public decimal Reject_No21
        {
            get { return _Reject_No21; }
            set
            {
                _Reject_No21 = value;
                OnPropertyChanged("Reject_No21");
            }
        }

        //private bool _IsEnableDate;
        //public bool IsEnableDate
        //{
        //    get { return _IsEnableDate; }
        //    set
        //    {
        //        _IsEnableDate = value;
        //        OnPropertyChanged("IsEnableDate");
        //    }
        //}
        
        #endregion

        #region [Constructor]

        public SVP수동검사불량유형입력ViewModel()
        {
            _RejectionDetails = new RejectionDetail.OUTDATACollection();
            _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO = new BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO();
            _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO = new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO();
        }

        #endregion

        #region [BizRule]
        private BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO;
        public BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO
        {
            get { return _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO; }
            set
            {
                _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO = value;
                OnPropertyChanged("BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO");
            }
        }

        private BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO;
        public BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO
        {
            get { return _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO; }
            set
            {
                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO = value;
                OnPropertyChanged("BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO");
            }
        }
        #endregion

        #region [Command]
        public ICommand LoadCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["LoadCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["LoadCommandAsync"] = false;
                            CommandCanExecutes["LoadCommandAsync"] = false;
                            IsBusy = true;

                            if (arg != null && arg is SVP수동검사불량유형입력)
                            {
                                _mainWnd = arg as SVP수동검사불량유형입력;

                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Clear();
                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Clear();
                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Add(new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    INSPECTIONDATE = DateTime.Now.ToString("yyyy-MM-dd"),
                                    INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID
                                });

                                if (await _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.Execute() && _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Count == 0)
                                {
                                    //IsEnableDate = true;

                                    INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID;
                                    Date = DateTime.Now.ToString("yyyy-MM-dd");
                                    RejectQTY = 0;
                                    Reject_No1 = 0;
                                    Reject_No2 = 0;
                                    Reject_No3 = 0;
                                    Reject_No4 = 0;
                                    Reject_No5 = 0;
                                    Reject_No6 = 0;
                                    Reject_No7 = 0;
                                    Reject_No8 = 0;
                                    Reject_No9 = 0;
                                    Reject_No10 = 0;
                                    Reject_No11 = 0;
                                    Reject_No12 = 0;
                                    Reject_No13 = 0;
                                    Reject_No14 = 0;
                                    Reject_No15 = 0;
                                    Reject_No16 = 0;
                                    Reject_No17 = 0;
                                    Reject_No18 = 0;
                                    Reject_No19 = 0;
                                    Reject_No20 = 0;
                                    Reject_No21 = 0;
                                }else
                                {
                                    var outData = _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs[0];
                                    //IsEnableDate = false;

                                    INSUSER = outData.INSUSER;
                                    Date = outData.INSPECTIONDATE;
                                    RejectQTY = Convert.ToDecimal(outData.RejectQTY);
                                    Reject_No1 = Convert.ToDecimal(outData.Reject_No1);
                                    Reject_No2 = Convert.ToDecimal(outData.Reject_No2);
                                    Reject_No3 = Convert.ToDecimal(outData.Reject_No3);
                                    Reject_No4 = Convert.ToDecimal(outData.Reject_No4);
                                    Reject_No5 = Convert.ToDecimal(outData.Reject_No5);
                                    Reject_No6 = Convert.ToDecimal(outData.Reject_No6);
                                    Reject_No7 = Convert.ToDecimal(outData.Reject_No7);
                                    Reject_No8 = Convert.ToDecimal(outData.Reject_No8);
                                    Reject_No9 = Convert.ToDecimal(outData.Reject_No9);
                                    Reject_No10 = Convert.ToDecimal(outData.Reject_No10);
                                    Reject_No11 = Convert.ToDecimal(outData.Reject_No11);
                                    Reject_No12 = Convert.ToDecimal(outData.Reject_No12);
                                    Reject_No13 = Convert.ToDecimal(outData.Reject_No13);
                                    Reject_No14 = Convert.ToDecimal(outData.Reject_No14);
                                    Reject_No15 = Convert.ToDecimal(outData.Reject_No15);
                                    Reject_No16 = Convert.ToDecimal(outData.Reject_No16);
                                    Reject_No17 = Convert.ToDecimal(outData.Reject_No17);
                                    Reject_No18 = Convert.ToDecimal(outData.Reject_No18);
                                    Reject_No19 = Convert.ToDecimal(outData.Reject_No19);
                                    Reject_No20 = Convert.ToDecimal(outData.Reject_No20);
                                    Reject_No21 = Convert.ToDecimal(outData.Reject_No21);
                                }

                            }

                            IsBusy = false;
                            ///

                            CommandResults["LoadCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            CommandResults["LoadCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadCommandAsync") ?
                        CommandCanExecutes["LoadCommandAsync"] : (CommandCanExecutes["LoadCommandAsync"] = true);
                });
            }
        }

        public ICommand NoRecordConfirmCommand
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

        public ICommand SaveCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                using (await AwaitableLocks["SaveCommandAsync"].EnterAsync())
                {
                    try
                    {
                        CommandResults["SaveCommandAsync"] = false;
                        CommandCanExecutes["SaveCommandAsync"] = false;
                            
                        var authHelper = new iPharmAuthCommandHelper();                            
                        // 조회내용 기록
                        authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                        if (await authHelper.ClickAsync(
                            Common.enumCertificationType.Function,
                            Common.enumAccessType.Create,
                            "SVP수동검사불량유형입력",
                            "SVP수동검사불량유형입력",
                            false,
                            "OM_ProductionOrder_SUI",
                            "", null, null) == false)
                        {
                            throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                        }

                            
                            _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.INDATAs.Clear();

                            _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.INDATAs.Add(new BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.INDATA
                            {
                                POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                SEQ = 1,    //2023.01.10 박희돈 HIST 테이블에 값이 없을경우 SEQ값을 1로한다. 값이 있다면 비즈룰에서 HIST테이블의 SEQ값을 조회함.
                                OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                INSPECTIONDATE = Convert.ToString(Date.ToString()).Substring(0, 10),
                                RejectQTY = RejectQTY,
                                Reject_No1 = Reject_No1,
                                Reject_No2 = Reject_No2,
                                Reject_No3 = Reject_No3,
                                Reject_No4 = Reject_No4,
                                Reject_No5 = Reject_No5,
                                Reject_No6 = Reject_No6,
                                Reject_No7 = Reject_No7,
                                Reject_No8 = Reject_No8,
                                Reject_No9 = Reject_No9,
                                Reject_No10 = Reject_No10,
                                Reject_No11 = Reject_No11,
                                Reject_No12 = Reject_No12,
                                Reject_No13 = Reject_No13,
                                Reject_No14 = Reject_No14,
                                Reject_No15 = Reject_No15,
                                Reject_No16 = Reject_No16,
                                Reject_No17 = Reject_No17,
                                Reject_No18 = Reject_No18,
                                Reject_No19 = Reject_No19,
                                Reject_No20 = Reject_No20,
                                Reject_No21 = Reject_No21,
                                INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                INSDTTM = Convert.ToString(await AuthRepositoryViewModel.GetDBDateTimeNow())
                            });

                            if (!await _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.Execute()) return;

                            var ds = new DataSet();
                            var dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            dt.Columns.Add(new DataColumn("검사자"));
                            dt.Columns.Add(new DataColumn("검사일자"));
                            dt.Columns.Add(new DataColumn("검사수량"));
                            dt.Columns.Add(new DataColumn("흰티"));
                            dt.Columns.Add(new DataColumn("검은티"));
                            dt.Columns.Add(new DataColumn("유색"));
                            dt.Columns.Add(new DataColumn("금속성"));
                            dt.Columns.Add(new DataColumn("유리조각"));
                            dt.Columns.Add(new DataColumn("섬유1mm초과"));
                            dt.Columns.Add(new DataColumn("섬유1mm이하"));
                            dt.Columns.Add(new DataColumn("충전량불량"));
                            dt.Columns.Add(new DataColumn("바이알손상"));
                            dt.Columns.Add(new DataColumn("내부오염"));
                            dt.Columns.Add(new DataColumn("바이알흠집"));
                            dt.Columns.Add(new DataColumn("성형불량"));
                            dt.Columns.Add(new DataColumn("캡씰링불량"));
                            dt.Columns.Add(new DataColumn("이종캡"));
                            dt.Columns.Add(new DataColumn("캡외관불량"));
                            dt.Columns.Add(new DataColumn("고무전없음"));
                            dt.Columns.Add(new DataColumn("이종고무전"));
                            dt.Columns.Add(new DataColumn("고무전이물"));
                            dt.Columns.Add(new DataColumn("Cake상태불량"));
                            dt.Columns.Add(new DataColumn("바이알내부기벽고무전약액뭍음"));
                            dt.Columns.Add(new DataColumn("기타불량"));

                            DataRow row = dt.NewRow();
                            row["검사자"] = INSUSER;
                            row["검사일자"] = Convert.ToString(Date.ToString()).Substring(0, 10);
                            row["검사수량"] = RejectQTY;
                            row["흰티"] = Reject_No1;
                            row["검은티"] = Reject_No2;
                            row["유색"] = Reject_No3;
                            row["금속성"] = Reject_No4;
                            row["유리조각"] = Reject_No5;
                            row["섬유1mm초과"] = Reject_No6;
                            row["섬유1mm이하"] = Reject_No7;
                            row["충전량불량"] = Reject_No8;
                            row["바이알손상"] = Reject_No9;
                            row["내부오염"] = Reject_No10;
                            row["바이알흠집"] = Reject_No11;
                            row["성형불량"] = Reject_No12;
                            row["캡씰링불량"] = Reject_No13;
                            row["이종캡"] = Reject_No14;
                            row["캡외관불량"] = Reject_No15;
                            row["고무전없음"] = Reject_No16;
                            row["이종고무전"] = Reject_No17;
                            row["고무전이물"] = Reject_No18;
                            row["Cake상태불량"] = Reject_No19;
                            row["바이알내부기벽고무전약액뭍음"] = Reject_No20;
                            row["기타불량"] = Reject_No21;

                            dt.Rows.Add(row);
                            
                            var xml = BizActorRuleBase.CreateXMLStream(ds);
                            var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                            _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                            _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, false, false, true);
                            if (result != enumInstructionRegistErrorType.Ok)
                            {
                                throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                            }

                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                            CommandResults["SaveCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SaveCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["SaveCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SaveCommandAsync") ?
                        CommandCanExecutes["SaveCommandAsync"] : (CommandCanExecutes["SaveCommandAsync"] = true);
                });
            }
        }
        #endregion

        #region User Define
        public class RejectionDetail : BizActorRuleBase
        {
            public sealed partial class OUTDATACollection : BufferedObservableCollection<OUTDATA>
            {
            }
            private OUTDATACollection _OUTDATAs;
            [BizActorOutputSetAttribute()]
            public OUTDATACollection OUTDATAs
            {
                get
                {
                    return this._OUTDATAs;
                }
            }
            [BizActorOutputSetDefineAttribute(Order = "0")]
            [CustomValidation(typeof(ViewModelBase), "ValidateRow")]
            public partial class OUTDATA : BizActorDataSetBase
            {
                public OUTDATA()
                {
                    RowLoadedFlag = false;
                }
                private bool _RowLoadedFlag;
                public bool RowLoadedFlag
                {
                    get
                    {
                        return this._RowLoadedFlag;
                    }
                    set
                    {
                        this._RowLoadedFlag = value;
                        this.OnPropertyChanged("_RowLoadedFlag");
                    }
                }
                private string _RowIndex;
                public string RowIndex
                {
                    get
                    {
                        return this._RowIndex;
                    }
                    set
                    {
                        this._RowIndex = value;
                        this.OnPropertyChanged("RowIndex");
                    }
                }
                private string _RowEditSec;
                public string RowEditSec
                {
                    get
                    {
                        return this._RowEditSec;
                    }
                    set
                    {
                        this._RowEditSec = value;
                        this.OnPropertyChanged("RowEditSec");
                    }
                }
                private DateTime _INSDTTM = DateTime.Now;
                [BizActorOutputItemAttribute()]
                public DateTime INSDTTM
                {
                    get
                    {
                        return this._INSDTTM;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._INSDTTM = value;
                            this.CheckIsOriginal("INSDTTM", value);
                            this.OnPropertyChanged("INSDTTM");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private string _INSUSER;
                [BizActorOutputItemAttribute()]
                public string INSUSER
                {
                    get
                    {
                        return this._INSUSER;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._INSUSER = value;
                            this.CheckIsOriginal("INSUSER", value);
                            this.OnPropertyChanged("INSUSER");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _FIBER = 0;
                [BizActorOutputItemAttribute()]
                public int FIBER
                {
                    get
                    {
                        return this._FIBER;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._FIBER = value;
                            this.CheckIsOriginal("INSDTTM", value);
                            this.OnPropertyChanged("INSDTTM");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _WHITE = 0;
                [BizActorOutputItemAttribute()]
                public int WHITE
                {
                    get
                    {
                        return this._WHITE;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._WHITE = value;
                            this.CheckIsOriginal("WHITE", value);
                            this.OnPropertyChanged("WHITE");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _BLACK = 0;
                [BizActorOutputItemAttribute()]
                public int BLACK
                {
                    get
                    {
                        return this._BLACK;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._BLACK = value;
                            this.CheckIsOriginal("BLACK", value);
                            this.OnPropertyChanged("BLACK");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _DISCOLOR = 0;
                [BizActorOutputItemAttribute()]
                public int DISCOLOR
                {
                    get
                    {
                        return this._DISCOLOR;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._DISCOLOR = value;
                            this.CheckIsOriginal("DISCOLOR", value);
                            this.OnPropertyChanged("DISCOLOR");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _BROKEN = 0;
                [BizActorOutputItemAttribute()]
                public int BROKEN
                {
                    get
                    {
                        return this._BROKEN;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._BROKEN = value;
                            this.CheckIsOriginal("BROKEN", value);
                            this.OnPropertyChanged("BROKEN");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _CRACK = 0;
                [BizActorOutputItemAttribute()]
                public int CRACK
                {
                    get
                    {
                        return this._CRACK;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._CRACK = value;
                            this.CheckIsOriginal("CRACK", value);
                            this.OnPropertyChanged("CRACK");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _CAPREJ = 0;
                [BizActorOutputItemAttribute()]
                public int CAPREJ
                {
                    get
                    {
                        return this._CAPREJ;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._CAPREJ = value;
                            this.CheckIsOriginal("CAPREJ", value);
                            this.OnPropertyChanged("CAPREJ");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private int _OTHER = 0;
                [BizActorOutputItemAttribute()]
                public int OTHER
                {
                    get
                    {
                        return this._OTHER;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._OTHER = value;
                            this.CheckIsOriginal("BLACK", value);
                            this.OnPropertyChanged("BLACK");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
            }
            public RejectionDetail()
            {
                _OUTDATAs = new OUTDATACollection();
            }
        }
        public class RejectionSummary : ViewModelBase
        {
            private string _REJECTIONTYPE;
            public string REJECTIONTYPE
            {
                get { return _REJECTIONTYPE; }
                set
                {
                    _REJECTIONTYPE = value;
                    OnPropertyChanged("REJECTIONTYPE");
                }
            }
            private int _REJECTIONQTY;
            public int REJECTIONQTY
            {
                get { return _REJECTIONQTY; }
                set
                {
                    _REJECTIONQTY = value;
                    OnPropertyChanged("REJECTIONQTY");
                }
            }
            private decimal _REJECTIONRATIO;
            public decimal REJECTIONRATIO
            {
                get { return _REJECTIONRATIO; }
                set
                {
                    _REJECTIONRATIO = value;
                    OnPropertyChanged("REJECTIONRATIO");
                }
            }
        }
        #endregion
    }
}

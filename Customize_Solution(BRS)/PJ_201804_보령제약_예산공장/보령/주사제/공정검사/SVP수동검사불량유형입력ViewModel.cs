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

        private string _curUserName;
        public string curUserName
        {
            get { return _curUserName; }
            set
            {
                _curUserName = value;
                OnPropertyChanged("curUserName");
            }
        }
        private DateTime _curDate;
        public DateTime curDate
        {
            get { return _curDate; }
            set
            {
                _curDate = value;
                OnPropertyChanged("curDate");
            }
        }
        private int _curRejectQTY;
        public int curRejectQTY
        {
            get { return _curRejectQTY; }
            set
            {
                _curRejectQTY = value;
                OnPropertyChanged("curRejectQTY");
            }
        }
        private int _curReject_No1;
        public int curReject_No1
        {
            get { return _curReject_No1; }
            set
            {
                _curReject_No1 = value;
                OnPropertyChanged("curReject_No1");
            }
        }
        private int _curReject_No2;
        public int curReject_No2
        {
            get { return _curReject_No2; }
            set
            {
                _curReject_No2 = value;
                OnPropertyChanged("curReject_No2");
            }
        }
        private int _curReject_No3;
        public int curReject_No3
        {
            get { return _curReject_No3; }
            set
            {
                _curReject_No3 = value;
                OnPropertyChanged("curReject_No3");
            }
        }
        private int _curReject_No4;
        public int curReject_No4
        {
            get { return _curReject_No4; }
            set
            {
                _curReject_No4 = value;
                OnPropertyChanged("curReject_No4");
            }
        }
        private int _curReject_No5;
        public int curReject_No5
        {
            get { return _curReject_No5; }
            set
            {
                _curReject_No5 = value;
                OnPropertyChanged("curReject_No5");
            }
        }
        private int _curReject_No6;
        public int curReject_No6
        {
            get { return _curReject_No6; }
            set
            {
                _curReject_No6 = value;
                OnPropertyChanged("curReject_No6");
            }
        }
        private int _curReject_No7;
        public int curReject_No7
        {
            get { return _curReject_No7; }
            set
            {
                _curReject_No7 = value;
                OnPropertyChanged("curReject_No7");
            }
        }
        private int _curReject_No8;
        public int curReject_No8
        {
            get { return _curReject_No8; }
            set
            {
                _curReject_No8 = value;
                OnPropertyChanged("curReject_No8");
            }
        }
        private int _curReject_No9;
        public int curReject_No9
        {
            get { return _curReject_No9; }
            set
            {
                _curReject_No9 = value;
                OnPropertyChanged("curReject_No9");
            }
        }
        private int _curReject_No10;
        public int curReject_No10
        {
            get { return _curReject_No10; }
            set
            {
                _curReject_No10 = value;
                OnPropertyChanged("curReject_No10");
            }
        }
        private int _curReject_No11;
        public int curReject_No11
        {
            get { return _curReject_No11; }
            set
            {
                _curReject_No11 = value;
                OnPropertyChanged("curReject_No11");
            }
        }
        private int _curReject_No12;
        public int curReject_No12
        {
            get { return _curReject_No12; }
            set
            {
                _curReject_No12 = value;
                OnPropertyChanged("curReject_No12");
            }
        }
        private int _curReject_No13;
        public int curReject_No13
        {
            get { return _curReject_No13; }
            set
            {
                _curReject_No13 = value;
                OnPropertyChanged("curReject_No13");
            }
        }
        private int _curReject_No14;
        public int curReject_No14
        {
            get { return _curReject_No14; }
            set
            {
                _curReject_No14 = value;
                OnPropertyChanged("curReject_No14");
            }
        }
        private int _curReject_No15;
        public int curReject_No15
        {
            get { return _curReject_No15; }
            set
            {
                _curReject_No15 = value;
                OnPropertyChanged("curReject_No15");
            }
        }
        private int _curReject_No16;
        public int curReject_No16
        {
            get { return _curReject_No16; }
            set
            {
                _curReject_No16 = value;
                OnPropertyChanged("curReject_No16");
            }
        }
        private int _curReject_No17;
        public int curReject_No17
        {
            get { return _curReject_No17; }
            set
            {
                _curReject_No17 = value;
                OnPropertyChanged("curReject_No17");
            }
        }
        private int _curReject_No18;
        public int curReject_No18
        {
            get { return _curReject_No18; }
            set
            {
                _curReject_No18 = value;
                OnPropertyChanged("curReject_No18");
            }
        }
        private int _curReject_No19;
        public int curReject_No19
        {
            get { return _curReject_No19; }
            set
            {
                _curReject_No19 = value;
                OnPropertyChanged("curReject_No19");
            }
        }
        private int _curReject_No20;
        public int curReject_No20
        {
            get { return _curReject_No20; }
            set
            {
                _curReject_No20 = value;
                OnPropertyChanged("curReject_No20");
            }
        }
        private int _curReject_No21;
        public int curReject_No21
        {
            get { return _curReject_No21; }
            set
            {
                _curReject_No21 = value;
                OnPropertyChanged("curReject_No21");
            }
        }
        #endregion

        #region [Constructor]

        public SVP수동검사불량유형입력ViewModel()
        {
            _RejectionDetails = new RejectionDetail.OUTDATACollection();
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
                                //_mainWnd = arg as SVP수동검사불량유형입력;

                                //_BR_BRS_SEL_ProductionOrderCustomValue.INDATAs.Clear();
                                //_BR_BRS_SEL_ProductionOrderCustomValue.OUTDATAs.Clear();
                                //_BR_BRS_SEL_ProductionOrderCustomValue.INDATAs.Add(new BR_BRS_SEL_ProductionOrderCustomValue.INDATA
                                //{
                                //    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                //    OPSGNAME = "조제",
                                //    POCDID = "FIT_RESULT"
                                //});

                                //if (await _BR_BRS_SEL_ProductionOrderCustomValue.Execute())
                                //{
                                //    if (_BR_BRS_SEL_ProductionOrderCustomValue.OUTDATAs.Count > 0)
                                //    {
                                //        var outData = _BR_BRS_SEL_ProductionOrderCustomValue.OUTDATAs[0];
                                //        curLotNo = outData.POCDVAL1;
                                //        curUnderVal = Convert.ToInt32(outData.POCDVAL2);
                                //        curFitCount = Convert.ToInt32(outData.POCDVAL3);
                                //        curUpperVal = Convert.ToInt32(outData.POCDVAL4);
                                //        curResult = outData.POCDVAL5;

                                //    }
                                //    else
                                //    {
                                //        if (string.IsNullOrEmpty(_mainWnd.CurrentInstruction.Raw.MINVAL))
                                //        {
                                //            curUnderVal = 0;
                                //        }
                                //        else
                                //        {
                                //            curUnderVal = Convert.ToDecimal(_mainWnd.CurrentInstruction.Raw.MINVAL);
                                //        }

                                //        if (string.IsNullOrEmpty(_mainWnd.CurrentInstruction.Raw.MAXVAL))
                                //        {
                                //            curUpperVal = 0;
                                //        }
                                //        else
                                //        {
                                //            curUpperVal = Convert.ToDecimal(_mainWnd.CurrentInstruction.Raw.MAXVAL);
                                //        }
                                //    }
                                //}
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

                            var ds = new DataSet();
                            var dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            dt.Columns.Add(new DataColumn("FitLotNo"));
                            dt.Columns.Add(new DataColumn("하한값"));
                            dt.Columns.Add(new DataColumn("FIT결과값"));
                            dt.Columns.Add(new DataColumn("상한값"));
                            dt.Columns.Add(new DataColumn("적부판단"));

                            DataRow row = dt.NewRow();
                            //row["FitLotNo"] = curLotNo;
                            //row["하한값"] = curUnderVal;
                            //row["FIT결과값"] = curFitCount;
                            //row["상한값"] = curUpperVal;
                            //row["적부판단"] = curResult;

                            dt.Rows.Add(row);

                            // 조제공정에서만 기록 값 남도록

                            var xml = BizActorRuleBase.CreateXMLStream(ds);
                            var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                            //_mainWnd.CurrentInstruction.Raw.ACTVAL = curFitCount.ToString();
                            //_mainWnd.CurrentInstruction.Raw.NOTE = imageToByteArray();
                            _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                            _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction);
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

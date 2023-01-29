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
    public class SVP수동검사불량유형조회ViewModel : ViewModelBase
    {
        #region Properties
        public SVP수동검사불량유형조회ViewModel()
        {
            _RejectionDetails = new RejectionDetail.OUTDATACollection();
            _RejectionDetails2 = new RejectionDetail2.OUTDATACollection();
            _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO = new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO();
        }

        SVP수동검사불량유형조회 _mainWnd;

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

        private RejectionDetail2.OUTDATACollection _RejectionDetails2;
        public RejectionDetail2.OUTDATACollection RejectionDetails2
        {
            get { return _RejectionDetails2; }
            set
            {
                _RejectionDetails2 = value;
                OnPropertyChanged("RejectionDetails2");
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

        #region BizRule      
        #endregion

        #region Command
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

                            if (arg != null && arg is SVP수동검사불량유형조회)
                            {
                                _mainWnd = arg as SVP수동검사불량유형조회;

                                IsBusy = true;

                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Clear();
                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Clear();
                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Add(new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATA
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                                });

                                if (await _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.Execute() && _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Count > 0)
                                {
                                    var outData = _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs[0];
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
        public ICommand ComfirmCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ComfirmCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ComfirmCommandAsync"] = false;
                            CommandCanExecutes["ComfirmCommandAsync"] = false;

                            //이미지 저장시 서명화면으로 인해 이미지가 잘 안보임. 그에 따른 이미지 데이터만 먼저 생성해 놓도록 함.
                            Brush background = _mainWnd.PrintArea.Background;
                            _mainWnd.PrintArea.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD6, 0xD4, 0xD4));
                            _mainWnd.PrintArea.BorderThickness = new System.Windows.Thickness(1);
                            _mainWnd.PrintArea.Background = new SolidColorBrush(Colors.White);
                            
                            _mainWnd.CurrentInstruction.Raw.NOTE = imageToByteArray();
                            _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;

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
                            
                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction);
                            if (result != enumInstructionRegistErrorType.Ok)
                            {
                                throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                            }

                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                            IsBusy = false;

                            CommandResults["ComfirmCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ComfirmCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ComfirmCommandAsync"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ComfirmCommandAsync") ?
                        CommandCanExecutes["ComfirmCommandAsync"] : (CommandCanExecutes["ComfirmCommandAsync"] = true);
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

                private string _USERNAME = string.Empty;
                [BizActorOutputItemAttribute()]
                public string USERNAME
                {
                    get
                    {
                        return this._USERNAME;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._USERNAME = value;
                            this.CheckIsOriginal("USERNAME", value);
                            this.OnPropertyChanged("USERNAME");
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
                private DateTime _REJECTDATE = DateTime.Now;
                [BizActorOutputItemAttribute()]
                public DateTime REJECTDATE
                {
                    get
                    {
                        return this._REJECTDATE;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTDATE = value;
                            this.CheckIsOriginal("REJECTDATE", value);
                            this.OnPropertyChanged("REJECTDATE");
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
                private int _REJECTQTY = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTQTY
                {
                    get
                    {
                        return this._REJECTQTY;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTQTY = value;
                            this.CheckIsOriginal("RejectQty", value);
                            this.OnPropertyChanged("RejectQty");

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

                private int _REJECTNO_1 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_1
                {
                    get
                    {
                        return this._REJECTNO_1;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_1 = value;
                            this.CheckIsOriginal("RejectNo_1", value);
                            this.OnPropertyChanged("RejectNo_1");

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

                private int _REJECTNO_2 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_2
                {
                    get
                    {
                        return this._REJECTNO_2;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_2 = value;
                            this.CheckIsOriginal("RejectNo_2", value);
                            this.OnPropertyChanged("RejectNo_2");

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

                private int _REJECTNO_3 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_3
                {
                    get
                    {
                        return this._REJECTNO_3;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_3 = value;
                            this.CheckIsOriginal("RejectNo_3", value);
                            this.OnPropertyChanged("RejectNo_3");

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

                private int _REJECTNO_4 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_4
                {
                    get
                    {
                        return this._REJECTNO_4;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_4 = value;
                            this.CheckIsOriginal("RejectNo_4", value);
                            this.OnPropertyChanged("RejectNo_4");
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

                private int _REJECTNO_5 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_5
                {
                    get
                    {
                        return this._REJECTNO_5;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_5 = value;
                            this.CheckIsOriginal("RejectNo_5", value);
                            this.OnPropertyChanged("RejectNo_5");
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

                private int _REJECTNO_6 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_6
                {
                    get
                    {
                        return this._REJECTNO_6;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_6 = value;
                            this.CheckIsOriginal("RejectNo_6", value);
                            this.OnPropertyChanged("RejectNo_6");
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

                private int _REJECTNO_7 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_7
                {
                    get
                    {
                        return this._REJECTNO_7;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_7 = value;
                            this.CheckIsOriginal("RejectNo_7", value);
                            this.OnPropertyChanged("RejectNo_7");
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

                private int _REJECTNO_8 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_8
                {
                    get
                    {
                        return this._REJECTNO_8;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_8 = value;
                            this.CheckIsOriginal("RejectNo_8", value);
                            this.OnPropertyChanged("RejectNo_8");
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

                private int _REJECTNO_9 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_9
                {
                    get
                    {
                        return this._REJECTNO_9;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_9 = value;
                            this.CheckIsOriginal("REJECTNO_9", value);
                            this.OnPropertyChanged("REJECTNO_9");
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

                private int _REJECTNO_10 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_10
                {
                    get
                    {
                        return this._REJECTNO_10;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_10 = value;
                            this.CheckIsOriginal("RejectNo_10", value);
                            this.OnPropertyChanged("RejectNo_10");
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

                private int _REJECTNO_11 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_11
                {
                    get
                    {
                        return this._REJECTNO_11;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_11 = value;
                            this.CheckIsOriginal("RejectNo_11", value);
                            this.OnPropertyChanged("RejectNo_11");
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

        public class RejectionDetail2 : BizActorRuleBase
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

                private string _USERNAME2 = string.Empty;
                [BizActorOutputItemAttribute()]
                public string USERNAME2
                {
                    get
                    {
                        return this._USERNAME2;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._USERNAME2 = value;
                            this.CheckIsOriginal("USERNAME2", value);
                            this.OnPropertyChanged("USERNAME2");
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

                private DateTime _REJECTDATE2 = DateTime.Now;
                [BizActorOutputItemAttribute()]
                public DateTime REJECTDATE2
                {
                    get
                    {
                        return this._REJECTDATE2;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTDATE2 = value;
                            this.CheckIsOriginal("REJECTDATE2", value);
                            this.OnPropertyChanged("REJECTDATE2");
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

                private int _REJECTQTY2 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTQTY2
                {
                    get
                    {
                        return this._REJECTQTY2;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTQTY2 = value;
                            this.CheckIsOriginal("REJECTQTY2", value);
                            this.OnPropertyChanged("REJECTQTY2");

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

                private int _REJECTNO_12 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_12
                {
                    get
                    {
                        return this._REJECTNO_12;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_12 = value;
                            this.CheckIsOriginal("REJECTNO_12", value);
                            this.OnPropertyChanged("REJECTNO_12");
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
                private int _REJECTNO_13 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_13
                {
                    get
                    {
                        return this._REJECTNO_13;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_13 = value;
                            this.CheckIsOriginal("REJECTNO_13", value);
                            this.OnPropertyChanged("REJECTNO_13");
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

                private int _REJECTNO_14 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_14
                {
                    get
                    {
                        return this._REJECTNO_14;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_14 = value;
                            this.CheckIsOriginal("REJECTNO_14", value);
                            this.OnPropertyChanged("REJECTNO_14");
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

                private int _REJECTNO_15 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_15
                {
                    get
                    {
                        return this._REJECTNO_15;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_15 = value;
                            this.CheckIsOriginal("REJECTNO_15", value);
                            this.OnPropertyChanged("REJECTNO_15");
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

                private int _REJECTNO_16 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_16
                {
                    get
                    {
                        return this._REJECTNO_16;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_16 = value;
                            this.CheckIsOriginal("REJECTNO_16", value);
                            this.OnPropertyChanged("REJECTNO_16");

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

                private int _REJECTNO_17 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_17
                {
                    get
                    {
                        return this._REJECTNO_17;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_17 = value;
                            this.CheckIsOriginal("REJECTNO_17", value);
                            this.OnPropertyChanged("REJECTNO_17");

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

                private int _REJECTNO_18 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_18
                {
                    get
                    {
                        return this._REJECTNO_18;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_18 = value;
                            this.CheckIsOriginal("REJECTNO_18", value);
                            this.OnPropertyChanged("REJECTNO_18");

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

                private int _REJECTNO_19 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_19
                {
                    get
                    {
                        return this._REJECTNO_19;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_19 = value;
                            this.CheckIsOriginal("REJECTNO_19", value);
                            this.OnPropertyChanged("REJECTNO_19");
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

                private int _REJECTNO_20 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_20
                {
                    get
                    {
                        return this._REJECTNO_20;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_20 = value;
                            this.CheckIsOriginal("REJECTNO_20", value);
                            this.OnPropertyChanged("REJECTNO_20");
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

                private int _REJECTNO_21 = 0;
                [BizActorOutputItemAttribute()]
                public int REJECTNO_21
                {
                    get
                    {
                        return this._REJECTNO_21;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._REJECTNO_21 = value;
                            this.CheckIsOriginal("REJECTNO_21", value);
                            this.OnPropertyChanged("REJECTNO_21");
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
            public RejectionDetail2()
            {
                _OUTDATAs = new OUTDATACollection();
            }
        }

        public byte[] imageToByteArray()
        {
            try
            {
                C1Bitmap bitmap = new C1Bitmap(new WriteableBitmap(_mainWnd.PrintMain, null));
                System.IO.Stream stream = bitmap.GetStream(C1.Silverlight.Imaging.ImageFormat.Png, true);

                int len = (int)stream.Seek(0, SeekOrigin.End);

                byte[] datas = new byte[len];

                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(datas, 0, datas.Length);

                return datas;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}

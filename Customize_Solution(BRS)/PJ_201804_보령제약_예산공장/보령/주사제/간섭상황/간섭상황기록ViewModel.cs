using C1.Silverlight.Data;
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
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
using System.Collections.Generic;
using C1.Silverlight.Imaging;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Collections.ObjectModel;
using LGCNS.iPharmMES.Recipe.Common;

namespace 보령
{
    public class 간섭상황기록ViewModel : ViewModelBase
    {
        #region [Property]
        public 간섭상황기록ViewModel()
        {
            _InterferSituations = new InterferSituation.OUTDATACollection();

        }

        간섭상황기록 _mainWnd;

        private InterferSituation.OUTDATACollection _InterferSituations;
        public InterferSituation.OUTDATACollection InterferSituations
        {
            get { return this._InterferSituations; }
            set
            {
                this._InterferSituations = value;
                this.OnPropertyChanged("InterferSituations");
            }
        }
        private List<TimeCombobox> _COMBOItems;
        public List<TimeCombobox> COMBOItems
        {
            get { return _COMBOItems; }
            set
            {
                _COMBOItems = value;
                OnPropertyChanged("COMBOItems");
            }
        }
        #endregion

        #region [Bizrule]
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

                            if (arg != null && arg is 간섭상황기록)
                            {
                                _mainWnd = arg as 간섭상황기록;
                                IsBusy = true;
                                //IPCResultSections.Clear();

                                COMBOItems = SetComboBox(_COMBOItems);
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
        #endregion
        #region User Define
        private List<TimeCombobox> SetComboBox(List<TimeCombobox> cur)
        {
            cur = cur ?? new List<TimeCombobox>();
            cur.Clear();

            cur.Add(new TimeCombobox
            {
                KEY = "1",
                CONTENT = "환경모니터링",
                ACT = "환경모니터링을 수행한다."
            });
            cur.Add(new TimeCombobox
            {
                KEY = "2",
                CONTENT = "라인 Set-up",
                ACT = "1) 충전기와 충전 라인을 세팅한다. 2)여과 라인을 세팅한다."
            });
            cur.Add(new TimeCombobox
            {
                KEY = "3",
                CONTENT = "고무전 공급",
                ACT = "모듈 1번 고무전부에 적재된 고무전을 Hopper에 공급한다."
            });
            cur.Add(new TimeCombobox
            {
                KEY = "4",
                CONTENT = "Vial Reject용 무균 PE bag 교체",
                ACT = "Rejected vial로 가득 찬 무균 PE bag 들어올려 꺼낸 후 새로운 PE bag으로 교체한다."
            });
            cur.Add(new TimeCombobox
            {
                KEY = "5",
                CONTENT = "바이알 넘어짐 (Empty)",
                ACT = "핀셋을 이용해 제건한다."
            });

            return cur;
        }
        public class TimeCombobox
        {
            private string _KEY;
            public string KEY
            {
                get { return _KEY; }
                set { _KEY = value; }
            }
            private string _CONTENT;
            public string CONTENT
            {
                get { return _CONTENT; }
                set { _CONTENT = value; }
            }
            private string _ACT;
            public string ACT
            {
                get { return _ACT; }
                set { _ACT = value; }
            }
        }
        public class InterferSituation : BizActorRuleBase
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
                private int _NO = 0;
                [BizActorOutputItemAttribute()]
                public int NO
                {
                    get
                    {
                        return this._NO;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._NO = value;
                            this.CheckIsOriginal("NO", value);
                            this.OnPropertyChanged("NO");
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
                private string _CONTENTS;
                [BizActorOutputItemAttribute()]
                public string CONTENTS
                {
                    get
                    {
                        return this._CONTENTS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._CONTENTS = value;
                            this.CheckIsOriginal("CONTENTS", value);
                            this.OnPropertyChanged("CONTENTS");
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
                private string _ACTION;
                [BizActorOutputItemAttribute()]
                public string ACTION
                {
                    get
                    {
                        return this._ACTION;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._ACTION = value;
                            this.CheckIsOriginal("ACTION", value);
                            this.OnPropertyChanged("ACTION");
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
                private int _MODULE;
                [BizActorOutputItemAttribute()]
                public int MODULE
                {
                    get
                    {
                        return this._MODULE;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MODULE = value;
                            this.CheckIsOriginal("MODULE", value);
                            this.OnPropertyChanged("MODULE");
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
                private int _DISPOSAL;
                [BizActorOutputItemAttribute()]
                public int DISPOSAL
                {
                    get
                    {
                        return this._DISPOSAL;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._DISPOSAL = value;
                            this.CheckIsOriginal("DISPOSAL", value);
                            this.OnPropertyChanged("DISPOSAL");
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
                private DateTime _STRTDTTM;
                [BizActorOutputItemAttribute()]
                public DateTime STRTDTTM
                {
                    get
                    {
                        return this._STRTDTTM;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._STRTDTTM = value;
                            this.CheckIsOriginal("STRTDTTM", value);
                            this.OnPropertyChanged("STRTDTTM");
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
                private DateTime _ENDDTTM;
                [BizActorOutputItemAttribute()]
                public DateTime ENDDTTM
                {
                    get
                    {
                        return this._ENDDTTM;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._ENDDTTM = value;
                            this.CheckIsOriginal("ENDDTTM", value);
                            this.OnPropertyChanged("ENDDTTM");
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
                private string _NOTE;
                [BizActorOutputItemAttribute()]
                public string NOTE
                {
                    get
                    {
                        return this._NOTE;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._NOTE = value;
                            this.CheckIsOriginal("NOTE", value);
                            this.OnPropertyChanged("NOTE");
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
            public InterferSituation()
            {
                _OUTDATAs = new OUTDATACollection();
            }
        }
        #endregion
    }
}

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
using System.Globalization;
using LGCNS.iPharmMES.Recipe.Common;

namespace 보령
{
    public class 간섭상황기록ViewModel : ViewModelBase
    {
        #region [Property]
        public 간섭상황기록ViewModel()
        {
            _BR_PHR_SEL_CommonCode = new BR_PHR_SEL_CommonCode();
            _ListInterfer = new ObservableCollection<InterferSituation>();
        }

        간섭상황기록 _mainWnd;

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
                if (_STRTDTTM != null) this.KeyPadStrtTIme = String.Format("{0:HHmmss}", _STRTDTTM);
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
                if (_ENDDTTM != null) this.KeyPadEndTIme = String.Format("{0:HHmmss}", _ENDDTTM);
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

        string _KeyPadStrtTIme;
        public string KeyPadStrtTIme
        {
            get { return _KeyPadStrtTIme; }
            set
            {
                _KeyPadStrtTIme = value;
                OnPropertyChanged("KeyPadStrtTIme");
            }
        }
        string _KeyPadEndTIme;
        public string KeyPadEndTIme
        {
            get { return _KeyPadEndTIme; }
            set
            {
                _KeyPadEndTIme = value;
                OnPropertyChanged("KeyPadEndTIme");
            }
        }

        //간섭상황기록List
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

                            if (arg != null && arg is 간섭상황기록)
                            {
                                _mainWnd = arg as 간섭상황기록;

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
                                    return;
                                }
                                foreach (var item in _BR_PHR_SEL_CommonCode.OUTDATAs)
                                {
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

        public ICommand FromTimeChangedCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["FromTimeChangedCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["FromTimeChangedCommandAsync"] = false;
                            CommandCanExecutes["FromTimeChangedCommandAsync"] = false;

                            ///

                            var formats = new[] { "yyyyMMddHHmmss" };
                            DateTime dtRtn;

                            if (!DateTime.TryParseExact(String.Format("{0:yyyyMMdd}{1:D6}", this.STRTDTTM, Convert.ToInt32(this.KeyPadStrtTIme)), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtRtn))
                            {
                                OnMessage("잘못된 시간 형식입니다! 발생시각을 정확하게 입력하세요.");
                                return;
                            }

                            this.STRTDTTM = dtRtn;

                            ///

                            CommandResults["FromTimeChangedCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["FromTimeChangedCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["FromTimeChangedCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("FromTimeChangedCommandAsync") ?
                        CommandCanExecutes["FromTimeChangedCommandAsync"] : (CommandCanExecutes["FromTimeChangedCommandAsync"] = true);
                });
            }
        }

        public ICommand ToTimeChangedCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ToTimeChangedCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ToTimeChangedCommandAsync"] = false;
                            CommandCanExecutes["ToTimeChangedCommandAsync"] = false;

                            ///

                            var formats = new[] { "yyyyMMddHHmmss" };
                            DateTime dtRtn;

                            if (!DateTime.TryParseExact(String.Format("{0:yyyyMMdd}{1:D6}", this.ENDDTTM, Convert.ToInt32(this.KeyPadEndTIme)), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtRtn))
                            {
                                OnMessage("잘못된 시간 형식입니다! 완료시각을 정확하게 입력하세요.");
                                return;
                            }

                            this.ENDDTTM = dtRtn;

                            ///

                            CommandResults["ToTimeChangedCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ToTimeChangedCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ToTimeChangedCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ToTimeChangedCommandAsync") ?
                        CommandCanExecutes["ToTimeChangedCommandAsync"] : (CommandCanExecutes["ToTimeChangedCommandAsync"] = true);
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

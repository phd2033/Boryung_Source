using C1.Silverlight.Excel;
using LGCNS.iPharmMES.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Board
{
    public class InterfaceInfoViewModel : ViewModelBase
    {
        #region property

        private string _SelectedMode;
        public string SelectedMode
        {
            get { return _SelectedMode; }
            set
            {
                _SelectedMode = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _sapOrder = Visibility.Collapsed;
        public Visibility sapOrder
        {
            get { return _sapOrder; }
            set
            {
                _sapOrder = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _sapRoute = Visibility.Collapsed;
        public Visibility sapRoute
        {
            get { return _sapRoute; }
            set
            {
                _sapRoute = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _tnt = Visibility.Collapsed;
        public Visibility tnt
        {
            get { return _tnt; }
            set
            {
                _tnt = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _wms = Visibility.Collapsed;
        public Visibility wms
        {
            get { return _wms; }
            set
            {
                _wms = value;
                NotifyPropertyChanged();
            }
        }

        private InterfaceInfo _mainWnd;

        private DateTime _PeriodSTDTTM;
        public DateTime PeriodSTDTTM
        {
            get { return _PeriodSTDTTM; }
            set
            {
                _PeriodSTDTTM = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _PeriodEDDTTM;
        public DateTime PeriodEDDTTM
        {
            get { return _PeriodEDDTTM; }
            set
            {
                _PeriodEDDTTM = value;
                NotifyPropertyChanged();
            }
        }

        private bool _ISFERT;
        public bool ISFERT
        {
            get { return _ISFERT; }
            set
            {
                _ISFERT = value;
                NotifyPropertyChanged();
            }
        }

        private bool _ISHALB;
        public bool ISHALB
        {
            get { return _ISHALB; }
            set
            {
                _ISHALB = value;
                NotifyPropertyChanged();
            }
        }

        private string _MTRLID;
        public string MTRLID
        {
            get { return _MTRLID; }
            set
            {
                _MTRLID = value;
                NotifyPropertyChanged();
            }
        }

        private string _MTRLNAME;
        public string MTRLNAME
        {
            get { return _MTRLNAME; }
            set
            {
                _MTRLNAME = value;
                NotifyPropertyChanged();
            }
        }

        private string _BATCHNO;
        public string BATCHNO
        {
            get { return _BATCHNO; }
            set
            {
                _BATCHNO = value;
                NotifyPropertyChanged();
            }
        }

        private string _POID;
        public string POID
        {
            get { return _POID; }
            set
            {
                _POID = value;
                NotifyPropertyChanged();
            }
        }

        private string _PALLETID;
        public string PALLETID
        {
            get { return _PALLETID; }
            set
            {
                _PALLETID = value;
                NotifyPropertyChanged();
            }
        }

        private string _LD_CTN_NO;
        public string LD_CTN_NO
        {
            get { return _LD_CTN_NO; }
            set
            {
                _LD_CTN_NO = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Data

        private BR_PHR_GET_DEFAULT_DATE _BR_PHR_GET_DEFAULT_DATE;
        public BR_PHR_GET_DEFAULT_DATE BR_PHR_GET_DEFAULT_DATE
        {
            get { return _BR_PHR_GET_DEFAULT_DATE; }
            set
            {
                _BR_PHR_GET_DEFAULT_DATE = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_INTERFACE_INFO _BR_BRS_SEL_INTERFACE_INFO;
        public BR_BRS_SEL_INTERFACE_INFO BR_BRS_SEL_INTERFACE_INFO
        {
            get { return _BR_BRS_SEL_INTERFACE_INFO; }
            set
            {
                _BR_BRS_SEL_INTERFACE_INFO = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_INTERFACE_DETAIL_INFO _BR_BRS_SEL_INTERFACE_DETAIL_INFO;
        public BR_BRS_SEL_INTERFACE_DETAIL_INFO BR_BRS_SEL_INTERFACE_DETAIL_INFO
        {
            get { return _BR_BRS_SEL_INTERFACE_DETAIL_INFO; }
            set
            {
                _BR_BRS_SEL_INTERFACE_DETAIL_INFO = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region Command

        public ICommand LoadedCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["LoadedCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["LoadedCommand"] = false;
                            CommandCanExecutes["LoadedCommand"] = false;

                            sapOrder = Visibility.Visible;
                            ///

                            if (arg == null || !(arg is InterfaceInfo))
                                return;
                            _mainWnd = arg as InterfaceInfo;

                            BR_PHR_GET_DEFAULT_DATE.INDATAs.Clear();
                            BR_PHR_GET_DEFAULT_DATE.OUTDATAs.Clear();

                            BR_PHR_GET_DEFAULT_DATE.INDATAs.Add(new BR_PHR_GET_DEFAULT_DATE.INDATA()
                            {
                                PROGRAMID = "인터페이스정보조회"
                            });

                            if (!await BR_PHR_GET_DEFAULT_DATE.Execute()) throw new Exception();

                            PeriodSTDTTM = DateTime.Parse(BR_PHR_GET_DEFAULT_DATE.OUTDATAs[0].FROMDATE.Substring(0, 4) + "-" +
                                                          BR_PHR_GET_DEFAULT_DATE.OUTDATAs[0].FROMDATE.Substring(4, 2) + "-" +
                                                          BR_PHR_GET_DEFAULT_DATE.OUTDATAs[0].FROMDATE.Substring(6, 2));
                            PeriodEDDTTM = DateTime.Parse(BR_PHR_GET_DEFAULT_DATE.OUTDATAs[0].TODATE.Substring(0, 4) + "-" +
                                                          BR_PHR_GET_DEFAULT_DATE.OUTDATAs[0].TODATE.Substring(4, 2) + "-" +
                                                          BR_PHR_GET_DEFAULT_DATE.OUTDATAs[0].TODATE.Substring(6, 2));

                            ///

                            CommandResults["LoadedCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["LoadedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadedCommand") ?
                        CommandCanExecutes["LoadedCommand"] : (CommandCanExecutes["LoadedCommand"] = true);
                });
            }
        }

        public ICommand BtnSearchCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["BtnSearchCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;
                            
                            CommandResults["BtnSearchCommand"] = false;
                            CommandCanExecutes["BtnSearchCommand"] = false;

                            sapOrder = Visibility.Collapsed;
                            sapRoute = Visibility.Collapsed;
                            tnt = Visibility.Collapsed;
                            wms = Visibility.Collapsed;

                            if (string.IsNullOrEmpty(SelectedMode))
                            {
                                throw new Exception("I/F 시스템 정보가 올바르지않습니다.");
                            }
                            else if (SelectedMode.Equals("SAP_Order"))
                            {
                                sapOrder = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("SAP_Route"))
                            {
                                sapRoute = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("TnT"))
                            {
                                tnt = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("WMS"))
                            {
                                wms = Visibility.Visible;
                            }
                            

                            BR_BRS_SEL_INTERFACE_INFO.INDATAs.Clear();
                            BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_ORDERs.Clear();
                            BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_Routes.Clear();
                            BR_BRS_SEL_INTERFACE_INFO.OUTDATA_TNTs.Clear();
                            BR_BRS_SEL_INTERFACE_INFO.OUTDATA_WMSs.Clear();

                            BR_BRS_SEL_INTERFACE_INFO.INDATAs.Add(new BR_BRS_SEL_INTERFACE_INFO.INDATA()
                            {   
                                GUBUN = SelectedMode,
                                FROMDATE = PeriodSTDTTM,
                                TODATE = PeriodEDDTTM,
                                POID = POID,
                                BATCHNO = BATCHNO,
                                PALLETID = PALLETID,
                                LD_CTN_NO = LD_CTN_NO != "" ? LD_CTN_NO : null
                            });

                            if (!await BR_BRS_SEL_INTERFACE_INFO.Execute()) throw new Exception();
                            ///

                            CommandResults["BtnSearchCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["BtnSearchCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["BtnSearchCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("BtnSearchCommand") ?
                        CommandCanExecutes["BtnSearchCommand"] : (CommandCanExecutes["BtnSearchCommand"] = true);
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

                            if (arg == null) return;
                        
                            if (arg is BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_ORDER)
                            {
                                var rowdata = arg as BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_ORDER;

                                BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATAs.Clear();

                                BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATAs.Add(new BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATA()
                                {
                                    POID = rowdata.AUFNR,
                                    GUBUN = SelectedMode
                                });
                            }
                            else if (arg is BR_BRS_SEL_INTERFACE_INFO.OUTDATA_TNT)
                            {
                                var rowdata = arg as BR_BRS_SEL_INTERFACE_INFO.OUTDATA_TNT;

                                BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATAs.Clear();

                                BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATAs.Add(new BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATA()
                                {
                                    POID = rowdata.POID,
                                    GUBUN = SelectedMode
                                });
                            }
                            else if (arg is BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_Route)
                            {
                                var rowdata = arg as BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_Route;

                                BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATAs.Clear();

                                BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATAs.Add(new BR_BRS_SEL_INTERFACE_DETAIL_INFO.INDATA()
                                {
                                    MATNR = rowdata.MATNR,
                                    PLNAL = rowdata.PLNAL,
                                    PLNNR = rowdata.PLNNR,
                                    GUBUN = SelectedMode
                                });
                            }

                            if (!await BR_BRS_SEL_INTERFACE_DETAIL_INFO.Execute()) throw new Exception();

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

        public ICommand ComboFieldDataChangedCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ComboFieldDataChangedCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["ComboFieldDataChangedCommand"] = false;
                            CommandCanExecutes["ComboFieldDataChangedCommand"] = false;

                            IsBusy = true;

                            sapOrder = Visibility.Collapsed;
                            sapRoute = Visibility.Collapsed;
                            tnt = Visibility.Collapsed;
                            wms = Visibility.Collapsed;

                            if (SelectedMode.Equals("SAP_Order"))
                            {
                                sapOrder = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("SAP_Route"))
                            {
                                sapRoute = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("TnT"))
                            {
                                tnt = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("WMS"))
                            {
                                wms = Visibility.Visible;
                            }


                            IsBusy = false;

                            CommandResults["ComboFieldDataChangedCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ComboFieldDataChangedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ComboFieldDataChangedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ComboFieldDataChangedCommand") ?
                        CommandCanExecutes["ComboFieldDataChangedCommand"] : (CommandCanExecutes["ComboFieldDataChangedCommand"] = true);
                });
            }
        }
        #endregion

        public InterfaceInfoViewModel()
        {
            _BR_PHR_GET_DEFAULT_DATE = new BR_PHR_GET_DEFAULT_DATE();
            _BR_BRS_SEL_INTERFACE_INFO = new BR_BRS_SEL_INTERFACE_INFO();
            _BR_BRS_SEL_INTERFACE_DETAIL_INFO = new BR_BRS_SEL_INTERFACE_DETAIL_INFO();

            ISFERT = true;
        }
    }
}

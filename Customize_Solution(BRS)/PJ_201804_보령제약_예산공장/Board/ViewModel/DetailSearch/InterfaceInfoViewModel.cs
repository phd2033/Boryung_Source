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

        private BR_BRS_SEL_SAP_R_ProductionOrderInfo _BR_BRS_SEL_SAP_R_ProductionOrderInfo;
        public BR_BRS_SEL_SAP_R_ProductionOrderInfo BR_BRS_SEL_SAP_R_ProductionOrderInfo
        {
            get { return _BR_BRS_SEL_SAP_R_ProductionOrderInfo; }
            set
            {
                _BR_BRS_SEL_SAP_R_ProductionOrderInfo = value;
                NotifyPropertyChanged();
            }
        }

        private BR_BRS_SEL_SAP_R_ProductionOrderDetail _BR_BRS_SEL_SAP_R_ProductionOrderDetail;
        public BR_BRS_SEL_SAP_R_ProductionOrderDetail BR_BRS_SEL_SAP_R_ProductionOrderDetail
        {
            get { return _BR_BRS_SEL_SAP_R_ProductionOrderDetail; }
            set
            {
                _BR_BRS_SEL_SAP_R_ProductionOrderDetail = value;
                NotifyPropertyChanged();
            }
        }

        //private BR_BRS_SEL_TnTOutputInfoList _BR_BRS_SEL_TnTOutputInfoList;
        //public BR_BRS_SEL_TnTOutputInfoList BR_BRS_SEL_TnTOutputInfoList
        //{
        //    get { return _BR_BRS_SEL_TnTOutputInfoList; }
        //    set
        //    {
        //        _BR_BRS_SEL_TnTOutputInfoList = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //private BR_BRS_SEL_TnTWorkOrderComplete _BR_BRS_SEL_TnTWorkOrderComplete;
        //public BR_BRS_SEL_TnTWorkOrderComplete BR_BRS_SEL_TnTWorkOrderComplete
        //{
        //    get { return _BR_BRS_SEL_TnTWorkOrderComplete; }
        //    set
        //    {
        //        _BR_BRS_SEL_TnTWorkOrderComplete = value;
        //        NotifyPropertyChanged();
        //    }
        //}


        private BR_PHR_RPT_ProductionOrderDetail_Result _BR_PHR_RPT_ProductionOrderDetail_Result;
        public BR_PHR_RPT_ProductionOrderDetail_Result BR_PHR_RPT_ProductionOrderDetail_Result
        {
            get { return _BR_PHR_RPT_ProductionOrderDetail_Result; }
            set
            {
                _BR_PHR_RPT_ProductionOrderDetail_Result = value;
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

                            if (string.IsNullOrEmpty(SelectedMode))
                            {
                                throw new Exception("I/F 시스템 정보가 올바르지않습니다.");
                            }
                            else if (SelectedMode.Equals("SAP_Order"))
                            {
                                sapOrder = Visibility.Visible;
                            }
                            else if (SelectedMode.Equals("TnT"))
                            {
                                tnt = Visibility.Visible;
                            }

                            BR_BRS_SEL_SAP_R_ProductionOrderInfo.INDATAs.Clear();
                            BR_BRS_SEL_SAP_R_ProductionOrderInfo.OUTDATAs.Clear();

                            BR_BRS_SEL_SAP_R_ProductionOrderInfo.INDATAs.Add(new BR_BRS_SEL_SAP_R_ProductionOrderInfo.INDATA()
                            {                               
                                FROMDATE = PeriodSTDTTM,
                                TODATE = PeriodEDDTTM,
                                POID = POID,
                                BATCHNO = BATCHNO,
                                MTRLID = MTRLID,
                                MTRLNAME = MTRLNAME
                            });

                            if (!await BR_BRS_SEL_SAP_R_ProductionOrderInfo.Execute()) throw new Exception();
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

                            ///
                            if (arg == null || !(arg is BR_BRS_SEL_SAP_R_ProductionOrderInfo.OUTDATA))
                                return;
                            var rowdata = arg as BR_BRS_SEL_SAP_R_ProductionOrderInfo.OUTDATA;

                            BR_BRS_SEL_SAP_R_ProductionOrderDetail.INDATAs.Clear();
                            BR_BRS_SEL_SAP_R_ProductionOrderDetail.OUTDATAs.Clear();
                            BR_BRS_SEL_SAP_R_ProductionOrderDetail.INDATAs.Add(new BR_BRS_SEL_SAP_R_ProductionOrderDetail.INDATA()
                            {
                                POID = rowdata.AUFNR
                            });

                            if (!await BR_BRS_SEL_SAP_R_ProductionOrderDetail.Execute()) throw new Exception();
                            ///

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
        #endregion

        public InterfaceInfoViewModel()
        {
            _BR_PHR_GET_DEFAULT_DATE = new BR_PHR_GET_DEFAULT_DATE();
            _BR_BRS_SEL_SAP_R_ProductionOrderInfo = new BR_BRS_SEL_SAP_R_ProductionOrderInfo();
            _BR_PHR_RPT_ProductionOrderDetail_Result = new BR_PHR_RPT_ProductionOrderDetail_Result();
            _BR_BRS_SEL_SAP_R_ProductionOrderDetail = new BR_BRS_SEL_SAP_R_ProductionOrderDetail();
            //_BR_BRS_SEL_TnTOutputInfoList = new BR_BRS_SEL_TnTOutputInfoList();
            //_BR_BRS_SEL_TnTWorkOrderComplete = new BR_BRS_SEL_TnTWorkOrderComplete();

            ISFERT = true;
        }
    }
}

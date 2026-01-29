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

        private string _VesselType;
        public string VesselType
        {
            get { return _VesselType; }
            set
            {
                _VesselType = value;
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

        private Visibility _lims = Visibility.Collapsed;
        public Visibility lims
        {
            get { return _lims; }
            set
            {
                _lims = value;
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

        private string _OPSGNAME;
        public string OPSGNAME
        {
            get { return _OPSGNAME; }
            set
            {
                _OPSGNAME = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Data

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

        private BR_PHR_SEL_ProcessSegment _BR_PHR_SEL_ProcessSegment;
        public BR_PHR_SEL_ProcessSegment BR_PHR_SEL_ProcessSegment
        {
            get { return _BR_PHR_SEL_ProcessSegment; }
            set
            {
                _BR_PHR_SEL_ProcessSegment = value;
                NotifyPropertyChanged();
            }
        }
        // 시험의뢰 적합여부 확인
        private BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM;
        public BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM
        {
            get { return _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM; }
            set
            {
                _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM = value;
                OnPropertyChanged("BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM");
            }
        }

        private BR_BRS_INS_TST_APPROVAL_CANCELL _BR_BRS_INS_TST_APPROVAL_CANCELL;
        public BR_BRS_INS_TST_APPROVAL_CANCELL BR_BRS_INS_TST_APPROVAL_CANCELL
        {
            get { return _BR_BRS_INS_TST_APPROVAL_CANCELL; }
            set
            {
                _BR_BRS_INS_TST_APPROVAL_CANCELL = value;
                OnPropertyChanged("BR_BRS_INS_TST_APPROVAL_CANCELL");
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

                            if (arg == null || !(arg is InterfaceInfo))
                                return;
                            _mainWnd = arg as InterfaceInfo;

                            PeriodEDDTTM = await AuthRepositoryViewModel.GetDBDateTimeNow();
                            PeriodSTDTTM = PeriodEDDTTM.AddDays(-7);

                            _BR_PHR_SEL_ProcessSegment.INDATAs.Clear();
                            _BR_PHR_SEL_ProcessSegment.OUTDATAs.Clear();

                            _BR_PHR_SEL_ProcessSegment.INDATAs.Add(new BR_PHR_SEL_ProcessSegment.INDATA()
                            {
                                ISUSE = "Y"
                            });
                            if (await _BR_PHR_SEL_ProcessSegment.Execute())
                            {
                                _BR_PHR_SEL_ProcessSegment.OUTDATAs.Insert(0, new BR_PHR_SEL_ProcessSegment.OUTDATA
                                {
                                    PCSGNAME = "N/A"
                                });
                            }
                            else throw new Exception();

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
                            lims = Visibility.Collapsed;

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
                            else if (SelectedMode.Equals("LIMS"))
                            {
                                lims = Visibility.Visible;
                            }


                            _BR_BRS_SEL_INTERFACE_INFO.INDATAs.Clear();
                            _BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_ORDERs.Clear();
                            _BR_BRS_SEL_INTERFACE_INFO.OUTDATA_SAP_Routes.Clear();
                            _BR_BRS_SEL_INTERFACE_INFO.OUTDATA_TNTs.Clear();
                            _BR_BRS_SEL_INTERFACE_INFO.OUTDATA_WMSs.Clear();

                            _BR_BRS_SEL_INTERFACE_INFO.INDATAs.Add(new BR_BRS_SEL_INTERFACE_INFO.INDATA()
                            {   
                                GUBUN = SelectedMode,
                                FROMDATE = PeriodSTDTTM,
                                TODATE = PeriodEDDTTM,
                                POID = POID != "" ? POID : null,
                                MTRLID = MTRLID != "" ? MTRLID : null,
                                MTRLNAME = MTRLNAME != "" ? MTRLNAME : null,
                                BATCHNO = BATCHNO != "" ? BATCHNO : null,
                                PALLETID = PALLETID != "" ? PALLETID : null,
                                LD_CTN_NO = LD_CTN_NO != "" ? LD_CTN_NO : null,
                                OPSGNAME = (string.IsNullOrEmpty(OPSGNAME) || OPSGNAME == "N/A") ? null : OPSGNAME,
                                LD_CTN_TYP = VesselType
                            });

                            await _BR_BRS_SEL_INTERFACE_INFO.Execute();

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
                                    GUBUN = SelectedMode,
                                    SEQ = rowdata.SEQ
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
                                    IF_NO = rowdata.IF_NO,
                                    MATNR = rowdata.MATNR,
                                    PLNAL = rowdata.PLNAL,
                                    PLNNR = rowdata.PLNNR,
                                    GUBUN = SelectedMode,
                                    SEQ = rowdata.SEQ
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
                            lims = Visibility.Collapsed;

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
                            else if (SelectedMode.Equals("LIMS"))
                            {
                                lims = Visibility.Visible;
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

        public ICommand BtnApprovalCancelCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ApprovalCancelCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ApprovalCancelCommand"] = false;
                            CommandCanExecutes["ApprovalCancelCommand"] = false;

                            var temp = _mainWnd.dgLims.SelectedItem as BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM.OUTDATA;

                            if (temp == null)
                            {
                                OnMessage("승인 취소할 시험의뢰를 선택해주세요");
                            }
                            else
                            {
                                if (temp.UD_TYPE == "승인전")
                                {
                                    _BR_BRS_INS_TST_APPROVAL_CANCELL.INDATAs.Clear();

                                    _BR_BRS_INS_TST_APPROVAL_CANCELL.INDATAs.Add(new BR_BRS_INS_TST_APPROVAL_CANCELL.INDATA()
                                    {
                                        POID = temp.POID,
                                        ITEM_TYPE = temp.ITEM_TYPE,
                                        TST_REQ_NO = temp.TST_REQ_NO
                                    });

                                    await _BR_BRS_INS_TST_APPROVAL_CANCELL.Execute();

                                    OnMessage("시험의뢰 취소 완료");

                                    _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM.INDATAs.Clear();
                                    _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM.OUTDATAs.Clear();

                                    _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM.INDATAs.Add(new BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM.INDATA()
                                    {
                                        FROMDATE = PeriodSTDTTM,
                                        TODATE = PeriodEDDTTM
                                    });

                                    await _BR_BRS_SEL_TST_REQUEST_NUMBER_CONFIRM.Execute();
                                }
                                else
                                {
                                    OnMessage("승인전 시험의뢰가 아닙니다");
                                }
                            }


                            CommandResults["ApprovalCancelCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ApprovalCancelCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ApprovalCancelCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ApprovalCancelCommand") ?
                        CommandCanExecutes["ApprovalCancelCommand"] : (CommandCanExecutes["ApprovalCancelCommand"] = true);
                });
            }
        }

        public ICommand ClickExportExcelCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ClickExportExcelCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ClickExportExcelCommand"] = false;
                            CommandCanExecutes["ClickExportExcelCommand"] = false;

                            ///
                            Custom_C1ExportExcel customExcel = new Custom_C1ExportExcel();

                            if (SelectedMode.Equals("SAP_Order"))
                            {
                                customExcel.SaveBook(book =>
                                {
                                    C1.Silverlight.Excel.XLSheet sheet = book.Sheets[0];
                                    sheet.Name = "오더 정보";
                                    customExcel.InitHeaderExcel(book, sheet, _mainWnd.dgSapOrder);

                                    C1.Silverlight.Excel.XLSheet sheet2 = book.Sheets.Add();
                                    sheet2.Name = "오더 상세 정보";
                                    customExcel.InitHeaderExcel(book, sheet2, _mainWnd.dgSapOrderDetail);

                                    C1.Silverlight.Excel.XLSheet sheet3 = book.Sheets.Add();
                                    sheet3.Name = "오더 BOM 정보";
                                    customExcel.InitHeaderExcel(book, sheet3, _mainWnd.dgSapOrdrBom);
                                });
                            }
                            else if (SelectedMode.Equals("SAP_Route"))
                            {
                                customExcel.SaveBook(book =>
                                {
                                    C1.Silverlight.Excel.XLSheet sheet = book.Sheets[0];
                                    sheet.Name = "라우트 정보";
                                    customExcel.InitHeaderExcel(book, sheet, _mainWnd.dgSapRoute);

                                    C1.Silverlight.Excel.XLSheet sheet2 = book.Sheets.Add();
                                    sheet2.Name = "라우트 공정 정보";
                                    customExcel.InitHeaderExcel(book, sheet2, _mainWnd.dgSapRouteDetail);

                                    C1.Silverlight.Excel.XLSheet sheet3 = book.Sheets.Add();
                                    sheet3.Name = "라우트 BOM 정보";
                                    customExcel.InitHeaderExcel(book, sheet3, _mainWnd.dgSapRouteBom);
                                });
                            }
                            else if (SelectedMode.Equals("TnT"))
                            {
                                customExcel.SaveBook(book =>
                                {
                                    C1.Silverlight.Excel.XLSheet sheet = book.Sheets[0];
                                    sheet.Name = "실적 정보";
                                    customExcel.InitHeaderExcel(book, sheet, _mainWnd.dgTnt);

                                    C1.Silverlight.Excel.XLSheet sheet2 = book.Sheets.Add();
                                    sheet2.Name = "최종 실적 정보";
                                    customExcel.InitHeaderExcel(book, sheet2, _mainWnd.dgTntLast);
                                });
                            }
                            else if (SelectedMode.Equals("WMS"))
                            {
                                customExcel.SaveBook(book =>
                                {
                                    C1.Silverlight.Excel.XLSheet sheet = book.Sheets[0];
                                    sheet.Name = "WMS 용기 입고 정보";
                                    customExcel.InitHeaderExcel(book, sheet, _mainWnd.dgWms);
                                });
                            }
                            else if (SelectedMode.Equals("LIMS"))
                            {
                                customExcel.SaveBook(book =>
                                {
                                    C1.Silverlight.Excel.XLSheet sheet = book.Sheets[0];
                                    sheet.Name = "LIMS 시험 의뢰 결과";
                                    customExcel.InitHeaderExcel(book, sheet, _mainWnd.dgLims);
                                });
                            }

                            //customExcel.SaveBook(book =>
                            //{
                            //    C1.Silverlight.Excel.XLSheet sheet = book.Sheets[0];
                            //    customExcel.InitHeaderExcel(book, sheet, _mainWnd.dgLims);
                            //});
                            /////

                            CommandResults["ClickExportExcelCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ClickExportExcelCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ClickExportExcelCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ClickExportExcelCommand") ?
                        CommandCanExecutes["ClickExportExcelCommand"] : (CommandCanExecutes["ClickExportExcelCommand"] = true);
                });
            }
        }
        #endregion

        public InterfaceInfoViewModel()
        {
            _BR_BRS_SEL_INTERFACE_INFO = new BR_BRS_SEL_INTERFACE_INFO();
            _BR_BRS_SEL_INTERFACE_DETAIL_INFO = new BR_BRS_SEL_INTERFACE_DETAIL_INFO();
            _BR_PHR_SEL_ProcessSegment = new BR_PHR_SEL_ProcessSegment();

            ISFERT = true;
        }
    }
}

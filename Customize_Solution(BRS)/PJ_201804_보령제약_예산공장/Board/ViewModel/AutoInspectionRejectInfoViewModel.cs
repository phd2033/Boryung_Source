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
using Board;

namespace Board
{
    public class AutoInspectionRejectInfoViewModel : ViewModelBase
    {
        #region ##### property ##### 
        private AutoInspectionRejectInfo _mainWnd;
        

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

        private string _BatchNo;
        public string BatchNo
        {
            get { return _BatchNo; }
            set
            {
                _BatchNo = value;
                NotifyPropertyChanged();
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                NotifyPropertyChanged();
            }
        }

        private string _MtrlId;
        public string MtrlId
        {
            get { return _MtrlId; }
            set
            {
                _MtrlId = value;
                NotifyPropertyChanged();
            }
        }

        private string _MtrlName;
        public string MtrlName
        {
            get { return _MtrlName; }
            set
            {
                _MtrlName = value;
                NotifyPropertyChanged();
            }
        }
        private string _OAIMID;
        public string OAIMID
        {
            get { return _OAIMID; }
            set
            {
                _OAIMID = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _OAIM2001 = Visibility.Collapsed;
        public Visibility OAIM2001
        {
            get { return _OAIM2001; }
            set
            {
                _OAIM2001 = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _OAIM2002 = Visibility.Collapsed;
        public Visibility OAIM2002
        {
            get { return _OAIM2002; }
            set
            {
                _OAIM2002 = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _OAIM2003_C = Visibility.Collapsed;
        public Visibility OAIM2003_C
        {
            get { return _OAIM2003_C; }
            set
            {
                _OAIM2003_C = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _OAIM2003_T = Visibility.Collapsed;
        public Visibility OAIM2003_T
        {
            get { return _OAIM2003_T; }
            set
            {
                _OAIM2003_T = value;
                NotifyPropertyChanged();
            }
        }

        #endregion ##### property #####

        #region [BizRule]

        private BR_BRS_SEL_AUTO_INSPECTION _BR_BRS_SEL_AUTO_INSPECTION;
        public BR_BRS_SEL_AUTO_INSPECTION BR_BRS_SEL_AUTO_INSPECTION
        {
            get { return _BR_BRS_SEL_AUTO_INSPECTION; }
            set
            {
                _BR_BRS_SEL_AUTO_INSPECTION = value;
                OnPropertyChanged("BR_BRS_SEL_AUTO_INSPECTION");
            }
        }

        private BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO;
        public BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO
        {
            get { return _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO; }
            set
            {
                _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO = value;
                OnPropertyChanged("BR_BRS_SEL_AUTO_INSPECTION");
            }
        }
        #endregion

        public AutoInspectionRejectInfoViewModel()
        {
            _BR_BRS_SEL_AUTO_INSPECTION = new BR_BRS_SEL_AUTO_INSPECTION();
            _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO = new BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO();
        }

        public ICommand LoadedCommandAsync
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

                            if (arg == null || !(arg is AutoInspectionRejectInfo))
                                return;
                            _mainWnd = arg as AutoInspectionRejectInfo;

                            PeriodEDDTTM = await AuthRepositoryViewModel.GetDBDateTimeNow();
                            PeriodSTDTTM = PeriodEDDTTM.AddDays(-7);

                            _BR_BRS_SEL_AUTO_INSPECTION.INDATAs.Clear();
                            _BR_BRS_SEL_AUTO_INSPECTION.OUTDATAs.Clear();

                            await _BR_BRS_SEL_AUTO_INSPECTION.Execute();                            

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
                    using (await AwaitableLocks["SearchCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["SearchCommand"] = false;
                            CommandCanExecutes["SearchCommand"] = false;

                            OAIM2001 = Visibility.Collapsed;
                            OAIM2003_T = Visibility.Collapsed;
                            OAIM2003_C = Visibility.Collapsed;

                            if (string.IsNullOrEmpty(OAIMID))
                            {
                                throw new Exception("선별기 정보가 올바르지않습니다.");
                            }
                            else if (OAIMID.Equals("OAIM2001") || OAIMID.Equals("OAIM2002"))
                            {
                                OAIM2001 = Visibility.Visible;                                
                            }
                            else if (OAIMID.Equals("OAIM2003(Tablet)"))
                            {
                                OAIM2003_T = Visibility.Visible;
                            }
                            else
                            {
                                OAIM2003_C = Visibility.Visible;
                            }

                            _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO.INDATAs.Clear();
                            _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO.OUTDATAs.Clear();

                            _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO.INDATAs.Add(new BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO.INDATA()
                            {
                                FROMDTTM = PeriodSTDTTM,
                                TODTTM = PeriodEDDTTM,
                                MTRLID = MtrlId != "" ? MtrlId : null,
                                MTRLNAME = MtrlName != "" ? MtrlName : null,
                                BATCHNO = BatchNo != "" ? BatchNo : null,
                                EQPTID = OAIMID != "" ? OAIMID : null
                            });

                            await _BR_BRS_SEL_AUTO_INSPECTION_REJECT_INFO.Execute();

                            CommandResults["SearchCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SearchCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["SearchCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SearchCommand") ?
                        CommandCanExecutes["SearchCommand"] : (CommandCanExecutes["SearchCommand"] = true);
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

                            customExcel.SaveBook(book =>
                            {
                                book.Sheets.Add();
                                C1.Silverlight.Excel.XLSheet Firsheet = book.Sheets[0];
                                if (OAIMID.Equals("OAIM2001") || OAIMID.Equals("OAIM2002"))
                                {
                                    customExcel.InitHeaderExcel(book, Firsheet, _mainWnd.OAIM2001);
                                }
                                else if (OAIMID.Equals("OAIM2003(Tablet)"))
                                {
                                    customExcel.InitHeaderExcel(book, Firsheet, _mainWnd.OAIM2003_T);
                                }
                                else
                                {
                                    customExcel.InitHeaderExcel(book, Firsheet, _mainWnd.OAIM2003_C);
                                }
                                
                            });
                            ///

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
    }
}

using C1.Silverlight.Excel;
using LGCNS.iPharmMES.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class MailRecipientViewViewModel : ViewModelBase
    {
        #region ##### property ##### 
        private MailRecipientView _mainWnd;
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

        private string _USERID;
        public string USERID
        {
            get { return _USERID; }
            set
            {
                _USERID = value;
                NotifyPropertyChanged();
            }
        }

        private string _CMCDTYPE;
        public string CMCDTYPE
        {
            get { return _CMCDTYPE; }
            set
            {
                _CMCDTYPE = value;
                NotifyPropertyChanged();
            }
        }

        private string _CMCODE;
        public string CMCODE
        {
            get { return _CMCODE; }
            set
            {
                _CMCODE = value;
                NotifyPropertyChanged();
            }
        }

        private string _CMCDNAME;
        public string CMCDNAME
        {
            get { return _CMCDNAME; }
            set
            {
                _CMCDNAME = value;
                NotifyPropertyChanged();
            }
        }

        private string _USERNAME;
        public string USERNAME
        {
            get { return _USERNAME; }
            set
            {
                _USERNAME = value;
                NotifyPropertyChanged();
            }
        }

        private string _USERMAIL;
        public string USERMAIL
        {
            get { return _USERMAIL; }
            set
            {
                _USERMAIL = value;
                NotifyPropertyChanged();
            }
        }

        private string _ISUSE;
        public string ISUSE
        {
            get { return _ISUSE; }
            set
            {
                _ISUSE = value;
                NotifyPropertyChanged();
            }
        }



        private Boolean _CHK;
        public Boolean CHK
        {
            get { return _CHK; }
            set
            {
                if (_CHK != value)
                {
                    _CHK = value;
                    NotifyPropertyChanged(nameof(CHK));

                }
            }
        }

        #endregion ##### property #####

        #region [BizRule]

        private BR_BRS_SEL_SEND_MAIL_TO_LIST _BR_BRS_SEL_SEND_MAIL_TO_LIST;
        public BR_BRS_SEL_SEND_MAIL_TO_LIST BR_BRS_SEL_SEND_MAIL_TO_LIST
        {
            get { return _BR_BRS_SEL_SEND_MAIL_TO_LIST; }
            set
            {
                _BR_BRS_SEL_SEND_MAIL_TO_LIST = value;
                OnPropertyChanged("BR_BRS_SEL_SEND_MAIL_TO_LIST");
            }
        }

        private BR_BRS_REG_UDT_SEND_MAIL_TO_LIST _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST;
        public BR_BRS_REG_UDT_SEND_MAIL_TO_LIST BR_BRS_REG_UDT_SEND_MAIL_TO_LIST
        {
            get { return _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST; }
            set
            {
                _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST = value;
                OnPropertyChanged("BR_BRS_REG_UDT_SEND_MAIL_TO_LIST");
            }
        }

        #endregion

        public MailRecipientViewViewModel()
        {
            _BR_BRS_SEL_SEND_MAIL_TO_LIST = new BR_BRS_SEL_SEND_MAIL_TO_LIST();
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
                            
                            
                            _mainWnd = arg as MailRecipientView;
                            

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

                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.INDATAs.Clear();
                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATAs.Clear();

                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.INDATAs.Add(new BR_BRS_SEL_SEND_MAIL_TO_LIST.INDATA()
                            {
                                USERID = string.IsNullOrWhiteSpace(USERID) ? null : USERID
                            });

                            await _BR_BRS_SEL_SEND_MAIL_TO_LIST.Execute();

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

        public ICommand BtnUpdateCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["BtnUpdateCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["BtnUpdateCommand"] = false;
                            CommandCanExecutes["BtnUpdateCommand"] = false;
                       
                            //foreach (var item in _BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATAs)
                            //{
                            //    if (item.CHK == "Y")
                            //    {
                            //        _BR_BRS_REG_UDT_OperationProcessSegmentReady.INDATAs.Add(new BR_BRS_REG_UDT_OperationProcessSegmentReady.INDATA
                            //        {
                            //            ODID = item.ODID,
                            //            ODVER = item.ODVER,
                            //            MTRLID = item.MTRLID,
                            //            MTRLVER = item.MTRLVER,
                            //            OPSGGUID = item.OPSGGUID,
                            //            READYVAL = item.READYVAL,
                            //            READYVER = item.READYVER,
                            //            EQPTREADYVAL = item.EQPTREADYVAL,
                            //            INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID
                            //        });
                            //    }
                            //}

                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATAs.Clear();

                            await _BR_BRS_SEL_SEND_MAIL_TO_LIST.Execute();

                            CommandResults["BtnUpdateCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["BtnUpdateCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["BtnUpdateCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("BtnUpdateCommand") ?
                        CommandCanExecutes["BtnUpdateCommand"] : (CommandCanExecutes["BtnUpdateCommand"] = true);
                });
            }
        }

        public ICommand RowEditCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RowEditCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["RowEditCommand"] = false;
                            CommandCanExecutes["RowEditCommand"] = false;

                            IsBusy = true;

                            var temp = _mainWnd.MailRecipientViewGrid.SelectedItem as BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATA;
                            //if (temp.READYVAL != 0)
                            //{
                            //    temp.CHK = "Y";
                            //}                                                       

                            IsBusy = false;

                            CommandResults["RowEditCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RowEditCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RowEditCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RowEditCommand") ?
                        CommandCanExecutes["RowEditCommand"] : (CommandCanExecutes["RowEditCommand"] = true);
                });
            }
        }

        public ICommand RowAddCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RowAddCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["RowAddCommand"] = false;
                            CommandCanExecutes["RowAddCommand"] = false;

                            IsBusy = true;

                            //var temp = _mainWnd.MailRecipientViewGrid.SelectedItem as BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.OUTDATA;
                            var temp = _mainWnd.MailRecipientViewGrid.SelectedItem;
                            //temp.CHK = "Y";

                            IsBusy = false;

                            CommandResults["RowAddCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RowAddCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RowAddCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RowAddCommand") ?
                        CommandCanExecutes["RowAddCommand"] : (CommandCanExecutes["RowAddCommand"] = true);
                });
            }
        }


        //public ICommand RowDeleteCommand
        //{
        //    get
        //    {
        //        return new AsyncCommandBase(async arg =>
        //        {
        //            using (await AwaitableLocks["RowDeleteCommand"].EnterAsync())
        //            {
        //                try
        //                {
        //                    CommandResults["RowDeleteCommand"] = false;
        //                    CommandCanExecutes["RowDeleteCommand"] = false;

        //                    IsBusy = true;
        //                    ///
        //                    ///
        //                    IsBusy = false;

        //                    CommandResults["RowDeleteCommand"] = true;
        //                    return;
        //                }
        //                catch (Exception ex)
        //                {
        //                    CommandResults["RowDeleteCommand"] = false;
        //                    OnException(ex.Message, ex);
        //                }
        //                finally
        //                {
        //                    CommandCanExecutes["RowDeleteCommand"] = true;

        //                    IsBusy = false;
        //                }
        //            }
        //        }, arg =>
        //        {
        //            return CommandCanExecutes.ContainsKey("RowDeleteCommand") ?
        //                CommandCanExecutes["RowDeleteCommand"] : (CommandCanExecutes["RowDeleteCommand"] = true);
        //        });
        //    }
        //}

    }
}

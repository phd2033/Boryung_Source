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

        public MailRecipientViewViewModel()
        {
            _BR_BRS_SEL_SEND_MAIL_TO_LIST = new BR_BRS_SEL_SEND_MAIL_TO_LIST();
            _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST = new BR_BRS_REG_UDT_SEND_MAIL_TO_LIST();
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

                            _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.INDATAs.Clear();

                            foreach (var item in _BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATAs)
                            {
                                if (item.CHK == "Y")
                                {
                                    _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.INDATAs.Add(new BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.INDATA
                                    {
                                        SEQ = Convert.ToInt32(item.RowIndex) + 1,
                                        CMCDTYPE = item.CMCDTYPE,
                                        CMCODE = item.CMCODE,
                                        CMCDNAME = item.CMCDNAME,
                                        USERID = item.USERID,
                                        USERNAME = item.USERNAME,
                                        USERMAIL = item.USERMAIL,
                                        UPDUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                        ISUSE = item.ISUSE
                                    });
                                }
                            }
                            await _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.Execute();

                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.INDATAs.Clear();
                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATAs.Clear();

                            _BR_BRS_SEL_SEND_MAIL_TO_LIST.INDATAs.Add(new BR_BRS_SEL_SEND_MAIL_TO_LIST.INDATA()
                            {
                                USERID = string.IsNullOrWhiteSpace(USERID) ? null : USERID
                            });

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
                            if (temp.USERID != null)
                            {
                                temp.CHK = "Y";
                            }

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

        //public ICommand RowAddCommand
        //{
        //    get
        //    {
        //        return new AsyncCommandBase(async arg =>
        //        {
        //            using (await AwaitableLocks["RowAddCommand"].EnterAsync())
        //            {
        //                try
        //                {
        //                    CommandResults["RowAddCommand"] = false;
        //                    CommandCanExecutes["RowAddCommand"] = false;

        //                    IsBusy = true;

        //                    var temp = _mainWnd.CleanningSettingGrid.SelectedItem as BR_BRS_SEL_UDT_OperationProcessSegmentReady.OUTDATA;
        //                    temp.CHK = "Y";

        //                    IsBusy = false;

        //                    CommandResults["RowAddCommand"] = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                    CommandResults["RowAddCommand"] = false
        //                    OnException(ex.Message, ex);
        //                }
        //                finally
        //                {
        //                    CommandCanExecutes["RowAddCommand"] = true;

        //                    IsBusy = false;
        //                }
        //            }
        //        }, arg =>
        //        {
        //            return CommandCanExecutes.ContainsKey("RowAddCommand") ?
        //                CommandCanExecutes["RowAddCommand"] : (CommandCanExecutes["RowAddCommand"] = true);
        //        });
        //    }
        //}

    }
}

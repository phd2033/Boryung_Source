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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Board
{
    public class AdminInformationViewModel : ViewModelBase
    {
        #region ##### property ##### 
        private AdminInformation _mainWnd;

        public AdminInformationViewModel()
        {
            _BR_BRS_SEL_SEND_MAIL_TO_LIST = new BR_BRS_SEL_SEND_MAIL_TO_LIST();
            _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST = new BR_BRS_REG_UDT_SEND_MAIL_TO_LIST();
            _BR_BRS_SEL_COMMONCODE_CMCODE = new BR_BRS_SEL_COMMONCODE_CMCODE();
            _BR_BRS_SEL_COMMONCODE_CMCDTYPE = new BR_BRS_SEL_COMMONCODE_CMCDTYPE();
            _BR_BRS_SEL_PERSON_DEC = new BR_BRS_SEL_PERSON_DEC();
            _BR_BRS_SEL_ISUSE_YN = new BR_BRS_SEL_ISUSE_YN();

    }

    private string _SEQ;
        public string SEQ
        {
            get { return _SEQ; }
            set
            {
                _SEQ = value;
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

        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
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

        private BR_BRS_SEL_COMMONCODE_CMCODE _BR_BRS_SEL_COMMONCODE_CMCODE;
        public BR_BRS_SEL_COMMONCODE_CMCODE BR_BRS_SEL_COMMONCODE_CMCODE
        {
            get { return _BR_BRS_SEL_COMMONCODE_CMCODE; }
            set
            {
                _BR_BRS_SEL_COMMONCODE_CMCODE = value;
                OnPropertyChanged("BR_BRS_SEL_COMMONCODE_CMCODE");
            }
        }

        private BR_BRS_SEL_ISUSE_YN _BR_BRS_SEL_ISUSE_YN;
        public BR_BRS_SEL_ISUSE_YN BR_BRS_SEL_ISUSE_YN
        {
            get { return _BR_BRS_SEL_ISUSE_YN; }
            set
            {
                _BR_BRS_SEL_ISUSE_YN = value;
                OnPropertyChanged("BR_BRS_SEL_ISUSE_YN");
            }
        }

        private BR_BRS_SEL_COMMONCODE_CMCDTYPE _BR_BRS_SEL_COMMONCODE_CMCDTYPE;
        public BR_BRS_SEL_COMMONCODE_CMCDTYPE BR_BRS_SEL_COMMONCODE_CMCDTYPE
        {
            get { return _BR_BRS_SEL_COMMONCODE_CMCDTYPE; }
            set
            {
                _BR_BRS_SEL_COMMONCODE_CMCDTYPE = value;
                OnPropertyChanged("BR_BRS_SEL_COMMONCODE_CMCDTYPE");
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

        private BR_BRS_SEL_PERSON_DEC _BR_BRS_SEL_PERSON_DEC;
        public BR_BRS_SEL_PERSON_DEC BR_BRS_SEL_PERSON_DEC
        {
            get { return _BR_BRS_SEL_PERSON_DEC; }
            set
            {
                _BR_BRS_SEL_PERSON_DEC = value;
                OnPropertyChanged("BR_BRS_SEL_PERSON_DEC");
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

                            _mainWnd = arg as AdminInformation;

                            _BR_BRS_SEL_COMMONCODE_CMCDTYPE.INDATAs.Clear();
                            _BR_BRS_SEL_COMMONCODE_CMCDTYPE.OUTDATAs.Clear();

                            _BR_BRS_SEL_ISUSE_YN.INDATAs.Clear();
                            _BR_BRS_SEL_ISUSE_YN.OUTDATAs.Clear();

                            await _BR_BRS_SEL_ISUSE_YN.Execute();

                            await _BR_BRS_SEL_COMMONCODE_CMCDTYPE.Execute();

                            CommandResults["LoadedCommand"] = true;

                            if (BtnSearchCommand != null && BtnSearchCommand.CanExecute(null))
                            {
                                BtnSearchCommand.Execute(null);
                            }
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

        public ICommand IsCheckedCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["IsCheckedCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["IsCheckedCommand"] = false;
                            CommandCanExecutes["IsCheckedCommand"] = false;

                            if (IsChecked)
                            {
                                IsChecked = false;
                            }
                            else if (!IsChecked)
                            {
                                IsChecked = true;
                            }

                            CommandResults["IsCheckedCommand"] = true;

                            if (BtnSearchCommand != null && BtnSearchCommand.CanExecute(null))
                            {
                                BtnSearchCommand.Execute(null);
                            }
                        }
                        catch (Exception ex)
                        {
                            CommandResults["IsCheckedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["IsCheckedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("IsCheckedCommand") ?
                        CommandCanExecutes["IsCheckedCommand"] : (CommandCanExecutes["IsCheckedCommand"] = true);
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
                                USERID = string.IsNullOrWhiteSpace(USERID) ? null : USERID,
                                IsChecked = IsChecked                                                             
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
                                if (item.CHK == "Y" && item != null)
                                {
                                    _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.INDATAs.Add(new BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.INDATA
                                    {
                                        //SEQ = Convert.ToInt32(item.RowIndex) + 1,
                                        //SEQ = item.SEQ,
                                        CMCDTYPE = item.CMCDTYPE,
                                        CMCODE = item.CMCODE,
                                        CMCDNAME = item.CMCDNAME,
                                        USERID = item.USERID,
                                        UPDUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                        ISUSE = item.ISUSE,
                                        SECTION = item.RowEditSec
                                    });
                                }
                            }
                            await _BR_BRS_REG_UDT_SEND_MAIL_TO_LIST.Execute();

                            CommandResults["BtnUpdateCommand"] = true;

                            if (BtnSearchCommand != null && BtnSearchCommand.CanExecute(null))
                            {
                                BtnSearchCommand.Execute(null);
                            }
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

        public ICommand RowGotFocusCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RowGotFocusCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["RowGotFocusCommand"] = false;
                            CommandCanExecutes["RowGotFocusCommand"] = false;

                            IsBusy = true;

                            var temp = _mainWnd.AdminInformationGrid.SelectedItem as BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATA;
                            if (temp != null)
                            {
                                temp.CHK = "Y";

                            }

                            IsBusy = false;

                            CommandResults["RowGotFocusCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["RowGotFocusCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["RowGotFocusCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RowGotFocusCommand") ?
                        CommandCanExecutes["RowGotFocusCommand"] : (CommandCanExecutes["RowGotFocusCommand"] = true);
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

                            _BR_BRS_SEL_COMMONCODE_CMCODE.INDATAs.Clear();
                            _BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs.Clear();

                            var temp = _mainWnd.AdminInformationGrid.SelectedItem as BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATA;
                            var filter = _mainWnd.AdminInformationGrid.CurrentColumn;

                            if (temp != null && filter != null && !string.IsNullOrWhiteSpace(filter.FilterMemberPath))
                            {
                                if(filter.FilterMemberPath == "CMCDTYPE_NAMEFIELD")
                                {
                                    _BR_BRS_SEL_COMMONCODE_CMCODE.INDATAs.Add(new BR_BRS_SEL_COMMONCODE_CMCODE.INDATA()
                                    {
                                        CMCDTYPE = temp.CMCDTYPE
                                    });
                                    await _BR_BRS_SEL_COMMONCODE_CMCODE.Execute();
                                }
                                else if (filter.FilterMemberPath == "CMCODE_NAMEFIELD")
                                {
                                    _BR_BRS_SEL_COMMONCODE_CMCODE.INDATAs.Add(new BR_BRS_SEL_COMMONCODE_CMCODE.INDATA()
                                    {
                                        CMCODE = temp.CMCODE
                                    });
                                    await _BR_BRS_SEL_COMMONCODE_CMCODE.Execute();   
                                }

                                if (_BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs != null && _BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs.Count > 0)
                                {
                                    foreach (var outdata in _BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs)
                                    {
                                        if (outdata?.CMCDNAME != null && outdata.CMCODE == temp.CMCODE)
                                        {
                                            temp.CMCDNAME = outdata.CMCDNAME;

                                            if (_mainWnd.AdminInformationGrid.Columns.Count > 5)
                                            {
                                                _mainWnd.AdminInformationGrid.Columns[5].IsReadOnly = true;
                                            }
                                        }
                                    }
                                }
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
        public ICommand CommittingRowEditCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["CommittingRowEditCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["CommittingRowEditCommand"] = false;
                            CommandCanExecutes["CommittingRowEditCommand"] = false;

                            IsBusy = true;

                            _BR_BRS_SEL_COMMONCODE_CMCODE.INDATAs.Clear();
                            _BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs.Clear();

                            _BR_BRS_SEL_PERSON_DEC.INDATAs.Clear();
                            _BR_BRS_SEL_PERSON_DEC.OUTDATAs.Clear();


                            var temp = _mainWnd.AdminInformationGrid.SelectedItem as BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATA;
                            var filter = _mainWnd.AdminInformationGrid.CurrentColumn;

                            if (temp != null)
                            {
                                if (filter.FilterMemberPath == "USERID")
                                {
                                    _BR_BRS_SEL_PERSON_DEC.INDATAs.Add(new BR_BRS_SEL_PERSON_DEC.INDATA()
                                    {
                                        USERID = temp.USERID
                                    });
                                    await _BR_BRS_SEL_PERSON_DEC.Execute();
                                }

                                foreach (var outdata in BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs)
                                {
                                    if (outdata.CMCDNAME != null)
                                    {
                                        if (temp.CMCODE == outdata.CMCODE)
                                        {
                                            temp.CMCDNAME = outdata.CMCDNAME;
                                        }
                                    }
                                }

                                foreach (var outdata in BR_BRS_SEL_PERSON_DEC.OUTDATAs)
                                {
                                    if (outdata.USERNAME != null && outdata.USERMAIL != null)
                                    {
                                        temp.USERNAME = outdata.USERNAME;
                                        temp.USERMAIL = outdata.USERMAIL;

                                        _mainWnd.AdminInformationGrid.Columns[7].IsReadOnly = true;
                                        _mainWnd.AdminInformationGrid.Columns[8].IsReadOnly = true;
                                    }
                                }
                            }
                        
                            IsBusy = false;

                        CommandResults["CommittingRowEditCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["CommittingRowEditCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["CommittingRowEditCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("CommittingRowEditCommand") ?
                        CommandCanExecutes["CommittingRowEditCommand"] : (CommandCanExecutes["CommittingRowEditCommand"] = true);
                });
            }
        }

        public ICommand CheckFieldDataChangedCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["CommittingRowEditCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["CommittingRowEditCommand"] = false;
                            CommandCanExecutes["CommittingRowEditCommand"] = false;

                            IsBusy = true;

                            _BR_BRS_SEL_COMMONCODE_CMCODE.INDATAs.Clear();
                            _BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs.Clear();

                            _BR_BRS_SEL_PERSON_DEC.INDATAs.Clear();
                            _BR_BRS_SEL_PERSON_DEC.OUTDATAs.Clear();


                            var temp = _mainWnd.AdminInformationGrid.SelectedItem as BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATA;
                            var filter = _mainWnd.AdminInformationGrid.CurrentColumn;

                            if (temp != null)
                            {
                                if (filter.FilterMemberPath == "USERID")
                                {
                                    _BR_BRS_SEL_PERSON_DEC.INDATAs.Add(new BR_BRS_SEL_PERSON_DEC.INDATA()
                                    {
                                        USERID = temp.USERID
                                    });
                                    await _BR_BRS_SEL_PERSON_DEC.Execute();
                                }

                                foreach (var outdata in BR_BRS_SEL_COMMONCODE_CMCODE.OUTDATAs)
                                {
                                    if (outdata.CMCDNAME != null)
                                    {
                                        if (temp.CMCODE == outdata.CMCODE)
                                        {
                                            temp.CMCDNAME = outdata.CMCDNAME;
                                        }
                                    }
                                }

                                foreach (var outdata in BR_BRS_SEL_PERSON_DEC.OUTDATAs)
                                {
                                    if (outdata.USERNAME != null && outdata.USERMAIL != null)
                                    {
                                        temp.USERNAME = outdata.USERNAME;
                                        temp.USERMAIL = outdata.USERMAIL;

                                        _mainWnd.AdminInformationGrid.Columns[7].IsReadOnly = true;
                                        _mainWnd.AdminInformationGrid.Columns[8].IsReadOnly = true;
                                    }
                                }
                            }

                            IsBusy = false;

                            CommandResults["CommittingRowEditCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["CommittingRowEditCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["CommittingRowEditCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("CommittingRowEditCommand") ?
                        CommandCanExecutes["CommittingRowEditCommand"] : (CommandCanExecutes["CommittingRowEditCommand"] = true);
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

                            var temp = _mainWnd.AdminInformationGrid.SelectedItem as BR_BRS_SEL_SEND_MAIL_TO_LIST.OUTDATA;
                            if (temp != null)
                            {
                                temp.CHK = "Y";
                                temp.CMCDNAME = "(자동 입력)";
                                temp.USERNAME = "(자동 입력)";
                                temp.USERMAIL = "(자동 입력)";

                                _mainWnd.AdminInformationGrid.Columns[5].IsReadOnly = true;
                                _mainWnd.AdminInformationGrid.Columns[7].IsReadOnly = true;
                                _mainWnd.AdminInformationGrid.Columns[8].IsReadOnly = true;

                            }


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

    }
}

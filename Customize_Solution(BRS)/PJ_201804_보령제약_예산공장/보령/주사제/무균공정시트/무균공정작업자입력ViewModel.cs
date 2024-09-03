using C1.Silverlight.Data;
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace 보령
{
    public class 무균공정작업자입력ViewModel : ViewModelBase
    {
        #region [Property]
        public 무균공정작업자입력ViewModel()
        {
            _UserList = new ObservableCollection<ChargedContainer>();
        }

        private 무균공정작업자입력 _mainWnd;
        
        private string _UserId;
        public string UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                OnPropertyChanged("UserId");
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }
        #endregion

        #region [BizRule]

        BR_PHR_SEL_PERSON _BR_PHR_SEL_PERSON;
        public BR_PHR_SEL_PERSON BR_PHR_SEL_PERSON
        {
            get { return _BR_PHR_SEL_PERSON; }
            set
            {
                _BR_PHR_SEL_PERSON = value;
                OnPropertyChanged("BR_PHR_SEL_PERSON");
            }
        }

        private ObservableCollection<ChargedContainer> _UserList;
        public ObservableCollection<ChargedContainer> UserList
        {
            get { return _UserList; }
            set
            {
                _UserList = value;
                OnPropertyChanged("UserList");
            }
        }
        #endregion

        #region [Command]
        public ICommand LoadedCommand
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

                            if (arg != null && arg is 무균공정작업자입력)
                            {
                                _mainWnd = arg as 무균공정작업자입력;

                                OnPropertyChanged("IBCList");
                            }

                            _mainWnd.txtUserId.Focus();

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
                    using (await AwaitableLocks["ConfirmCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ConfirmCommandAsync"] = false;
                            CommandCanExecutes["ConfirmCommandAsync"] = false;

                            ///

                            var authHelper = new iPharmAuthCommandHelper();
                            if (_mainWnd.CurrentInstruction.Raw.INSERTEDYN == "Y" && _mainWnd.CurrentInstruction.PhaseState.Equals("COMP"))
                            {
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

                            //XML 생성. 비즈룰 INDATA생성
                            DataSet ds = new DataSet();
                            DataTable dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            dt.Columns.Add(new DataColumn("작업자"));

                            var row = dt.NewRow();
                            
                            row["작업자"] = "";
                            dt.Rows.Add(row);

                            
                            IsBusy = false;
                            CommandResults["ConfirmCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ConfirmCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ConfirmCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ConfirmCommandAsync") ?
                        CommandCanExecutes["ConfirmCommandAsync"] : (CommandCanExecutes["ConfirmCommandAsync"] = true);
                });
            }
        }
        #endregion

        #region [etc]
        public ICommand CheckUserIdCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    try
                    {
                        CommandCanExecutes["CheckIBCInfoCommandAsync"] = false;
                        CommandResults["CheckIBCInfoCommandAsync"] = false;
                        
                        UserId = arg as string;

                        _BR_PHR_SEL_PERSON.INDATAs.Clear();
                        _BR_PHR_SEL_PERSON.OUTDATAs.Clear();

                        _BR_PHR_SEL_PERSON.INDATAs.Add(new BR_PHR_SEL_PERSON.INDATA
                        {
                            USERIUSE = "Y",
                            LANGID = "",
                            USERID = UserId
                        });

                        if (await _BR_PHR_SEL_PERSON.Execute())
                        {
                            if (_BR_PHR_SEL_PERSON.OUTDATAs.Count > 0)
                            {
                                UserId = _BR_PHR_SEL_PERSON.OUTDATAs[0].USERID;
                                UserName = _BR_PHR_SEL_PERSON.OUTDATAs[0].USERNAME;
                            }
                            else
                                OnMessage("BIN 정보가 없습니다.");
                        }

                        CommandResults["CheckIBCInfoCommandAsync"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["CheckIBCInfoCommandAsync"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        CommandCanExecutes["CheckIBCInfoCommandAsync"] = true;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("CheckIBCInfoCommandAsync") ?
                        CommandCanExecutes["CheckIBCInfoCommandAsync"] : (CommandCanExecutes["CheckIBCInfoCommandAsync"] = true);
                });
            }
        }

        public class ChargedContainer : WIPContainer
        {
            private string _USERID;
            public string USERID
            {
                get { return this._USERID; }
                set
                {
                    this._USERID = value;
                    this.OnPropertyChanged("USERID");
                }
            }
            private string _USERNAME;
            public string USERNAME
            {
                get { return this._USERNAME; }
                set
                {
                    this._USERNAME = value;
                    this.OnPropertyChanged("USERNAME");
                }
            }
        }
        #endregion
    }
}

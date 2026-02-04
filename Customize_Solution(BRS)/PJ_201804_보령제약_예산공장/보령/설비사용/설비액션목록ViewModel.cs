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
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
using System.Collections.ObjectModel;
using System.Linq;
using C1.Silverlight.Data;
using System.Text;
using System.Threading.Tasks;

namespace 보령
{
    public class 설비액션목록ViewModel : ViewModelBase
    {
        #region [Property]
        public 설비액션목록ViewModel()
        {
            _ListRequestOut = new ObservableCollection<EQPTInfo>();
            _BR_PHR_SEL_Equipment_GetInfo = new BR_PHR_SEL_Equipment_GetInfo();
        }

        private 설비액션목록 _mainWnd;
        private string scaleid;

        private ObservableCollection<EQPTInfo> _ListRequestOut;
        public ObservableCollection<EQPTInfo> ListRequestOut
        {
            get { return _ListRequestOut; }
            set
            {
                _ListRequestOut = value;
                OnPropertyChanged("ListRequestOut");
            }
        }

        #endregion
        #region [Bizrule]
        
        /// <summary>
        /// 설비 정보 조회
        /// </summary>
        private BR_PHR_SEL_Equipment_GetInfo _BR_PHR_SEL_Equipment_GetInfo;
        public BR_PHR_SEL_Equipment_GetInfo BR_PHR_SEL_Equipment_GetInfo
        {
            get { return _BR_PHR_SEL_Equipment_GetInfo; }
            set
            {
                _BR_PHR_SEL_Equipment_GetInfo = value;
                OnPropertyChanged("BR_PHR_SEL_Equipment_GetInfo");
            }
        }
        // 용기(반제품) 정보 조회
        private BR_BRS_GET_VESSEL_INFO_DETAIL _BR_BRS_GET_VESSEL_INFO_DETAIL;
        public BR_BRS_GET_VESSEL_INFO_DETAIL BR_BRS_GET_VESSEL_INFO_DETAIL
        {
            get { return _BR_BRS_GET_VESSEL_INFO_DETAIL; }
            set
            {
                _BR_BRS_GET_VESSEL_INFO_DETAIL = value;
                OnPropertyChanged("BR_BRS_GET_VESSEL_INFO_DETAIL");
            }
        }
        // 검량 확인
        private BR_PHR_UPD_MaterialSubLot_CheckWeight _BR_PHR_UPD_MaterialSubLot_CheckWeight;
        public BR_PHR_UPD_MaterialSubLot_CheckWeight BR_PHR_UPD_MaterialSubLot_CheckWeight
        {
            get { return _BR_PHR_UPD_MaterialSubLot_CheckWeight; }
            set
            {
                _BR_PHR_UPD_MaterialSubLot_CheckWeight = value;
                OnPropertyChanged("BR_PHR_UPD_MaterialSubLot_CheckWeight");
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
                            CommandCanExecutes["LoadedCommandAsync"] = false;
                            CommandResults["LoadedCommandAsync"] = false;

                            ///
                            IsBusy = true;

                            if (arg != null && arg is 설비액션목록)
                            {
                                _mainWnd = arg as 설비액션목록;
                                
                                _mainWnd.txtSelEqptId.Focus();
                            }

                            IsBusy = false;
                            ///

                            CommandResults["LoadedCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            CommandResults["LoadedCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadedCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadedCommandAsync") ?
                        CommandCanExecutes["LoadedCommandAsync"] : (CommandCanExecutes["LoadedCommandAsync"] = true);
                });
            }
        }
        
        public ICommand RemoveEqptCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["RemoveEqptCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandCanExecutes["RemoveEqptCommandAsync"] = false;
                            CommandResults["RemoveEqptCommandAsync"] = false;
                            
                            IsBusy = true;

                            EQPTInfo removeEqpt = new EQPTInfo();
                            removeEqpt = (EQPTInfo)_mainWnd.GridRequestOutList.SelectedItem;

                            _ListRequestOut.Remove(removeEqpt);

                            OnPropertyChanged("ListRequestOut");

                            IsBusy = false;
                            ///

                            CommandResults["RemoveEqptCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            _mainWnd.GridRequestOutList.SelectedItem = null;
                            CommandResults["RemoveEqptCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            _mainWnd.txtSelEqptId.Text = "";
                            _mainWnd.txtSelEqptId.Focus();
                            CommandCanExecutes["RemoveEqptCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("RemoveEqptCommandAsync") ?
                        CommandCanExecutes["RemoveEqptCommandAsync"] : (CommandCanExecutes["RemoveEqptCommandAsync"] = true);
                });
            }
        }

        public ICommand ConfirmCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    var eqptList = arg as 설비액션목록ViewModel;
                    using (await AwaitableLocks["ConfirmCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandCanExecutes["ConfirmCommandAsync"] = false;
                            CommandResults["ConfirmCommandAsync"] = false;

                            ///
                            IsBusy = true;


                            ObservableCollection<EQPTInfo> compRequestOut = new ObservableCollection<EQPTInfo>();

                            foreach (var item in _ListRequestOut)
                            {
                                compRequestOut.Add(item);
                            }


                            if (compRequestOut.Count > 0)
                            {
                                var popupViewModel = new 설비액션기록ViewModel(eqptList._ListRequestOut);

                                설비액션기록 popup = new 설비액션기록();

                                popup.DataContext = popupViewModel;

                                popup.Show();
                            }

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

        public async Task<EQPTInfo> AddEqptGridList(string target)
        {
            try
            {
                string eqptid = string.Empty;

                if (string.IsNullOrEmpty(_mainWnd.txtSelEqptId.ToString()))
                {
                    OnMessage("설비코드를 입력하세요.");
                    return null;
                }

                eqptid = _mainWnd.txtSelEqptId.ToString().ToUpper();
                _BR_PHR_SEL_Equipment_GetInfo.INDATAs.Clear();
                _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs.Clear();
                _BR_PHR_SEL_Equipment_GetInfo.INDATAs.Add(new BR_PHR_SEL_Equipment_GetInfo.INDATA
                {
                    LANGID = AuthRepositoryViewModel.Instance.LangID,
                    EQPTID = target.ToString()
                });

                await _BR_PHR_SEL_Equipment_GetInfo.Execute();

                if (_BR_PHR_SEL_Equipment_GetInfo.OUTDATAs.Count == 0)
                {
                    OnMessage("설비코드가 등록되어있지 않습니다.");
                    return null;
                }

                //다른 설비코드가 있을 때
                if (_ListRequestOut.Count > 0)
                {
                    foreach (var item in _ListRequestOut)
                    {
                        if (target.ToString() == item.EQPTID)
                        {
                            OnMessage("이미 등록된 설비입니다.");
                            return null;
                        }

                    }
                }
                EQPTInfo addEqpt = new EQPTInfo();

                foreach (var item in _BR_PHR_SEL_Equipment_GetInfo.OUTDATAs)
                {
                    addEqpt.EQPTID = item.EQPTID;
                    addEqpt.EQCLID = item.EQCLID;
                    addEqpt.EQPTNAME = item.EQPTNAME;
                    addEqpt.EQCLNAME = item.EQCLNAME;
                };

                _ListRequestOut.Add(addEqpt);

                OnPropertyChanged("ListRequestOut");

                return addEqpt;
            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
                return null;
            }
        }
        
        #endregion

        #region [User Define]
        public class EQPTInfo : ViewModelBase
        {
            private bool _CHK;
            public bool CHK
            {
                get { return _CHK; }
                set
                {
                    _CHK = value;
                    OnPropertyChanged("CHK");
                }
            }

            private string _EQPTID;
            public string EQPTID
            {
                get { return _EQPTID; }
                set
                {
                    _EQPTID = value;
                    OnPropertyChanged("EQPTID");
                }
            }

            private string _EQPTNAME;
            public string EQPTNAME
            {
                get { return _EQPTNAME; }
                set
                {
                    _EQPTNAME = value;
                    OnPropertyChanged("EQPTNAME");
                }
            }

            private string _EQCLID;
            public string EQCLID
            {
                get { return _EQCLID; }
                set
                {
                    _EQCLID = value;
                    OnPropertyChanged("EQCLID");
                }
            }

            private string _EQCLNAME;
            public string EQCLNAME
            {
                get { return _EQCLNAME; }
                set
                {
                    _EQCLNAME = value;
                    OnPropertyChanged("EQCLNAME");
                }
            }

            private BR_BRS_GET_VESSEL_INFO_DETAIL.OUTDATA_WIPCollection _WIPs;
            public BR_BRS_GET_VESSEL_INFO_DETAIL.OUTDATA_WIPCollection WIPs
            {
                get { return _WIPs; }
                set
                {
                    _WIPs = value;
                    OnPropertyChanged("WIPs");
                }
            }

            private BR_BRS_GET_VESSEL_INFO_DETAIL.OUTDATA_DETAILCollection _DETAILs;
            public BR_BRS_GET_VESSEL_INFO_DETAIL.OUTDATA_DETAILCollection DETAILs
            {
                get { return _DETAILs; }
                set
                {
                    _DETAILs = value;
                    OnPropertyChanged("DETAILs");
                }
            }
        }

        public EQPTInfo ChangeTargetEqpt(string target)
        {
            foreach (var item in ListRequestOut)
            {
                if (item.EQPTID.ToUpper().Equals(target))
                    return item;
            }

            return null;
        }
        #endregion
    }
}

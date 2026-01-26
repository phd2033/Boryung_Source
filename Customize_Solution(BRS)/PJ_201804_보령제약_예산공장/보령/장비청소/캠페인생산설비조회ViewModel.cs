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
using System.Collections.Generic;
using ShopFloorUI;
using C1.Silverlight.Data;
using LGCNS.iPharmMES.Recipe.Common;
using System.Collections.ObjectModel;

namespace 보령
{
    public class 캠페인생산설비조회ViewModel : ViewModelBase
    {
        #region [Property]
        public 캠페인생산설비조회ViewModel()
        {
            _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID = new BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID();
            SelectedYnValue = null;
            _CampaignList = new ObservableCollection<CampaignInfo>();
        }

        캠페인생산설비조회 _mainWnd;

        private ObservableCollection<CampaignInfo> _CampaignList;
        public ObservableCollection<CampaignInfo> CampaignList
        {
            get { return _CampaignList; }
            set
            {
                _CampaignList = value;
                OnPropertyChanged("CampaignList");
            }
        }

        public ObservableCollection<string> YnOptions { get; } = new ObservableCollection<string> { "Y", "N" };

        private string _SelectedYnValue;
        public string SelectedYnValue
        {
            get { return _SelectedYnValue; }
            set
            {
                if (_SelectedYnValue != value)
                {
                    _SelectedYnValue = value;
                    OnPropertyChanged("SelectedYnValue");

                    UpdateCampaignDisplay(); 
                }
            }

        }
        #endregion

        #region [Bizrule]
        private BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID;
        public BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID
        {
            get { return _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID; }
            set
            {
                _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID = value;
                OnPropertyChanged("BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID ");
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
                    using (await AwaitableLocks["LoadedCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandCanExecutes["LoadedCommand"] = false;
                            CommandResults["LoadedCommand"] = false;
                            IsBusy = true;


                            if (arg != null)
                            {
                                _mainWnd = arg as 캠페인생산설비조회;
                                var paramInsts = InstructionModel.GetParameterSender(_mainWnd.CurrentInstruction, _mainWnd.Instructions);

                                _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.INDATAs.Clear();
                                _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.OUTDATAs.Clear();

                                if (paramInsts.Count > 0 && !string.IsNullOrWhiteSpace(paramInsts[0].Raw.ACTVAL))
                                {
                                    var instruction = paramInsts[0];

                                    if (instruction.Raw.NOTE != null)
                                    {
                                        DataSet ds = new DataSet();
                                        DataTable dt = new DataTable();
                                        var bytearray = instruction.Raw.NOTE;
                                        string xml = System.Text.Encoding.UTF8.GetString(bytearray, 0, bytearray.Length);

                                        ds.ReadXmlFromString(xml);
                                        dt = ds.Tables["DATA"];

                                        foreach (var row in dt.Rows)
                                        {
                                            if (row["장비번호"] != null & row["룸장비여부"] != null)
                                            {
                                                _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.INDATAs.Add(new BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.INDATA
                                                {
                                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                                    EQPTID = row["장비번호"].ToString(),
                                                    ROOMEQUIPYN = row["룸장비여부"].ToString()
                                                });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception(string.Format("생산 설비 청소 점검 정보가 없습니다"));
                                }
                                if (await _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.Execute() == false) return;
                            }
                            CommandResults["LoadedCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandCanExecutes["LoadedCommand"] = false;
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

        public void UpdateCampaignDisplay()
        {
            _CampaignList.Clear();
            // CAMPAIGNYN 값을 기반으로 표시할 정보 결정
            if (SelectedYnValue?.ToUpper() == "Y")
            {
                foreach (var info in _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.OUTDATAs)
                {
                    _CampaignList.Add(new CampaignInfo
                    {
                        CAMPAIGNYN = SelectedYnValue?.ToUpper(),
                        EQPTID = info.EQPTID.ToString(),
                        CAMPAIGNCNT = info.CAMPAIGNCNT.ToString(),
                        CAMPAIGNPERIOD = info.CAMPAIGNPERIOD.ToString(),
                        ROOMEQUIPYN = info.ROOMEQUIPYN.ToString()
                    });
                }
            }
            else // N이거나 다른 값일 때
            {
                foreach (var info in _BR_BRS_SEL_ProductionOrder_CAMPAIGN_BY_EQPTID.OUTDATAs)
                {
                    _CampaignList.Add(new CampaignInfo
                    {
                        CAMPAIGNYN = SelectedYnValue?.ToUpper(),
                        EQPTID = info.EQPTID.ToString(),
                        CAMPAIGNCNT = "N/A",
                        CAMPAIGNPERIOD = "N/A",
                        ROOMEQUIPYN = info.ROOMEQUIPYN.ToString()
                    });
                }
            }
        }

        public ICommand ConfirmCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ConfirmCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["ConfirmCommand"] = false;
                            CommandCanExecutes["ConfirmCommand"] = false;
                            IsBusy = true;

                            iPharmAuthCommandHelper authHelper = new iPharmAuthCommandHelper();

                            if (_mainWnd.CurrentInstruction.Raw.INSERTEDYN.Equals("Y") && _mainWnd.Phase.CurrentPhase.STATE.Equals("COMP")) // 값 수정
                            {
                                // 전자서명 요청
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

                            var ds = new DataSet();
                            var dt = new DataTable("DATA");
                            ds.Tables.Add(dt);

                            dt.Columns.Add(new DataColumn("장비번호"));
                            dt.Columns.Add(new DataColumn("캠페인생산여부"));
                            dt.Columns.Add(new DataColumn("캠페인횟수"));
                            dt.Columns.Add(new DataColumn("캠페인생산일수"));
                            dt.Columns.Add(new DataColumn("룸장비여부"));

                            foreach (var item in CampaignList)
                            {
                                var row = dt.NewRow();
                                row["장비번호"] = item.EQPTID != null ? item.EQPTID : "";
                                row["캠페인생산여부"] = item.CAMPAIGNYN != null ? item.CAMPAIGNYN : "";
                                row["캠페인횟수"] = item.CAMPAIGNCNT != null ? item.CAMPAIGNCNT : "";
                                row["캠페인생산일수"] = item.CAMPAIGNPERIOD != null ? item.CAMPAIGNPERIOD : "";
                                row["룸장비여부"] = item.ROOMEQUIPYN != null ? item.ROOMEQUIPYN : "";
                                dt.Rows.Add(row);
                            }
                            
                            var xml = BizActorRuleBase.CreateXMLStream(ds);
                            var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);
                            
                            _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                            _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                            if (result != enumInstructionRegistErrorType.Ok)
                            {
                                throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                            }
                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);
                      
                            IsBusy = false;

                            CommandResults["ConfirmCommand"] = false;
                        }
                        catch (Exception ex)
                        {
                            CommandCanExecutes["ConfirmCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ConfirmCommand"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ConfirmCommand") ?
                        CommandCanExecutes["ConfirmCommand"] : (CommandCanExecutes["ConfirmCommand"] = true);
                });
            }
        }

        #endregion

        #region [User Define]
        public class CampaignInfo : ViewModelBase
        {
            private string _CAMPAIGNYN;
            public string CAMPAIGNYN
            {
                get { return _CAMPAIGNYN; }
                set
                {
                    _CAMPAIGNYN = value;
                    OnPropertyChanged("CAMPAIGNYN");
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
            private string _CAMPAIGNCNT;
            public string CAMPAIGNCNT
            {
                get { return _CAMPAIGNCNT; }
                set
                {
                    _CAMPAIGNCNT = value;
                    OnPropertyChanged("CAMPAIGNCNT");
                }
            }
            private string _CAMPAIGNPERIOD;
            public string CAMPAIGNPERIOD
            {
                get { return _CAMPAIGNPERIOD; }
                set
                {
                    _CAMPAIGNPERIOD = value;
                    OnPropertyChanged("CAMPAIGNPERIOD");
                }
            }
            private string _ROOMEQUIPYN;
            public string ROOMEQUIPYN
            {
                get { return _ROOMEQUIPYN; }
                set
                {
                    _ROOMEQUIPYN = value;
                    OnPropertyChanged("ROOMEQUIPYN");
                }
            }

        }
        #endregion
    }
}
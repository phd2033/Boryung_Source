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
using System.Windows.Threading;
using System.Linq;
using 보령.UserControls;
using C1.Silverlight.Data;
using ShopFloorUI;
using System.Threading.Tasks;
using System.Text;

namespace 보령
{
    public class SVP소분원료현장칭량_기준량ViewModel : ViewModelBase
    {
        #region [Property]
        public SVP소분원료현장칭량_기준량ViewModel()
        {
            _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID = new BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID();
            _BR_PHR_SEL_System_Printer = new BR_PHR_SEL_System_Printer();
            _BR_PHR_REG_ScaleSetTare = new BR_PHR_REG_ScaleSetTare();

            _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD = new BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD();
            _BR_BRS_SEL_ReDispensing_Charging = new BR_BRS_SEL_ReDispensing_Charging();
            _BR_BRS_REG_MaterialSubLot_Dispense_Scrap = new BR_BRS_REG_MaterialSubLot_Dispense_Scrap();

            _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW = new BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW();

            _BR_PHR_SEL_PRINT_LabelImage = new BR_PHR_SEL_PRINT_LabelImage();

            _BR_BRS_SEND_WMS_WEIGHINGRESULT = new BR_BRS_SEND_WMS_WEIGHINGRESULT();

            int interval = 2000;
            string interval_str = ShopFloorUI.App.Current.Resources["GetWeightInterval"].ToString();
            if (int.TryParse(interval_str, out interval) == false)
            {
                interval = 2000;
            }
            _DispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            _DispatcherTimer.Tick += _DispatcherTimer_Tick;
        }

        SVP소분원료현장칭량_기준량 _mainWnd;
        DispatcherTimer _DispatcherTimer = new DispatcherTimer(); // 저울값 타이머
        private ScaleWebAPIHelper _restScaleService = new ScaleWebAPIHelper();

        private BR_PHR_SEL_System_Printer.OUTDATA _selectedPrint;
        public string curPrintName
        {
            get
            {
                if (_selectedPrint != null)
                    return _selectedPrint.PRINTERNAME;
                else
                    return "N/A";
            }
        }

        #region [자재정보]
        string _MTRLID;
        public string MTRLID
        {
            get { return _MTRLID; }
            set
            {
                _MTRLID = value;
                OnPropertyChanged("MTRLID");
            }
        }

        string _MTRLNAME;
        public string MTRLNAME
        {
            get { return _MTRLNAME; }
            set
            {
                _MTRLNAME = value;
                OnPropertyChanged("MTRLNAME");
            }
        }

        string _RESERVEQTY;
        public string RESERVEQTY
        {
            get { return _RESERVEQTY; }
            set
            {
                _RESERVEQTY = value;
                OnPropertyChanged("RESERVEQTY");
            }
        }

        private Weight _UpperWeight = new Weight();
        public string UpperWeight
        {
            get { return _UpperWeight.WeightUOMString; }
        }
        private Weight _LowerWeight = new Weight();
        public string LowerWeight
        {
            get { return _LowerWeight.WeightUOMString; }
        }

        private BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATA _curSelectedSourceContainer;
        /// <summary>
        /// 현재 칭량에 사용중인 소분된 원료
        /// </summary>
        public BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATA curSelectedSourceContainer
        {
            get { return _curSelectedSourceContainer; }
            set
            {
                _curSelectedSourceContainer = value;
                OnPropertyChanged("curSelectedSourceContainer");
            }
        }
     
        private bool _MtrlbtnEnable;
        /// <summary>
        /// 원료바코드 스캔 버튼 Enable
        /// </summary>
        public bool MtrlbtnEnable
        {
            get { return _MtrlbtnEnable; }
            set
            {
                _MtrlbtnEnable = value;
                OnPropertyChanged("MtrlbtnEnable");
            }
        }

        private BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATA _SelectedAllocationInfo;
        public BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATA SelectedAllocationInfo
        {
            set
            {
                _SelectedAllocationInfo = value;
                if (value != null)
                {
                    MTRLID = value.MTRLID;
                    MTRLNAME = value.MTRLNAME;
                    RESERVEQTY = value.INITRESERVEQTY.GetValueOrDefault().ToString("F" + value.BOMPRECISION) + value.NOTATION;

                    _UpperWeight.SetWeight(value.UPPER.GetValueOrDefault().ToString("F" + value.BOMPRECISION), value.NOTATION.ToString());
                    _LowerWeight.SetWeight(value.LOWER.GetValueOrDefault().ToString("F" + value.BOMPRECISION), value.NOTATION.ToString());
                    OnPropertyChanged("UpperWeight");
                    OnPropertyChanged("LowerWeight");
                    OnPropertyChanged("DspWeight");
                    

                    MtrlbtnEnable = true;
                    ScalebtnEnable = true;
                }
            }
        }
        //BR_BRS_SEL_Charging_Solvent_to_Dispense.OUTDATACollection _filteredComponents;
        ///// <summary>
        ///// 사용 가능한 원료백 정보
        ///// </summary>
        //public BR_BRS_SEL_Charging_Solvent_to_Dispense.OUTDATACollection FilteredComponents
        //{
        //    get { return _filteredComponents; }
        //    set
        //    {
        //        _filteredComponents = value;
        //        OnPropertyChanged("FilteredComponents");
        //    }
        //}
        #endregion

        #region [저울정보]
        private BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATA _ScaleInfo;
        private int _scalePrecision = 3;
        public int scalePrecision
        {
            set
            {
                _scalePrecision = value;
                _LowerWeight.Precision = _scalePrecision;
                _UpperWeight.Precision = _scalePrecision;
                OnPropertyChanged("ScaleWeight");
                OnPropertyChanged("DspWeight");
                OnPropertyChanged("NowWeight");
                OnPropertyChanged("UpperWeight");
                OnPropertyChanged("LowerWeight");
            }
        }

        // 저울에러
        /// <summary>
        /// True : TARE 실시, False : TARE 미실시
        /// </summary>
        private bool _SetTare;
        private bool _ScaleException;
        private string _ScaleExceptionMsg = "저울 연결 실패";
        string _scaleId;
        public string ScaleId
        {
            get { return _scaleId; }
            set
            {
                _scaleId = value;
                NotifyPropertyChanged();
            }
        }

        private Weight _ScaleWeight = new Weight();
        public string ScaleWeight
        {
            get
            {
                if (_ScaleException)
                    return _ScaleExceptionMsg;
                else
                    return _ScaleWeight.WeightUOMString;
            }
        }
        private Weight _TareWeight = new Weight();
        public string TareWeight
        {
            get
            {
                if (_ScaleException)
                    return _ScaleExceptionMsg;
                else
                    return _TareWeight.WeightUOMString;
            }
        }
        public string DspWeight
        {
            get
            {
                if (_ScaleException)
                    return _ScaleExceptionMsg;
                else
                {
                    if (_SetTare)
                    {
                        return _ScaleWeight.Add(_DisepenQty).WeightUOMString;
                    }
                    else return _ScaleWeight.WeightUOMString;
                }
            }
        }
        public string NowWeight
        {
            get
            {
                if (_ScaleException)
                    return _ScaleExceptionMsg;
                else
                {
                    return _ScaleWeight.Subtract(_DisepenQty).WeightUOMString;
                }
            }
        }

        private SolidColorBrush _ScaleBackground;
        public SolidColorBrush ScaleBackground
        {
            get { return _ScaleBackground; }
            set
            {
                _ScaleBackground = value;
                OnPropertyChanged("ScaleBackground");
            }
        }

        private bool _ScalebtnEnable;
        /// <summary>
        /// 저울스캔 Enable
        /// </summary>
        public bool ScalebtnEnable
        {
            get { return _ScalebtnEnable; }
            set
            {
                _ScalebtnEnable = value;
                OnPropertyChanged("ScalebtnEnable");
            }
        }

        private bool _TarebtnEnable;
        /// <summary>
        /// Tare버튼 Enable
        /// </summary>
        public bool TarebtnEnable
        {
            get { return _TarebtnEnable; }
            set
            {
                _TarebtnEnable = value;
                OnPropertyChanged("TarebtnEnable");
            }
        }
        #endregion

        #region [칭량정보]
        /// <summary>
        /// 칭량시작시간
        /// </summary>
        private DateTime _DspStartDttm;
        private Weight _DisepenQty = new Weight();

        private bool _DispensebtnEnable;
        /// <summary>
        /// 소분버튼
        /// </summary>
        public bool DispensebtnEnable
        {
            get { return _DispensebtnEnable; }
            set
            {
                _DispensebtnEnable = value;
                OnPropertyChanged("DispensebtnEnable");
            }
        }

        private bool _ChargebtnEnable;
        /// <summary>
        /// 투입버튼
        /// </summary>
        public bool ChargebtnEnable
        {
            get { return _ChargebtnEnable; }
            set
            {
                _ChargebtnEnable = value;
                OnPropertyChanged("ChargebtnEnable");
            }
        }
        private bool _ScrapbtnEnable;
        /// <summary>
        /// 잔량폐기 버튼
        /// </summary>
        public bool ScrapbtnEnable
        {
            get { return _ScrapbtnEnable; }
            set
            {
                _ScrapbtnEnable = value;
                OnPropertyChanged("ScrapbtnEnable");
            }
        }
        #endregion

        #endregion

        #region [BizRule]
        private BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD;
        public BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD
        {
            get { return _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD; }
            set
            {
                _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD = value;
                OnPropertyChanged("BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD");
            }
        }

        private BR_BRS_SEL_ReDispensing_Charging _BR_BRS_SEL_ReDispensing_Charging;
        /// <summary>
        /// 현장칭량 소분 정보 조회
        /// </summary>
        public BR_BRS_SEL_ReDispensing_Charging BR_BRS_SEL_ReDispensing_Charging
        {
            get { return _BR_BRS_SEL_ReDispensing_Charging; }
            set
            {
                _BR_BRS_SEL_ReDispensing_Charging = value;
                OnPropertyChanged("BR_BRS_SEL_ReDispensing_Charging");
            }
        }
        private BR_BRS_REG_MaterialSubLot_Dispense_Scrap _BR_BRS_REG_MaterialSubLot_Dispense_Scrap;
        /// <summary>
        /// 현장칭량 소분 정보 조회
        /// </summary>
        public BR_BRS_REG_MaterialSubLot_Dispense_Scrap BR_BRS_REG_MaterialSubLot_Dispense_Scrap
        {
            get { return _BR_BRS_REG_MaterialSubLot_Dispense_Scrap; }
            set
            {
                _BR_BRS_REG_MaterialSubLot_Dispense_Scrap = value;
                OnPropertyChanged("BR_BRS_REG_MaterialSubLot_Dispense_Scrap");
            }
        }
        /// <summary>
        /// WMS 칭량실적 전송
        /// </summary>
        private BR_BRS_SEND_WMS_WEIGHINGRESULT _BR_BRS_SEND_WMS_WEIGHINGRESULT;
        public BR_BRS_SEND_WMS_WEIGHINGRESULT BR_BRS_SEND_WMS_WEIGHINGRESULT
        {
            get { return _BR_BRS_SEND_WMS_WEIGHINGRESULT; }
            set
            {
                _BR_BRS_SEND_WMS_WEIGHINGRESULT = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// 저울 조회
        /// </summary>
        private BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID;
        /// <summary>
        /// 저울 Tare
        /// </summary>
        BR_PHR_REG_ScaleSetTare _BR_PHR_REG_ScaleSetTare;
        /// <summary>
        /// 라벨발행
        /// </summary>
        BR_PHR_SEL_PRINT_LabelImage _BR_PHR_SEL_PRINT_LabelImage;
        /// <summary>
        /// 프린터 조회
        /// </summary>
        BR_PHR_SEL_System_Printer _BR_PHR_SEL_System_Printer;

        /// <summary>
        /// 자재투입 비즈룰
        /// </summary>
        private BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW;

        ///// <summary>
        ///// 용매 조회 비즈룰
        ///// </summary>
        //BR_BRS_SEL_Charging_Solvent_to_Dispense _BR_BRS_SEL_Charging_Solvent_to_Dispense;

        //public BR_BRS_SEL_Charging_Solvent_to_Dispense BR_BRS_SEL_Charging_Solvent_to_Dispense

        //{
        //    get { return _BR_BRS_SEL_Charging_Solvent_to_Dispense; }
        //    set
        //    {
        //        _BR_BRS_SEL_Charging_Solvent_to_Dispense = value;
        //        OnPropertyChanged("BR_BRS_SEL_Charging_Solvent_to_Dispense");
        //    }
        //}
        ///// <summary>
        ///// 용매 소분 후 투입 비즈룰
        ///// </summary>
        //private BR_BRS_REG_Dispense_Charging_Solvent _BR_BRS_REG_Dispense_Charging_Solvent;

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
                            IsBusy = true;

                            CommandResults["LoadedCommand"] = false;
                            CommandCanExecutes["LoadedCommand"] = false;

                            ///
                            if (arg != null && arg is SVP소분원료현장칭량_기준량)
                            {
                                _mainWnd = arg as SVP소분원료현장칭량_기준량;
                                _mainWnd.Closed += (s, e) =>
                                {
                                    if (_DispatcherTimer != null)
                                        _DispatcherTimer.Stop();

                                    _DispatcherTimer = null;
                                };

                                // 지시문 Validation
                                if (string.IsNullOrWhiteSpace(_mainWnd.CurrentInstruction.Raw.BOMID))
                                    throw new Exception(string.Format("해당 Instruction에 BOM ID가 설정되지 않았습니다."));
                                else if (string.IsNullOrWhiteSpace(_mainWnd.CurrentInstruction.Raw.EXPRESSION))
                                    throw new Exception(string.Format("해당 Instruction에 Chg Seq가 설정되지 않았습니다."));

                                // 저울 설정
                                ScaleId = _mainWnd.CurrentInstruction.Raw.EQPTID;
                                if (string.IsNullOrWhiteSpace(ScaleId) || !await GetScaleInfo(ScaleId))
                                    ScaleId = "";

                                // 프린터 설정
                                _BR_PHR_SEL_System_Printer.INDATAs.Add(new BR_PHR_SEL_System_Printer.INDATA
                                {
                                    LANGID = AuthRepositoryViewModel.Instance.LangID,
                                    ROOMID = AuthRepositoryViewModel.Instance.RoomID,
                                    IPADDRESS = Common.ClientIP
                                });
                                if (await _BR_PHR_SEL_System_Printer.Execute() && _BR_PHR_SEL_System_Printer.OUTDATAs.Count > 0)
                                {
                                    _selectedPrint = _BR_PHR_SEL_System_Printer.OUTDATAs[0];
                                    OnPropertyChanged("curPrintName");
                                }
                                else
                                    OnMessage("연결된 프린트가 없습니다.");

                                // 할당정보 조회
                                // 할당이 2개 이상인 경우 자재정보를 조회하지 않는다.                           
                                await GetAllocationInfo();

                                if (BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs.Count < 1)
                                {
                                    OnMessage("할당정보가 없습니다.");
                                    return;
                                }
                                else
                                {
                                    _DspStartDttm = await AuthRepositoryViewModel.GetDBDateTimeNow();
                                    SelectedAllocationInfo = BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs[0];

                                    await GetDispenseHistory();

                                    _DisepenQty.Value = Convert.ToDecimal(_BR_BRS_SEL_ReDispensing_Charging.OUTDATAs.Sum(o => o.DSPQTY));
                                    OnPropertyChanged("DspWeight");
                                }

                                foreach (var CHGCheck in BR_BRS_SEL_ReDispensing_Charging.OUTDATAs)
                                {
                                    // 투입 여부 확인 (투입은 한번에 다같이 실행됨. 0이 하나라도 존재해서는 안됨)
                                    if (CHGCheck.CHGQTY == 0)
                                    {
                                        ScrapbtnEnable = false;
                                        break;
                                    }
                                    else
                                    {
                                        ScrapbtnEnable = true;
                                    }
                                }
                                ScrapbtnEnable = true;
                            }
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

        public ICommand ScanMtrlCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ScanMtrlCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["ScanMtrlCommand"] = false;
                            CommandCanExecutes["ScanMtrlCommand"] = false;

                            ///
                            IsBusy = true;

                            _DispatcherTimer.Stop();

                            // Tare 확인 메세지
                            if (!_SetTare && !await OnMessageAsync("Tare가 0입니다. 진행하시겠습니까?", true))
                            {
                                return;
                            }
                            else
                            {
                                if (!_SetTare)
                                {
                                    _TareWeight = _ScaleWeight.Copy();
                                    _TareWeight.Value = 0;
                                    OnPropertyChanged("TareWeight");
                                }
                            }

                            await GetDispenseHistory();

                            // 현재 소분량 확인 후 진행
                            decimal usedweight = Convert.ToDecimal(_BR_BRS_SEL_ReDispensing_Charging.OUTDATAs.Sum(o => o.DSPQTY));


                            // 사용중인 원료백이 있으면 사용량 처리 후 원료백 팝업요청
                            if (curSelectedSourceContainer != null && curSelectedSourceContainer.MSUBLOTQTY > 0)
                            {
                                if (BR_BRS_SEL_ReDispensing_Charging.OUTDATAs.Count < 0 & _ScaleWeight.Value > 0)
                                {
                                    OnMessage("소분 먼저 진행해주세요.");
                                    _DispatcherTimer.Start();
                                    return;
                                }

                                
                                string msg = string.Format("현재 소분된 총 양은 [{0}]입니다, 변경하시겠습니까?", usedweight);
                                if (await OnMessageAsync(msg, true)) { }
                                else
                                {
                                    _DispatcherTimer.Start();
                                    return;
                                }
                            }

                            BarcodePopup popup = new BarcodePopup();
                            popup.tbMsg.Text = "원료바코드를 스캔하세요.";
                            popup.Closed += (sender, e) =>
                            {
                                if (popup.DialogResult.GetValueOrDefault() && !string.IsNullOrWhiteSpace(popup.tbText.Text))
                                {
                                    string text = popup.tbText.Text.ToUpper();

                                    if (BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs.Count > 0)
                                    {
                                        var select = BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs.Where(o => o.MSUBLOTBCD == text).FirstOrDefault();

                                        if (curSelectedSourceContainer != null)
                                        {
                                            if (curSelectedSourceContainer.MSUBLOTBCD == text)
                                            {
                                                OnMessage("현재 사용중인 원료백입니다.");
                                                _DispatcherTimer.Start();
                                                return;
                                            }
                                        }

                                        if (select != null)
                                        {
                                            if (select.STATUS == "사용")
                                            {
                                                OnMessage("이미 사용한 원료백입니다.");
                                                if (curSelectedSourceContainer != null)
                                                {
                                                    curSelectedSourceContainer.IsSelected = false;
                                                    curSelectedSourceContainer = null;
                                                }
                                                OnPropertyChanged("BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD");
                                                _DispatcherTimer.Start();
                                                return;
                                            }

                                            // 원료백 변경
                                            select.IsSelected = true;
                                            foreach (var item in BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs)
                                            {
                                                if (item.MSUBLOTBCD != select.MSUBLOTBCD)
                                                    item.IsSelected = false;
                                            }
                                            curSelectedSourceContainer = select;
                                            TarebtnEnable = false;
                                            OnPropertyChanged("BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD");
                                        }
                                    }
                                }
                                else
                                {
                                    if (curSelectedSourceContainer != null)
                                    {
                                        curSelectedSourceContainer.IsSelected = false;
                                        curSelectedSourceContainer = null;
                                    }
                                    OnPropertyChanged("BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD");
                                }


                                _DispatcherTimer.Start();
                            };

                            popup.Show();
                            ///

                            CommandResults["ScanMtrlCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ScanMtrlCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            IsBusy = false;
                            CommandCanExecutes["ScanMtrlCommand"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ScanMtrlCommand") ?
                        CommandCanExecutes["ScanMtrlCommand"] : (CommandCanExecutes["ScanMtrlCommand"] = true);
                });
            }
        }

        public ICommand ScanScaleCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ScanScaleCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["ScanScaleCommand"] = false;
                            CommandCanExecutes["ScanScaleCommand"] = false;

                            ///
                            IsBusy = true;
                            _DispatcherTimer.Stop();

                            BarcodePopup popup = new BarcodePopup();
                            popup.tbMsg.Text = "저울바코드를 스캔하세요.";
                            popup.Closed += async (sender, e) =>
                            {
                                if (popup.DialogResult.GetValueOrDefault() && !string.IsNullOrWhiteSpace(popup.tbText.Text))
                                {
                                    string text = popup.tbText.Text.ToUpper();

                                    if (await GetScaleInfo(text))
                                        _DispatcherTimer.Start();
                                    else
                                        ScaleId = "";
                                }
                                else
                                    ScaleId = "";
                            };

                            popup.Show();

                            IsBusy = false;
                            ///

                            CommandResults["ScanScaleCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            CommandResults["ScanScaleCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ScanScaleCommand"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ScanScaleCommand") ?
                        CommandCanExecutes["ScanScaleCommand"] : (CommandCanExecutes["ScanScaleCommand"] = true);
                });
            }
        }

        public ICommand SetTareCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["SetTareCommand"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["SetTareCommand"] = false;
                            CommandCanExecutes["SetTareCommand"] = false;

                            ///
                            IsBusy = true;

                            _DispatcherTimer.Stop();

                            if (_TareWeight.Value <= 0)
                            {
                                OnMessage("TARE가 0보다 작을 수 없습니다.");
                                return;
                            }

                            bool success = false;

                            if (_ScaleInfo.INTERFACE.ToUpper() == "REST")
                            {
                                var result = await _restScaleService.DownloadString(_ScaleInfo.EQPTID, ScaleCommand.ST);

                                if (result.code != "0")
                                    success = true;
                            }
                            else
                            {
                                _BR_PHR_REG_ScaleSetTare.INDATAs.Clear();
                                _BR_PHR_REG_ScaleSetTare.INDATAs.Add(new BR_PHR_REG_ScaleSetTare.INDATA
                                {
                                    ScaleID = ScaleId
                                });

                                if (await _BR_PHR_REG_ScaleSetTare.Execute())
                                    success = true;
                            }

                            if (!success)
                                _TareWeight.Value = 0m;
                            else
                            {
                                _SetTare = true;
                                TarebtnEnable = false;
                            }


                            OnPropertyChanged("TareWeight");
                            ///

                            CommandResults["SetTareCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SetTareCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            IsBusy = false;
                            _DispatcherTimer.Start();
                            CommandCanExecutes["SetTareCommand"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SetTareCommand") ?
                        CommandCanExecutes["SetTareCommand"] : (CommandCanExecutes["SetTareCommand"] = true);
                });
            }
        }

        public ICommand DispensingCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["DispensingCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["DispensingCommandAsync"] = false;
                            CommandCanExecutes["DispensingCommandAsync"] = false;

                            ///
                            _DispatcherTimer.Stop();
                            //
                            if (curSelectedSourceContainer != null && curSelectedSourceContainer.MSUBLOTQTY > 0)
                            {
                                // UI를 나가지 않는다는 가정하에 코드를 짰는데 이렇게 진행해도 되는지 문의
                                // UI를 나갈 시 화면에 표시되는 소분량+저울 무게와 실제 저울 무게가 달라 문제가 발생할 것으로 보임 논의 필요.
                                // 다른 원료백의 사용량 확인
                                decimal usedweight = _ScaleWeight.Value - _DisepenQty.Value;
                                if (usedweight < 0)
                                {
                                    OnMessage("사용량이 0보다 작을 수 없습니다.");
                                    return;
                                }
                                string msg = string.Format("소분량 : [{0}], 소분하시겠습니까?", usedweight);
                                if (await OnMessageAsync(msg, true))
                                {
                                    // 사용량 설정
                                    curSelectedSourceContainer.UsedWeight = usedweight;
                                    TarebtnEnable = false;

                                    // 소분전자서명
                                    var authHelper = new iPharmAuthCommandHelper();
                                    authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_Charging");
                                    if (await authHelper.ClickAsync(
                                        Common.enumCertificationType.Function,
                                        Common.enumAccessType.Create,
                                        "SVP소분원료현장칭량_기준량",
                                        "SVP소분원료현장칭량_기준량",
                                        false,
                                        "OM_ProductionOrder_Charging",
                                        "", null, null) == false)
                                    {
                                        throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                    }

                                    BR_BRS_REG_MaterialSubLot_Dispense_SVP_Loss DispenseBR = new BR_BRS_REG_MaterialSubLot_Dispense_SVP_Loss();
                                    DateTime dspend = await AuthRepositoryViewModel.GetDBDateTimeNow();
                                    DispenseBR.INDATAs.Add(new BR_BRS_REG_MaterialSubLot_Dispense_SVP_Loss.INDATA
                                    {
                                        MSUBLOTID = curSelectedSourceContainer.MSUBLOTID,
                                        MSUBLOTBCD = curSelectedSourceContainer.MSUBLOTBCD,
                                        INSDTTM = DateTime.Now,
                                        INSUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Charging"),
                                        DEPLETFLAG = "P",
                                        VESSELID = null,
                                        POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                        MSUBLOTTYPE = "DSP",
                                        COMPONENTGUID = curSelectedSourceContainer.COMPONENTGUID,
                                        TARE = _TareWeight.Value,
                                        LOCATIONID = AuthRepositoryViewModel.Instance.RoomID,
                                        INVENTORYQTY = curSelectedSourceContainer.MSUBLOTQTY,
                                        DISPENSEQTY = curSelectedSourceContainer.UsedWeight,
                                        ISDISPSTRT = "Y",
                                        ACTID = "ReDispensing",
                                        CHECKINDTTM = DateTime.Now,
                                        CHECKINUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Charging"),
                                        WEIGHINGMETHOD = "WH007",
                                        UPPERVALUE = _UpperWeight.Value,
                                        LOWERVALUE = _LowerWeight.Value,
                                        OPSGGUID = new Guid(_mainWnd.CurrentOrder.OrderProcessSegmentID),
                                        TAREWEIGHT = _TareWeight.Value,
                                        TAREUOMID = _ScaleWeight.Uom,
                                        SCALEID = ScaleId,
                                        INSSIGNATURE = AuthRepositoryViewModel.Instance.ConfirmedGuid,
                                        DSPSTRTDTTM = _DspStartDttm.ToString("yyyy-MM-dd HH:mm:ss"),
                                        DSPENDDTTM = dspend.ToString("yyyy-MM-dd HH:mm:ss"),
                                        WEIGHINGROOM = AuthRepositoryViewModel.Instance.RoomID
                                    });

                                    string text = DispenseBR.INDATAs[0].MSUBLOTBCD;

                                    await DispenseBR.Execute();

                                    _DisepenQty.Value += curSelectedSourceContainer.UsedWeight;

                                    await GetAllocationInfo();

                                    // 소분한 원료 유지
                                    var select = BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs.Where(o => o.MSUBLOTBCD == text).FirstOrDefault();
                                    select.IsSelected = true;
                                    foreach (var item in BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs)
                                    {
                                        if (item.MSUBLOTBCD != select.MSUBLOTBCD)
                                            item.IsSelected = false;
                                    }
                                    curSelectedSourceContainer = select;

                                    OnPropertyChanged("BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD");

                                    await GetDispenseHistory();
                                }
                                else
                                    return;
                            }
                            else
                                OnMessage("소분 정보가 없습니다.");

                            CommandResults["DispensingCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["DispensingCommandAsync"] = false;
                            OnException(ex.Message, ex);
                            
                            if (curSelectedSourceContainer != null)
                            {
                                curSelectedSourceContainer.UsedWeight = 0;
                                curSelectedSourceContainer.IsSelected = true;
                            }
                        }
                        finally
                        {
                            CommandCanExecutes["DispensingCommandAsync"] = true;
                            _DispatcherTimer.Start();
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("DispensingCommandAsync") ?
                        CommandCanExecutes["DispensingCommandAsync"] : (CommandCanExecutes["DispensingCommandAsync"] = true);
                });
            }
        }

        public ICommand ChargingCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ChargingCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ChargingCommandAsync"] = false;
                            CommandCanExecutes["ChargingCommandAsync"] = false;

                            ///
                            _DispatcherTimer.Stop();

                            _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.INDATAs.Clear();
                            _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.INDATA_INVs.Clear();

                            StringBuilder msg = new StringBuilder("투입처리된 소분백 목록\n");

                            foreach (var item in BR_BRS_SEL_ReDispensing_Charging.OUTDATAs)
                            {
                                if (item.DSPQTY > 0)
                                {
                                    _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.INDATAs.Add(new BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.INDATA
                                    {
                                        MSUBLOTID = item.MSUBLOTID,
                                        MSUBLOTBCD = item.MSUBLOTBCD,
                                        MSUBLOTQTY = item.DSPQTY,
                                        INSUSER = "",
                                        POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                        INSDTTM = null,
                                        IS_NEED_CHKWEIGHT = "N",
                                        IS_FULL_CHARGE = "Y",
                                        IS_CHECKONLY = "N",
                                        IS_INVEN_CHARGE = "N",
                                        CHECKINUSER = "",
                                        IS_OUTPUT = "N",
                                        OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                                    });

                                    msg.AppendLine(item.MSUBLOTBCD);
                                }
                            }

                            if (_BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.INDATAs.Count > 0)
                            {
                                // 투입전자서명
                                var authHelper = new iPharmAuthCommandHelper();
                                authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_Charging");
                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    "소분백 투입",
                                    "소분백 투입",
                                    false,
                                    "OM_ProductionOrder_Charging",
                                    "", null, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }

                                DateTime curDttm = await AuthRepositoryViewModel.GetDBDateTimeNow();

                                foreach (var item in _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.INDATAs)
                                {
                                    item.INSDTTM = curDttm;
                                    item.INSUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Charging");
                                    item.CHECKINUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Charging");
                                }

                                if (!await _BR_RHR_REG_MaterialSubLot_Dispense_Charging_NEW.Execute())
                                {
                                    throw new Exception(string.Format("투입처리 중 오류가 발생했습니다."));
                                }

                                await GetDispenseHistory();

                                OnMessage(msg.ToString());
                                ScrapbtnEnable = true;
                            }
                            else
                                OnMessage("투입할 소분백이 없습니다.");

                            _DispatcherTimer.Start();
                            ///

                            CommandResults["ChargingCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ChargingCommandAsync"] = false;
                            _DispatcherTimer.Start();
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ChargingCommandAsync"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ChargingCommandAsync") ?
                        CommandCanExecutes["ChargingCommandAsync"] : (CommandCanExecutes["ChargingCommandAsync"] = true);
                });
            }
        }
        public ICommand ScrapCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ScrapCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["ScrapCommandAsync"] = false;
                            CommandCanExecutes["ScrapCommandAsync"] = false;
                            
                            _DispatcherTimer.Stop();
                            
                            foreach (var item in BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs)
                            {
                                if (item.REMAINQTY > 0)
                                {
                                    _BR_BRS_REG_MaterialSubLot_Dispense_Scrap.INDATAs.Add(new BR_BRS_REG_MaterialSubLot_Dispense_Scrap.INDATA
                                    {
                                        LANGID = "ko-KR",
                                        MSUBLOTID = item.MSUBLOTID,
                                        MSUBLOTBCD = item.MSUBLOTBCD,
                                        INSDTTM = DateTime.Now,
                                        INSUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Charging"),
                                        REASON = "잔량 폐기",
                                        COMPONENTGUID = item.COMPONENTGUID,
                                        POID = _mainWnd.CurrentOrder.ProductionOrderID
                                    });
                                    
                                }
                            }

                            if (!await _BR_BRS_REG_MaterialSubLot_Dispense_Scrap.Execute())
                            {
                                throw new Exception(string.Format("잔량 폐기 처리 중 오류가 발생했습니다."));
                            }else
                            {
                                OnMessage("정상적으로 잔량 폐기 되었습니다.");
                            }

                            _DispatcherTimer.Start();

                            CommandResults["ScrapCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["ScrapCommandAsync"] = false;
                            _DispatcherTimer.Start();
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["ScrapCommandAsync"] = true;
                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ScrapCommandAsync") ?
                        CommandCanExecutes["ScrapCommandAsync"] : (CommandCanExecutes["ScrapCommandAsync"] = true);
                });
            }
        }

        public ICommand ConfirmCommandAsync
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

                            _DispatcherTimer.Stop();

                            var authHelper = new iPharmAuthCommandHelper();
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

                            authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_Charging");
                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                "투입내용기록",
                                "투입내용기록",
                                false,
                                "OM_ProductionOrder_Charging",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            var ds = new DataSet();
                            var dt = new DataTable("DATA");
                            ds.Tables.Add(dt);
                            dt.Columns.Add(new DataColumn("자재ID"));
                            dt.Columns.Add(new DataColumn("자재명"));
                            dt.Columns.Add(new DataColumn("원료배치번호"));
                            dt.Columns.Add(new DataColumn("바코드"));
                            dt.Columns.Add(new DataColumn("무게"));
                            dt.Columns.Add(new DataColumn("단위"));

                            foreach (var item in BR_BRS_SEL_ReDispensing_Charging.OUTDATAs)
                            {
                                if (item.CHGQTY.GetValueOrDefault() > 0)
                                {
                                    var row = dt.NewRow();
                                    row["자재ID"] = item.MTRLID ?? "";
                                    row["자재명"] = item.MTRLNAME ?? "";
                                    row["원료배치번호"] = item.MSUBLOTID ?? "";
                                    row["바코드"] = item.MSUBLOTBCD ?? "";
                                    row["무게"] = item.CHGQTY.GetValueOrDefault().ToString("F" + item.PRECISION);
                                    row["단위"] = item.NOTATION;
                                    dt.Rows.Add(row);
                                }
                            }

                            if (dt.Rows.Count > 0)
                            {
                                var xml = BizActorRuleBase.CreateXMLStream(ds);
                                var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                                _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                                _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                                var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                                if (result != enumInstructionRegistErrorType.Ok)
                                {
                                    throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                                }

                                _DispatcherTimer.Stop();
                                if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                                else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);
                            }
                            else
                                OnMessage("투입된 소분백이 없습니다.");
                            ///

                            CommandResults["ConfirmCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            _DispatcherTimer.Start();
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

        public ICommand NoRecordConfirmCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["NoRecordConfirmCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["NoRecordConfirmCommand"] = false;
                            CommandCanExecutes["NoRecordConfirmCommand"] = false;

                            _DispatcherTimer.Stop();

                            // 전자서명
                            var authHelper = new iPharmAuthCommandHelper();
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

                            authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_Charging");
                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                "투입내용기록",
                                "투입내용기록",
                                false,
                                "OM_ProductionOrder_Charging",
                                "", null, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }

                            //XML 형식으로 저장
                            var ds = new DataSet();
                            var dt = new DataTable("DATA");
                            ds.Tables.Add(dt);
                            dt.Columns.Add(new DataColumn("자재ID"));
                            dt.Columns.Add(new DataColumn("자재명"));
                            dt.Columns.Add(new DataColumn("원료배치번호"));
                            dt.Columns.Add(new DataColumn("바코드"));
                            dt.Columns.Add(new DataColumn("무게"));
                            dt.Columns.Add(new DataColumn("단위"));

                            var row = dt.NewRow();
                            row["자재ID"] = "N/A";
                            row["자재명"] = "N/A";
                            row["원료배치번호"] = "N/A";
                            row["바코드"] = "N/A";
                            row["무게"] = "N/A";
                            row["단위"] = "N/A";
                            dt.Rows.Add(row);

                            var xml = BizActorRuleBase.CreateXMLStream(ds);
                            var bytesArray = System.Text.Encoding.UTF8.GetBytes(xml);

                            _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                            _mainWnd.CurrentInstruction.Raw.NOTE = bytesArray;

                            var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                            if (result != enumInstructionRegistErrorType.Ok)
                            {
                                throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                            }

                            _DispatcherTimer.Stop();
                            if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                            else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                            _mainWnd.Close();

                            //
                            CommandResults["NoRecordConfirmCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["NoRecordConfirmCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["NoRecordConfirmCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("NoRecordConfirmCommand") ?
                        CommandCanExecutes["NoRecordConfirmCommand"] : (CommandCanExecutes["NoRecordConfirmCommand"] = true);
                });
            }
        }

        public ICommand ChangePrintCommand
        {
            get
            {
                return new CommandBase(arg =>
                {
                    try
                    {
                        IsBusy = true;

                        CommandResults["ChangePrintCommand"] = false;
                        CommandCanExecutes["ChangePrintCommand"] = false;

                        ///
                        SelectPrinterPopup popup = new SelectPrinterPopup();

                        popup.Closed += (s, e) =>
                        {
                            if (popup.DialogResult.GetValueOrDefault())
                            {
                                if (popup.SourceGrid.SelectedItem != null && popup.SourceGrid.SelectedItem is BR_PHR_SEL_System_Printer.OUTDATA)
                                {
                                    _selectedPrint = popup.SourceGrid.SelectedItem as BR_PHR_SEL_System_Printer.OUTDATA;
                                    OnPropertyChanged("curPrintName");
                                }
                            }
                        };

                        popup.Show();
                        ///

                        CommandResults["ChangePrintCommand"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["ChangePrintCommand"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        CommandCanExecutes["ChangePrintCommand"] = true;

                        IsBusy = false;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("ChangePrintCommand") ?
                        CommandCanExecutes["ChangePrintCommand"] : (CommandCanExecutes["ChangePrintCommand"] = true);
                });
            }
        }

        public ICommand PrintLabelCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["PrintLabelCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["PrintLabelCommandAsync"] = false;
                            CommandCanExecutes["PrintLabelCommandAsync"] = false;

                            ///
                            if (arg != null && arg is BR_BRS_SEL_ReDispensing_Charging.OUTDATA)
                            {
                                var authHelper = new iPharmAuthCommandHelper();
                                authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_Charging");
                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    "투입내용기록",
                                    "투입내용기록",
                                    true,
                                    "OM_ProductionOrder_Charging",
                                    "", null, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }

                                await LabelPrint((arg as BR_BRS_SEL_ReDispensing_Charging.OUTDATA).MSUBLOTID);
                            }
                            ///

                            CommandResults["PrintLabelCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["PrintLabelCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["PrintLabelCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("PrintLabelCommandAsync") ?
                        CommandCanExecutes["PrintLabelCommandAsync"] : (CommandCanExecutes["PrintLabelCommandAsync"] = true);
                });
            }
        }

        #endregion

        #region [Custom]
        async void _DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _DispatcherTimer.Stop();

                if (_ScaleInfo != null)
                {
                    bool success = false;
                    if (_ScaleInfo.INTERFACE.ToUpper() == "REST")
                    {
                        var result = await _restScaleService.DownloadString(_ScaleInfo.EQPTID, ScaleCommand.GW);

                        if (result.code == "1")
                        {
                            success = true;
                            _ScaleWeight.SetWeight(Convert.ToString(Math.Abs(Convert.ToDecimal(result.data))), result.unit);
                        }
                    }
                    else
                    {
                        BR_BRS_SEL_CurrentWeight current_wight = new BR_BRS_SEL_CurrentWeight();
                        current_wight.INDATAs.Add(new BR_BRS_SEL_CurrentWeight.INDATA()
                        {
                            ScaleID = _ScaleInfo.EQPTID
                        });

                        if (await current_wight.Execute(exceptionHandle: LGCNS.iPharmMES.Common.Common.enumBizRuleXceptionHandleType.FailEvent) == true
                            && current_wight.OUTDATAs.Count > 0 && current_wight.OUTDATAs[0].Weight.HasValue)
                        {
                            success = true;
                            _ScaleWeight.SetWeight(Math.Abs(current_wight.OUTDATAs[0].Weight.Value), current_wight.OUTDATAs[0].UOM, _scalePrecision);
                        }
                    }

                    if (success)
                    {
                        if (!_SetTare)
                        {
                            _TareWeight = _ScaleWeight.Copy();
                            _ScaleException = false;
                            DispensebtnEnable = true;
                        }
                        else
                        {
                            _ScaleException = false;
                            DispensebtnEnable = true;
                            if (_LowerWeight.Value <= _ScaleWeight.Add(_DisepenQty).Value && _ScaleWeight.Add(_DisepenQty).Value <= _UpperWeight.Value)
                            {
                                ScaleBackground = new SolidColorBrush(Colors.Green);
                            }
                            else
                            {
                                ScaleBackground = new SolidColorBrush(Colors.Yellow);
                            }
                        }
                    }
                    else
                    {
                        _ScaleException = true;
                        _ScaleWeight.SetWeight(0, _ScaleWeight.Uom, _scalePrecision);
                        ScaleBackground = new SolidColorBrush(Colors.Red);
                        DispensebtnEnable = false;
                    }

                    OnPropertyChanged("ScaleWeight");
                    OnPropertyChanged("DspWeight");
                    OnPropertyChanged("NowWeight");
                    OnPropertyChanged("TareWeight");
                    OnPropertyChanged("UpperWeight");
                    OnPropertyChanged("LowerWeight");
                    _DispatcherTimer.Start();
                }
            }
            catch (Exception ex)
            {
                _DispatcherTimer.Stop();
                OnException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 저울정보 조회
        /// </summary>
        /// <param name="scaleid"></param>
        /// <returns>True:조회성공</returns>
        private async Task<bool> GetScaleInfo(string scaleid)
        {
            try
            {
                // 저울 정보 조회
                _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.INDATAs.Clear();
                _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATAs.Clear();
                _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.INDATAs.Add(new BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.INDATA
                {
                    LANGID = AuthRepositoryViewModel.Instance.LangID,
                    EQPTID = scaleid
                });

                if (await _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.Execute() && _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATAs.Count > 0)
                {
                    _ScaleInfo = _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATAs[0];
                    TarebtnEnable = true;

                    scalePrecision = _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATAs[0].PRECISION.HasValue ? Convert.ToInt32(_BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATAs[0].PRECISION.Value) : 3;
                    ScaleId = _BR_BRS_SEL_EquipmentCustomAttributeValue_ScaleInfo_EQPTID.OUTDATAs[0].EQPTID;

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
                return false;
            }
        }

        private async Task GetAllocationInfo()
        {
            try
            {
                _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.INDATAs.Clear();
                _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.OUTDATAs.Clear();
                _BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.INDATAs.Add(new BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.INDATA
                {
                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                    MTRLID = _mainWnd.CurrentInstruction.Raw.BOMID,
                    CHGSEQ = _mainWnd.CurrentInstruction.Raw.EXPRESSION,
                    TOLERANCE = _mainWnd.CurrentInstruction.Raw.TARGETVAL
                });

                await BR_BRS_SEL_POAllocation_AreaWeighing_CHG_STD.Execute();

            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
            }
        }
        /// <summary>
        /// 소분정보 조회
        /// </summary>
        private async Task GetDispenseHistory()
        {
            try
            {
                // 소분정보 조회
                if (_SelectedAllocationInfo != null)
                {
                    _BR_BRS_SEL_ReDispensing_Charging.INDATAs.Clear();
                    _BR_BRS_SEL_ReDispensing_Charging.OUTDATAs.Clear();
                    _BR_BRS_SEL_ReDispensing_Charging.INDATAs.Add(new BR_BRS_SEL_ReDispensing_Charging.INDATA
                    {
                        POID = _SelectedAllocationInfo.POID,
                        COMPONENTGUID = _SelectedAllocationInfo.COMPONENTGUID
                    });

                    await BR_BRS_SEL_ReDispensing_Charging.Execute();

                    OnPropertyChanged("BR_BRS_SEL_ReDispensing_Charging");

                    if (BR_BRS_SEL_ReDispensing_Charging.OUTDATAs.Count > 0)
                        ChargebtnEnable = true;
                }
            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 라벨발행
        /// </summary>
        /// <param name="msublotid"></param>
        private async Task LabelPrint(string msublotid)
        {
            try
            {
                if (_selectedPrint != null)
                {
                    _BR_PHR_SEL_PRINT_LabelImage.INDATAs.Clear();
                    _BR_PHR_SEL_PRINT_LabelImage.Parameterss.Clear();

                    _BR_PHR_SEL_PRINT_LabelImage.INDATAs.Add(new BR_PHR_SEL_PRINT_LabelImage.INDATA
                    {
                        ReportPath = "/Reports/Label/LABEL_WEIGHING_SOLUTION_SVP",
                        PrintName = _selectedPrint.PRINTERNAME,
                        USERID = AuthRepositoryViewModel.Instance.LoginedUserID
                    });
                    _BR_PHR_SEL_PRINT_LabelImage.Parameterss.Add(new BR_PHR_SEL_PRINT_LabelImage.Parameters
                    {
                        ParamName = "MSUBLOTID",
                        ParamValue = msublotid
                    });

                    await _BR_PHR_SEL_PRINT_LabelImage.Execute(Common.enumBizRuleOutputClearMode.Always, Common.enumBizRuleXceptionHandleType.FailEvent);
                }

                return;
            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
                return;
            }
        }
        #endregion
    }
}
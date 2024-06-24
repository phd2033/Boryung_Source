using C1.Silverlight.Data;
using LGCNS.iPharmMES.Common;
using ShopFloorUI;
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
using System.Collections.Generic;
using C1.Silverlight.Imaging;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Collections.ObjectModel;

namespace 보령
{
    public class 정제체크마스터입력ViewModel : ViewModelBase
    {
        #region [Property]
        public 정제체크마스터입력ViewModel()
        {
            _BR_PHR_SEL_CODE = new BR_PHR_SEL_CODE();
            //_BR_BRS_GET_Selector_Check_Master = new BR_BRS_GET_Selector_Check_Master();
            _BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit = new BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit();
            _BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD = new BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD();
            _IPCResultSections = new IPCResultSection.OUTDATACollection();
            _IPC_RESULTS = new ObservableCollection<EACH_INDATA>();
            _IPC_STANDARDS = new ObservableCollection<EACH_INDATA>();

        }

        정제체크마스터입력 _mainWnd;

        private IPCResultSection.OUTDATACollection _IPCResultSections;
        public IPCResultSection.OUTDATACollection IPCResultSections
        {
            get { return this._IPCResultSections; }
            set
            {
                this._IPCResultSections = value;
                this.OnPropertyChanged("IPCResultSections");
            }
        }

        private ObservableCollection<EACH_INDATA> _IPC_RESULTS;
        public ObservableCollection<EACH_INDATA> IPC_RESULTS
        {
            get { return _IPC_RESULTS; }
            set
            {
                _IPC_RESULTS = value;
                OnPropertyChanged("IPC_RESULTS");
            }
        }
        private ObservableCollection<EACH_INDATA> _IPC_STANDARDS;
        public ObservableCollection<EACH_INDATA> IPC_STANDARDS
        {
            get { return _IPC_STANDARDS; }
            set
            {
                _IPC_STANDARDS = value;
                OnPropertyChanged("IPC_STANDARDS");
            }
        }
        private string _EQPTID;
        public string EQPTID
        {
            get { return this._EQPTID; }
            set
            {
                this._EQPTID = value;
                this.OnPropertyChanged("EQPTID");
            }
        }

        private string _EQPTNAME;
        public string EQPTNAME
        {
            get { return this._EQPTNAME; }
            set
            {
                this._EQPTNAME = value;
                this.OnPropertyChanged("EQPTNAME");
            }
        }
        private bool _INPUT_ENABLE;
        public bool INPUT_ENABLE
        {
            get { return _INPUT_ENABLE; }
            set
            {
                _INPUT_ENABLE = value;
                OnPropertyChanged("INPUT_ENABLE");
            }
        }
        private bool _EQPTID_ENABLE;
        public bool EQPTID_ENABLE
        {
            get { return _EQPTID_ENABLE; }
            set
            {
                _EQPTID_ENABLE = value;
                OnPropertyChanged("EQPTID_ENABLE");
            }
        }
        private decimal _AVG_RESULT_WEIGHT;
        public decimal AVG_RESULT_WEIGHT
        {
            get { return this._AVG_RESULT_WEIGHT; }
            set
            {
                this._AVG_RESULT_WEIGHT = value;
                this.OnPropertyChanged("AVG_RESULT_WEIGHT");
            }
        }
        private decimal _MIN_RESULT_WEIGHT;
        public decimal MIN_RESULT_WEIGHT
        {
            get { return this._MIN_RESULT_WEIGHT; }
            set
            {
                this._MIN_RESULT_WEIGHT = value;
                this.OnPropertyChanged("MIN_RESULT_WEIGHT");
            }
        }
        private decimal _MAX_RESULT_WEIGHT;
        public decimal MAX_RESULT_WEIGHT
        {
            get { return this._MAX_RESULT_WEIGHT; }
            set
            {
                this._MAX_RESULT_WEIGHT = value;
                this.OnPropertyChanged("MAX_RESULT_WEIGHT");
            }
        }
        private decimal _SD_RESULT_WEIGHT;
        public decimal SD_RESULT_WEIGHT
        {
            get { return this._SD_RESULT_WEIGHT; }
            set
            {
                this._SD_RESULT_WEIGHT = value;
                this.OnPropertyChanged("SD_RESULT_WEIGHT");
            }
        }
        private decimal _AVG_RESULT_THICKNESS;
        public decimal AVG_RESULT_THICKNESS
        {
            get { return this._AVG_RESULT_THICKNESS; }
            set
            {
                this._AVG_RESULT_THICKNESS = value;
                this.OnPropertyChanged("AVG_RESULT_THICKNESS");
            }
        }
        private decimal _MIN_RESULT_THICKNESS;
        public decimal MIN_RESULT_THICKNESS
        {
            get { return this._MIN_RESULT_THICKNESS; }
            set
            {
                this._MIN_RESULT_THICKNESS = value;
                this.OnPropertyChanged("MIN_RESULT_THICKNESS");
            }
        }
        private decimal _MAX_RESULT_THICKNESS;
        public decimal MAX_RESULT_THICKNESS
        {
            get { return this._MAX_RESULT_THICKNESS; }
            set
            {
                this._MAX_RESULT_THICKNESS = value;
                this.OnPropertyChanged("MAX_RESULT_THICKNESS");
            }
        }
        private decimal _AVG_RESULT_HARDNESS;
        public decimal AVG_RESULT_HARDNESS
        {
            get { return this._AVG_RESULT_HARDNESS; }
            set
            {
                this._AVG_RESULT_HARDNESS = value;
                this.OnPropertyChanged("AVG_RESULT_HARDNESS");
            }
        }
        private decimal _MIN_RESULT_HARDNESS;
        public decimal MIN_RESULT_HARDNESS
        {
            get { return this._MIN_RESULT_HARDNESS; }
            set
            {
                this._MIN_RESULT_HARDNESS = value;
                this.OnPropertyChanged("MIN_RESULT_HARDNESS");
            }
        }
        private decimal _MAX_RESULT_HARDNESS;
        public decimal MAX_RESULT_HARDNESS
        {
            get { return this._MAX_RESULT_HARDNESS; }
            set
            {
                this._MAX_RESULT_HARDNESS = value;
                this.OnPropertyChanged("MAX_RESULT_HARDNESS");
            }
        }
        private DateTime _AVG_DTTM;
        public DateTime AVG_DTTM
        {
            get { return this._AVG_DTTM; }
            set
            {
                this._AVG_DTTM = value;
                this.OnPropertyChanged("AVG_DTTM");
            }
        }
        private String _AVG_STD_WEIGHT;
        public String AVG_STD_WEIGHT
        {
            get { return this._AVG_STD_WEIGHT; }
            set
            {
                this._AVG_STD_WEIGHT = value;
                this.OnPropertyChanged("AVG_STD_WEIGHT");
            }
        }
        private String _MIN_STD_WEIGHT;
        public String MIN_STD_WEIGHT
        {
            get { return this._MIN_STD_WEIGHT; }
            set
            {
                this._MIN_STD_WEIGHT = value;
                this.OnPropertyChanged("MIN_STD_WEIGHT");
            }
        }
        private String _MAX_STD_WEIGHT;
        public String MAX_STD_WEIGHT
        {
            get { return this._MAX_STD_WEIGHT; }
            set
            {
                this._MAX_STD_WEIGHT = value;
                this.OnPropertyChanged("MAX_STD_WEIGHT");
            }
        }
        private String _SD_STD_WEIGHT;
        public String SD_STD_WEIGHT
        {
            get { return this._SD_STD_WEIGHT; }
            set
            {
                this._SD_STD_WEIGHT = value;
                this.OnPropertyChanged("SD_STD_WEIGHT");
            }
        }
        private String _AVG_STD_THICKNESS;
        public String AVG_STD_THICKNESS
        {
            get { return this._AVG_STD_THICKNESS; }
            set
            {
                this._AVG_STD_THICKNESS = value;
                this.OnPropertyChanged("AVG_STD_THICKNESS");
            }
        }
        private String _MIN_STD_THICKNESS;
        public String MIN_STD_THICKNESS
        {
            get { return this._MIN_STD_THICKNESS; }
            set
            {
                this._MIN_STD_THICKNESS = value;
                this.OnPropertyChanged("MIN_STD_THICKNESS");
            }
        }
        private String _MAX_STD_THICKNESS;
        public String MAX_STD_THICKNESS
        {
            get { return this._MAX_STD_THICKNESS; }
            set
            {
                this._MAX_STD_THICKNESS = value;
                this.OnPropertyChanged("MAX_STD_THICKNESS");
            }
        }
        private String _AVG_STD_HARDNESS;
        public String AVG_STD_HARDNESS
        {
            get { return this._AVG_STD_HARDNESS; }
            set
            {
                this._AVG_STD_HARDNESS = value;
                this.OnPropertyChanged("AVG_STD_HARDNESS");
            }
        }
        private String _MIN_STD_HARDNESS;
        public String MIN_STD_HARDNESS
        {
            get { return this._MIN_STD_HARDNESS; }
            set
            {
                this._MIN_STD_HARDNESS = value;
                this.OnPropertyChanged("MIN_STD_HARDNESS");
            }
        }
        private String _MAX_STD_HARDNESS;
        public String MAX_STD_HARDNESS
        {
            get { return this._MAX_STD_HARDNESS; }
            set
            {
                this._MAX_STD_HARDNESS = value;
                this.OnPropertyChanged("MAX_STD_HARDNESS");
            }
        }

        #endregion

        #region [Bizrule]

        private BR_PHR_SEL_CODE _BR_PHR_SEL_CODE;
        private BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit _BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit;
        private BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD _BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD;
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
                            CommandResults["LoadedCommandAsync"] = false;
                            CommandCanExecutes["LoadedCommandAsync"] = false;

                            if (arg != null && arg is 정제체크마스터입력)
                            {
                                _mainWnd = arg as 정제체크마스터입력;
                                IsBusy = true;
                                IPCResultSections.Clear();
                            }

                            INPUT_ENABLE = true;
                            EQPTID_ENABLE = false;


                            _BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD.INDATAs.Add(new BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD.INDATA()
                            {
                                POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                            });

                            if (await _BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD.Execute())
                            {
                                IPC_STANDARDS.Clear();
                                foreach (var std in _BR_PHR_SEL_ProductionOrderTestSpecification_STANDARD.OUTDATAs)
                                {
                                    switch (std.TIID)
                                    {
                                        case "IPC-030":
                                            AVG_STD_HARDNESS = std.CSL != "" ? std.CSL : "N/A";
                                            MIN_STD_HARDNESS = std.LSL != "" ? std.LSL : "N/A";
                                            MAX_STD_HARDNESS = std.USL != "" ? std.USL : "N/A";
                                            break;
                                        case "IPC-027":
                                            AVG_STD_THICKNESS = std.CSL != "" ? std.CSL : "N/A";
                                            MIN_STD_THICKNESS = std.LSL != "" ? std.LSL : "N/A";
                                            MAX_STD_THICKNESS = std.USL != "" ? std.USL : "N/A";
                                            break;
                                        case "IPC-026":
                                            SD_STD_WEIGHT = std.CSL != "" ? std.CSL : "N/A";
                                            break;
                                        case "IPC-023":
                                            AVG_STD_WEIGHT = std.CSL != "" ? std.CSL : "N/A";
                                            MIN_STD_WEIGHT = std.LSL != "" ? std.LSL : "N/A";
                                            MAX_STD_WEIGHT = std.USL != "" ? std.USL : "N/A";
                                            break;
                                        default:
                                            break;

                                    }
                                }

                                IPC_STANDARDS.Add(new EACH_INDATA()
                                {
                                    RSLT_AVG_WEIGHT = AVG_STD_WEIGHT,
                                    RSLT_MIN_WEIGHT = MIN_STD_WEIGHT,
                                    RSLT_MAX_WEIGHT = MAX_STD_WEIGHT,
                                    RSLT_SD_WEIGHT = SD_STD_WEIGHT,
                                    RSLT_AVG_THICKNESS = AVG_STD_THICKNESS,
                                    RSLT_MIN_THICKNESS = MIN_STD_THICKNESS,
                                    RSLT_MAX_THICKNESS = MAX_STD_THICKNESS,
                                    RSLT_AVG_HARDNESS = AVG_STD_HARDNESS,
                                    RSLT_MIN_HARDNESS = MIN_STD_HARDNESS,
                                    RSLT_MAX_HARDNESS = MAX_STD_HARDNESS

                                });

                            }
                            else
                            {
                                //throw new Exception(string.Format("IPC 기준 정보가 없습니다."));
                            }
                            // 이전 기록 조회
                            if (_mainWnd.CurrentInstruction.Raw.ACTVAL == _mainWnd.TableTypeName && _mainWnd.CurrentInstruction.Raw.NOTE != null)
                            {
                                DataSet ds = new DataSet();
                                DataTable dt = new DataTable();
                                var bytearray = _mainWnd.CurrentInstruction.Raw.NOTE;
                                string xml = Encoding.UTF8.GetString(bytearray, 0, bytearray.Length);

                                ds.ReadXmlFromString(xml);
                                if (ds.Tables[0].TableName == "DATA")
                                {
                                    dt = ds.Tables[0];
                                    EQPTID = dt.Rows[0]["장비번호"].ToString();

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (i < dt.Rows.Count - 1)
                                        {
                                            _IPCResultSections.Add(new IPCResultSection.OUTDATA
                                            {
                                                STRTDTTM =  Convert.ToDateTime(dt.Rows[i]["점검일시"].ToString()),
                                                AVG_WEIGHT = dt.Rows[i]["평균질량"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["평균질량"]),
                                                MIN_WEIGHT = dt.Rows[i]["개별최소질량"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["개별최소질량"]),
                                                MAX_WEIGHT = dt.Rows[i]["개별최대질량"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["개별최대질량"]),
                                                SD_WEIGHT = dt.Rows[i]["개별질량RSD"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["개별질량RSD"]),
                                                AVG_THICKNESS = dt.Rows[i]["평균두께"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["평균두께"]),
                                                MIN_THICKNESS = dt.Rows[i]["최소두께"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["최소두께"]),
                                                MAX_THICKNESS = dt.Rows[i]["최대두께"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["최대두께"]),
                                                AVG_HARDNESS = dt.Rows[i]["평균경도"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["평균경도"]),
                                                MIN_HARDNESS = dt.Rows[i]["최소경도"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["최소경도"]),
                                                MAX_HARDNESS = dt.Rows[i]["최대경도"].Equals("") ? 0 : Convert.ToDecimal(dt.Rows[i]["최대경도"]),
                                                RowEditSec = "INS"
                                            });
                                        }
                                        //2024.05.24 김도연 : 화면에 평균을 띄우지 않음. 작업자가 평균 버튼을 눌러 확인할 수 있게 변경.(작업자의 실수를 줄이기 위함)
                                    }
                                }
                            }

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


        public ICommand InputEquipmentCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["InputEquipmentCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["InputEquipmentCommandAsync"] = false;
                            CommandCanExecutes["InputEquipmentCommandAsync"] = false;

                            // 설비 체크
                            _BR_PHR_SEL_CODE.INDATAs.Clear();
                            _BR_PHR_SEL_CODE.OUTDATAs.Clear();
                            if (String.IsNullOrWhiteSpace(EQPTID))
                            {
                                throw new Exception(string.Format("타정기 정보를 입력하십시오."));
                            }
                            else
                            {
                                _BR_PHR_SEL_CODE.INDATAs.Add(new BR_PHR_SEL_CODE.INDATA()
                                {
                                    TYPE = "Equipment",
                                    LANGID = "ko-KR",
                                    CODE = _EQPTID
                                });

                                await _BR_PHR_SEL_CODE.Execute();

                                if (_BR_PHR_SEL_CODE.OUTDATAs.Count > 0)
                                {
                                    EQPTID = _BR_PHR_SEL_CODE.OUTDATAs[0].CODE;
                                    EQPTNAME = _BR_PHR_SEL_CODE.OUTDATAs[0].NAME.Replace("[" + _EQPTID + "]", "");
                                    INPUT_ENABLE = false;
                                    EQPTID_ENABLE = true;
                                }
                                else
                                {
                                    throw new Exception(string.Format("타정기 정보가 없습니다."));
                                }

                                CommandResults["InputEquipmentCommandAsync"] = true;
                            }

                        }
                        catch (Exception ex)
                        {
                            CommandResults["InputEquipmentCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["InputEquipmentCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("InputEquipmentCommandAsync") ?
                        CommandCanExecutes["InputEquipmentCommandAsync"] : (CommandCanExecutes["InputEquipmentCommandAsync"] = true);
                });
            }
        }
        public ICommand AVGCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["AVGCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["AVGCommandAsync"] = false;
                            CommandCanExecutes["AVGCommandAsync"] = false;

                            if (String.IsNullOrWhiteSpace(EQPTID))
                            {
                                throw new Exception(string.Format("타정기 정보를 입력하십시오."));
                            }
                            else
                            {
                                AVG_DTTM = System.DateTime.Now;
                                IPC_RESULTS.Clear();
                                AVG_RESULT_WEIGHT = IPCResultSections[0].AVG_WEIGHT;
                                MIN_RESULT_WEIGHT = IPCResultSections[0].MIN_WEIGHT;
                                MAX_RESULT_WEIGHT = IPCResultSections[0].MAX_WEIGHT;
                                SD_RESULT_WEIGHT = IPCResultSections[0].SD_WEIGHT;
                                AVG_RESULT_THICKNESS = IPCResultSections[0].AVG_THICKNESS;
                                MIN_RESULT_THICKNESS = IPCResultSections[0].MIN_THICKNESS;
                                MAX_RESULT_THICKNESS = IPCResultSections[0].MAX_THICKNESS;
                                AVG_RESULT_HARDNESS = IPCResultSections[0].AVG_HARDNESS;
                                MIN_RESULT_HARDNESS = IPCResultSections[0].MIN_HARDNESS;
                                MAX_RESULT_HARDNESS = IPCResultSections[0].MAX_HARDNESS;

                                for (int i = 1; i < IPCResultSections.Count; i++)
                                {

                                    AVG_RESULT_WEIGHT += IPCResultSections[i].AVG_WEIGHT;
                                    MIN_RESULT_WEIGHT = Math.Min(MIN_RESULT_WEIGHT, IPCResultSections[i].MIN_WEIGHT);
                                    MAX_RESULT_WEIGHT = Math.Max(MAX_RESULT_WEIGHT, IPCResultSections[i].MAX_WEIGHT);
                                    SD_RESULT_WEIGHT += IPCResultSections[i].SD_WEIGHT;
                                    AVG_RESULT_THICKNESS += IPCResultSections[i].AVG_THICKNESS;
                                    MIN_RESULT_THICKNESS = Math.Min(MIN_RESULT_THICKNESS, IPCResultSections[i].MIN_THICKNESS);
                                    MAX_RESULT_THICKNESS = Math.Max(MAX_RESULT_THICKNESS, IPCResultSections[i].MAX_THICKNESS);
                                    AVG_RESULT_HARDNESS += IPCResultSections[i].AVG_HARDNESS;
                                    MIN_RESULT_HARDNESS = Math.Min(MIN_RESULT_HARDNESS, IPCResultSections[i].MIN_HARDNESS);
                                    MAX_RESULT_HARDNESS = Math.Max(MAX_RESULT_HARDNESS, IPCResultSections[i].MAX_HARDNESS);
                                }

                                int num = IPCResultSections.Count;
                                AVG_RESULT_WEIGHT = Math.Round((AVG_RESULT_WEIGHT / num), 2);
                                SD_RESULT_WEIGHT = Math.Round((SD_RESULT_WEIGHT / num), 2);
                                AVG_RESULT_THICKNESS = Math.Round((AVG_RESULT_THICKNESS / num), 2);
                                AVG_RESULT_HARDNESS = Math.Round((AVG_RESULT_HARDNESS / num), 2);

                                IPC_RESULTS.Add(new EACH_INDATA()
                                {
                                    RSLT_AVG_DTTM = AVG_DTTM.ToString("yyyy-MM-dd HH:mm"),
                                    RSLT_AVG_WEIGHT = AVG_RESULT_WEIGHT.ToString(),
                                    RSLT_MIN_WEIGHT = MIN_RESULT_WEIGHT.ToString(),
                                    RSLT_MAX_WEIGHT = MAX_RESULT_WEIGHT.ToString(),
                                    RSLT_SD_WEIGHT = SD_RESULT_WEIGHT.ToString(),
                                    RSLT_AVG_THICKNESS = AVG_RESULT_THICKNESS.ToString(),
                                    RSLT_MIN_THICKNESS = MIN_RESULT_THICKNESS.ToString(),
                                    RSLT_MAX_THICKNESS = MAX_RESULT_THICKNESS.ToString(),
                                    RSLT_AVG_HARDNESS = AVG_RESULT_HARDNESS.ToString(),
                                    RSLT_MIN_HARDNESS = MIN_RESULT_HARDNESS.ToString(),
                                    RSLT_MAX_HARDNESS = MAX_RESULT_HARDNESS.ToString()

                                });

                                CommandResults["AVGCommandAsync"] = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            CommandResults["AVGCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["AVGCommandAsync"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("AVGCommandAsync") ?
                        CommandCanExecutes["AVGCommandAsync"] : (CommandCanExecutes["AVGCommandAsync"] = true);
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

                            //2024.05.27 김도연 : 평균 정보가 없을 때 기록이 안되도록 수정
                            if (AVG_RESULT_WEIGHT == 0)
                            {
                                throw new Exception(string.Format("평균 정보가 없습니다."));
                            }
                            else
                            {
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
                                            false,
                                            "OM_ProductionOrder_SUI",
                                            "", _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID, null) == false)
                                    {
                                        throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                    }
                                }

                                // 전자서명 후 BR 실행
                                authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");
                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    string.Format("정제체크마스터입력"),
                                    string.Format("정제체크마스터입력"),
                                    false,
                                    "OM_ProductionOrder_SUI",
                                    _mainWnd.CurrentOrderInfo.EquipmentID, _mainWnd.CurrentOrderInfo.RecipeID, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }


                                // BR_BRS_REG_IPC_CHECKMASTER_MULTI IPC 결과 테이블에 저장
                                _BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit.INDATAs.Clear();

                                _BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit.INDATAs.Add(new BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit.INDATA
                                {
                                    EQPTID = EQPTID,
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    SMPQTY = 1,
                                    USERID = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_SUI"),
                                    STRTDTTM = AVG_DTTM,
                                    LOCATIONID = AuthRepositoryViewModel.Instance.RoomID,
                                    AVG_WEIGHT = AVG_RESULT_WEIGHT != 0 ? AVG_RESULT_WEIGHT.ToString() : "",
                                    MIN_WEIGHT = MIN_RESULT_WEIGHT != 0 ? MIN_RESULT_WEIGHT.ToString() : "",
                                    MAX_WEIGHT = MAX_RESULT_WEIGHT != 0 ? MAX_RESULT_WEIGHT.ToString() : "",
                                    SD_WEIGHT = SD_RESULT_WEIGHT != 0 ? SD_RESULT_WEIGHT.ToString() : "",
                                    AVG_THICKNESS = AVG_RESULT_THICKNESS != 0 ? AVG_RESULT_THICKNESS.ToString() : "",
                                    MIN_THICKNESS = MIN_RESULT_THICKNESS != 0 ? MIN_RESULT_THICKNESS.ToString() : "",
                                    MAX_THICKNESS = MAX_RESULT_THICKNESS != 0 ? MAX_RESULT_THICKNESS.ToString() : "",
                                    AVG_HARDNESS = AVG_RESULT_HARDNESS != 0 ? AVG_RESULT_HARDNESS.ToString() : "",
                                    MIN_HARDNESS = MIN_RESULT_HARDNESS != 0 ? MIN_RESULT_HARDNESS.ToString() : "",
                                    MAX_HARDNESS = MAX_RESULT_HARDNESS != 0 ? MAX_RESULT_HARDNESS.ToString() : ""
                                });


                                if (await _BR_BRS_REG_IPC_CHECKMASTER_MULTI_Edit.Execute())
                                {
                                    DataSet ds = new DataSet();
                                    DataTable dt = new DataTable("DATA");
                                    DataTable dtEbr = new DataTable("DATA2");
                                    ds.Tables.Add(dt);
                                    ds.Tables.Add(dtEbr);

                                    dt.Columns.Add(new DataColumn("장비번호"));
                                    dt.Columns.Add(new DataColumn("점검일시"));
                                    dt.Columns.Add(new DataColumn("평균질량"));
                                    dt.Columns.Add(new DataColumn("개별최소질량"));
                                    dt.Columns.Add(new DataColumn("개별최대질량"));
                                    dt.Columns.Add(new DataColumn("개별질량RSD"));
                                    dt.Columns.Add(new DataColumn("평균두께"));
                                    dt.Columns.Add(new DataColumn("최소두께"));
                                    dt.Columns.Add(new DataColumn("최대두께"));
                                    dt.Columns.Add(new DataColumn("평균경도"));
                                    dt.Columns.Add(new DataColumn("최소경도"));
                                    dt.Columns.Add(new DataColumn("최대경도"));

                                    //2024.05.27 김도연 : EBR에 평균값을 기록하기 위해 DataTable 따로 생성
                                    dtEbr.Columns.Add(new DataColumn("장비번호"));
                                    dtEbr.Columns.Add(new DataColumn("점검일시"));
                                    dtEbr.Columns.Add(new DataColumn("평균질량"));
                                    dtEbr.Columns.Add(new DataColumn("개별최소질량"));
                                    dtEbr.Columns.Add(new DataColumn("개별최대질량"));
                                    dtEbr.Columns.Add(new DataColumn("개별질량RSD"));
                                    dtEbr.Columns.Add(new DataColumn("평균두께"));
                                    dtEbr.Columns.Add(new DataColumn("최소두께"));
                                    dtEbr.Columns.Add(new DataColumn("최대두께"));
                                    dtEbr.Columns.Add(new DataColumn("평균경도"));
                                    dtEbr.Columns.Add(new DataColumn("최소경도"));
                                    dtEbr.Columns.Add(new DataColumn("최대경도"));

                                    var row = dt.NewRow();
                                    foreach (var rowdata in IPCResultSections)
                                    {
                                        row = dt.NewRow();
                                        row["장비번호"] = EQPTID;
                                        row["점검일시"] = rowdata.STRTDTTM != null ? rowdata.STRTDTTM.ToString("yyyy-MM-dd HH:mm") : "";
                                        row["평균질량"] = rowdata.AVG_WEIGHT != 0 ? rowdata.AVG_WEIGHT.ToString() : "";
                                        row["개별최소질량"] = rowdata.MIN_WEIGHT != 0 ? rowdata.MIN_WEIGHT.ToString() : "";
                                        row["개별최대질량"] = rowdata.MAX_WEIGHT != 0 ? rowdata.MAX_WEIGHT.ToString() : "";
                                        row["개별질량RSD"] = rowdata.SD_WEIGHT != 0 ? rowdata.SD_WEIGHT.ToString() : "";
                                        row["평균두께"] = rowdata.AVG_THICKNESS != 0 ? rowdata.AVG_THICKNESS.ToString() : "";
                                        row["최소두께"] = rowdata.MIN_THICKNESS != 0 ? rowdata.MIN_THICKNESS.ToString() : "";
                                        row["최대두께"] = rowdata.MAX_THICKNESS != 0 ? rowdata.MAX_THICKNESS.ToString() : "";
                                        row["평균경도"] = rowdata.AVG_HARDNESS != 0 ? rowdata.AVG_HARDNESS.ToString() : "";
                                        row["최소경도"] = rowdata.MIN_HARDNESS != 0 ? rowdata.MIN_HARDNESS.ToString() : "";
                                        row["최대경도"] = rowdata.MAX_HARDNESS != 0 ? rowdata.MAX_HARDNESS.ToString() : "";

                                        dt.Rows.Add(row);
                                    }

                                    //2024.05.24 김도연 : 지시문에 평균값 도출
                                    row = dt.NewRow();
                                    row["장비번호"] = "결과";
                                    row["점검일시"] = AVG_DTTM != null ? AVG_DTTM.ToString("yyyy-MM-dd HH:mm") : ""; ;
                                    row["평균질량"] = AVG_RESULT_WEIGHT != 0 ? AVG_RESULT_WEIGHT.ToString() : "";
                                    row["개별최소질량"] = MIN_RESULT_WEIGHT != 0 ? MIN_RESULT_WEIGHT.ToString() : "";
                                    row["개별최대질량"] = MAX_RESULT_WEIGHT != 0 ? MAX_RESULT_WEIGHT.ToString() : "";
                                    row["개별질량RSD"] = SD_RESULT_WEIGHT != 0 ? SD_RESULT_WEIGHT.ToString() : "";
                                    row["평균두께"] = AVG_RESULT_THICKNESS != 0 ? AVG_RESULT_THICKNESS.ToString() : "";
                                    row["최소두께"] = MIN_RESULT_THICKNESS != 0 ? MIN_RESULT_THICKNESS.ToString() : "";
                                    row["최대두께"] = MAX_RESULT_THICKNESS != 0 ? MAX_RESULT_THICKNESS.ToString() : "";
                                    row["평균경도"] = AVG_RESULT_HARDNESS != 0 ? AVG_RESULT_HARDNESS.ToString() : "";
                                    row["최소경도"] = MIN_RESULT_HARDNESS != 0 ? MIN_RESULT_HARDNESS.ToString() : "";
                                    row["최대경도"] = MAX_RESULT_HARDNESS != 0 ? MAX_RESULT_HARDNESS.ToString() : "";
                                    dt.Rows.Add(row);

                                    //2024.05.24 김도연 : EBR에 평균값 보이도록 데이터 input
                                    var rowEbr = dtEbr.NewRow();
                                    rowEbr["장비번호"] = EQPTID;
                                    rowEbr["점검일시"] = AVG_DTTM != null ? AVG_DTTM.ToString("yyyy-MM-dd HH:mm") : ""; ;
                                    rowEbr["평균질량"] = AVG_RESULT_WEIGHT != 0 ? AVG_RESULT_WEIGHT.ToString() : "";
                                    rowEbr["개별최소질량"] = MIN_RESULT_WEIGHT != 0 ? MIN_RESULT_WEIGHT.ToString() : "";
                                    rowEbr["개별최대질량"] = MAX_RESULT_WEIGHT != 0 ? MAX_RESULT_WEIGHT.ToString() : "";
                                    rowEbr["개별질량RSD"] = SD_RESULT_WEIGHT != 0 ? SD_RESULT_WEIGHT.ToString() : "";
                                    rowEbr["평균두께"] = AVG_RESULT_THICKNESS != 0 ? AVG_RESULT_THICKNESS.ToString() : "";
                                    rowEbr["최소두께"] = MIN_RESULT_THICKNESS != 0 ? MIN_RESULT_THICKNESS.ToString() : "";
                                    rowEbr["최대두께"] = MAX_RESULT_THICKNESS != 0 ? MAX_RESULT_THICKNESS.ToString() : "";
                                    rowEbr["평균경도"] = AVG_RESULT_HARDNESS != 0 ? AVG_RESULT_HARDNESS.ToString() : "";
                                    rowEbr["최소경도"] = MIN_RESULT_HARDNESS != 0 ? MIN_RESULT_HARDNESS.ToString() : "";
                                    rowEbr["최대경도"] = MAX_RESULT_HARDNESS != 0 ? MAX_RESULT_HARDNESS.ToString() : "";
                                    dtEbr.Rows.Add(rowEbr);

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
                                }
                                IsBusy = false;
                                ///

                                CommandResults["ConfirmCommandAsync"] = true;
                            }

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
        #region User Define
        public class IPCResultSection : BizActorRuleBase
        {
            public sealed partial class OUTDATACollection : BufferedObservableCollection<OUTDATA>
            {
            }
            private OUTDATACollection _OUTDATAs;
            [BizActorOutputSetAttribute()]
            public OUTDATACollection OUTDATAs
            {
                get
                {
                    return this._OUTDATAs;
                }
            }
            [BizActorOutputSetDefineAttribute(Order = "0")]
            [CustomValidation(typeof(ViewModelBase), "ValidateRow")]
            public partial class OUTDATA : BizActorDataSetBase
            {
                public OUTDATA()
                {
                    RowLoadedFlag = false;
                }
                private bool _RowLoadedFlag;
                public bool RowLoadedFlag
                {
                    get
                    {
                        return this._RowLoadedFlag;
                    }
                    set
                    {
                        this._RowLoadedFlag = value;
                        this.OnPropertyChanged("_RowLoadedFlag");
                    }
                }
                private string _RowIndex;
                public string RowIndex
                {
                    get
                    {
                        return this._RowIndex;
                    }
                    set
                    {
                        this._RowIndex = value;
                        this.OnPropertyChanged("RowIndex");
                    }
                }
                private string _RowEditSec;
                public string RowEditSec
                {
                    get
                    {
                        return this._RowEditSec;
                    }
                    set
                    {
                        this._RowEditSec = value;
                        this.OnPropertyChanged("RowEditSec");
                    }
                }
                private DateTime _STRTDTTM = DateTime.Now;
                [BizActorOutputItemAttribute()]
                public DateTime STRTDTTM
                {
                    get
                    {
                        return this._STRTDTTM;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._STRTDTTM = value;
                            this.CheckIsOriginal("STRTDTTM", value);
                            this.OnPropertyChanged("STRTDTTM");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _AVG_WEIGHT;
                [BizActorOutputItemAttribute()]
                public decimal AVG_WEIGHT
                {
                    get
                    {
                        return this._AVG_WEIGHT;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._AVG_WEIGHT = value;
                            this.CheckIsOriginal("AVG_WEIGHT", value);
                            this.OnPropertyChanged("AVG_WEIGHT");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _MIN_WEIGHT;
                [BizActorOutputItemAttribute()]
                public decimal MIN_WEIGHT
                {
                    get
                    {
                        return this._MIN_WEIGHT;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MIN_WEIGHT = value;
                            this.CheckIsOriginal("MIN_WEIGHT", value);
                            this.OnPropertyChanged("MIN_WEIGHT");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _MAX_WEIGHT;
                [BizActorOutputItemAttribute()]
                public decimal MAX_WEIGHT
                {
                    get
                    {
                        return this._MAX_WEIGHT;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MAX_WEIGHT = value;
                            this.CheckIsOriginal("MAX_WEIGHT", value);
                            this.OnPropertyChanged("MAX_WEIGHT");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _SD_WEIGHT;
                [BizActorOutputItemAttribute()]
                public decimal SD_WEIGHT
                {
                    get
                    {
                        return this._SD_WEIGHT;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._SD_WEIGHT = value;
                            this.CheckIsOriginal("SD_WEIGHT", value);
                            this.OnPropertyChanged("SD_WEIGHT");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _AVG_THICKNESS;
                [BizActorOutputItemAttribute()]
                public decimal AVG_THICKNESS
                {
                    get
                    {
                        return this._AVG_THICKNESS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._AVG_THICKNESS = value;
                            this.CheckIsOriginal("AVG_THICKNESS", value);
                            this.OnPropertyChanged("AVG_THICKNESS");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _MIN_THICKNESS;
                [BizActorOutputItemAttribute()]
                public decimal MIN_THICKNESS
                {
                    get
                    {
                        return this._MIN_THICKNESS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MIN_THICKNESS = value;
                            this.CheckIsOriginal("MIN_THICKNESS", value);
                            this.OnPropertyChanged("MIN_THICKNESS");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _MAX_THICKNESS;
                [BizActorOutputItemAttribute()]
                public decimal MAX_THICKNESS
                {
                    get
                    {
                        return this._MAX_THICKNESS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MAX_THICKNESS = value;
                            this.CheckIsOriginal("MAX_THICKNESS", value);
                            this.OnPropertyChanged("MAX_THICKNESS");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }

                private decimal _AVG_HARDNESS;
                [BizActorOutputItemAttribute()]
                public decimal AVG_HARDNESS
                {
                    get
                    {
                        return this._AVG_HARDNESS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._AVG_HARDNESS = value;
                            this.CheckIsOriginal("AVG_HARDNESS", value);
                            this.OnPropertyChanged("AVG_HARDNESS");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }

                private decimal _MIN_HARDNESS;
                [BizActorOutputItemAttribute()]
                public decimal MIN_HARDNESS
                {
                    get
                    {
                        return this._MIN_HARDNESS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MIN_HARDNESS = value;
                            this.CheckIsOriginal("MIN_HARDNESS", value);
                            this.OnPropertyChanged("MIN_HARDNESS");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
                private decimal _MAX_HARDNESS;
                [BizActorOutputItemAttribute()]
                public decimal MAX_HARDNESS
                {
                    get
                    {
                        return this._MAX_HARDNESS;
                    }
                    set
                    {
                        if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                        {
                        }
                        else
                        {
                            this._MAX_HARDNESS = value;
                            this.CheckIsOriginal("MAX_HARDNESS", value);
                            this.OnPropertyChanged("MAX_HARDNESS");
                            if (RowLoadedFlag)
                            {
                                if (this.CheckIsOriginalRow())
                                {
                                    RowEditSec = "SEL";
                                }
                                else
                                {
                                    RowEditSec = "UPD";
                                }
                            }
                        }
                    }
                }
            }
            public IPCResultSection()
            {
                _OUTDATAs = new OUTDATACollection();
            }
        }

        public class EACH_INDATA : BizActorDataSetBase
        {

            private String _RSLT_AVG_WEIGHT;
            public String RSLT_AVG_WEIGHT
            {
                get { return _RSLT_AVG_WEIGHT; }
                set
                {
                    _RSLT_AVG_WEIGHT = value;
                    OnPropertyChanged("RSLT_AVG_WEIGHT");
                }
            }
            private String _RSLT_MIN_WEIGHT;
            public String RSLT_MIN_WEIGHT
            {
                get { return _RSLT_MIN_WEIGHT; }
                set
                {
                    _RSLT_MIN_WEIGHT = value;
                    OnPropertyChanged("RSLT_MIN_WEIGHT");
                }
            }
            private String _RSLT_MAX_WEIGHT;
            public String RSLT_MAX_WEIGHT
            {
                get { return _RSLT_MAX_WEIGHT; }
                set
                {
                    _RSLT_MAX_WEIGHT = value;
                    OnPropertyChanged("RSLT_MAX_WEIGHT");
                }
            }
            private String _RSLT_SD_WEIGHT;
            public String RSLT_SD_WEIGHT
            {
                get { return _RSLT_SD_WEIGHT; }
                set
                {
                    _RSLT_SD_WEIGHT = value;
                    OnPropertyChanged("RSLT_SD_WEIGHT");
                }
            }
            private String _RSLT_AVG_THICKNESS;
            public String RSLT_AVG_THICKNESS
            {
                get { return _RSLT_AVG_THICKNESS; }
                set
                {
                    _RSLT_AVG_THICKNESS = value;
                    OnPropertyChanged("RSLT_AVG_THICKNESS");
                }
            }
            private String _RSLT_MIN_THICKNESS;
            public String RSLT_MIN_THICKNESS
            {
                get { return _RSLT_MIN_THICKNESS; }
                set
                {
                    _RSLT_MIN_THICKNESS = value;
                    OnPropertyChanged("RSLT_MIN_THICKNESS");
                }

            }
            private String _RSLT_MAX_THICKNESS;
            public String RSLT_MAX_THICKNESS
            {
                get { return _RSLT_MAX_THICKNESS; }
                set
                {
                    _RSLT_MAX_THICKNESS = value;
                    OnPropertyChanged("RSLT_MAX_THICKNESS");
                }

            }
            private String _RSLT_AVG_HARDNESS;
            public String RSLT_AVG_HARDNESS
            {
                get { return _RSLT_AVG_HARDNESS; }
                set
                {
                    _RSLT_AVG_HARDNESS = value;
                    OnPropertyChanged("RSLT_AVG_HARDNESS");
                }

            }
            private String _RSLT_MIN_HARDNESS;
            public String RSLT_MIN_HARDNESS
            {
                get { return _RSLT_MIN_HARDNESS; }
                set
                {
                    _RSLT_MIN_HARDNESS = value;
                    OnPropertyChanged("RSLT_MIN_HARDNESS");
                }

            }
            private String _RSLT_MAX_HARDNESS;
            public String RSLT_MAX_HARDNESS
            {
                get { return _RSLT_MAX_HARDNESS; }
                set
                {
                    _RSLT_MAX_HARDNESS = value;
                    OnPropertyChanged("RSLT_MAX_HARDNESS");
                }
            }
            private String _RSLT_AVG_DTTM;
            public String RSLT_AVG_DTTM
            {
                get { return _RSLT_AVG_DTTM; }
                set
                {
                    _RSLT_AVG_DTTM = value;
                    OnPropertyChanged("RSLT_AVG_DTTM");
                }
            }
        }
        #endregion
    }
}
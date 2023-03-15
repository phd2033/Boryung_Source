using LGCNS.iPharmMES.Common;
using Order;
using System;
using System.Windows.Input;
using System.Linq;
using ShopFloorUI;
using System.Text;
using C1.Silverlight.Data;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using C1.Silverlight.Imaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using LGCNS.iPharmMES.Recipe.Common;
using System.Threading.Tasks;

namespace 보령
{
    public class SVP수동검사불량유형입력ViewModel : ViewModelBase
    {
        #region Properties
        SVP수동검사불량유형입력 _mainWnd;

        private string _INSUSER;
        public string INSUSER
        {
            get { return _INSUSER; }
            set
            {
                _INSUSER = value;
                OnPropertyChanged("INSUSER");
            }
        }
        private string _Date;
        public string Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                OnPropertyChanged("Date");
            }
        }
        private decimal _InspectCnt;
        public decimal InspectCnt
        {
            get { return _InspectCnt; }
            set
            {
                _InspectCnt = value;
                OnPropertyChanged("InspectCnt");
            }
        }
        private decimal _RejectQTY;
        public decimal RejectQTY
        {
            get { return _RejectQTY; }
            set
            {
                _RejectQTY = value;
                OnPropertyChanged("RejectQTY");
            }
        }
        private decimal _Reject_No1;
        public decimal Reject_No1
        {
            get { return _Reject_No1; }
            set
            {
                _Reject_No1 = value;
                OnPropertyChanged("Reject_No1");
            }
        }
        private decimal _Reject_No2;
        public decimal Reject_No2
        {
            get { return _Reject_No2; }
            set
            {
                _Reject_No2 = value;
                OnPropertyChanged("Reject_No2");
            }
        }
        private decimal _Reject_No3;
        public decimal Reject_No3
        {
            get { return _Reject_No3; }
            set
            {
                _Reject_No3 = value;
                OnPropertyChanged("Reject_No3");
            }
        }
        private decimal _Reject_No4;
        public decimal Reject_No4
        {
            get { return _Reject_No4; }
            set
            {
                _Reject_No4 = value;
                OnPropertyChanged("Reject_No4");
            }
        }
        private decimal _Reject_No5;
        public decimal Reject_No5
        {
            get { return _Reject_No5; }
            set
            {
                _Reject_No5 = value;
                OnPropertyChanged("Reject_No5");
            }
        }
        private decimal _Reject_No6;
        public decimal Reject_No6
        {
            get { return _Reject_No6; }
            set
            {
                _Reject_No6 = value;
                OnPropertyChanged("Reject_No6");
            }
        }
        private decimal _Reject_No7;
        public decimal Reject_No7
        {
            get { return _Reject_No7; }
            set
            {
                _Reject_No7 = value;
                OnPropertyChanged("Reject_No7");
            }
        }
        private decimal _Reject_No8;
        public decimal Reject_No8
        {
            get { return _Reject_No8; }
            set
            {
                _Reject_No8 = value;
                OnPropertyChanged("Reject_No8");
            }
        }
        private decimal _Reject_No9;
        public decimal Reject_No9
        {
            get { return _Reject_No9; }
            set
            {
                _Reject_No9 = value;
                OnPropertyChanged("Reject_No9");
            }
        }
        private decimal _Reject_No10;
        public decimal Reject_No10
        {
            get { return _Reject_No10; }
            set
            {
                _Reject_No10 = value;
                OnPropertyChanged("Reject_No10");
            }
        }
        private decimal _Reject_No11;
        public decimal Reject_No11
        {
            get { return _Reject_No11; }
            set
            {
                _Reject_No11 = value;
                OnPropertyChanged("Reject_No11");
            }
        }
        private decimal _Reject_No12;
        public decimal Reject_No12
        {
            get { return _Reject_No12; }
            set
            {
                _Reject_No12 = value;
                OnPropertyChanged("Reject_No12");
            }
        }
        private decimal _Reject_No13;
        public decimal Reject_No13
        {
            get { return _Reject_No13; }
            set
            {
                _Reject_No13 = value;
                OnPropertyChanged("Reject_No13");
            }
        }
        private decimal _Reject_No14;
        public decimal Reject_No14
        {
            get { return _Reject_No14; }
            set
            {
                _Reject_No14 = value;
                OnPropertyChanged("Reject_No14");
            }
        }
        private decimal _Reject_No15;
        public decimal Reject_No15
        {
            get { return _Reject_No15; }
            set
            {
                _Reject_No15 = value;
                OnPropertyChanged("Reject_No15");
            }
        }
        private decimal _Reject_No16;
        public decimal Reject_No16
        {
            get { return _Reject_No16; }
            set
            {
                _Reject_No16 = value;
                OnPropertyChanged("Reject_No16");
            }
        }
        private decimal _Reject_No17;
        public decimal Reject_No17
        {
            get { return _Reject_No17; }
            set
            {
                _Reject_No17 = value;
                OnPropertyChanged("Reject_No17");
            }
        }
        private decimal _Reject_No18;
        public decimal Reject_No18
        {
            get { return _Reject_No18; }
            set
            {
                _Reject_No18 = value;
                OnPropertyChanged("Reject_No18");
            }
        }
        private decimal _Reject_No19;
        public decimal Reject_No19
        {
            get { return _Reject_No19; }
            set
            {
                _Reject_No19 = value;
                OnPropertyChanged("Reject_No19");
            }
        }
        private decimal _Reject_No20;
        public decimal Reject_No20
        {
            get { return _Reject_No20; }
            set
            {
                _Reject_No20 = value;
                OnPropertyChanged("Reject_No20");
            }
        }
        private decimal _Reject_No21;
        public decimal Reject_No21
        {
            get { return _Reject_No21; }
            set
            {
                _Reject_No21 = value;
                OnPropertyChanged("Reject_No21");
            }
        }

        private string _Comments;
        public string Comments
        {
            get { return _Comments; }
            set
            {
                _Comments = value;
                OnPropertyChanged("Comments");
            }
        }
        
        #endregion

        #region [Constructor]

        public SVP수동검사불량유형입력ViewModel()
        {
            _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO = new BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO();
            _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO = new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO();
        }

        #endregion

        #region [BizRule]
        private BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO;
        public BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO
        {
            get { return _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO; }
            set
            {
                _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO = value;
                OnPropertyChanged("BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO");
            }
        }

        private BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO;
        public BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO
        {
            get { return _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO; }
            set
            {
                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO = value;
                OnPropertyChanged("BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO");
            }
        }
        #endregion

        #region [Command]
        public ICommand LoadCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["LoadCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["LoadCommandAsync"] = false;
                            CommandCanExecutes["LoadCommandAsync"] = false;
                            IsBusy = true;

                            if (arg != null && arg is SVP수동검사불량유형입력)
                            {
                                _mainWnd = arg as SVP수동검사불량유형입력;

                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Clear();
                                _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Clear();

                                BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Add(new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATA
                                {
                                    RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                    ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                    IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                    IRTSEQ = _mainWnd.CurrentInstruction.Raw.IRTSEQ,
                                    POID = _mainWnd.CurrentOrder.OrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID
                                });

                                if (await _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.Execute() && _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Count == 0)
                                {
                                    INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID;
                                    Date = DateTime.Now.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    var outData = _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs[0];

                                    InspectCnt = Convert.ToDecimal(outData.INSPECTCNT);
                                    INSUSER = outData.INSUSER;
                                    Date = outData.INSPECTIONDATE;
                                    RejectQTY = Convert.ToDecimal(outData.RejectQTY);
                                    Reject_No1 = Convert.ToDecimal(outData.Reject_No1);
                                    Reject_No2 = Convert.ToDecimal(outData.Reject_No2);
                                    Reject_No3 = Convert.ToDecimal(outData.Reject_No3);
                                    Reject_No4 = Convert.ToDecimal(outData.Reject_No4);
                                    Reject_No5 = Convert.ToDecimal(outData.Reject_No5);
                                    Reject_No6 = Convert.ToDecimal(outData.Reject_No6);
                                    Reject_No7 = Convert.ToDecimal(outData.Reject_No7);
                                    Reject_No8 = Convert.ToDecimal(outData.Reject_No8);
                                    Reject_No9 = Convert.ToDecimal(outData.Reject_No9);
                                    Reject_No10 = Convert.ToDecimal(outData.Reject_No10);
                                    Reject_No11 = Convert.ToDecimal(outData.Reject_No11);
                                    Reject_No12 = Convert.ToDecimal(outData.Reject_No12);
                                    Reject_No13 = Convert.ToDecimal(outData.Reject_No13);
                                    Reject_No14 = Convert.ToDecimal(outData.Reject_No14);
                                    Reject_No15 = Convert.ToDecimal(outData.Reject_No15);
                                    Reject_No16 = Convert.ToDecimal(outData.Reject_No16);
                                    Reject_No17 = Convert.ToDecimal(outData.Reject_No17);
                                    Reject_No18 = Convert.ToDecimal(outData.Reject_No18);
                                    Reject_No19 = Convert.ToDecimal(outData.Reject_No19);
                                    Reject_No20 = Convert.ToDecimal(outData.Reject_No20);
                                    Reject_No21 = Convert.ToDecimal(outData.Reject_No21);
                                    Comments = outData.COMMENTS;
                                }
                                
                            }

                            //    DataSet ds = new DataSet();
                            //    DataTable dt = new DataTable();

                            //    var bytearray = _mainWnd.CurrentInstruction.Raw.NOTE;
                            //    string xml = Encoding.UTF8.GetString(bytearray, 0, bytearray.Length);
                            //    ds.ReadXmlFromString(xml);

                            //    if (ds.Tables.Count == 2 && ds.Tables[1].TableName == "DATA2")
                            //    {
                            //        dt = ds.Tables["DATA2"];

                            //        foreach (DataRow row in dt.Rows)
                            //        {
                            //            BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATAs.Add(new BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.INDATA
                            //            {
                            //                REJECTGUID = row["REJECTGUID"] != null ? row["REJECTGUID"].ToString() : ""
                            //            });
                            //        }

                            //        if (await _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.Execute() && _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs.Count == 0)
                            //        {
                            //            INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID;
                            //            Date = DateTime.Now.ToString("yyyy-MM-dd");
                            //        }
                            //        else
                            //        {
                            //            var outData = _BR_BRS_SEL_UDT_BRS_SVP_REJECT_INFO.OUTDATAs[0];

                            //            RejectGuid = outData.REJECTGUID;
                            //            INSUSER = outData.INSUSER;
                            //            Date = outData.INSPECTIONDATE;
                            //            RejectQTY = Convert.ToDecimal(outData.RejectQTY);
                            //            Reject_No1 = Convert.ToDecimal(outData.Reject_No1);
                            //            Reject_No2 = Convert.ToDecimal(outData.Reject_No2);
                            //            Reject_No3 = Convert.ToDecimal(outData.Reject_No3);
                            //            Reject_No4 = Convert.ToDecimal(outData.Reject_No4);
                            //            Reject_No5 = Convert.ToDecimal(outData.Reject_No5);
                            //            Reject_No6 = Convert.ToDecimal(outData.Reject_No6);
                            //            Reject_No7 = Convert.ToDecimal(outData.Reject_No7);
                            //            Reject_No8 = Convert.ToDecimal(outData.Reject_No8);
                            //            Reject_No9 = Convert.ToDecimal(outData.Reject_No9);
                            //            Reject_No10 = Convert.ToDecimal(outData.Reject_No10);
                            //            Reject_No11 = Convert.ToDecimal(outData.Reject_No11);
                            //            Reject_No12 = Convert.ToDecimal(outData.Reject_No12);
                            //            Reject_No13 = Convert.ToDecimal(outData.Reject_No13);
                            //            Reject_No14 = Convert.ToDecimal(outData.Reject_No14);
                            //            Reject_No15 = Convert.ToDecimal(outData.Reject_No15);
                            //            Reject_No16 = Convert.ToDecimal(outData.Reject_No16);
                            //            Reject_No17 = Convert.ToDecimal(outData.Reject_No17);
                            //            Reject_No18 = Convert.ToDecimal(outData.Reject_No18);
                            //            Reject_No19 = Convert.ToDecimal(outData.Reject_No19);
                            //            Reject_No20 = Convert.ToDecimal(outData.Reject_No20);
                            //            Reject_No21 = Convert.ToDecimal(outData.Reject_No21);
                            //            Comments = outData.COMMENTS;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID;
                            //        Date = DateTime.Now.ToString("yyyy-MM-dd");
                            //    }
                            //}

                            IsBusy = false;
                            ///

                            CommandResults["LoadCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            CommandResults["LoadCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["LoadCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadCommandAsync") ?
                        CommandCanExecutes["LoadCommandAsync"] : (CommandCanExecutes["LoadCommandAsync"] = true);
                });
            }
        }

        public ICommand NoRecordConfirmCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["ConfirmCommandAsync"].EnterAsync())
                    {
                        try
                        {
                            CommandResults["ConfirmCommandAsync"] = false;
                            CommandCanExecutes["ConfirmCommandAsync"] = false;
                            IsBusy = true;

                            
                            IsBusy = false;
                            ///

                            CommandResults["ConfirmCommandAsync"] = true;
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
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

        public ICommand SaveCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                using (await AwaitableLocks["SaveCommandAsync"].EnterAsync())
                {
                    try
                    {
                        CommandResults["SaveCommandAsync"] = false;
                        CommandCanExecutes["SaveCommandAsync"] = false;
                            
                        var authHelper = new iPharmAuthCommandHelper();                            
                        // 조회내용 기록
                        authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                        if (await authHelper.ClickAsync(
                            Common.enumCertificationType.Function,
                            Common.enumAccessType.Create,
                            "SVP수동검사불량유형입력",
                            "SVP수동검사불량유형입력",
                            false,
                            "OM_ProductionOrder_SUI",
                            "", null, null) == false)
                        {
                            throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                        }
                        
                            _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.INDATAs.Clear();
                            _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.INDATAs.Add(new BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.INDATA
                            {
                                RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                IRTSEQ = _mainWnd.CurrentInstruction.Raw.IRTSEQ,
                                POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                BATCHNO = _mainWnd.CurrentOrder.BatchNo,
                                SEQ = 1,    //2023.01.10 박희돈 HIST 테이블에 값이 없을경우 SEQ값을 1로한다. 값이 있다면 비즈룰에서 HIST테이블의 SEQ값을 조회함.
                                OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                INSPECTCNT = InspectCnt,
                                INSPECTIONDATE = Convert.ToString(Date.ToString()).Substring(0, 10),
                                RejectQTY = RejectQTY,
                                Reject_No1 = Reject_No1,
                                Reject_No2 = Reject_No2,
                                Reject_No3 = Reject_No3,
                                Reject_No4 = Reject_No4,
                                Reject_No5 = Reject_No5,
                                Reject_No6 = Reject_No6,
                                Reject_No7 = Reject_No7,
                                Reject_No8 = Reject_No8,
                                Reject_No9 = Reject_No9,
                                Reject_No10 = Reject_No10,
                                Reject_No11 = Reject_No11,
                                Reject_No12 = Reject_No12,
                                Reject_No13 = Reject_No13,
                                Reject_No14 = Reject_No14,
                                Reject_No15 = Reject_No15,
                                Reject_No16 = Reject_No16,
                                Reject_No17 = Reject_No17,
                                Reject_No18 = Reject_No18,
                                Reject_No19 = Reject_No19,
                                Reject_No20 = Reject_No20,
                                Reject_No21 = Reject_No21,
                                COMMENTS = Comments,
                                INSUSER = AuthRepositoryViewModel.Instance.LoginedUserID,
                                INSDTTM = Convert.ToString(await AuthRepositoryViewModel.GetDBDateTimeNow())
                            });

                            if (await _BR_BRS_REG_UDT_BRS_SVP_REJECT_INFO.Execute() == true)
                            {
                                Brush background = _mainWnd.PrintArea.Background;
                                _mainWnd.PrintArea.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD6, 0xD4, 0xD4));
                                _mainWnd.PrintArea.BorderThickness = new System.Windows.Thickness(1);
                                _mainWnd.PrintArea.Background = new SolidColorBrush(Colors.White);

                                _mainWnd.CurrentInstruction.Raw.ACTVAL = _mainWnd.TableTypeName;
                                _mainWnd.CurrentInstruction.Raw.NOTE = imageToByteArray();

                                var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, false, false, true);
                                if (result != enumInstructionRegistErrorType.Ok)
                                {
                                    throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                                }

                                if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                                else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                                CommandResults["SaveCommandAsync"] = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            CommandResults["SaveCommandAsync"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["SaveCommandAsync"] = true;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("SaveCommandAsync") ?
                        CommandCanExecutes["SaveCommandAsync"] : (CommandCanExecutes["SaveCommandAsync"] = true);
                });
            }
        }
        #endregion

        #region User Define
        public byte[] imageToByteArray()
        {
            try
            {

                C1Bitmap bitmap = new C1Bitmap(new WriteableBitmap(_mainWnd.PrintArea, null));
                System.IO.Stream stream = bitmap.GetStream(ImageFormat.Png, true);

                int len = (int)stream.Seek(0, SeekOrigin.End);

                byte[] datas = new byte[len];

                stream.Seek(0, SeekOrigin.Begin);

                stream.Read(datas, 0, datas.Length);

                return datas;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}

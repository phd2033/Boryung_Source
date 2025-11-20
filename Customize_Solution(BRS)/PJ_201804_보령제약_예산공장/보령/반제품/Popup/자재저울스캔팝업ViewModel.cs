using LGCNS.iPharmMES.Common;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using ShopFloorUI;
using LGCNS.EZMES.Common;

namespace 보령
{
    public class 자재저울스캔팝업ViewModel : ViewModelBase
    {

        enum enumScanType
        {
            Material,
            Scale
        };

        enum enumCheckStep
        {
            WaitBarcode,
            ComponentChecked,
            End
            
        };

        enumCheckStep _checkStep;
        private enumCheckStep CheckStep
        {
            get { return _checkStep; }
            set
            {
                _checkStep = value;

                switch (value)
                {
                    case enumCheckStep.WaitBarcode:
                        IsBarcodeReaderVisible = Visibility.Visible;
                        if (_ScanType.ToString() == enumScanType.Material.ToString())
                        {
                            Message = "자재 바코드를 스캔하세요";
                        }
                        else if (_ScanType.ToString() == enumScanType.Scale.ToString())
                        {
                            Message = "저울 바코드를 스캔하세요";
                        }
                        break;
                    //case enumCheckStep.ComponentChecked:
                    //    if (_ScanType.ToString() == enumScanType.Material.ToString())
                    //    {
                    //        Message = string.Format("확인 완료. 다음 자재 바코드를 스캔하세요");
                    //    }
                    //    break;
                    default:
                        break;
                }
            }
        }

        enumScanType _ScanType;
        private enumScanType ScanType
        {
            get { return _ScanType; }
            set
            {
                _ScanType = value;
                NotifyPropertyChanged();
            }
        }


        string _barcode;
        public string Barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;
                NotifyPropertyChanged();
            }
        }

        string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        Visibility _isBarcodeReaderVisible;
        public Visibility IsBarcodeReaderVisible
        {
            get { return _isBarcodeReaderVisible; }
            set
            {
                _isBarcodeReaderVisible = value;
                NotifyPropertyChanged();
            }
        }

        용매확인및투입ViewModel _parentVM;
        public 용매확인및투입ViewModel ParentVM
        {
            get { return _parentVM; }
            set { _parentVM = value; }
        }

        정제수확인및투입ViewModel _parentVM2;
        public 정제수확인및투입ViewModel ParentVM2
        {
            get { return _parentVM2; }
            set { _parentVM2 = value; }
        }
        SVP용매확인및투입ViewModel _parentVM3;
        public SVP용매확인및투입ViewModel ParentVM3
        {
            get { return _parentVM3; }
            set { _parentVM3 = value; }
        }

        iPharmMESChildWindow _popup;
        public iPharmMESChildWindow Popup
        {
            get { return _popup; }
            set { _popup = value; }
        }

      
        async Task<bool> IsMatchedComponent(string barcode)
        {

            if (ScanType.Equals(enumScanType.Material))
            {
                if (ParentVM != null)
                {
                    if (ParentVM.BR_BRS_SEL_Charging_Solvent.OUTDATAs.Count > 0)
                    {

                        var item = ParentVM.FilteredComponents.Where(o => o.MSUBLOTBCD == Barcode).LastOrDefault();
                        var CheckItem = ParentVM.BR_BRS_SEL_ProductionOrderBOM_CheckDuplicate.OUTDATAs;

                        if (item != null)
                        {
                            if(CheckItem.Count() > 0)
                            {   //2025.04.21 김도연 동일 BOM의 동일 팩이 사용되었을 때, 팩이 소분된 경우
                                if (CheckItem[0].CHECKDSP == "Y")
                                {
                                    if (CheckItem[0].MSUBLOTID != item.MSUBLOTID) //2025.04.21 김도연 바코드 찍은 팩과 중복 팩이 같지 않을 경우
                                    {
                                        var CheckDsp = ParentVM.FilteredComponents.Where(o => o.MSUBLOTID == CheckItem[0].MSUBLOTID).LastOrDefault(); //중복 팩이 소분되었는지 확인

                                        if (CheckDsp.CHECK == "투입완료")
                                        {
                                            ParentVM.curSeletedItem = item;
                                            item.CHECK = item.CHECK == "투입대기" ? "투입가능" : item.CHECK;
                                            CheckStep = enumCheckStep.End;
                                        }
                                        else
                                        {
                                            Message = "원료 바코드 : " + CheckItem[0].MSUBLOTBCD + "\n소분된 원료부터 투입 부탁드립니다.";
                                            return false;
                                        }
                                    }
                                    else //바코드 찍은 팩과 중복 팩이 동일한 경우
                                    {
                                        ParentVM.curSeletedItem = item;
                                        item.CHECK = item.CHECK == "투입대기" ? "투입가능" : item.CHECK;
                                        CheckStep = enumCheckStep.End;
                                    }
                                }//동일 BOM의 동일 팩이 사용되었을 때, 팩이 소분되지 않은 경우
                                else
                                {
                                    if (CheckItem[0].MSUBLOTID.Equals(item.MSUBLOTID))
                                    {
                                        if (ParentVM.FilteredComponents.Where(o => (o.CHECK == "투입대기" || o.CHECK == "투입가능") && o.MSUBLOTID != CheckItem[0].MSUBLOTID).Count() > 0)//중복 팩을 제외한 팩이 전부 투입 완료 되었는지 확인
                                        {
                                            Message = "원료 바코드 : " + CheckItem[0].MSUBLOTBCD + "\n해당 팩은 마지막으로 투입 부탁드립니다.";
                                            return false;
                                        }else
                                        {
                                            ParentVM.curSeletedItem = item;
                                            item.CHECK = item.CHECK == "투입대기" ? "투입가능" : item.CHECK;
                                            CheckStep = enumCheckStep.End;
                                        }
                                    }
                                    else
                                    {
                                        ParentVM.curSeletedItem = item;
                                        item.CHECK = item.CHECK == "투입대기" ? "투입가능" : item.CHECK;
                                        CheckStep = enumCheckStep.End;
                                    }
                                }
                            }else
                            {
                                ParentVM.curSeletedItem = item;
                                item.CHECK = item.CHECK == "투입대기" ? "투입가능" : item.CHECK;
                                CheckStep = enumCheckStep.End;
                            }
                        }
                        else
                        {
                            Message = "원료 정보가 일치하지 않습니다.";
                            return false;
                        }
                    }
                    else
                    {
                        Message = "투입 대상 원료가 존재하지 않습니다.";
                        return false;
                    }
                }
                if (ParentVM3 != null)
                {
                    if (ParentVM3.BR_BRS_SEL_Charging_Solvent_to_Dispense.OUTDATAs.Count > 0)
                    {

                        var item = ParentVM3.FilteredComponents.Where(o => o.MSUBLOTBCD == Barcode).FirstOrDefault();

                        if (item != null)
                        {
                            ParentVM3.curSeletedItem = item;
                            item.CHECK = item.CHECK == "투입대기" ? "투입가능" : item.CHECK;
                            CheckStep = enumCheckStep.End;
                        }
                        else
                        {
                            Message = "원료 정보가 일치하지 않습니다.";
                            return false;
                        }
                    }
                    else
                    {
                        Message = "투입 대상 원료가 존재하지 않습니다.";
                        return false;
                    }
                }
            }
            else if (ScanType.Equals(enumScanType.Scale))
            {

                var bizRule = new BR_BRS_CHK_Equipment_Is_Scale();
                bizRule.INDATAs.Add(new BR_BRS_CHK_Equipment_Is_Scale.INDATA()
                {
                    EQPTID = barcode
                });

                if (await bizRule.Execute() == false) return false;

                if (ParentVM != null)
                    ParentVM.ScaleId = barcode;
                else if (ParentVM2 != null)
                    ParentVM2.ScaleId = barcode;
                else if (ParentVM3 != null)
                    ParentVM3.ScaleId = barcode;

                CheckStep = enumCheckStep.End;
            }
            ///

            return true;
        }

        public ICommand LoadedCommand
        {
            get
            {
                return new CommandBase(arg =>
                {
                    try
                    {
                        IsBusy = true;

                        CommandResults["LoadedCommand"] = false;
                        CommandCanExecutes["LoadedCommand"] = false;

                        CheckStep = enumCheckStep.WaitBarcode;
                        ///

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
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("LoadedCommand") ?
                        CommandCanExecutes["LoadedCommand"] : (CommandCanExecutes["LoadedCommand"] = true);
                });
            }
        }

        //바코드 입력
        public ICommand BarcodeChangedCommandAsync
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    using (await AwaitableLocks["BarcodeChangedCommand"].EnterAsync())
                    {
                        try
                        {
                            IsBusy = true;

                            CommandResults["BarcodeChangedCommand"] = false;
                            CommandCanExecutes["BarcodeChangedCommand"] = false;

                            ///
                            if (await IsMatchedComponent(Barcode.ToUpper()) == true)
                            {
                                if (ScanType.Equals(enumScanType.Scale))
                                {
                                    //var bizRule = new BR_PHR_SEL_CurrentWeight();
                                    //bizRule.INDATAs.Add(new BR_PHR_SEL_CurrentWeight.INDATA()
                                    //{
                                    //    ScaleID = ParentVM != null ? ParentVM.ScaleId : (ParentVM2 != null ? ParentVM2.ScaleId : "")
                                    //});

                                    //await bizRule.Execute(exceptionHandle: LGCNS.iPharmMES.Common.Common.enumBizRuleXceptionHandleType.FailEvent);

                                    //if (bizRule.OUTDATAs.Count > 0)
                                    //{
                                    //    if (ParentVM != null)
                                    //    {
                                    //        ParentVM.ScaleUOM = bizRule.OUTDATAs[0].UOM.ToString();
                                    //        ParentVM.ScaleValue = string.Format("{0}{1}", decimal.Parse(bizRule.OUTDATAs[0].Weight.ToString()).ToString("0.##0"), bizRule.OUTDATAs[0].UOM);
                                    //    }
                                    //    else if (ParentVM2 != null)
                                    //    {
                                    //        ParentVM2.ScaleUOM = bizRule.OUTDATAs[0].UOM.ToString();
                                    //        ParentVM2.ScaleValue = string.Format("{0}{1}", decimal.Parse(bizRule.OUTDATAs[0].Weight.ToString()).ToString("0.##0"), bizRule.OUTDATAs[0].UOM);
                                    //    }

                                    //}
                                    CheckStep = enumCheckStep.End;
                                }

                                if (CheckStep == enumCheckStep.End)
                                    (arg as 자재저울스캔팝업).DialogResult = true;
 
                            };

           
                            ///

                            CommandResults["BarcodeChangedCommand"] = true;
                        }
                        catch (Exception ex)
                        {
                            CommandResults["BarcodeChangedCommand"] = false;
                            OnException(ex.Message, ex);
                        }
                        finally
                        {
                            CommandCanExecutes["BarcodeChangedCommand"] = true;

                            IsBusy = false;
                        }
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("BarcodeChangedCommand") ?
                        CommandCanExecutes["BarcodeChangedCommand"] : (CommandCanExecutes["BarcodeChangedCommand"] = true);
                });
            }
        }

      

        public 자재저울스캔팝업ViewModel(Enum strScanType)
        {

            if (strScanType.ToString().Equals(enumScanType.Material.ToString()))
            {
                _ScanType = enumScanType.Material;
            }
            else if (strScanType.ToString().Equals(enumScanType.Scale.ToString()))
            {
                _ScanType = enumScanType.Scale;
            }

            _checkStep = enumCheckStep.WaitBarcode;
 
           
        }
    }
}

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

namespace 보령
{
    public class SVP사후멸균제품검사공정수율ViewModel : ViewModelBase
    {
        #region Properties
        public SVP사후멸균제품검사공정수율ViewModel()
        {
            _BR_BRS_REG_ProductionOrderDetailYield = new BR_BRS_REG_ProductionOrderDetailYield();
        }

        SVP사후멸균제품검사공정수율 _mainWnd;

        private string _result_IN;
        public string Result_IN
        {
            get { return _result_IN; }
            set
            {
                _result_IN = value;
                CalculateYield(_result_IN, _result_OUT);
                NotifyPropertyChanged();
            }
        }

        private string _result_OUT;
        public string Result_OUT
        {
            get { return _result_OUT; }
            set
            {
                _result_OUT = value;
                CalculateYield(_result_IN, _result_OUT);
                NotifyPropertyChanged();
            }
        }

        private decimal? _result_SUM;
        public decimal? Result_SUM
        {
            get { return _result_SUM; }
            set
            {
                _result_SUM = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region BizRule
        private BR_BRS_REG_ProductionOrderDetailYield _BR_BRS_REG_ProductionOrderDetailYield;
        public BR_BRS_REG_ProductionOrderDetailYield BR_BRS_REG_ProductionOrderDetailYield
        {
            get { return _BR_BRS_REG_ProductionOrderDetailYield; }
            set { _BR_BRS_REG_ProductionOrderDetailYield = value; }
        }
        #endregion
        #region Command
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

                            if (arg != null)
                            {
                                IsBusy = true;
                                _mainWnd = arg as SVP사후멸균제품검사공정수율;
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
                            IsBusy = false;
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

        public ICommand NoRecordConfirmCommand
        {
            get
            {
                return new AsyncCommandBase(async arg =>
                {
                    try
                    {
                        CommandCanExecutes["NoRecordConfirmCommand"] = false;
                        CommandResults["NoRecordConfirmCommand"] = false;

                        ///
                        IsBusy = true;
                        bool FirstNoRecord = false; //2025.08.05 김도연 처음 기록없음 버튼을 통해 지시문을 기록할 경우 코멘트가 남도록 하는 Flag

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
                        else
                        {
                            // 전자서명 요청
                            authHelper.InitializeAsync(Common.enumCertificationType.Role, Common.enumAccessType.Create, "OM_ProductionOrder_SUI");

                            if (await authHelper.ClickAsync(
                                Common.enumCertificationType.Function,
                                Common.enumAccessType.Create,
                                string.Format("SVP사후멸균 제품 검사 공정 수율 UI 기록없음"),
                                string.Format("SVP사후멸균 제품 검사 공정 수율 UI 기록없음"),
                                true,
                                "OM_ProductionOrder_SUI",
                                "", _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID, null) == false)
                            {
                                throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                            }
                            FirstNoRecord = true;
                        }

                        //이미지 저장시 서명화면으로 인해 이미지가 잘 안보임.그에 따른 이미지 데이터만 먼저 생성해 놓도록 함.
                        Brush background = _mainWnd.PrintArea.Background;
                        _mainWnd.PrintArea.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD6, 0xD4, 0xD4));
                        _mainWnd.PrintArea.BorderThickness = new System.Windows.Thickness(1);
                        _mainWnd.PrintArea.Background = new SolidColorBrush(Colors.White);

                        _mainWnd.CurrentInstruction.Raw.NOTE = imageToByteArray();
                        _mainWnd.CurrentInstruction.Raw.ACTVAL = "Image attached";

                        var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, true);
                        if (result != enumInstructionRegistErrorType.Ok)
                        {
                            throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                        }

                        //2025.08.05 김도연 처음 기록없음 버튼을 통해 지시문을 기록할 경우 코멘트가 남도록 함
                        if (FirstNoRecord)
                        {
                            var bizrule = new BR_PHR_REG_InstructionComment();

                            bizrule.IN_Comments.Add(
                                new BR_PHR_REG_InstructionComment.IN_Comment
                                {
                                    POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                    OPSGGUID = _mainWnd.CurrentOrder.OrderProcessSegmentID,
                                    COMMENTTYPE = "CM008",
                                    COMMENT = AuthRepositoryViewModel.GetCommentByFunctionCode("OM_ProductionOrder_SUI"),
                                    INSUSER = AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_SUI")
                                }
                                );
                            bizrule.IN_IntructionResults.Add(
                                new BR_PHR_REG_InstructionComment.IN_IntructionResult
                                {
                                    RECIPEISTGUID = _mainWnd.CurrentInstruction.Raw.RECIPEISTGUID,
                                    ACTIVITYID = _mainWnd.CurrentInstruction.Raw.ACTIVITYID,
                                    IRTGUID = _mainWnd.CurrentInstruction.Raw.IRTGUID,
                                    IRTRSTGUID = _mainWnd.CurrentInstruction.Raw.IRTRSTGUID,
                                    IRTSEQ = (int)_mainWnd.CurrentInstruction.Raw.IRTSEQ
                                }
                                );

                            await bizrule.Execute();
                        }

                        if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                        else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);

                        ///
                        CommandResults["NoRecordConfirmCommand"] = true;
                    }
                    catch (Exception ex)
                    {
                        CommandResults["NoRecordConfirmCommand"] = false;
                        OnException(ex.Message, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                        CommandCanExecutes["NoRecordConfirmCommand"] = true;
                    }
                }, arg =>
                {
                    return CommandCanExecutes.ContainsKey("NoRecordConfirmCommand") ?
                        CommandCanExecutes["NoRecordConfirmCommand"] : (CommandCanExecutes["NoRecordConfirmCommand"] = true);
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

                            ///

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
                            else
                            {
                                authHelper.InitializeAsync(Common.enumCertificationType.Function, Common.enumAccessType.Create, "OM_ProductionOrder_Yield");

                                if (await authHelper.ClickAsync(
                                    Common.enumCertificationType.Function,
                                    Common.enumAccessType.Create,
                                    "SVP사후멸균제품검사공정수율 수율 기록",
                                    "SVP사후멸균제품검사공정수율 수율 기록",
                                    false,
                                    "OM_ProductionOrder_Yield",
                                    "", null, null) == false)
                                {
                                    throw new Exception(string.Format("서명이 완료되지 않았습니다."));
                                }
                            }
                            // 수율기록
                            _BR_BRS_REG_ProductionOrderDetailYield.INDATAs.Clear();
                            _BR_BRS_REG_ProductionOrderDetailYield.INDATAs.Add(new BR_BRS_REG_ProductionOrderDetailYield.INDATA
                            {
                                POID = _mainWnd.CurrentOrder.ProductionOrderID,
                                OPSGGUID = Guid.Parse(_mainWnd.CurrentOrder.OrderProcessSegmentID),
                                COMMENT = AuthRepositoryViewModel.GetCommentByFunctionCode("OM_ProductionOrder_Yield"),
                                INSUSER = !string.IsNullOrWhiteSpace(AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Yield")) ? AuthRepositoryViewModel.GetUserIDByFunctionCode("OM_ProductionOrder_Yield") : AuthRepositoryViewModel.Instance.LoginedUserID,
                                YIELD1 = Result_SUM
                            });

                            if(await BR_BRS_REG_ProductionOrderDetailYield.Execute() == true)
                            {
                                Brush background = _mainWnd.PrintArea.Background;
                                _mainWnd.PrintArea.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD6, 0xD4, 0xD4));
                                _mainWnd.PrintArea.BorderThickness = new System.Windows.Thickness(1);
                                _mainWnd.PrintArea.Background = new SolidColorBrush(Colors.White);


                                _mainWnd.CurrentInstruction.Raw.ACTVAL = Result_SUM.ToString();
                                _mainWnd.CurrentInstruction.Raw.NOTE = imageToByteArray();

                                var outputValues = InstructionModel.GetResultReceiver(_mainWnd.CurrentInstruction, _mainWnd.Instructions);

                                var result = await _mainWnd.Phase.RegistInstructionValue(_mainWnd.CurrentInstruction, false, false, true);
                                if (result != enumInstructionRegistErrorType.Ok)
                                {
                                    throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", _mainWnd.CurrentInstruction.Raw.IRTGUID, result));
                                }

                                foreach (var item in outputValues)
                                {
                                    item.Raw.ACTVAL = _mainWnd.CurrentInstruction.Raw.ACTVAL;

                                    result = await _mainWnd.Phase.RegistInstructionValue(item);
                                    if (result != enumInstructionRegistErrorType.Ok)
                                    {
                                        throw new Exception(string.Format("값 등록 실패, ID={0}, 사유={1}", item.Raw.IRTGUID, result));
                                    }
                                }

                                if (_mainWnd.Dispatcher.CheckAccess()) _mainWnd.DialogResult = true;
                                else _mainWnd.Dispatcher.BeginInvoke(() => _mainWnd.DialogResult = true);
                            }

                            IsBusy = false;
                            ///

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
        #region User Define
        private void CalculateYield(string input, string output)
        {
            try
            {
                decimal IN, OUT;

                if (decimal.TryParse(input, out IN) && decimal.TryParse(output, out OUT))
                    Result_SUM = MathExt.Round((OUT / IN) * 100, 1, MidpointRounding.AwayFromZero);

            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
            }
        }
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



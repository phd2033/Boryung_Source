using LGCNS.iPharmMES.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace 보령
{
    
    /// <summary>
    /// summary of BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID
    /// </summary>
    public partial class BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID : BizActorRuleBase
    {
        public sealed partial class INDATACollection : BufferedObservableCollection<INDATA>
        {
        }
        private INDATACollection _INDATAs;
        [BizActorInputSetAttribute()]
        public INDATACollection INDATAs
        {
            get
            {
                return this._INDATAs;
            }
        }
        [BizActorInputSetDefineAttribute(Order="0")]
        [CustomValidation(typeof(ViewModelBase), "ValidateRow")]
        public partial class INDATA : BizActorDataSetBase
        {
            public INDATA()
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
            private string _EQCLID;
            [BizActorInputItemAttribute()]
            public string EQCLID
            {
                get
                {
                    return this._EQCLID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQCLID = value;
                        this.CheckIsOriginal("EQCLID", value);
                        this.OnPropertyChanged("EQCLID");
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
            private string _EQACID;
            [BizActorInputItemAttribute()]
            public string EQACID
            {
                get
                {
                    return this._EQACID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQACID = value;
                        this.CheckIsOriginal("EQACID", value);
                        this.OnPropertyChanged("EQACID");
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
        public sealed partial class STATUS_OUTDATACollection : BufferedObservableCollection<STATUS_OUTDATA>
        {
        }
        private STATUS_OUTDATACollection _STATUS_OUTDATAs;
        [BizActorOutputSetAttribute()]
        public STATUS_OUTDATACollection STATUS_OUTDATAs
        {
            get
            {
                return this._STATUS_OUTDATAs;
            }
        }
        [BizActorOutputSetDefineAttribute(Order="0")]
        [CustomValidation(typeof(ViewModelBase), "ValidateRow")]
        public partial class STATUS_OUTDATA : BizActorDataSetBase
        {
            public STATUS_OUTDATA()
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
            private string _EQPTID;
            [BizActorOutputItemAttribute()]
            public string EQPTID
            {
                get
                {
                    return this._EQPTID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPTID = value;
                        this.CheckIsOriginal("EQPTID", value);
                        this.OnPropertyChanged("EQPTID");
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
            private string _EQCLID;
            [BizActorOutputItemAttribute()]
            public string EQCLID
            {
                get
                {
                    return this._EQCLID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQCLID = value;
                        this.CheckIsOriginal("EQCLID", value);
                        this.OnPropertyChanged("EQCLID");
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
            private string _EQACID;
            [BizActorOutputItemAttribute()]
            public string EQACID
            {
                get
                {
                    return this._EQACID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQACID = value;
                        this.CheckIsOriginal("EQACID", value);
                        this.OnPropertyChanged("EQACID");
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
            private string _EQACNAME;
            [BizActorOutputItemAttribute()]
            public string EQACNAME
            {
                get
                {
                    return this._EQACNAME;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQACNAME = value;
                        this.CheckIsOriginal("EQACNAME", value);
                        this.OnPropertyChanged("EQACNAME");
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
            private string _EQSTID;
            [BizActorOutputItemAttribute()]
            public string EQSTID
            {
                get
                {
                    return this._EQSTID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQSTID = value;
                        this.CheckIsOriginal("EQSTID", value);
                        this.OnPropertyChanged("EQSTID");
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
            private string _EQSTNAME;
            [BizActorOutputItemAttribute()]
            public string EQSTNAME
            {
                get
                {
                    return this._EQSTNAME;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQSTNAME = value;
                        this.CheckIsOriginal("EQSTNAME", value);
                        this.OnPropertyChanged("EQSTNAME");
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
            private string _ISCONDITION;
            [BizActorOutputItemAttribute()]
            public string ISCONDITION
            {
                get
                {
                    return this._ISCONDITION;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._ISCONDITION = value;
                        this.CheckIsOriginal("ISCONDITION", value);
                        this.OnPropertyChanged("ISCONDITION");
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
            private string _CONDITIONVAL;
            [BizActorOutputItemAttribute()]
            public string CONDITIONVAL
            {
                get
                {
                    return this._CONDITIONVAL;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._CONDITIONVAL = value;
                        this.CheckIsOriginal("CONDITIONVAL", value);
                        this.OnPropertyChanged("CONDITIONVAL");
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
            private string _CONDITION_STATE;
            [BizActorOutputItemAttribute()]
            public string CONDITION_STATE
            {
                get
                {
                    return this._CONDITION_STATE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._CONDITION_STATE = value;
                        this.CheckIsOriginal("CONDITION_STATE", value);
                        this.OnPropertyChanged("CONDITION_STATE");
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
            private string _CURRENTVAL;
            [BizActorOutputItemAttribute()]
            public string CURRENTVAL
            {
                get
                {
                    return this._CURRENTVAL;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._CURRENTVAL = value;
                        this.CheckIsOriginal("CURRENTVAL", value);
                        this.OnPropertyChanged("CURRENTVAL");
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
            private string _CURRENT_STATE;
            [BizActorOutputItemAttribute()]
            public string CURRENT_STATE
            {
                get
                {
                    return this._CURRENT_STATE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._CURRENT_STATE = value;
                        this.CheckIsOriginal("CURRENT_STATE", value);
                        this.OnPropertyChanged("CURRENT_STATE");
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
            private string _ONEQACNAME;
            [BizActorOutputItemAttribute()]
            public string ONEQACNAME
            {
                get
                {
                    return this._ONEQACNAME;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._ONEQACNAME = value;
                        this.CheckIsOriginal("ONEQACNAME", value);
                        this.OnPropertyChanged("ONEQACNAME");
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
            private string _OFFEQACNAME;
            [BizActorOutputItemAttribute()]
            public string OFFEQACNAME
            {
                get
                {
                    return this._OFFEQACNAME;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._OFFEQACNAME = value;
                        this.CheckIsOriginal("OFFEQACNAME", value);
                        this.OnPropertyChanged("OFFEQACNAME");
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
            private string _RESULTVAL;
            [BizActorOutputItemAttribute()]
            public string RESULTVAL
            {
                get
                {
                    return this._RESULTVAL;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._RESULTVAL = value;
                        this.CheckIsOriginal("RESULTVAL", value);
                        this.OnPropertyChanged("RESULTVAL");
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
            private string _RESULT_STATE;
            [BizActorOutputItemAttribute()]
            public string RESULT_STATE
            {
                get
                {
                    return this._RESULT_STATE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._RESULT_STATE = value;
                        this.CheckIsOriginal("RESULT_STATE", value);
                        this.OnPropertyChanged("RESULT_STATE");
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
            private string _EQSTDESC;
            [BizActorOutputItemAttribute()]
            public string EQSTDESC
            {
                get
                {
                    return this._EQSTDESC;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQSTDESC = value;
                        this.CheckIsOriginal("EQSTDESC", value);
                        this.OnPropertyChanged("EQSTDESC");
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
        public sealed partial class PARAM_OUTDATACollection : BufferedObservableCollection<PARAM_OUTDATA>
        {
        }
        private PARAM_OUTDATACollection _PARAM_OUTDATAs;
        [BizActorOutputSetAttribute()]
        public PARAM_OUTDATACollection PARAM_OUTDATAs
        {
            get
            {
                return this._PARAM_OUTDATAs;
            }
        }
        [BizActorOutputSetDefineAttribute(Order="1")]
        [CustomValidation(typeof(ViewModelBase), "ValidateRow")]
        public partial class PARAM_OUTDATA : BizActorDataSetBase
        {
            public PARAM_OUTDATA()
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
            private string _EQCLID;
            [BizActorOutputItemAttribute()]
            public string EQCLID
            {
                get
                {
                    return this._EQCLID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQCLID = value;
                        this.CheckIsOriginal("EQCLID", value);
                        this.OnPropertyChanged("EQCLID");
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
            private string _EQSTID;
            [BizActorOutputItemAttribute()]
            public string EQSTID
            {
                get
                {
                    return this._EQSTID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQSTID = value;
                        this.CheckIsOriginal("EQSTID", value);
                        this.OnPropertyChanged("EQSTID");
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
            private string _EQPAID;
            [BizActorOutputItemAttribute()]
            public string EQPAID
            {
                get
                {
                    return this._EQPAID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPAID = value;
                        this.CheckIsOriginal("EQPAID", value);
                        this.OnPropertyChanged("EQPAID");
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
            private string _INPUTTYPE;
            [BizActorOutputItemAttribute()]
            public string INPUTTYPE
            {
                get
                {
                    return this._INPUTTYPE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._INPUTTYPE = value;
                        this.CheckIsOriginal("INPUTTYPE", value);
                        this.OnPropertyChanged("INPUTTYPE");
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
            private string _ISREQUIRED;
            [BizActorOutputItemAttribute()]
            public string ISREQUIRED
            {
                get
                {
                    return this._ISREQUIRED;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._ISREQUIRED = value;
                        this.CheckIsOriginal("ISREQUIRED", value);
                        this.OnPropertyChanged("ISREQUIRED");
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
            private string _ISSYSDEF;
            [BizActorOutputItemAttribute()]
            public string ISSYSDEF
            {
                get
                {
                    return this._ISSYSDEF;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._ISSYSDEF = value;
                        this.CheckIsOriginal("ISSYSDEF", value);
                        this.OnPropertyChanged("ISSYSDEF");
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
            private string _EQPANAME;
            [BizActorOutputItemAttribute()]
            public string EQPANAME
            {
                get
                {
                    return this._EQPANAME;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPANAME = value;
                        this.CheckIsOriginal("EQPANAME", value);
                        this.OnPropertyChanged("EQPANAME");
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
            private string _EQPADESC;
            [BizActorOutputItemAttribute()]
            public string EQPADESC
            {
                get
                {
                    return this._EQPADESC;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPADESC = value;
                        this.CheckIsOriginal("EQPADESC", value);
                        this.OnPropertyChanged("EQPADESC");
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
            private string _EQPAIUSE;
            [BizActorOutputItemAttribute()]
            public string EQPAIUSE
            {
                get
                {
                    return this._EQPAIUSE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPAIUSE = value;
                        this.CheckIsOriginal("EQPAIUSE", value);
                        this.OnPropertyChanged("EQPAIUSE");
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
            private string _EQPAIREP;
            [BizActorOutputItemAttribute()]
            public string EQPAIREP
            {
                get
                {
                    return this._EQPAIREP;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPAIREP = value;
                        this.CheckIsOriginal("EQPAIREP", value);
                        this.OnPropertyChanged("EQPAIREP");
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
            private string _EQPATYPE;
            [BizActorOutputItemAttribute()]
            public string EQPATYPE
            {
                get
                {
                    return this._EQPATYPE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPATYPE = value;
                        this.CheckIsOriginal("EQPATYPE", value);
                        this.OnPropertyChanged("EQPATYPE");
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
            private string _EQPADFLT;
            [BizActorOutputItemAttribute()]
            public string EQPADFLT
            {
                get
                {
                    return this._EQPADFLT;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPADFLT = value;
                        this.CheckIsOriginal("EQPADFLT", value);
                        this.OnPropertyChanged("EQPADFLT");
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
            private string _EQPAMIN;
            [BizActorOutputItemAttribute()]
            public string EQPAMIN
            {
                get
                {
                    return this._EQPAMIN;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPAMIN = value;
                        this.CheckIsOriginal("EQPAMIN", value);
                        this.OnPropertyChanged("EQPAMIN");
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
            private string _EQPAMAX;
            [BizActorOutputItemAttribute()]
            public string EQPAMAX
            {
                get
                {
                    return this._EQPAMAX;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPAMAX = value;
                        this.CheckIsOriginal("EQPAMAX", value);
                        this.OnPropertyChanged("EQPAMAX");
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
            private string _UOMID;
            [BizActorOutputItemAttribute()]
            public string UOMID
            {
                get
                {
                    return this._UOMID;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._UOMID = value;
                        this.CheckIsOriginal("UOMID", value);
                        this.OnPropertyChanged("UOMID");
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
            private string _EQPAINFO;
            [BizActorOutputItemAttribute()]
            public string EQPAINFO
            {
                get
                {
                    return this._EQPAINFO;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._EQPAINFO = value;
                        this.CheckIsOriginal("EQPAINFO", value);
                        this.OnPropertyChanged("EQPAINFO");
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
            private string _BIZRULE;
            [BizActorOutputItemAttribute()]
            public string BIZRULE
            {
                get
                {
                    return this._BIZRULE;
                }
                set
                {
                    if ((this.IsValid(value) == LGCNS.iPharmMES.Common.Common.enumValidationLevel.Error))
                    {
                    }
                    else
                    {
                        this._BIZRULE = value;
                        this.CheckIsOriginal("BIZRULE", value);
                        this.OnPropertyChanged("BIZRULE");
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
        public BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID()
        {
            RuleName = "BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID";
            BizName = "BR_PHR_SEL_EquipmentActionStatusWithParameter_EQCLID";
            _INDATAs = new INDATACollection();
            _STATUS_OUTDATAs = new STATUS_OUTDATACollection();
            _PARAM_OUTDATAs = new PARAM_OUTDATACollection();
        }
    }
}

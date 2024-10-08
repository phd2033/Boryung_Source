using LGCNS.iPharmMES.Common;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace 보령
{
    public partial class BR_BRS_SEL_ProductionOrder_OPSG_Component_Summary : BizActorRuleBase
    {
        public partial class OUTDATA
        {
            private Weight _INVQTY = new Weight();
            public Weight INVQTY
            {
                get { return _INVQTY; }
                set
                {
                    _INVQTY = value;
                    OnPropertyChanged("INVQTY");
                }
            }
            private Weight _MaterialQTY = new Weight();
            public Weight MaterialQTY
            {
                get { return _MaterialQTY; }
                set
                {
                    _INVQTY = value;
                    OnPropertyChanged("MaterialQTY");
                }
            }
        }
    }
}

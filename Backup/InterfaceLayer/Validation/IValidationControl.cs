using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    public interface IValidationControl
    {
        void Initialize();
        event EventHandler OnStateOk;
        event EventHandler OnStateError;
    }
}

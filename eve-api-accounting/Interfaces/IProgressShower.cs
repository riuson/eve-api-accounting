using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public interface IProgressShower
    {
        void ShowProgress(int progress, string message);
    }
}

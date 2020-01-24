using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    #region ENUM
    /// <summary>
    /// Which type of payment for a contract : monthly or hourly
    /// </summary>
    public enum PaymentType { Hour, Month }

    /// <summary>
    /// Type of vacation : according to education ministery or work ministery
    /// </summary>
    public enum VacationType { Education, Work }

    /// <summary>
    /// Enum for days week
    /// </summary>
    public enum DayWeek { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }
    #endregion
}

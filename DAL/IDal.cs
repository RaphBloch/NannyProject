using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    /// <summary>
    /// Interface to manage the existing list in DataSource
    /// </summary>
    public interface IDAL
    {
        #region ADD VALUES
        void AddNanny(Nanny n);
        void AddMother(Mother m);
        void AddChild(Child c);
        void AddContract(Contract c);
        #endregion

        #region DELETE VALUES
        void DeleteNanny(int n);
        void DeleteMother(int n);
        void DeleteChild(int n);
        void DeleteContract(int n);
        #endregion

        #region UPDATE VALUES
        void UpdateNanny(Nanny n);
        void UpdateMother(Mother m);
        void UpdateChild(Child c);
        void UpdateContract(Contract c);
        #endregion

        #region GET LISTS
        IEnumerable<Mother> GetAllMother(Func<Mother, bool> predicate = null);
        IEnumerable<Child> GetAllChild(Func<Child, bool> predicate = null);
        IEnumerable<Nanny> GetAllNanny(Func<Nanny, bool> predicate = null);
        IEnumerable<Contract> GetAllContract(Func<Contract, bool> predicate = null);
        #endregion
    }
}

using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        #region ADD VALUES
        void AddNanny(Nanny n);
        void AddMother(Mother m);
        void AddChild(Child c);
        void AddContract(Contract c);
        #endregion

        #region DELETE VALUES
        void DeleteNanny(int id);
        void DeleteMother(int id);
        void DeleteChild(int id);
        void DeleteContract(int id);
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

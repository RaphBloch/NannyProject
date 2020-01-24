using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using static DS.DataSource;

namespace DAL
{
    public class Dal_imp : IDAL
    {
        static DataSource d = new DataSource();

        static public int contractID = 1;

        public Dal_imp() { }

        #region ADD VALUES

        /// <summary>
        /// Function to add a nanny in the database
        /// </summary>
        /// <param name="n">The Nanny to add</param>
        public void AddNanny(Nanny n)
        {
            List<Nanny> l = DataSource.NannyList;
            if (l.Exists(x => x.ID == n.ID))
                throw new Exception("A nanny with the same ID already exists.");
            l.Add(n);
            l.Sort();
        }

        /// <summary>
        /// Function to add a child in the database
        /// </summary>
        /// <param name="n">The child to add</param>
        public void AddChild(Child n)
        {
            List<Child> l = DataSource.ChildList;
            List<Mother> m = DataSource.MotherList;
            
            //if a such child already exists
            if (l.Exists(x => x.ID == n.ID))
                throw new Exception("A child with the same ID already exists.");

            if (n.ID == 0)
                throw new Exception("Please select a valid ID for the child (not 0)");

            //if the mother of this child doesn't exist
            if (!m.Exists(x => x.ID == n.MotherID))
                throw new Exception("The mother with the ID specified for this child doesn't exist.");

          

            l.Add(n);
            l.Sort();
        }

        /// <summary>
        /// Function to add a mother in the database
        /// </summary>
        /// <param name="n">The mother to add</param>
        public void AddMother(Mother n)
        {
            List<Mother> l = DataSource.MotherList;
            if (l.Exists(x => x.ID == n.ID))
                throw new Exception("A mother with the same ID already exists.");
            l.Add(n);
            l.Sort();
        }

        /// <summary>
        /// Function to add a contract in the database
        /// </summary>
        /// <param name="n">The contract to add</param>
        public void AddContract(Contract n)
        {
            List<Nanny> ln = DataSource.NannyList;
            List<Child> lc = DataSource.ChildList;
            List<Mother> lm = DataSource.MotherList;
            List<Contract> c = DataSource.ContractList;

            //  if the nanny or/and the child or/and in the contract are not in our database
            if (!lc.Exists(x => x.ID == n.ChildID))
                throw new Exception("No such child exists to create this contract");
            if (!ln.Exists(x => x.ID == n.NannyID))
                throw new Exception("No such nanny exists to create this contract");

            //  if the mother doesn't exist in our database
            Child cc = lc.Find(x => x.ID == n.ChildID);
            if (!lm.Exists(x => x.ID == cc.MotherID))
                throw new Exception("No such mother exists to create this contract");

            n.ID = contractID;
            contractID++;

            c.Add(n);
            c.Sort();
            n.ChildName = cc.FamilyName;
            n.NannyName = ln.Find(x => x.ID == n.NannyID).FamilyName;
        }

        //=============================================
        #endregion

        #region DELETE VALUES

        /// <summary>
        /// Function to delete a Nanny from our database
        /// </summary>
        /// <param name="x">The Nanny to delete</param>
        public void DeleteNanny(int id)
        {
            List<Nanny> l = DataSource.NannyList;
            if (!l.Exists(n => n.ID == id))
                throw new Exception("There is no such nanny.");

            l.Remove(l.Find(n => n.ID == id));
        }

        /// <summary>
        /// Function to delete a Mother from our database
        /// </summary>
        /// <param name="x">The Mother to delete</param>
        public void DeleteMother(int id)
        {
            List<Mother> l = DataSource.MotherList;
            if (!l.Exists(n => n.ID == id))
                throw new Exception("There is no such mother.");

            DataSource.ChildList.RemoveAll(x => x.MotherID == id);
            l.Remove(l.Find(n => n.ID == id));
        }

        /// <summary>
        /// Function to delete a Child from our database
        /// </summary>
        /// <param name="x">The Child to delete</param>
        public void DeleteChild(int id)
        {
            List<Child> l = DataSource.ChildList;
            if (!l.Exists(n => n.ID == id))
                throw new Exception("There is no such child.");

            DataSource.ContractList.RemoveAll(x => x.ChildID == id);
            l.Remove(l.Find(n => n.ID == id));
        }

        /// <summary>
        /// Function to delete a Contract from our database
        /// </summary>
        /// <param name="x">The Contract to delete</param>
        public void DeleteContract(int id)
        {
            List<Contract> l = DataSource.ContractList;
            if (!l.Exists(n => n.ID == id))
                throw new Exception("There is no such contract.");

            Contract c = this.GetAllContract().Where(x => x.ID == id).First();
            Nanny nannywithcontract = (Nanny)this.GetAllNanny().Where(x => x.ID == c.NannyID).First();
            if(c.Signed)
                nannywithcontract.CountChild--;
            l.Remove(l.Find(n => n.ID == id));
        }

        //=============================================
        #endregion

        #region UPDATE VALUES
      

        /// <summary>
        /// Function to update a Nanny in our database
        /// </summary>
        /// <param name="x">The Nanny to update</param>
        public void UpdateNanny(Nanny n)
        {
            List<Nanny> l = DataSource.NannyList;

            if (!l.Exists(x => x.ID == n.ID))
                throw new Exception("There is no such nanny.");

            //implement the update according to the demands
            l.Remove(l.Find(x => x.ID == n.ID));
            l.Add(n);
            l.Sort();
        }

        /// <summary>
        /// Function to update a Mother in our database
        /// </summary>
        /// <param name="x">The Mother to update</param>
        public void UpdateMother(Mother n)
        {
            List<Mother> l = DataSource.MotherList;

            if (!l.Exists(x => x.ID == n.ID))
                throw new Exception("There is no such mother.");

            //implement the update according to the demands
            l.Remove(l.Find(x => x.ID == n.ID));
            l.Add(n);
            l.Sort();
        }

        /// <summary>
        /// Function to update a Child in our database
        /// </summary>
        /// <param name="x">The Child to update</param>
        public void UpdateChild(Child n)
        {
            List<Child> l = DataSource.ChildList;

            if (!l.Exists(x => x.ID == n.ID))
                throw new Exception("There is no such child.");

            //implement the update according to the demand
            l.Remove(l.Find(x => x.ID == n.ID));
            l.Add(n);
            l.Sort();
        }

        /// <summary>
        /// Function to update a Contract in our database
        /// </summary>
        /// <param name="x">The Contract to update</param>
        public void UpdateContract(Contract c)
        {
            List<Contract> l = DataSource.ContractList;

            if (!l.Exists(n => n.ID == c.ID))
                throw new Exception("That contract doesn't match any contract of our database.");

            //implement the update according to the demands
            l.Remove(l.Find( n => n.ID == c.ID));
            l.Add(c);
            l.Sort();
        }

        //=============================================
        #endregion

        #region GET LISTS

        //---------------------------------------------------
        //  FUNCTIONS TO GET LISTS VALUES FROM OUR DATABASE
        //---------------------------------------------------

        /// <summary>
        /// Return a list of mothers which satisfy a specific predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Mother> GetAllMother(Func<Mother, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.MotherList.AsEnumerable();

            return from n in DataSource.MotherList
                   where (predicate(n))
                   select n;
        }

        /// <summary>
        /// Return a list of nannies which satisfy a specific predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> GetAllNanny(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.NannyList.AsEnumerable();

            return from n in DataSource.NannyList
                   where (predicate(n))
                   select n;
        }

        /// <summary>
        /// Return a list of children which satisfy a specific predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Child> GetAllChild(Func<Child, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.ChildList.AsEnumerable();

            return DataSource.ChildList.Where(predicate);
        }

        /// <summary>
        /// Return a list of contracts which satisfy a specific predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Contract> GetAllContract(Func<Contract, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.ContractList.AsEnumerable();

            return DataSource.ContractList.Where(predicate);
        }

        //==============================================
        #endregion
    }

}

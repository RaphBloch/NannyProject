using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GoogleMapsApi;
using Newtonsoft.Json;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System.Threading;

namespace BL
{

    public class BL_imp : IBL
    {

        IDAL dal = FactoryDal.GetDal();



        /// <summary>
        /// constructor
        /// </summary>
        public BL_imp()
        {/*

            //Here we want to create our database
            Nanny[] listn = new Nanny[] { new Nanny(), new Nanny(), new Nanny(), new Nanny(), new Nanny() };
            Mother[] listm = new Mother[] { new Mother(), new Mother(), new Mother(), new Mother(), new Mother() };
            Child[] listc = new Child[] { new Child(), new Child(), new Child(), new Child(), new Child() };

            listc[0].MotherID = listm[0].ID;
            listc[1].MotherID = listm[0].ID;
            listc[2].MotherID = listm[0].ID;
            listc[3].MotherID = listm[0].ID;
            listc[4].MotherID = listm[0].ID;

            listn[0].MinAge = 2;
            listn[1].MinAge = 2;
            listn[2].MinAge = 4;
            listn[3].MinAge = 4;
            listn[4].MinAge = 2;

            listn[0].MaxAge = 12;
            listn[1].MaxAge = 13;
            listn[2].MaxAge = 15;
            listn[3].MaxAge = 6;
            listn[4].MaxAge = 5;


            for (int i = 0; i < 5; i++)
            {
                try
                {
                    AddNanny(listn[i]);
                }
                catch { }

                try
                {
                    AddMother(listm[i]);
                }
                catch { }

                try
                {
                    AddChild(listc[i]);
                }
                catch { }
            }

            */
        }

        #region ADD VALUES

        /// <summary>
        /// Add a child in our database
        /// </summary>
        /// <param name="c"></param>
        public void AddChild(Child c)
        {
            if ((DateTime.Now - c.Birth).TotalMilliseconds < 0)
                throw new Exception("This child isn't born yet.");
            dal.AddChild(c);
            c.FamilyName = GetAllMother(x => x.ID == c.MotherID).First().FamilyName;
        }

        /// <summary>
        /// Add a contract in our database, no logical conditions
        /// We will check conditions to sign it and to calculate the salary
        /// </summary>
        /// <param name="c"></param>
        public void AddContract(Contract c)
        {
            Child cc = GetAllChild(x => x.ID == c.ChildID).FirstOrDefault();
            Nanny n = GetAllNanny(x => x.ID == c.NannyID).FirstOrDefault();
            Mother m = GetAllMother(x => x.ID == cc.MotherID).FirstOrDefault();

            //  create the distance
            if (m.Request.SearchAddress != "")
                c.Distance = (new DistanceClass(m.Request.SearchAddress, n.Address)).Distance;
            else
                c.Distance = (new DistanceClass(m.Request.Address, n.Address)).Distance;

            if (c.Distance < m.Request.DistanceAccepted)
                SignContract(c);
            else
                throw new Exception("Your contract cannot be signed because the distance isn't acceptable.");

            dal.AddContract(c);
        }



        /// <summary>
        /// Add a mother in our database, no logical condition to add it
        /// </summary>
        /// <param name="m"></param>
        public void AddMother(Mother m)
        {
            dal.AddMother(m);
        }

        /// <summary>
        /// Add a nanny in our database, check if older than 18
        /// </summary>
        /// <param name="n"></param>
        public void AddNanny(Nanny n)
        {
            if ((DateTime.Now - n.Birth).Days < 365.25 * 18)
                throw new Exception("This nanny isn't 18 years yet.");
            dal.AddNanny(n);
        }
        #endregion


        #region DELETE VALUES

        /// <summary>
        /// Delete a child from database, also perform a delete for each contract in the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteChild(int id)
        {
            foreach (var c in GetAllContract())
            {
                if (c.ChildID == id)
                    DeleteContract(c.ID);
            }
            dal.DeleteChild(id);
        }

        public void DeleteContract(int id)
        {
            dal.DeleteContract(id);
        }

        /// <summary>
        /// Delete a mother and all of her children
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMother(int id)
        {
            dal.DeleteMother(id);
        }


        /// <summary>
        /// Delete a nanny and all the contracts where the Nanny is involved in
        /// </summary>
        /// <param name="id"></param>
        public void DeleteNanny(int id)
        {
            foreach (var c in GetAllContract())
            {
                if (c.NannyID == id)
                    DeleteContract(c.ID);
            }
            dal.DeleteNanny(id);

        }
        #endregion


        #region UPDATE VALUES

        /// <summary>
        /// To change an existing child
        /// </summary>
        /// <param name="c"></param>
        public void UpdateChild(Child c)
        {
            if ((DateTime.Now - c.Birth).Days < 90)
                throw new Exception("This child isn't 3 months yet.");
            dal.UpdateChild(c);
        }

        /// <summary>
        /// To change an existing contract
        /// </summary>
        /// <param name="c"></param>
        public void UpdateContract(Contract c)
        {
            SignContract(c);
            dal.UpdateContract(c);
        }

        /// <summary>
        /// To change an existing mother
        /// </summary>
        /// <param name="m"></param>
        public void UpdateMother(Mother m)
        {
            dal.UpdateMother(m);
            
        }

        /// <summary>
        /// To change an existing nanny
        /// </summary>
        /// <param name="n"></param>
        public void UpdateNanny(Nanny n)
        {
            if ((DateTime.Now - n.Birth).Days < 6574)
                throw new Exception("This nanny isn't 18 years yet.");
            dal.UpdateNanny(n);
        }
        #endregion



        #region GET VALUES

        public IEnumerable<Mother> GetAllMother(Func<Mother, bool> predicate = null)
        {
            return dal.GetAllMother(predicate);
        }

        public IEnumerable<Child> GetAllChild(Func<Child, bool> predicate = null)
        {
            return dal.GetAllChild(predicate);
        }

        public IEnumerable<Nanny> GetAllNanny(Func<Nanny, bool> predicate = null)
        {
            return dal.GetAllNanny(predicate);
        }

        public IEnumerable<Contract> GetAllContract(Func<Contract, bool> predicate = null)
        {
            return dal.GetAllContract(predicate);
        }

        #endregion



        #region METHODS

        private void DefineDistance(string address1, string address2, out int distance)
        {
            distance = Distance(address1, address2);
        }

        /// <summary>
        /// Check if conditions respected to sign the contract
        /// </summary>
        /// <param name="c"></param>
        public void SignContract(Contract c)
        {
            Child cc = GetAllChild(x => x.ID == c.ChildID).FirstOrDefault();
            Nanny n = GetAllNanny(x => x.ID == c.NannyID).FirstOrDefault();
            Mother m = GetAllMother(x => x.ID == cc.MotherID).FirstOrDefault();

            //  check if the child is already 3 months
            if ((DateTime.Now - cc.Birth).TotalDays < 90)
                throw new Exception("The child isn't 3 months yet, you can't sign the contract.");

            //  check if the nanny can take care of the child (too more child to take care of)
            if (n.CountChild >= n.MaxChilds)
                throw new Exception("The nanny can't take care of the child, you can't sign the contract.");

            if (n.MinAge > (DateTime.Now - cc.Birth).TotalDays / 30)
                throw new Exception("The child is too young for this nanny, you can't sign the contract.");

            if (n.MaxAge  < (DateTime.Now - cc.Birth).TotalDays / 30)
                throw new Exception("The child is too old for this nanny, you can't sign the contract.");

            //  call a function to define the salary for the contract
            DefineSalary(c);
            if(c.Signed == false)
                n.CountChild++;
            c.Signed = true;
            
        }

        /// <summary>
        /// Define the salary according to conditions
        /// </summary>
        /// <param name="c"></param>
        public void DefineSalary(Contract c)
        {
            Child cc = GetAllChild(x => x.ID == c.ChildID).FirstOrDefault();
            Nanny n = GetAllNanny(x => x.ID == c.NannyID).FirstOrDefault();
            Mother m = GetAllMother(x => x.ID == cc.MotherID).FirstOrDefault();

            int num = NumContract(cc, n); 

            switch (c.Payment)
            {
                case PaymentType.Hour:
                    //  if the payment type is hourly
                    if (n.SalaryType != c.Payment)
                        throw new Exception("The Payment Type for this nanny must be per month");
                    c.SalaryPerHour = n.Salary;
                    c.SalaryPerHour *= Math.Pow(0.98, num);
                    c.Salary = 0;
                    for (int i = 0; i < 7; i++)
                    {

                        if (m.Request.P.Plan[i].Selected)
                        {
                            TimeSpan t = m.Request.P.Plan[i].End - m.Request.P.Plan[i].Start;
                            c.Salary += t.TotalHours * 4 * c.SalaryPerHour;
                        }
                    }

                    break;


                case PaymentType.Month:
                    //  if the payment type is monthly
                    if (n.SalaryType != c.Payment)
                        throw new Exception("The PaymentType for this nanny must be per hour");
                    c.SalaryPerMonth = n.Salary;
                    c.SalaryPerMonth *= Math.Pow(0.98, num-1);
                    c.Salary = c.SalaryPerMonth;
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// Check how much child the nanny take care of, for a specific mother
        /// </summary>
        /// <param name="child"></param>
        /// <param name="nanny"></param>
        /// <returns></returns>
        public int NumContract(Child child, Nanny nanny)
        {
            Child cc;
            int num = 0;

            foreach (Contract c in GetAllContract())
            {
                cc = GetAllChild(x => x.ID == c.ChildID).FirstOrDefault();
                if (c.Signed && child.MotherID == cc.MotherID && c.NannyID == nanny.ID)
                    num++;
            }
            return num;
        }

        ///// <summary>
        ///// Print all the nannies which corresponds to the mother request
        ///// </summary>
        ///// <param name="l"></param>
        //public void AllNearNannies(IEnumerable<Nanny> l)
        //{
            
           
        //    Console.WriteLine("\n**********************************************************\n");
        //    foreach (Nanny n in l)
        //    {
        //        Console.WriteLine(n.ID + "\t" +n.FamilyName + "\t" + n.FirstName);
        //    }

        //    Console.WriteLine("\n**********************************************************");
        //}   

        /// <summary>
        /// function to calculate with google maps the distance between two adresses
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public int Distance(string src, string dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = src,
                Destination = dest,
            };
            DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = drivingDirections.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value;

        }


        
        /// <summary>
        /// group the nanny by the maximun or minimum ages of their child 
        /// </summary>
        /// <param name="maxormin"></param>
        public IEnumerable<IGrouping<int,string>> GroupAges(bool min)
        {

            switch (min)
            {
                case true:  //  if we want grouping according to the minimum age  
                    return from nanny in dal.GetAllNanny()
                           let str = nanny.SimplePresentation
                           orderby (int)(nanny.MinAge / 3) * 3 + 3
                           group str by (int)(nanny.MinAge / 3) * 3 + 3;
                    
                case false: //  if we want grouping according to the max age
                    return from nanny in dal.GetAllNanny()
                           let str = nanny.SimplePresentation
                           orderby (int)(nanny.MaxAge / 3) * 3 + 3
                           group str by (int)(nanny.MaxAge / 3) * 3 + 3;

                default:
                    return null;
            }
           
        }



        /// <summary>
        /// Return a list of nannies which satisfy the mother conditions
        /// </summary>
        /// <param name="R"></param>
        /// <returns></returns>
        public List<Nanny> PlanningAccordance(MotherRequest r)
        {
            //  the new list to return
            List<Nanny> ln = new List<Nanny>();
            List<CoupleMotherNanny> lcmn = new List<CoupleMotherNanny>();

            //  create a list of couple mother nanny and sort it
            foreach (Nanny n in GetAllNanny())
                lcmn.Add(new CoupleMotherNanny(r, n));
            lcmn.Sort();

            //  check existence of perfect nanny
            for (int i = 0; i < lcmn.Count; i++)
            {
                if (lcmn[i].AbsoluteConcordance)
                    ln.Add(lcmn[i].N);
            }

            if (ln.Count == 0) //if the list is empty
                ln = NearNanny(lcmn);

            return ln;
        }

        /// <summary>
        /// Function to return a list of the five nearest nannies
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public List<Nanny> NearNanny(List<CoupleMotherNanny> lcmn)
        {
            //  if call this function, there is no perfect nanny

            List<Nanny> ln = new List<Nanny>();
            for (int i = 0; i < lcmn.Count && i < 5; i++)
            {
                ln.Add(lcmn[i].N);
            }
            return ln;

        }

        /// <summary>
        /// function to show all the child without nanny yet
        /// </summary>
        /// <returns></returns> list of the childs without nanny yet
        public List<Child> NoNanny()
        {
            List<Child> l = new List<Child>();
            List<Child> ch = (List < Child >) GetAllChild();
            List<Contract> c = (List < Contract > )GetAllContract();
            foreach (Child child in ch)
            {
                if (!c.Exists(contract => contract.ChildID == child.ID))
                    l.Add(child);
            }

            l.Sort();
            return l;
        }

        /// <summary>
        /// function to show all the nanny who work with the vacation of the ministery of work 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Nanny> VacationWithMinisteryOfWork()
        {
            return from n
                   in GetAllNanny(n=> n.Vacation == VacationType.Work)
                   select n;

        }

        /// <summary>
        /// function to show all the nanny who work with the vacation of the ministery of education  
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Nanny> VacationWithMinisteryOfEducation()
        {
            return from n
                   in GetAllNanny()
                   where n.Vacation == VacationType.Education
                   select n;

        }


        /// <summary>
        /// function to show all the nanny without contract yet 
        /// </summary>
        /// <returns></returns>the list of the nannies without contact  to take care  of a child 
        public List<Nanny> NoChild()
        {
            List<Nanny> l = new List<Nanny>();

            List<Contract> lc = (List < Contract >) GetAllContract();

            foreach (Nanny n in GetAllNanny())
            {
                if (!lc.Exists(x => x.NannyID == n.ID && x.Signed == true))
                    l.Add(n);
            }

            l.Sort();
            return l;
        }


        /// <summary>
        /// Function to show allthe contracts in the list that are  not signed yet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contract> NotSigned()
        {
            return from c in GetAllContract()
                   where c.Signed == false
                   select c;
        }

        /// <summary>
        /// function to show allthe contracts in the list that are signed 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contract> SignedContracts()
        {
            return from c in GetAllContract()
                   where c.Signed == true
                   select c;
        }


        ///// <summary>
        ///// To print out all the signed contract
        ///// </summary>
        //public void AllContractsSigned()
        //{
        //    var n = SignedContracts();
        //    foreach (Contract item in n)
        //        Console.WriteLine(item);
        //}


        /// <summary>
        /// Function to show the list of the nanny that the address corresponds to the request of the mother 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public List<Nanny> GoodDistance(MotherRequest m)
         {
             List<Nanny> l = new List<Nanny>();
             foreach (Nanny n in GetAllNanny())
             {
                 //  if the mother asked for a specific address
                 if (m.SearchAddress != "")
                 {
                     //  if distance between mother's address and mother' search address 
                     //  bigger than distance between mother's and nanny's address
                     //  OR
                     //  distance between mother's search address and nanny's address 
                     //  lower than mother's accepted distance
                     if (Distance(m.Address, m.SearchAddress) > Distance(m.Address, n.Address)
                         || Distance(m.SearchAddress, n.Address) <= m.DistanceAccepted)
                         l.Add(n);
                 }

                 else if (Distance(m.Address, n.Address) <= m.DistanceWanted)
                     l.Add(n);
             }

             return l;
         }


        public delegate bool CheckConditionForContract(Contract c);
        /// <summary>
        /// Return all the contracts which satisfy a specific condition
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public IEnumerable<Contract> FindAll(CheckConditionForContract condition)
        {
            return from c in GetAllContract()
                   where condition(c)
                   select c;
        }


        /// <summary>
        /// function for counting the number of contract with a specific condition 
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public int NumFindAll(CheckConditionForContract condition)
        {
            int num = 0;
            foreach (var c in GetAllContract())
            {
                if (condition(c))
                    num++;
            }
            return num;
        }


        /// <summary>
        /// Group contract according to the distance between mother request and nanny
        /// </summary>
        public IEnumerable<IGrouping<int, string>> GroupDistance()
        { 
            return from contract in dal.GetAllContract()
                   where contract.Signed
                   let str = contract.SimplePresentation
                   orderby 200 * (contract.Distance / 200) + 200
                   group str by 200*(contract.Distance / 200) + 200;

        }
    }
}

        #endregion

    


    
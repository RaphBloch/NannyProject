using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace DAL
{
    
    public class DAL_XML : IDAL
    {
        XElement childRoot;
        string childPath = @"childXml.xml";

        XElement motherRoot;
        string motherPath = @"motherXml.xml";

        XElement nannyRoot;
        string nannyPath = @"nannyXml.xml";

        XElement contractRoot;
        string contractPath = @"contractXml.xml";

        XElement configRoot;
        string configPath = @"config.xml";

        int contractID;

        public DAL_XML()
        {
            if (!File.Exists(childPath) 
                || !File.Exists(motherPath)
                || !File.Exists(nannyPath)
                || !File.Exists(contractPath)
                || !File.Exists(configPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            childRoot = new XElement("children");
            childRoot.Save(childPath);

            nannyRoot = new XElement("nannies");
            nannyRoot.Save(nannyPath);

            motherRoot = new XElement("mothers");
            motherRoot.Save(motherPath);

            contractRoot = new XElement("contracts");
            contractRoot.Save(contractPath);

            configRoot = new XElement("configuration",
                new XElement("ContractID", "1")); // the contractid template
            configRoot.Save(configPath);
        }

        private void LoadData()
        {
            try
            {
                childRoot = XElement.Load(childPath);
                nannyRoot = XElement.Load(nannyPath); 
                motherRoot = XElement.Load(motherPath);
                contractRoot = XElement.Load(contractPath);
                configRoot = XElement.Load(configPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        #region Conversion regions

        XElement ConvertChild(Child c)
        {
            return new XElement("Child",
                new XElement("ID", c.ID.ToString()),
                new XElement("FirstName", c.FirstName),
                new XElement("FamilyName", c.FamilyName),
                new XElement("MotherID", c.MotherID.ToString()),
                new XElement("Birth", c.Birth.ToShortDateString()),
                new XElement("SpecialNeeds", c.SpecialNeeds.ToString()),
                new XElement("Needs", c.Needs));
        }
        Child ConvertChild(XElement element)
        {
            Child c = new Child();

            c.ID = (from x in element.Elements()
                    where x.Name == "ID"
                    select Convert.ToInt32(x.Value)).FirstOrDefault();

            c.MotherID = (from x in element.Elements()
                          where x.Name == "MotherID"
                          select Convert.ToInt32(x.Value)).FirstOrDefault();

            c.FirstName = (from x in element.Elements()
                           where x.Name == "FirstName"
                           select (x.Value)).FirstOrDefault();


            c.FamilyName = (from x in element.Elements()
                            where x.Name == "FamilyName"
                            select (x.Value)).FirstOrDefault();

            c.Birth = (from x in element.Elements()
                       where x.Name == "Birth"
                       select (DateTime.Parse(x.Value))).FirstOrDefault();

            c.SpecialNeeds = (from x in element.Elements()
                              where x.Name == "SpecialNeeds"
                              select (Convert.ToBoolean(x.Value))).FirstOrDefault();

            c.Needs = (from x in element.Elements()
                              where x.Name == "Needs"
                              select (x.Value)).FirstOrDefault();

            return c;
        }

        XElement ConvertContract(Contract c)
        {
            return new XElement("Contract",
                new XElement("ID", c.ID.ToString()),
                new XElement("ChildID", c.ChildID.ToString()),
                new XElement("ChildName", c.ChildName),
                new XElement("NannyID", c.NannyID.ToString()),
                new XElement("NannyName", c.NannyName),
                new XElement("Distance", c.Distance.ToString()),
                new XElement("Meet", c.Meet.ToString()),
                new XElement("Signed", c.Signed.ToString()),
                new XElement("Salary", String.Format("{0:0.00}", c.Salary)),
                new XElement("Payment", c.Payment.ToString()),
                new XElement("SalaryPerMonth", String.Format("{0:0.00}", c.SalaryPerMonth)),
                new XElement("SalaryPerHour", String.Format("{0:0.00}", c.SalaryPerHour)),
                new XElement("IsSalaryPerHour", c.IsSalaryPerHour.ToString()),
                new XElement("Begin", c.Begin.ToShortDateString()),
                new XElement("End", c.End.ToShortDateString()));
        }
        Contract ConvertContract(XElement element)
        {
            Contract contract = new Contract();

            contract.ID = (from x in element.Elements()
                          where x.Name == "ID"
                          select int.Parse(x.Value)).FirstOrDefault();

            contract.ChildID = (from x in element.Elements()
                                where x.Name == "ChildID"
                                select int.Parse(x.Value)).FirstOrDefault();

            contract.ChildName = (from x in element.Elements()
                                where x.Name == "ChildName"
                                select x.Value).FirstOrDefault();

            contract.NannyID = (from x in element.Elements()
                                where x.Name == "NannyID"
                                select int.Parse(x.Value)).FirstOrDefault();

            contract.NannyName = (from x in element.Elements()
                                where x.Name == "NannyName"
                                select x.Value).FirstOrDefault();

            contract.Distance = (from x in element.Elements()
                                 where x.Name == "Distance"
                                 select Convert.ToInt32(x.Value)).FirstOrDefault();

            contract.Meet = (from x in element.Elements()
                             where x.Name == "Meet"
                             select Convert.ToBoolean(x.Value)).FirstOrDefault();

            contract.Signed = (from x in element.Elements()
                               where x.Name == "Signed"
                               select Convert.ToBoolean(x.Value)).FirstOrDefault();

            contract.IsSalaryPerHour = (from x in element.Elements()
                             where x.Name == "IsSalaryPerHour"
                             select Convert.ToBoolean(x.Value)).FirstOrDefault();

            contract.Salary = (from x in element.Elements()
                               where x.Name == "Salary"
                               select Convert.ToDouble(x.Value)).FirstOrDefault();

            contract.SalaryPerHour = (from x in element.Elements()
                               where x.Name == "SalaryPerHour"
                               select Convert.ToDouble(x.Value)).FirstOrDefault();

            contract.SalaryPerMonth = (from x in element.Elements()
                               where x.Name == "SalaryPerMonth"
                               select Convert.ToDouble(x.Value)).FirstOrDefault();

            contract.Payment = (from x in element.Elements()
                                where x.Name == "Payment"
                                select x.Value).FirstOrDefault() == "Hour" ? PaymentType.Hour : PaymentType.Month;

            contract.Begin = (from x in element.Elements()
                              where x.Name == "Begin"
                              select DateTime.Parse(x.Value)).FirstOrDefault();

            contract.End = (from x in element.Elements()
                              where x.Name == "End"
                              select DateTime.Parse(x.Value)).FirstOrDefault();


            return contract;
        }

        XElement ConvertPlanning(Planning p)
        {
            XElement newElement = new XElement("Planning");
            for (int i = 0; i < 7; i++)
            {

                XElement select = new XElement("Selected", p.Plan[i].Selected.ToString());
                XElement day = new XElement("Name", p.Plan[i].Day);
                XElement start = new XElement("Start", p.Plan[i].Start.ToShortTimeString());
                XElement end = new XElement("End", p.Plan[i].End.ToShortTimeString());

                newElement.Add(new XElement(String.Format("Day{0}", i + 1), select, day, start, end));
            }

            return newElement;
        }
        Planning ConvertPlanning(XElement element)
        {
            Planning p = new Planning();

            for (int i = 0; i < 7; i++)
            {
                p.Plan[i].Selected = (from X in element.Elements()
                                      where X.Name == String.Format("Day{0}", i + 1)
                                      select Convert.ToBoolean(X.Element("Selected").Value)
                                      ).FirstOrDefault();

                p.Plan[i].Day = (from X in element.Elements()
                                 where X.Name == String.Format("Day{0}", i + 1)
                                 select (X.Element("Name").Value)).FirstOrDefault();

                p.Plan[i].Start = (from X in element.Elements()
                                   where X.Name == String.Format("Day{0}", i + 1)
                                   select DateTime.Parse(X.Element("Start").Value)).FirstOrDefault();

                p.Plan[i].End = (from X in element.Elements()
                                 where X.Name == String.Format("Day{0}", i + 1)
                                 select DateTime.Parse(X.Element("End").Value)).FirstOrDefault();
            }
            return p;
        }

        XElement ConvertMotherRequest(MotherRequest m)
        {
            return new XElement("MotherRequest",
                new XElement("Address", m.Address),
                new XElement("SearchAddress", m.SearchAddress),
                new XElement("IsSearchAddress", m.IsSearchAddress.ToString()),
                new XElement("DistanceWanted", m.DistanceWanted),
                new XElement("DistanceAccepted", m.DistanceAccepted),
                new XElement(ConvertPlanning(m.P)));

        }
        MotherRequest ConvertMotherRequest(XElement element)
        {
            MotherRequest m = new MotherRequest();

            m.Address = (from x in element.Elements()
                         where x.Name == "Address"
                        select x.Value).FirstOrDefault();

            m.SearchAddress = (from x in element.Elements()
                               where x.Name == "SearchAddress"
                               select x.Value).FirstOrDefault();

            m.DistanceAccepted = (from x in element.Elements()
                                  where x.Name == "DistanceAccepted"
                                  select Convert.ToInt32(x.Value)).FirstOrDefault();

            m.DistanceWanted = (from x in element.Elements()
                                where x.Name == "DistanceWanted"
                                select Convert.ToInt32(x.Value)).FirstOrDefault();

            m.P = (from x in element.Elements()
                   where x.Name=="Planning"
                  select ConvertPlanning(x)).FirstOrDefault();

            return m;
        }

        XElement ConvertMother(Mother m)
        {
            return new XElement("Mother",
                new XElement("ID", m.ID.ToString()),
                new XElement("FirstName", m.FirstName),
                new XElement("FamilyName", m.FamilyName),
                new XElement("Phone", m.Phone),
                new XElement("Commentaries", m.Commentaries),
                new XElement(ConvertMotherRequest(m.Request)));
        }
        Mother ConvertMother(XElement element)
        {
            Mother m = new Mother();
            
            m.ID = (from x in element.Elements()
                    where x.Name == "ID"
                    select Convert.ToInt32(x.Value)).FirstOrDefault();

            m.FirstName = (from x in element.Elements()
                           where x.Name == "FirstName"
                            select (x.Value)).FirstOrDefault();


            m.FamilyName = (from x in element.Elements()
                            where x.Name == "FamilyName"
                            select (x.Value)).FirstOrDefault();

            m.Phone = (from x in element.Elements()
                       where x.Name == "Phone"
                       select (x.Value)).FirstOrDefault();

            m.Commentaries = (from x in element.Elements()
                              where x.Name == "Commentaries"
                              select (x.Value)).FirstOrDefault();

            m.Request= (from x in element.Elements()
                        where x.Name=="MotherRequest"
                    select ConvertMotherRequest(x)).FirstOrDefault();


            return m;
        }

        XElement ConvertNanny(Nanny n)
        {
            return new XElement("Nanny",
                new XElement("ID", n.ID.ToString()),
                new XElement("FirstName", n.FirstName),
                new XElement("FamilyName", n.FamilyName),
                new XElement("Address", n.Address),
                new XElement("Phone", n.Phone),
                new XElement("Birth", n.Birth.ToShortDateString()),
                new XElement("Elevator", n.Elevator.ToString()),
                new XElement("MaxAge", n.MaxAge.ToString()),
                new XElement("MinAge", n.MinAge.ToString()),
                new XElement("MaxChildren", n.MaxChilds.ToString()),
                new XElement("CountChild", n.CountChild.ToString()),
                new XElement("Vacation", n.Vacation.ToString()),
                new XElement("SalaryType", n.SalaryType.ToString()),
                new XElement("Seniority", n.Seniority.ToString()),
                new XElement("Salary", String.Format("{0:0.00}", n.Salary)),
                new XElement("Recommandation", n.Recommandation),
                new XElement("Floor", n.Floor.ToString()),
                new XElement(ConvertPlanning(n.P)));
        }
        Nanny ConvertNanny(XElement element)
        {
            Nanny n = new Nanny();
            n.ID = (from x in element.Elements()
                    where x.Name == "ID"
                    select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.FirstName = (from x in element.Elements()
                           where x.Name == "FirstName"
                           select (x.Value)).FirstOrDefault();


            n.FamilyName = (from x in element.Elements()
                            where x.Name == "FamilyName"
                            select (x.Value)).FirstOrDefault();

            n.Address = (from x in element.Elements()
                         where x.Name == "Address"
                         select (x.Value)).FirstOrDefault();


            n.Phone = (from x in element.Elements()
                       where x.Name == "Phone"
                       select (x.Value)).FirstOrDefault();

            n.Recommandation = (from x in element.Elements()
                                where x.Name =="Recommandation"
                              select (x.Value)).FirstOrDefault();


            n.MinAge = (from x in element.Elements()
                        where x.Name == "MinAge"
                    select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.MaxAge = (from x in element.Elements()
                        where x.Name == "MaxAge"
                        select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.MaxChilds = (from x in element.Elements()
                           where x.Name == "MaxChildren"
                           select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.CountChild = (from x in element.Elements()
                            where x.Name == "CountChild"
                            select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.Floor = (from x in element.Elements()
                       where x.Name == "Floor"
                       select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.Seniority= (from x in element.Elements()
                          where x.Name == "Seniority"
                          select Convert.ToInt32(x.Value)).FirstOrDefault();

            n.Elevator = (from x in element.Elements()
                          where x.Name == "Elevator"
                          select Convert.ToBoolean(x.Value)).FirstOrDefault();

            n.Birth= (from x in element.Elements()
                      where x.Name == "Birth"
                      select Convert.ToDateTime(x.Value)).FirstOrDefault();

            n.Salary = (from x in element.Elements()
                        where x.Name == "Salary"
                        select Convert.ToDouble(x.Value)).FirstOrDefault();

            n.SalaryType = (from x in element.Elements()
                            where x.Name == "SalaryType"
                            select (x.Value == "Hour") ? 
                            PaymentType.Hour : PaymentType.Month).
                            FirstOrDefault();


            n.Vacation = (from x in element.Elements()
                          where x.Name == "Vacation"
                          select (x.Value == "Work") ?
                            VacationType.Work : VacationType.Education).
                            FirstOrDefault();

            n.P = (from x in element.Elements()
                   where x.Name == "Planning"
                   select ConvertPlanning(x)).FirstOrDefault();

            return n;

        }
        #endregion

        #region Child region

        public void AddChild(Child c)
        {

            //if a such child already exists
            if (GetAllChild(x=>x.ID == c.ID).FirstOrDefault() != null)
                throw new Exception("A child with the same ID already exists.");

            if (c.ID == 0)
                throw new Exception("Please select a valid ID for the child (not 0)");

            //if the mother of this child doesn't exist
            if (GetAllMother(x => x.ID == c.MotherID).FirstOrDefault() == null)
                throw new Exception("The mother with the ID specified for this child doesn't exist.");

            c.FamilyName = GetAllMother(x => x.ID == c.MotherID).FirstOrDefault().FamilyName;

            childRoot.Add(ConvertChild(c));
            childRoot.Save(childPath);
        }

        public void UpdateChild(Child c)
        {

            if (GetAllChild(x => x.ID == c.ID).FirstOrDefault() == null)
                throw new Exception("There is no such child.");

            //  remove the child before updating it
            (from item in childRoot.Elements()
             where int.Parse(item.Element("ID").Value) == c.ID
             select item).FirstOrDefault().Remove();

            //implement the update according to the demand
            childRoot.Add(ConvertChild(c));
            childRoot.Save(childPath);
        }

        public void DeleteChild(int id)
        {
            XElement toRemove = (from item in childRoot.Elements()
                                 where int.Parse(item.Element("ID").Value) == id
                                 select item).FirstOrDefault();

            if (toRemove == null)
                throw new Exception("There is no such child.");

            toRemove.Remove();
            childRoot.Save(childPath);

        }

        public IEnumerable<Child> GetAllChild(Func<Child, bool> predicat = null)
        {
            if (predicat == null)
            {
                return from item in childRoot.Elements()
                       select ConvertChild(item);
            }
            return from item in childRoot.Elements()
                   let c = ConvertChild(item)
                   where predicat(c)
                   select c;
        }




        #endregion

        #region Mother region

        public void DeleteMother(int id)
        {

            if (GetAllMother(x => x.ID == id).FirstOrDefault() == null)
                throw new Exception("There is no such mother.");

            
            foreach (Child child in GetAllChild(x=>x.MotherID == id))
                DeleteChild(child.ID);

            XElement toRemove = (from x in motherRoot.Elements()
                                where x.Element("ID").Value == id.ToString()
                                select x).FirstOrDefault();
            toRemove.Remove();
            motherRoot.Save(motherPath);
        }

        public void UpdateMother(Mother m)
        {

            if (GetAllMother(n => n.ID == m.ID).FirstOrDefault() == null)
                throw new Exception("There is no such mother.");

            XElement toRemove = (from x in motherRoot.Elements()
                                 where x.Element("ID").Value == m.ID.ToString()
                                 select x).FirstOrDefault();
            toRemove.Remove();
            motherRoot.Add(ConvertMother(m));
            motherRoot.Save(motherPath);

        }

        public IEnumerable<Mother> GetAllMother(Func<Mother, bool> predicate = null)
        {

            if (predicate == null)
            {
                return from item in motherRoot.Elements()
                       select ConvertMother(item);
            }
            return from item in motherRoot.Elements()
                   let m = ConvertMother(item)
                   where predicate(m)
                   select m;
        }

        public void AddMother(Mother m)
        {

            if(GetAllMother(x=>x.ID == m.ID).FirstOrDefault() != null)
                throw new Exception("A mother with the same ID already exists.");

            motherRoot.Add(ConvertMother(m));
            motherRoot.Save(motherPath);
        }

        #endregion

        #region Nanny region

        public void AddNanny(Nanny n)
        {
            if (GetAllNanny(x => x.ID == n.ID).FirstOrDefault() != null)
                throw new Exception("A nanny with the same ID already exists.");

            nannyRoot.Add(ConvertNanny(n));
            nannyRoot.Save(nannyPath);
        }

        public IEnumerable<Nanny> GetAllNanny(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in nannyRoot.Elements()
                       select ConvertNanny(item);
            }
            return from item in nannyRoot.Elements()
                   let n = ConvertNanny(item)
                   where predicate(n)
                   select n;
        }

        public void UpdateNanny(Nanny n)
        {

            if (GetAllNanny(x => x.ID == n.ID).FirstOrDefault() == null)
                throw new Exception("There is no such nanny.");

            XElement toRemove = (from x in nannyRoot.Elements()
                                 where x.Element("ID").Value == n.ID.ToString()
                                 select x).FirstOrDefault();
            toRemove.Remove();
            nannyRoot.Add(ConvertNanny(n));
            nannyRoot.Save(nannyPath);

        }

        public void DeleteNanny(int id)
        {

            if (GetAllNanny(n => n.ID == id).FirstOrDefault() == null)
                throw new Exception("There is no such nanny.");

            foreach (Contract item in GetAllContract(x=>x.NannyID == id))
                DeleteContract(item.ID);

            XElement toRemove = (from x in nannyRoot.Elements()
                                where x.Element("ID").Value == id.ToString()
                                select x).FirstOrDefault();
            toRemove.Remove();
            nannyRoot.Save(nannyPath);
        }

        #endregion

        #region Contract region

        public void AddContract(Contract contract)
        {

            //  if the nanny or/and the child or/and in the contract are not in our database
            if (GetAllChild(x => x.ID == contract.ChildID).FirstOrDefault() == null)
                throw new Exception("No such child exists to create this contract");
            if (GetAllNanny(x => x.ID == contract.NannyID).FirstOrDefault() == null)
                throw new Exception("No such nanny exists to create this contract");

            //  if the mother doesn't exist in our database
            Child cc = GetAllChild(x => x.ID == contract.ChildID).FirstOrDefault();
            if (GetAllMother(x => x.ID == cc.MotherID).FirstOrDefault() == null)
                throw new Exception("No such mother exists to create this contract");

            contractID = (from x in configRoot.Elements()
                          where x.Name == "ContractID"
                         select Convert.ToInt32(x.Value)).FirstOrDefault();
            contract.ID = contractID;
            XElement toRemove = (from x in configRoot.Elements()
                                 where x.Name == "ContractID"
                                 select x).FirstOrDefault();
            toRemove.Remove();
            contractID++;
            configRoot.Add(new XElement("ContractID", contractID.ToString()));
            configRoot.Save(configPath);

            contract.ChildName = cc.FamilyName;
            contract.NannyName = GetAllNanny(x => x.ID == contract.NannyID).FirstOrDefault().FamilyName;

            contractRoot.Add(ConvertContract(contract));
            contractRoot.Save(contractPath);
        }

        public void DeleteContract(int id)
        {
            XElement toRemove = (from item in contractRoot.Elements()
                                 where int.Parse(item.Element("ID").Value) == id
                                 select item).FirstOrDefault();

            Contract c = GetAllContract(x => x.ID == id).First();
            Nanny nannywithcontract = (Nanny)GetAllNanny().Where(x => x.ID == c.NannyID).First();
            if (c.Signed)
                nannywithcontract.CountChild--;
            UpdateNanny(nannywithcontract);

            

            if (toRemove == null)
                throw new Exception("There is no such contract.");

            toRemove.Remove();
            contractRoot.Save(contractPath);
        }

        public void UpdateContract(Contract c)
        {

            if (GetAllContract(n => n.ID == c.ID).FirstOrDefault() == null)
                throw new Exception("That contract doesn't match any contract of our database.");

            XElement toRemove = (from item in contractRoot.Elements()
                                 where int.Parse(item.Element("ID").Value) == c.ID
                                 select item).FirstOrDefault();
            toRemove.Remove();

            contractRoot.Add(ConvertContract(c));
            contractRoot.Save(contractPath);
        }

        public IEnumerable<Contract> GetAllContract(Func<Contract, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in contractRoot.Elements()
                       select ConvertContract(item);
            }
            return from item in contractRoot.Elements()
                   let c = ConvertContract(item)
                   where predicate(c)
                   select c;
        }

        #endregion

    }
}

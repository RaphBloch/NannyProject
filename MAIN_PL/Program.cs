using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace MAIN_PL
{
    /// <summary>
    /// To test our program
    /// </summary>
    class Program
    {

        static BL_imp bl; //= new BL_imp(); //FactoryBL.GetBL();

        /// <summary>
        /// The main test program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bl = new BL.BL_imp();
            int choice = Actions();
            

            while(choice!=0)
            {
                switch (choice)
                {
                    case 1:
                        //  add a nanny
                        AddNanny();
                        break;

                    case 2:
                        //  add a mother
                        AddMother();
                        break;

                    case 3:
                        //  add a child
                        AddChild();
                        break;

                    case 4:
                        //  add a contract
                        CreateContract();
                        break;

                    case 5:
                        //  see our complete data
                        PrintData();
                        break;


                    default:
                        break;
                }

                choice = Actions();
            }
            
        }

        /// <summary>
        /// See the complete data
        /// </summary>
        private static void PrintData()
        {
            Console.WriteLine("\n************************************************");
            Console.WriteLine("Mother's list:");
            Console.WriteLine(DS.DataSource.MothersToString());
            Console.WriteLine("************************************************\n");

            Console.WriteLine("\n************************************************");
            Console.WriteLine("Nanny's list:");
            Console.WriteLine(DS.DataSource.NanniesToString());
            Console.WriteLine("************************************************\n");

            Console.WriteLine("\n************************************************");
            Console.WriteLine("Child's list:");
            Console.WriteLine(DS.DataSource.ChildToString());
            Console.WriteLine("************************************************\n");

            Console.WriteLine("\n************************************************");
            Console.WriteLine("Contract's list:");
            Console.WriteLine(DS.DataSource.ContractsToString());
            Console.WriteLine("************************************************\n");
        }

        /// <summary>
        /// To choose what to do
        /// </summary>
        /// <returns></returns>
        static int Actions()
        {
            Console.WriteLine("====================================================================");
            Console.WriteLine("Welcome to our testing program");
            Console.WriteLine("====================================================================\n");
            Console.WriteLine("Do you want to:");
            Console.WriteLine("\t0) Exit");
            Console.WriteLine("\t1) Add a Nanny");
            Console.WriteLine("\t2) Add a Mother");
            Console.WriteLine("\t3) Add a Child");
            Console.WriteLine("\t4) Add a Contract");
            Console.WriteLine("\t5) See Data");

            Console.Write("Your choice: ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine("====================================================================\n");
            return i;
        }


        /// <summary>
        /// To change ID for each item
        /// </summary>
        /// <param name="id"></param>
        static void ChangeID(ref int id)
        {
            Console.WriteLine("Do you want to change the ID? (y/n)");
            string key = Console.ReadLine();
            switch (key)
            {
                case "Y":
                    Console.WriteLine("Choose an other ID: ");
                    id = int.Parse(Console.ReadLine());
                    break;

                case "y":
                    Console.WriteLine("Choose an other ID: ");
                    id = int.Parse(Console.ReadLine());
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Create a nanny (random or not)
        /// </summary>
        static void AddNanny()
        {
            Nanny n = new Nanny();
            Console.WriteLine("\n=============================================================");
            Console.WriteLine("The new Nanny:");
            Console.WriteLine("=============================================================");
            Console.WriteLine(n);
            Console.WriteLine("=============================================================\n");

            int help = n.ID;
            ChangeID(ref help);
            n.ID = help;

            Console.WriteLine("=============================================================\n");


            try
            {
                bl.AddNanny(n);
            }
            catch(Exception e)
            {
                Console.WriteLine("\n***************************************");
                Console.WriteLine(e.Message);
                Console.WriteLine("***************************************\n");
            }
            
            
        }


        /// <summary>
        /// Create a mother (random or not)
        /// </summary>
        static void AddMother()
        {
            Mother n = new Mother();
            Console.WriteLine("\n=============================================================");
            Console.WriteLine("The new Mother:");
            Console.WriteLine("=============================================================");
            Console.WriteLine(n);
            Console.WriteLine("=============================================================\n");

            int help = n.ID;
            ChangeID(ref help);
            n.ID = help;
            Console.WriteLine("=============================================================\n");

            try
            {
                bl.AddMother(n);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n***************************************");
                Console.WriteLine(e.Message);
                Console.WriteLine("***************************************\n");
            }
        }

        /// <summary>
        /// Create a child
        /// </summary>
        static void AddChild()
        {
            Child n = new Child();
            Console.WriteLine("\n=============================================================");
            Console.WriteLine("The new Child:");
            Console.WriteLine("=============================================================");
            Console.WriteLine(n);
            Console.WriteLine("=============================================================\n");

            Console.WriteLine("Do you want to change the mother ID? (y/n)");
            string key = Console.ReadLine();

            switch (key)
            {
                case "Y":
                    Console.Write("Choose an other mother ID: ");
                    n.MotherID = int.Parse(Console.ReadLine());
                    break;

                case "y":
                    Console.Write("Choose an other mother ID: ");
                    n.MotherID = int.Parse(Console.ReadLine());
                    break;

                default:
                    break;
            }

            Console.WriteLine("=============================================================\n");

            try
            {
                bl.AddChild(n);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n**************************************************************");
                Console.WriteLine(e.Message);
                Console.WriteLine("**************************************************************\n");
            }
        }


        /// <summary>
        /// Create new contract
        /// </summary>
        static void CreateContract()
        {
            Console.Write("Choose your Nanny's ID: ");
            int id1 = int.Parse(Console.ReadLine());

            Console.Write("Choose your child's ID: ");
            int id2 = int.Parse(Console.ReadLine());

            Contract c = new Contract(id1, id2);

            try
            {
                bl.AddContract(c);
                //  print the contract
                Console.WriteLine("\nYour new Contract");
                Console.WriteLine(c);
                Console.WriteLine("=============================================");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n**********************************************************");
                Console.WriteLine(e.Message);
                Console.WriteLine("**********************************************************\n");
            }
        }
    }
}

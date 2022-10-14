using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YouTubeDemo;

namespace YouTubeDemo
{
    internal class Program
    {
        static void Main(string[] args)        // interface genellikle iş yapan sınıfların (CustomerManager ve CreditManager) opearasyonlarını sadece imza seviyesinde yazarak yazılımda bağımlılığı azaltmaya yarayan çalışmalar
        {
            /*CreditManager creditManager = new CreditManager();
            creditManager.Calculate();
            creditManager.Save();

            Customer customer = new Customer();     //örneğini oluşturmak, instance oluşturmak, instance creation
            customer.Id = 1;
            customer.City = "Ankara";

            

            CustomerManager customerManager = new CustomerManager(customer);
            customerManager.Save();
            customerManager.Delete();

            Company company = new Company();
            company.TaxNumber = "10000";
            company.CompanyName = "Arçelik";
            company.Id = 100;
            

            CustomerManager customerManager2 = new CustomerManager(new Person());

            Person person = new Person();
            person.NationalIdentity = "";
            person.FirstName = "";

            Customer c1 = new Customer();
            Customer c2 = new Person();
            Customer c3 = new Company();  */  //customer company nin heap deki referansını tutabilir. 

            CustomerManager customerManager = new CustomerManager(new Customer(), new TeacherCreditManager());

            Console.ReadLine();
        }
    }

    class CreditManager                                       //class lar içinde fonksiyonları ( operasyonları) tutan bir ortam
    {                                                         // burada krediye yönelik işlemler tutuluyor
        public void Calculate(int creditType)                //bir sınıf sadece bir sınıfı implemente edebilir,bir sınıf birden fazla interface i implemente edebilir
        {
            Console.WriteLine("Hesaplandı");
        }

        public void Save()
        {
            Console.WriteLine("Kredi verildi");
        }

        interface ICreditManager
        {                                    // bu kodun anlamı şudur; kredi operasyonlarının içinde calculate ve save diye bir metot istiyorum
            void Calculate();
            void Save();       // bunlar imzadır yani metodun sadece ne döndürdüğü, ismi ve varsa parametreleri yazılır
        }

        abstract class BaseCreditManager : ICreditManager
        {
            public abstract void Calculate();       //calculate ortak değil her yerde değişken bu nedenle abstract halde bırakıldı, tamamlanmamış operasyon
            
            
            

            public virtual void Save()               //bu ortak olduğu için aynen yazıldı      //tamamlanmış operasyon => abstract class larda ikisi olmalı 
            {
                Console.WriteLine("Kaydedildi");
            }
            
        }

        class TeacherCreditManager : BaseCreditManager, ICreditManager  // bu class interface in operasyonlarını doldurmak zorundadır
        {
            public override void Calculate()
            {
                Console.WriteLine("Öğretmen kredisi hesaplandı");
            }

            public override void Save()                          //hem class ı hem de abstract sınıfı inherit etmek yok, abstract class lar ve interface ler asla new lenmez
            {
                base.Save();
            }

           
        }

        class MilitaryCreditManager : BaseCreditManager, ICreditManager      //ampule tıklayıp implement deyince throw lu kod gelir sonra kod silinir ve yazılması gereken kodlar yazılır
        {
            public override void Calculate()
            {
                Console.WriteLine("Asker kredisi hesaplandı");
            }

            
        }

        class CarCreditManager : BaseCreditManager, ICreditManager
        {
            public override void Calculate ()
            {
                Console.WriteLine("Araba kredisi hesaplandı");
            }

            
        }

        internal void Calculate()
        {
            throw new NotImplementedException();
        }
    }



    

    class Customer             //temel class
    {
        public Customer()                 //constructor
        {
            Console.WriteLine("müşteri nesnesi başlatıldı");
        }
        
        public int Id { get; set; }                       //bunlar property, nesneyi tanımlayan alanları tutar
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string City { get; set; }

    }


    class Company : Customer          //inheritance dır, java da extends yazılır
    {
       public string CompanyName { get; set; }
        public string TaxNumber {  get; set; }
    }

    class Person : Customer
    {
        public string NationalIdentity { get; set; }
    }

    //Katmanlı Mimariler
    class CustomerManager
    {
        private Customer _customer;                                                 // _ this anlamına geliyor 
        private CreditManager creditManager;
        private Customer customer;
        private TeacherCreditManager teacherCreditManager;

        public CustomerManager(Customer customer, ICreditManager creditManager)     //interface ler referans tiplerdir,  her kim ICreditManager ı implemente ediorsa military ve teacher ın referansını tutabilir
        {                                                                           // ICreditManager creditManager polimorfizm dir, çokbiçimlilik  
            _customer = customer;                                                   // interface ile diller arasında geçiş kolay olur
            this.creditManager = (CreditManager)creditManager;
        }

        public CustomerManager(Customer customer, TeacherCreditManager teacherCreditManager)
        {
            this.customer = customer;
            this.teacherCreditManager = teacherCreditManager;
        }

        public void Save()
        {
            Console.WriteLine("Müşteri kaydedildi ");
        }
        public void Delete()
        {
            Console.WriteLine("Müşteri silindi ");
        }

        public void GiveCredit()                         
        {
            creditManager.Calculate();                                     //GiveCredit fonk. farklı biçimlerde davranması sağlanıyor

            Console.WriteLine("Kredi Verildi");
        }
    }

    public interface ICreditManager
    {
    }
}

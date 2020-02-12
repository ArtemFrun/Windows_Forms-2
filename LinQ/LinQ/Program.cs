using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQ
{
    class Program
    {
        static void Main(string[] args)
        {
            IList <Student> students = new List<Student>()
           {
               new Student { name = "Sara", surname= "Mills", group = 1, age = 24, email = "smills@gmail.com", phone = "02435521", rating = new List<int>(){ 6, 6, 6, 5 }, FucNum = 215314},
               new Student { name = "Andrew", surname ="Gibson", group = 2, age = 21, email = "agibson@abv.bg", phone = "0895223344", rating = new List<int>(){ 3, 4, 5, 6  }, FucNum = 203115},
               new Student { name = "Craig", surname="Ellis", group = 1, age = 19, email = "cellis@cs.edu.gov", phone = "+3592667710", rating = new List<int>(){ 4, 2, 3, 4  }, FucNum = 203313},
               new Student { name = "Steven", surname="Cole", group = 2, age = 35, email = "themachine@abv.bg", phone = "3242133312", rating = new List<int>(){ 5, 6, 5, 5 }, FucNum = 203914},
               new Student { name = "Andrew", surname="Carter", group = 3, age = 15, email = "ac147@gmail.com", phone = "+001234532", rating = new List<int>(){ 5, 3, 4, 2  }, FucNum = 203814},
           };

            IList<StudentSpecialty> studentSpecialties = new List<StudentSpecialty>()
            {
                new StudentSpecialty { SpecialtyName = "Web Developer", FucNum = 203313},
                new StudentSpecialty { SpecialtyName = "Web Developer", FucNum = 203115},
                new StudentSpecialty { SpecialtyName = "PHP Developer", FucNum = 203814},
                new StudentSpecialty { SpecialtyName = "PHP Developer", FucNum = 203914},
                new StudentSpecialty { SpecialtyName = "QA Engineer", FucNum = 203313},
                new StudentSpecialty { SpecialtyName = "Web Developer", FucNum = 203914}
            };
           

            Console.WriteLine("--------------Group-----------------");


            var StudentByGroup = students.Where(s => s.group == 2);

            foreach(var st in StudentByGroup)
            {
                Console.WriteLine(st.name + " " + st.surname);
            }

            Console.WriteLine("--------------Lexicographically-----------------");

            var StudentByLexic = students.Where(s => 0 > string.Compare(s.name, s.surname));

            foreach (var st in StudentByLexic)
            {
                Console.WriteLine(st.name + " " + st.surname);
            }

            Console.WriteLine("--------------Age-----------------");

            var StudentByAge = students.Where(s => s.age >= 18).Where(s => s.age <= 24);

            foreach (var st in StudentByAge)
            {
                Console.WriteLine(st.name + " " + st.surname + " " + st.age);
            }

            Console.WriteLine("--------------Sort-----------------");

            var SortStudents = students.OrderBy(s => s.surname).ThenByDescending(s => s.name);

            foreach (var st in SortStudents)
            {
                Console.WriteLine(st.name + " " + st.surname + " ");
            }

            Console.WriteLine("--------------Email-----------------");

            var StudentByEmail = students.Where(s => s.email.EndsWith("@gmail.com"));

            foreach (var st in StudentByEmail)
            {
                Console.WriteLine(st.name + " " + st.surname + " ");
            }

            Console.WriteLine("--------------Phone-----------------");

            var StudentByPhone = students.Where(s => s.phone.StartsWith("02") || s.phone.StartsWith("+3592"));

            foreach (var st in StudentByPhone)
            {
                Console.WriteLine(st.name + " " + st.surname + " ");
            }

            Console.WriteLine("--------------Top Rating-----------------");

            var StudentByTopRating = students.Where(s => s.rating.Contains(6));

            foreach (var st in StudentByTopRating)
            {
                Console.WriteLine(st.name + " " + st.surname + " ");
            }

            Console.WriteLine("--------------Worst Rating-----------------");

            var StudentByWorstRating = students.Where(s => s.rating.Count( r => r <=3) >= 2);
                          
            foreach (var st in StudentByWorstRating)
            {
                Console.WriteLine(st.name + " " + st.surname + " ");
            }

            Console.WriteLine("--------------Students Enrolled in 2014 or 2015-----------------");

            var StudentByYear = students.Where(s => s.FucNum.ToString().EndsWith("15") || s.FucNum.ToString().EndsWith("14"));

            foreach (var st in StudentByYear)
            {
                Console.WriteLine(st.name + " " + st.surname + " ");
            }

            Console.WriteLine("--------------Group By-----------------");

            var StudentByGroups = students.GroupBy(s => s.group);

            foreach (var st in StudentByGroups)
            {
                Console.Write(st.Key + " - ");
                foreach(var gr in st)
                    Console.Write(gr.name + " " + gr.surname + ", ");
                Console.Write("\n");
            }

            Console.WriteLine("--------------Students Joined to Specialties-----------------");

            var StudentBySpecialties = students.Join(studentSpecialties,
                s => s.FucNum,
                sspec => sspec.FucNum,
                (s, stSpec) => new
                {
                    StName = s.name + " " + s.surname,
                    FucNumber = stSpec.FucNum,
                    FucName = stSpec.SpecialtyName
                });

            foreach (var st in StudentBySpecialties)
            {
               Console.WriteLine(st.StName + " " + st.FucNumber + " " + st.FucName);
            }

        }
    }

    public class Student
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int group { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public List<int> rating { get; set; }
        public int FucNum { get; set; }
    }

    public class StudentSpecialty
    {
        public string SpecialtyName { get; set; }
        public int FucNum { get; set; }
    }

}
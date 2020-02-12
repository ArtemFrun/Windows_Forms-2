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
               new Student { name = "Sara", surname= "Mills", group = 1, age = 24, email = "smills@gmail.com", phone = "02435521", rating = new List<int>(){ 6, 6, 6, 5 } },
               new Student { name = "Andrew", surname ="Gibson", group = 2, age = 21, email = "agibson@abv.bg", phone = "0895223344", rating = new List<int>(){ 3, 4, 5, 6  }},
               new Student { name = "Craig", surname="Ellis", group = 1, age = 19, email = "cellis@cs.edu.gov", phone = "+3592667710", rating = new List<int>(){ 4, 2, 3, 4  }},
               new Student { name = "Steven", surname="Cole", group = 2, age = 35, email = "themachine@abv.bg", phone = "3242133312", rating = new List<int>(){ 5, 6, 5, 5 }},
               new Student { name = "Andrew", surname="Carter", group = 2, age = 15, email = "ac147@gmail.com", phone = "+001234532", rating = new List<int>(){ 5, 3, 4, 2  }},
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
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// In this project we use some specials of C#
namespace CSharpPro
{
    class Program
    {
        static void Main(string[] args)
        {
            ImplicitelyTypedLocalVariables();
            Console.WriteLine();

            Initializers();
            Console.WriteLine();

            Extensionmethods();
            Console.WriteLine();

            AnonymousTypes();
            Console.WriteLine();

            LambdaExpressions();
            Console.WriteLine();

            Linq();
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void ImplicitelyTypedLocalVariables()
        {
            //int i = 5;
            //string x = "Hello, World!";

            // only possible for local variables
            // type is automatically set when using var
            var i = 5;
            var x = "Hello, World!";

            if (x.StartsWith("Hello"))
                Console.WriteLine("Nice");

            var balance = 123.45m; // casts do decimal

            //Student student = new Student();
            var student1 = new Student(); // more readable
            student1.First = "Michael";
            student1.Last = "Rieger";

            var student2 = new Student(); // more readable
            student2.First = "Chuck";
            student2.Last = "Norris";

            var students = new ObservableCollection<Student>();
            students.Add(student1);
            students.Add(student2);

            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
        }

        private static void Initializers()
        {
            // Object initializer
            // first calls the empty constructor and assigns the properties afterwards
            var student1 = new Student
            {
                First = "Michael",
                Last = "Rieger",
                ID = 1337,        // comma in last line is not necessarily needed
            };

            // creates a new object instantly filled with the given values
            var student2 = new Student("Chuck", "Norris", 666);

            // mix of both perevious versions
            var student3 = new Student(100)
            {
                First = "Bob",
                Last = "Ross",
            };

            /* // fill with students
            var students = new List<Student>();
            students.Add(student1);
            students.Add(student2);
            students.Add(student3);
            */

            // use collection initializer
            var students = new List<Student>
                                    { student1,
                                      student2,
                                      student3,
                                      new Student
                                      {
                                          First = "Sepp",
                                          Last = "Huber",
                                          Scores = new List<int>(0)
                                      }};
        }

        private static void Extensionmethods()
        {
            var name = "Michael";
            // use the method normally
            //var spacedName = StringHelper.ToSpaceType(name);

            // use a static class
            // when class is static, we can use it as extension method
            var spacedName = name.ToSpaceType();

            Console.WriteLine(spacedName);
            Console.WriteLine("Chuck Norris".ToSpaceType());

            // use second method call
            Console.WriteLine("Chuck Norris".ToSpaceType(2));

            var student = new Student("Michael", "Rieger", 1337);
            student.AddTestGrade(1);
        }
        private static void AnonymousTypes()
        {
            // anonymously declared class/type
            var car1 = new
            {
                Brand = "Audi",
                Power = 115,
                Price = 43000m
            };

            var car2 = new
            {
                Brand = "Ford",
                Power = 80,
                Price = 22222m
            };

            // only works if the types are declared in the same way
            car2 = car1;

            Console.WriteLine(car2.ToString());
        }

        private delegate int CalcOperation(int a, int b);

        private static int Add(int a, int b)
        {
            return a + b;
        }

        private static void LambdaExpressions()
        {
            var result0 = Add(1, 2);
            Console.WriteLine(result0);

            // delegate
            // pointer to the function Add is assigned to the delegate
            CalcOperation calc1 = Add;
            var result1 = calc1(3, 4);
            Console.WriteLine(result1);

            // anonymous method C# 2.0
            CalcOperation calc2 = delegate (int a, int b)
                {
                    return a + b;
                };

            var result2 = calc2(5, 6);
            Console.WriteLine(result2);

            // generic - e.g two input parameters as int, return value as int
            // Action<int, int> - if the method is void
            // Predicate<int, int> - returns bool -> nearly deprecated
            Func<int, int, int> calc3 = delegate (int a, int b)
                {
                    return a + b;
                };

            var result3 = calc3(7, 8);
            Console.WriteLine(result3);

            // anonymous method as lambda expression C# 3.0
            // with statement body
            Func<int, int, int> calc4 = (a, b) =>
                {
                    return a + b;
                };
            var result4 = calc4(9, 10);
            Console.WriteLine(result4);

            // with an expression body
            // no braces needed
            Func<int, int, int> calc5 = (a, b) => a + b;
            var result5 = calc5(11, 12);
            Console.WriteLine(result5);

            // parenthesis needed for functions without parameters
            Func<int> x1 = () => 42;
            // no parenthesis used/needed for only one input parameter
            Func<int, bool> x2 = a => a >= 0 ? true : false;

            Console.WriteLine(x1().ToString());
            Console.WriteLine(x2(-5).ToString());
            Console.WriteLine(x2(5).ToString());


            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // passing a method as filter expression
            // resultList will have all values in this list that are e.g. greater than severn
            var filteredList = list.Filter(x => x > 7);

            /* // new Dump method created
            foreach (var item in filteredList)
            {
                Console.WriteLine(item);
            }
            */

            filteredList.Dump();

            list.Filter(y => y <= 5).Dump();
            list.Filter(a => a < 6).Filter(a => a >= 2).Dump();
            list.Filter(a => a < 6 && a >= 2).Dump();
        }

        private static void Linq()
        {
            var students = StudentHelper.GetAllStudents();

            var query = students.Where(student => student.Last == "Garcia");

            query.Dump();

            // we can concatenate those methods
            students.Where(student => student.Scores.Average() > 85)
                        .OrderBy(s => s.Last)
                        .ThenBy(s => s.First)
                        .Dump();

            // if no element is found => return null
            // students.Single() would throw an exception
            var student1 = students.SingleOrDefault(s => s.ID == 111);
            Console.WriteLine(student1);

            // take the 5 best students and create an anonymous type
            // s.ID => the anonymous type's ID has the same name as the student's property
            students.OrderByDescending(s => s.Scores.Average())
                    .Take(5)
                    .Select(s => new { Firstname = s.First, Surname = s.Last, s.ID })
                    .Dump();

            // different syntax style
            var query2 = from s in students
                         where s.Last.Contains("l")
                         orderby s.Last, s.First
                         select new
                         {
                             Firstname = s.First,
                             Surname = s.Last,
                             s.ID
                         };

            query2.Dump();
        }
    }

    class Student
    {
        public Student()
        {

        }

        public Student(string first, string last, int id)
        {
            First = first;
            Last = last;
            ID = id;
        }

        public Student(int id)
        {
            ID = id;
        }

        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public List<int> Scores { get; set; } = new List<int>();

        public override string ToString()
        {
            var avgScore = Scores.Count > 0 ? Scores.Average() : 0;
            return $"{Last}, {First} avg. Score {avgScore}";
        }
    }

    static class StringHelper
    {
        // add "this" before your variable so we can call it from eg. a string
        public static string ToSpaceType(this string value)
        {
            return ToSpaceType(value, 1);
        }

        public static string ToSpaceType(this string value, int numberOfSpaces)
        {
            var output = new StringBuilder();

            foreach (var character in value)
            {
                output.Append(character);
                for (int i = 0; i < numberOfSpaces; i++)
                {
                    output.Append(" ");
                }
            }

            return output.ToString().Substring(0, output.ToString().Length - 1);
        }

    }

    static class StudentHelper
    {
        public static void AddTestGrade(this Student student, int grade)
        {
            student.Scores.Add(grade);
        }
        public static List<Student> GetAllStudents()
        {
            return new List<Student>()
                       {
                            new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
                            new Student {First="Claire", Last="O’Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
                            new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int>{88, 94, 65, 91}},
                            new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int>{97, 89, 85, 82}},
                            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int>{35, 72, 91, 70}},
                            new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int>{99, 86, 90, 94}},
                            new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int>{93, 92, 80, 87}},
                            new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
                            new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int>{68, 79, 88, 92}},
                            new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
                            new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
                            new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int>{94, 92, 91, 91}},
                       };
        }
    }

    static class ListHelper
    {
        // predicate is the filter we want to apply
        public static List<int> Filter(this List<int> list, Func<int, bool> predicate)
        {
            var resultList = new List<int>();
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    resultList.Add(item);
                }
            }
            return resultList;
        }

        public static void Dump(this List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write(item);
                Console.Write(" - ");
            }
            Console.WriteLine();
        }

        public static void Dump(this IEnumerable list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }

}

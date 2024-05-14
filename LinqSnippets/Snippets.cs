using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace LinqSnippets
{
    //consultas avanzadas
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A4",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };
            // SELECT * of cars
            var carList = from car in cars select car;
            foreach( var car in carList )
            {
                Console.WriteLine( car );
            }
            //SELECT WHERE car is Audi
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // Each number multiplied by 3
            // Take all numbers, but 9
            // Order numbers by ascending value
            var processedNumberList = 
                numbers
                    .Select(num => num * 3) // { 3,6,9,12, etc. }
                    .Where(num => num != 9)// { all but the 9 }
                    .OrderBy(num => num);// at the end, we order ascending
        }
        static public void SerchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "g",
                "c"
            };

            // 1. First of all elements
            var first = textList.First();
            // 2. First element that is "c"
            var cText = textList.First(text => text.Equals("c"));
            // 3. First Element that contains "j"
            var jText = textList.First(text => text.Contains("j"));
            // 4. First element that contains "z" or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // "" or first element that contains "z"
            // 5. Last element that contains "z" or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); // "" or last element that contains "z"
            // 6. Single Values
            var uniquetexts = textList.Single();
            var uniqueOrDefaulttexts = textList.SingleOrDefault();
            int[] evenNumbers = { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20};
            int[] otherEvenNumbers = { 0, 2, 6,  };
            int[] oddNumbers = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25 };
            //obtain {4,8}
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);// {4, 8}
        }
        static public void MultipleSelects()
        {
            //SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"

            };
            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));
            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "volavaggen",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 1,
                            Name = "pepe",
                            Salary = 1000,
                            Email = "asd@example.com"
                        },
                        new Employee()
                        {
                            Id = 2,
                            Name = "trolencio",
                            Salary = 2000,
                            Email = "example@example.com"
                        },
                         new Employee()
                        {
                            Id = 3,
                            Name = "martin",
                            Salary = 2500,
                            Email = "martin@example.com"
                        }
                    } 
                },
                 new Enterprise()
                {
                    Id = 2,
                    Name = "fiat",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 4,
                            Name = "ana",
                            Salary = 1000,
                            Email = "ana@example.com"
                        },
                        new Employee()
                        {
                            Id = 5,
                            Name = "rocky",
                            Salary = 2000,
                            Email = "rocky@example.com"
                        },
                         new Employee()
                        {
                            Id = 6,
                            Name = "ignacio",
                            Salary = 2500,
                            Email = "ignacio@example.com"
                        }
                    }
                }, new Enterprise()
                {
                    Id = 3,
                    Name = "ferrari",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 7,
                            Name = "juan",
                            Salary = 1000,
                            Email = "juan@example.com"
                        },
                        new Employee()
                        {
                            Id = 8,
                            Name = "alan",
                            Salary = 2000,
                            Email = "alan@example.com"
                        },
                         new Employee()
                        {
                            Id = 9,
                            Name = "leandro",
                            Salary = 2500,
                            Email = "leandro@example.com"
                        }
                    }
                }
            };
            //Optain all employees of all Enterprises
            var employeeList = enterprises.SelectMany(entertprise => entertprise.Employees);
            //know if ana list is empty
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());
            // All enterprises at least employees with at least 1000$ of salary
            bool hasEmployeeWithSalaryThanOrEqual1000 =
                enterprises.Any(enterprises => enterprises.Employees.Any(employee => employee.Salary >= 1000));
        }
        static public void linqColections()
        {
            var firstList = new List<string>() { "a", "b", "c"};
            var secondList = new List<string>() { "a", "c", "d"};
            // INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element , secondElement };
            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element,secondElement) => new {element,secondElement}
                );
            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList //creamos una lista temporal
                                from temporalElement in temporalList.DefaultIfEmpty() // guardamos los elementos temporales que son iguales en ambas listas
                                where element != temporalElement // seleccionamos los elementos de la izquierda o la primera lista mientras que no sea igual a la lista temporal
                                select new { Element = element }; //se queda con los elementos que no son iguales en ambas listas pero de la izquierda
            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new {Element = element, SecondElement = secondElement};
            //OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                join element in firstList
                                on secondElement equals element
                                into temporalList //creamos una lista temporal
                                from temporalElement in temporalList.DefaultIfEmpty() // guardamos los elementos temporales que son iguales en ambas listas
                                where secondElement != temporalElement // seleccionamos los elementos de la derecha o la segunda lista mientras que no sea igual a la lista temporal
                                select new { Element = secondElement }; //se queda con los elementos que no son iguales en ambas listas pero de la derecha
            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }
        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };
            // SKIP
            var skipTwoFistValues = myList.Skip(2); // { 3, 4, 5, 6, 7, 8, 9, 10 }
            var skipLastTwoValues = myList.SkipLast(2); // { 1, 2, 3, 4, 5, 6, 7, 8 }
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // { 5, 6, 7, 8, 9, 10 }
            // TAKE
            var takeFristTwoValues = myList.Take(2); // { 1, 2 }
            var takeLastTwoValues = myList.TakeLast(2); // { 7, 8 }
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {  1, 2, 3, 4 }
        }
        // Paging Whith Skip & Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection,int pageNumber,int resultPerPage)
        {
            int startIndex = (pageNumber - 1) * resultPerPage;
            return collection.Skip(startIndex).Take(resultPerPage);
        }
        // Variable
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var aboveAverage =
                from number in numbers
                let average = numbers.Average() // definimos en una variable local "let" la media de los numeros
                let nSquare = Math.Pow(number, 2) //multiplicamos al cuadrado los cada numero y la guardamos en una variable local
                where nSquare > average // verificamos que el numero al cuadrado no sea mayor que la media
                select number; // los numeros que no pasen la media van a ser seleccionados y guardados en la variable
            Console.WriteLine("Average: {0}", numbers.Average());
            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Query: Number {0} Square: {1} ",number,Math.Pow(number,2));
            }
        }
        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            string[] stringNumbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nive", "ten"};
            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers,(number,word) => number+"="+word);
            //{"1=one","2=two",...}
        }
        // Repeat & Range
        static public void RepeatRangeLinq()
        {
            //generate collection 1 - 1000
            IEnumerable<int> range = Enumerable.Range(1,1000);
            //repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("x", 5); // {"x","x","x","x","x"}
        }
        static public void StudentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "pepe",
                    Grade = 10,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "tony",
                    Grade = 3,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "ricardo",
                    Grade = 2,
                    IsCertified = false,
                },
                new Student
                {
                    Id = 4,
                    Name = "rocio",
                    Grade = 4,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 5,
                    Name = "ignacio",
                    Grade = 2,
                    IsCertified = false,
                },
                new Student
                {
                    Id = 6,
                    Name = "dulce",
                    Grade = 5,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 7,
                    Name = "noe",
                    Grade = 4,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 8,
                    Name = "dylan",
                    Grade = 6,
                    IsCertified = true,

                },
                new Student
                {
                    Id = 9,
                    Name = "diego",
                    Grade = 3,
                    IsCertified = false,
                },
                new Student
                {
                    Id = 10,
                    Name = "guillermo",
                    Grade = 6,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 11,
                    Name = "melba",
                    Grade = 4,
                    IsCertified = false,

                },

            };
            var certifiedStudents = from student in classRoom
                                    where student.IsCertified == true
                                    select student;
            var norCertifiedStudent = from student in classRoom
                                      where !student.IsCertified
                                      select student;
            var approvedStudents = from student in classRoom
                                   where student.Grade >= 4
                                   select student.Name;
        }
        // ALL
        static public void AlllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false
            var emptyList = new List<int>() { };
            bool allNumberAreGreatherThan0 = numbers.All(x => x >= 0); // true
        }
        // Aggragate
        static public void AggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int sum = numbers.Aggregate((previusSum,current) => previusSum + current);
            // 0,1 => 1
            // 1,2 => 3
            // 3,4 => 7
            // etc.
            string[] words = { "hello, ", "my ", "name ", "is ", "eri" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
            // "","hello, " => "hello, "
            // "hello, ","my" => "hello, my"
            // "hello, my","name " =>""hello, my 
        }
        // Disctinc
        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }
        // GroupBy
        static public void GroupByExamples()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            // Obtain only even numbers  and generate two groupes
            var grouped = numbers.GroupBy(x => x % 2 == 0);
            // we will have 2 groupes
            // 1. The group that doesn´t fit the condition (odd numbers)
            // 2. the group that fit the condition (even numbers)
            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9...    2,4,6,8,10 // first the odds then the evens
                }
            }
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "pepe",
                    Grade = 10,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "tony",
                    Grade = 3,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "ricardo",
                    Grade = 2,
                    IsCertified = false,
                },
                new Student
                {
                    Id = 4,
                    Name = "rocio",
                    Grade = 4,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 5,
                    Name = "ignacio",
                    Grade = 2,
                    IsCertified = false,
                },
                new Student
                {
                    Id = 6,
                    Name = "dulce",
                    Grade = 5,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 7,
                    Name = "noe",
                    Grade = 4,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 8,
                    Name = "dylan",
                    Grade = 6,
                    IsCertified = true,

                },
                new Student
                {
                    Id = 9,
                    Name = "diego",
                    Grade = 3,
                    IsCertified = false,
                },
                new Student
                {
                    Id = 10,
                    Name = "guillermo",
                    Grade = 6,
                    IsCertified = true,
                },
                new Student
                {
                    Id = 11,
                    Name = "melba",
                    Grade = 4,
                    IsCertified = false,

                },

            };
            var certifiedQuery = classRoom.GroupBy(x => x.IsCertified);
            // we obtain 2 groupes
            // 1. Not Certified Students
            // 2. Certified Students
            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("---------------{0}----------------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }
        static public void RelationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first Post",
                    Content = "My first Content",
                    comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=1,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                         new Comment()
                        {
                            Id=2,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                         },
                             new Comment()
                        {
                            Id=3,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                             new Comment()
                        {
                            Id=4,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                             new Comment()
                        {
                            Id=5,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                             new Comment()
                        {
                            Id=6,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"

                        }   
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My first Post",
                    Content = "My first Content",
                    comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=7,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"

                        },
                         new Comment()
                        {
                            Id=8,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                         },
                             new Comment()
                        {
                            Id=9,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                             new Comment()
                        {
                            Id=10,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                             new Comment()
                        {
                            Id=11,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        },
                             new Comment()
                        {
                            Id=12,
                            Title="asd",
                            Content="dfssdfsdfsfsdfsdf"
                        }
                    } 
                }
            };
            var commentsWithContent = 
                posts.SelectMany(post => post.comments, (post, comment) => new { 
                    PostId = post.Id,
                    CommentContent = comment.Content,
                });
        }

    }
}

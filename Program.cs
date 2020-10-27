using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviePlexTheatre
{
    //Please, try get and check-it
    class Program
    {
        static ProcessStateEnum _Current = ProcessStateEnum.Main;
        static ProcessStateEnum ChangeState(ProcessStateEnum current, CommandEnum command) => (current, command) switch
        {
            (ProcessStateEnum.Main, CommandEnum.GoAdmin) => ProcessStateEnum.AdminPassword,
            (ProcessStateEnum.Main, CommandEnum.GoGuest) => ProcessStateEnum.Guest,
            (ProcessStateEnum.AdminPassword, CommandEnum.Next) => ProcessStateEnum.AdminMovies,
            (ProcessStateEnum.AdminPassword, CommandEnum.End) => ProcessStateEnum.Main,
            (ProcessStateEnum.AdminMovies, CommandEnum.Next) => ProcessStateEnum.AdminMoviesPreview,
            (ProcessStateEnum.AdminMovies, CommandEnum.Back) => ProcessStateEnum.AdminPassword,
            (ProcessStateEnum.AdminMovies, CommandEnum.End) => ProcessStateEnum.Main,
            (ProcessStateEnum.AdminMoviesPreview, CommandEnum.Back) => ProcessStateEnum.AdminMovies,
            (ProcessStateEnum.AdminMoviesPreview, CommandEnum.End) => ProcessStateEnum.Main,
            (ProcessStateEnum.Guest, CommandEnum.Next) => ProcessStateEnum.GuestResult,
            (ProcessStateEnum.Guest, CommandEnum.End) => ProcessStateEnum.Main,
            (ProcessStateEnum.GuestResult, CommandEnum.End) => ProcessStateEnum.Main,
            _ => throw new NotSupportedException($"{current} has no transition on {command}")
        };

        static List<Movie> _MovieList = new List<Movie>();
        static int _NumberOfMovies = 0;

        static void Main(string[] args)
        {

            while (true)
            {
                switch (_Current)
                {
                    case ProcessStateEnum.Main:
                        Init();
                        break;
                    case ProcessStateEnum.AdminPassword:
                        AdminPassword();
                        break;
                    case ProcessStateEnum.AdminMovies:
                        AdminMovies();
                        break;
                    case ProcessStateEnum.AdminMoviesPreview:
                        AdminMoviesPreview();
                        break;
                    case ProcessStateEnum.Guest:
                        Guest();
                        break;
                    case ProcessStateEnum.GuestResult:
                        GuestResult();
                        break;
                    default:
                        Init();
                        break;
                }
            }
        }

        public static string ReadLine()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var answer = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return answer;
        }

        public static void Header()
        {
            string Line1 = "*************************************************";
            string Line21 = "********** ";
            string Line22 = "Welcome to MoviePlex Theatre";
            string Line23 = " *********";
            string Line3 = "*************************************************";
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - Line1.Length) / 2, Console.CursorTop);
            Console.WriteLine(Line1);
            Console.SetCursorPosition((Console.WindowWidth - (Line21.Length + Line22.Length + Line23.Length)) / 2, Console.CursorTop);
            Console.Write(Line21);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Line22);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Line23 + "\n");
            Console.SetCursorPosition((Console.WindowWidth - Line3.Length) / 2, Console.CursorTop);
            Console.WriteLine(Line3);
            Console.WriteLine("\n\n");

        }

        public static void Init()
        {
            Header();
            Console.WriteLine("Please Select From The Following Options:");
            Console.WriteLine("1: Administrator\n2: Guests\n");
            Console.Write("Selection: ");
            var input = ReadLine();
            try
            {
                switch (input)
                {
                    case "1":
                        _Current = ChangeState(_Current, CommandEnum.GoAdmin);
                        break;
                    case "2":
                        _Current = ChangeState(_Current, CommandEnum.GoGuest);
                        break;
                }
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AdminPassword()
        {
            var counter = 5;
            Header();
            Console.Write("Please Enter The Admin Pasword: ");
            var input = ReadLine();
            while (counter >= 2 && input != "1234" && input != "b" & input != "B")
            {
                counter--;
                Console.WriteLine("\nYou entered an Invalid password.");
                Console.WriteLine($"You have {counter} more attempts to enter the correct password OR press B to go back to the previous screen.");
                Console.Write("\nPlease Enter The Admin Pasword: ");
                input = ReadLine();
            };
            try
            {
                switch (input)
                {
                    case "1234":
                        _Current = ChangeState(_Current, CommandEnum.Next);
                        break;
                    case "B":
                    case "b":
                        _Current = ChangeState(_Current, CommandEnum.End);
                        break;
                    default:
                        _Current = ChangeState(_Current, CommandEnum.End);
                        break;
                }
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AdminMovies()
        {
            Header();
            Console.WriteLine("Welcome MoviePlex Administrator\n");
            Console.Write("How many movies are you playing today? ");
            var input = ReadLine();
            var numberFlag = false;
            while (numberFlag == false)
            {
                if (int.TryParse(input, out _NumberOfMovies) && (_NumberOfMovies < 10))
                {
                    numberFlag = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Please enter just a number between 1-10: ");
                    input = ReadLine();
                }
            }
            var help = "********************************  Help: (Allowed Rating List)  ********************************";
            var line1 = "** G     *** General Audience, any age is good ************************************************";
            var line2 = "** PG    *** We will take PG as 10 years or older *********************************************";
            var line3 = "** PG-13 *** We will take PG-13 as 13 years or older ******************************************";
            var line4 = "** R     *** We will take R as 15 years or older. Don’t worry about accompany by parent case **";
            var line5 = "** PG-17 *** We will take NC-17 as 17 years or older ******************************************";
            var line6 = "***********************************************************************************************";
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - help.Length) / 2, Console.CursorTop);
            Console.WriteLine(help);
            Console.SetCursorPosition((Console.WindowWidth - line1.Length) / 2, Console.CursorTop);
            Console.WriteLine(line1);
            Console.SetCursorPosition((Console.WindowWidth - line2.Length) / 2, Console.CursorTop);
            Console.WriteLine(line2);
            Console.SetCursorPosition((Console.WindowWidth - line3.Length) / 2, Console.CursorTop);
            Console.WriteLine(line3);
            Console.SetCursorPosition((Console.WindowWidth - line4.Length) / 2, Console.CursorTop);
            Console.WriteLine(line4);
            Console.SetCursorPosition((Console.WindowWidth - line5.Length) / 2, Console.CursorTop);
            Console.WriteLine(line5);
            Console.SetCursorPosition((Console.WindowWidth - line6.Length) / 2, Console.CursorTop);
            Console.WriteLine(line6);

            string times;
            for (int i = 0; i < _NumberOfMovies; i++)
            {
                switch (i)
                {
                    case 0:
                        times = "First";
                        break;
                    case 1:
                        times = "Second";
                        break;
                    case 2:
                        times = "Third";
                        break;
                    case 3:
                        times = "Fourth";
                        break;
                    case 4:
                        times = "Fifth";
                        break;
                    case 5:
                        times = "Sixth";
                        break;
                    case 6:
                        times = "Seventh";
                        break;
                    case 7:
                        times = "Eighth";
                        break;
                    case 8:
                        times = "Ninth";
                        break;
                    case 9:
                        times = "Tenth";
                        break;
                    default:
                        times = "First";
                        break;
                }
                var movie = new Movie();
                Console.Write($"\nPlease Enter The {times} Movie's Name: ");
                movie.Name = ReadLine();
                Console.Write($"Please Enter The Age Limit or Rating For The {times} Movie: ");
                var rate = ReadLine();
                var rateFlag = false;
                while (rateFlag == false)
                {
                    switch (rate)
                    {
                        case "G":
                        case "g":
                            movie.Rate = MovieRateEnum.G;
                            _MovieList.Add(movie);
                            rateFlag = true;
                            break;
                        case "R":
                        case "r":
                            movie.Rate = MovieRateEnum.R;
                            _MovieList.Add(movie);
                            rateFlag = true;
                            break;
                        case "PG":
                        case "pg":
                            movie.Rate = MovieRateEnum.PG;
                            _MovieList.Add(movie);
                            rateFlag = true;
                            break;
                        case "pg-13":
                        case "PG-13":
                            movie.Rate = MovieRateEnum.PG13;
                            _MovieList.Add(movie);
                            rateFlag = true;
                            break;
                        case "PG-17":
                        case "pg-17":
                            movie.Rate = MovieRateEnum.PG17;
                            _MovieList.Add(movie);
                            rateFlag = true;
                            break;
                        default:
                            Console.Write($"Please Enter The Correct Age Limit or Rating For The {times} Movie: ");
                            rate = ReadLine();
                            break;
                    }
                }

            }
            try
            {
                _Current = ChangeState(_Current, CommandEnum.Next);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AdminMoviesPreview()
        {
            var i = 1;
            Console.WriteLine();
            foreach (var item in _MovieList)
            {
                Console.WriteLine($"\t{i}. {item.Name} [{item.Rate}]");
                i++;
            }
            Console.Write("Your Movies Playing Today Are Listed Above. Are You Satisfied? (Y/N)");
            var flag = false;
            string answer = string.Empty;
            while (flag == false)
            {
                answer = ReadLine();
                switch (answer)
                {
                    case "y":
                    case "Y":
                        flag = true;
                        try
                        {
                            _Current = ChangeState(_Current, CommandEnum.End);
                        }
                        catch (NotSupportedException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "N":
                    case "n":
                        flag = true;
                        _MovieList.RemoveAll(x => x.Name.Any());
                        try
                        {
                            _Current = ChangeState(_Current, CommandEnum.Back);
                        }
                        catch (NotSupportedException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        Console.Write("Are You Satisfied? (Y/N)");
                        break;
                }
            }

        }

        public static void Guest()
        {
            Header();

            if (_NumberOfMovies > 0)
            {
                Console.WriteLine($"There are {_NumberOfMovies} movies playing today. Please choose from the following movies:");
                var i = 1;
                Console.WriteLine();
                foreach (var item in _MovieList)
                {
                    Console.WriteLine($"\t{i}. {item.Name} [{item.Rate}]");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("There is no movie for today. press any key to go back to the previous screen.");
                Console.ReadLine();
            }

            try
            {
                if (_NumberOfMovies == 0)
                {
                    _Current = ChangeState(_Current, CommandEnum.End);
                }
                else
                {
                    _Current = ChangeState(_Current, CommandEnum.Next);
                }
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void GuestResult()
        {
            Console.WriteLine();
            Console.Write("Which Movie Would You Like To Watch: ");
            var input = ReadLine();
            int inOut = 0;
            var Flag = false;
            while (Flag == false)
            {
                if (int.TryParse(input, out inOut) && inOut <= _NumberOfMovies && inOut > 0)
                {
                    Flag = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Please Choose A Number From Movie List: ");
                    input = ReadLine();
                }
            }
            Console.WriteLine();
            Console.Write("Please Enter Your Age For Verification: ");
            var age = ReadLine();
            int ageout = -1;
            Flag = false;
            while (Flag == false)
            {
                if (int.TryParse(age, out ageout))
                {
                    if (ageout > 0 && ageout < 99)
                    {
                        Flag = true;
                        var movie = _MovieList.ElementAt(inOut - 1);
                        //todo if the movie was correct
                        Console.WriteLine();
                        if (ageout >= movie.Rate.GetHashCode())
                        {
                            Console.WriteLine("Enjoy The Movie!");
                        }
                        else
                        {
                            Console.WriteLine("Sory, You Are Under Age.");
                        }

                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Please Enter Correct Age: ");
                    age = ReadLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press M to go back to the Guet Main Menu");
            Console.WriteLine("Press S to go back to the Start Page");
            input = ReadLine();
            Flag = false;
            while (Flag == false)
            {
                try
                {
                    switch (input)
                    {
                        case "M":
                        case "m":
                            Flag = true;
                            _Current = ChangeState(_Current, CommandEnum.End);
                            _Current = ChangeState(_Current, CommandEnum.GoGuest);
                            break;
                        case "S":
                        case "s":
                            Flag = true;
                            _Current = ChangeState(_Current, CommandEnum.End);
                            break;
                        default:
                            Console.WriteLine("Press M to go back to the Guet Main Menu");
                            Console.WriteLine("Press S to go back to the Start Page");
                            input = ReadLine();
                            break;
                    }
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

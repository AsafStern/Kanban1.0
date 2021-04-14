﻿using IntroSE.Kanban.Backend.ServiceLayer;
using System;

namespace Test
{
    class Program
    {
        /*ervice service = new Service();*/

        static void Main(string[] args)
        {
            Service s = new Service();
           //Print(s.AddBoard("Rafa@gmail.com", "semster B"));
            //Print(s.Register("Rafa@gmail.com", "12345678"));
            //Print(s.Register("Rafagmail.com", "Rf12345678"));
            Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            //Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            //Print(s.Register("Rafa111111@gmail.com", "R12345678"));
            //Print(s.Register("Rafa111111@gmail.com", "ff12345678"));
            //Print(s.Register("Rafa111111@gmail.com", "RF12345678"));
            //Print(s.Register("Rafa111111@gmail.com", "Rf2"));
            //Print(s.Register("Rafa111111@gmail.com", "R12345678222222222222222222222222222222222f"));
            //Print(s.Register("Rafa111111@gmail.com", "R12345678ש"));
            Print(s.Register("Rafa111111@gmail.com", "R12345678f"));
            //Print(s.Login("Rafa@gmail.com", "R12345678f"));
            Print(s.AddBoard("Rafa@gmail.com", "semester B"));
            Print(s.Login("Rafa@gmail.com", "Rf12345678"));
            //Print(s.Login("Rafa123@gmail.com", "Rf12345678"));
            Print(s.Login("Rafa111111@gmail.com", "R12345678f"));
            //Print(s.Logout("Rafa@gmailcom"));
            //Print(s.Logout("Rafa@gmail.com"));

            //Print(s.Logout("Rafagmail.com"));
            //Print(s.Logout("Rada@gmail.com"));
            //Print(s.Logout("Rafa111111@gmail.com"));


            Print(s.AddBoard("Rafa@gmail.com", "semester B"));
            //Print(s.AddBoard("Rafa@gmail.com", "semester B"));
            //Print(s.AddBoard("Rafa111111@gmail.com", "semester B"));


            Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));
            Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0))); // possible the same name for task???
            //Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 3, 14, 0, 0, 0)));
            Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 0, 1));
            Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 1, 2));
            Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 1, 1));
            Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 1, 1));













            //Console.WriteLine(s.AddBoard("Rada@gmail.com", "Semester B").ErrorMessage);
            //Console.WriteLine(s.AddBoard("Rafa@gmail.com", "Semester B").ErrorMessage);
            //Console.WriteLine(s.Login("Rafa@gmail.com", "Rf12345678").ErrorMessage);
            //Console.WriteLine(s.AddBoard("Rafa@gmail.com", "Semester B").ErrorMessage);


        }
        static void Print(Response res)
        {
            string input = "";
            if (res.ErrorOccured)
                input = res.ErrorMessage;
            else
                input = "no error  " + res.GetType().Name;
            Console.WriteLine($"\n    -  ----------   {input}   ----------   -\n");
        }
    }
}

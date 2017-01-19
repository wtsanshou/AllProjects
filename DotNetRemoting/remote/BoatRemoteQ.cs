using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BoatClub {
    private int total=10;
    private int hired=0;

    public BoatClub() {
     
    }


    public bool hire() {

        if (this.hired == this.total)
        {
            return false;
        }
        else
        {
            this.hired++;
            return true;
        }
    }

    public bool return_boat()
    {
        if (this.hired < 1)
        {
            return false;
        }
        else
        {
            this.hired--;
            return true;
        }
    }


    public int read_avail()
    {
        return total - hired;
    }


    public string printDetails()
    {
        string res="\nTotal Number of Boats for Hire:{0} \nNumber of Boats on Hire: {1}\n" , total,hired;
        return res;
    }

}


public class BoatTest
{
    public static void Main()
    {
        int option = 0;
        BoatClub bc = new BoatClub();	// dynamically creates object



        while (option != 5)
        {
            Console.WriteLine("\nChoose Option\n");
            Console.WriteLine("================\n");
            Console.WriteLine("1. Hire");
            Console.WriteLine("2. Return");
            Console.WriteLine("3. Read Number Avail");
            Console.WriteLine("4. Print Details");
            Console.WriteLine("5. Exit");

            option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                bool res=bc.hire();
                if (res == false) Console.WriteLine("No more boats available");
            }
            else if (option == 2)
            {
                bool res=bc.return_boat();
                if (res == false) Console.WriteLine("All boats have been returned");
            }
            else if (option == 3)
            {
                int av = bc.read_avail();
                Console.WriteLine("Available: " + av);
            }
            else if (option == 4)
            {
                string res= bc.printDetails();
                Console.WriteLine(res);
            }

            else if (option == 5)
            {
                Console.WriteLine("Exit");
            }
        }
    }
}

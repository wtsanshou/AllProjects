using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Team
{
    private String name="MANU";
    private int played = 0;
    private int points = 0;

    public Team()
    {

    }

    public void win()
    {
        this.played++;
        this.points += 3;
    }

    public void draw()
    {
        this.played++;
        this.points += 1;
    }

    public void loss()
    {
        this.played++;
        this.points--;
    }

    public string printDetails()
    {
        string res="\nTeam: {0}\nPoints: {1}\nGames Played: {2}\n",name,points,played;
        return res;
    }

}


public class Test
{
    public static void Main()
    {
        int option = 0;
        Team t = new Team();	// dynamically creates object



        while (option != 5)
        {
            Console.WriteLine("\nChoose Option\n");
            Console.WriteLine("================\n");
            Console.WriteLine("1. Win");
            Console.WriteLine("2. Draw");
            Console.WriteLine("3. Loss");
            Console.WriteLine("4. Print Details");
            Console.WriteLine("5. Exit");

            option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                t.win();

            }
            else if (option == 2)
            {
                t.draw();
            }
            else if (option == 3)
            {
                t.loss();
            }
            else if (option == 4)
            {
                string res=t.printDetails();
                Console.WriteLine(res);
            }

            else if (option == 5)
            {
                Console.WriteLine("Exit");
            }
        }
    }
}

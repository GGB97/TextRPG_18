using System;

public class ConsoleManager
{
    static public void RedColor(string str)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(str);
        Console.ResetColor();

    }


}
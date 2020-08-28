using System;

namespace ClassWork4
{
    [Flags]
    enum colors
    {
        zero = 0,
        black = 0x1,
        blue = 0x2,
        cyan = 0x4,
        grey = 0x8,
        green = 0x10,
        magenta = 0x20,
        red = 0x40,
        white = 0x80,
        yellow = 0x100
    }
    class Program
    {
        static void Main(string[] args)
        {

            colors allColors = colors.zero;
            var colorsGetNames = Enum.GetNames(typeof(colors));
                        
            for (var i = 0; i < colorsGetNames.Length; i++)
            {
                allColors |= (colors)(1 << i);                
            }

            Console.WriteLine("Here are all colors. Please, choose 4 colors to add they to favorites.");
            for (int i = 1; i < colorsGetNames.Length; i++)
            {
                Console.WriteLine(colorsGetNames[i]);
            }
            colors favoriteColors = colors.zero;
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enter the color: ");
                var color = (colors)Enum.Parse(typeof(colors), Console.ReadLine());
                favoriteColors |= color;
            }
                                    
            Console.WriteLine("Your favorite colors are: " + favoriteColors);
            Console.WriteLine("Rest colors are: " + (favoriteColors ^ allColors) );
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication2
{
    class CodeChallenge12Aug2016
    {
        int rows, columns;
        string numsp = " numsp ";
        static void Main(string[] args)
        {
            CodeChallenge12Aug2016 p = new CodeChallenge12Aug2016();
            string a = p.encrypt();
            Console.WriteLine("Encrypted Message is : " + a);
            Console.WriteLine("Decrypted Message is : "+p.decrypt(a));
            Console.ReadLine();


        }
        
        public string encrypt()
        {
            //Reading text from user
            Console.WriteLine("Enter the text");
            string text = Console.ReadLine();
            //Removing the spaces in the input  
            string textToEncode = Regex.Replace(text, @" ", "");
            //Exiting if the input length is greater than 81
            if (textToEncode.Length > 81)
            {
                Console.WriteLine("Please enter the string less than 81 Characters");
                Console.ReadLine();
                Environment.Exit(0);
                return null;
            }
            else
            {
                //calculating rows and columns
                rows = Convert.ToInt32(Math.Ceiling(Math.Sqrt(textToEncode.Length)));
                columns = Convert.ToInt32(Math.Floor(Math.Sqrt(textToEncode.Length)));
               
            }
            int count = 0;
            //Getting the spaces index in the string
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    numsp = numsp + (i + 1 - count) + " ";
                    count++;
                }
            }
            string[] encodedString = new string[columns];
            // Preparing the encoded string by appending the values 
            for (int k = 0; k < textToEncode.Length; k++)
            {
                encodedString[k % columns] += textToEncode[k].ToString();
            }
            //Joining the encoded string array to get encrypted Message
            return (string.Join(" ", encodedString)) + numsp;



        }
        public string decrypt(string encodedMessage)
        {

            string[] input = Regex.Split(encodedMessage, "numsp");
            string[] encodedText = input[0].Split(' ');
            rows = encodedText[0].Length;
            columns = encodedText.Length;
            string[] decodedString = new string[rows];
            //looping through rows and columns for populating the decoded string
            for (int k = 0; k < rows; k++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (encodedText[j].Length > k) //check if the character exists in the specified row and column
                        decodedString[k] += encodedText[j][k];
                }
            }

            string decodedText = string.Join("", decodedString);
            string[] spaces = input[1].Split(' ');
          
            int p = 0;
            //Adding spaces to the decrypted Message calculated while encrypting
            for (int r = 1; r < spaces.Length; r++)
            {
                if (!string.IsNullOrEmpty(spaces[r]))
                {
                    decodedText = decodedText.Insert(Convert.ToInt16(spaces[r]) - 1 + p, " ");
                    p++;
                }
            }
            return decodedText;
          
          
        }



    }
}

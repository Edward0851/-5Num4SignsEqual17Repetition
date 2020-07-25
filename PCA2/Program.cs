using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCA2
{
    class Program
    {
        static List<string> signmix = new List<string> { };
        static void Main(string[] args)
        {
            List<double> numbers = new List<double> { 3, 5, 6, 7, 9 };
            List<char> givensigns = new List<char> { '+', '-', '*', '/' };
            int count = 0; //count whether all possible combinations are examined

            SignGenerator("", givensigns, ref signmix);//generate all possible combination of signs

            Permutation(new List<double> { }, numbers, ref count);//generate all possible combanation of numbers and calculate the result

            Console.WriteLine("Total number of signs are: " +signmix.Count+". Total number of number combinations are: "+count);
            Console.Read();
        }

        //generate all possible combination of signs
        static void SignGenerator(string fix, List<char> signs, ref List<string> combsigns)
        {
            foreach (char a in signs)
            {
                fix = fix + a.ToString();
                if (fix.Length < 4)
                {
                    SignGenerator(fix, signs, ref combsigns);
                }
                else
                {
                    combsigns.Add(fix);
                }
                fix = fix.Remove(fix.Length - 1);
            }
        }

        //"fix" is used to store determined numbers, "numbers" are undetermined numbers
        static void Permutation(List<double> fix, List<double> numbers, ref int count)
        {

            foreach (double a in numbers)
            {
                fix.Add(a);
                if (fix.Count < 5)
                {
                    Permutation(fix, numbers, ref count);
                }
                else
                {
                    Calculate(fix, signmix);
                    count++;
                }
                fix.Remove(a);
            }
        }

        //Calculate the result of each combination
        static void Calculate(List<double> Calnumbers, List<string> signs)
        {
            double aim = 17;//Target value**********************************************************
            foreach (string sign in signs)
            {
                double result = 17;
                List<double> temp = new List<double>(Calnumbers);
                CalculateEach(temp, sign, ref result);
                //Uncomment these two lines to display all possible combinations and their results
                //Console.WriteLine(Calnumbers[0].ToString() + sign[0] + Calnumbers[1].ToString() + sign[1] + Calnumbers[2].ToString() + sign[2] + Calnumbers[3].ToString() + sign[3] + Calnumbers[4].ToString() + "=" + result);
                if (result == aim)
                {
                    Console.WriteLine(Calnumbers[0].ToString() + sign[0] + Calnumbers[1].ToString() + sign[1] + Calnumbers[2].ToString() + sign[2] + Calnumbers[3].ToString() + sign[3] + Calnumbers[4].ToString() + "=" + result);
                }
            }
            return;
        }

        //Handle the order of calculation, following the rule of "*\" first then "+-", return the result
        static void CalculateEach(List<double> Calnumbers, string sign, ref double result)
        {
            if (sign.IndexOf('*') >= 0 && (sign.IndexOf('*') < sign.IndexOf('/') || sign.IndexOf('/') < 0))
            {
                int pointofcal = sign.IndexOf('*');
                result = Calnumbers[pointofcal] * Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, result);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref result);
            }
            else if (sign.IndexOf('/') >= 0 && (sign.IndexOf('/') < sign.IndexOf('*') || sign.IndexOf('*') < 0))
            {
                int pointofcal = sign.IndexOf('/');
                result = Calnumbers[pointofcal] / Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, result);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref result);
            }
            else if (sign.IndexOf('+') >= 0 && (sign.IndexOf('+') < sign.IndexOf('-') || sign.IndexOf('-') < 0))
            {
                int pointofcal = sign.IndexOf('+');
                result = Calnumbers[pointofcal] + Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, result);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref result);
            }
            else if (sign.IndexOf('-') >= 0 && (sign.IndexOf('-') < sign.IndexOf('+') || sign.IndexOf('+') < 0))
            {
                int pointofcal = sign.IndexOf('-');
                result = Calnumbers[pointofcal] - Calnumbers[pointofcal + 1];
                Calnumbers.RemoveRange(pointofcal, 2);
                Calnumbers.Insert(pointofcal, result);
                sign = sign.Remove(pointofcal, 1);
                CalculateEach(Calnumbers, sign, ref result);
            }
            else //when there is no signs left to calculate, return the result
            {
                return;
            }

        }
    }
}

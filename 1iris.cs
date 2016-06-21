using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace iris
{
    class Program
    {
        public static void dz(double a)
        {
            if (a >= 0.1 && a <= 0.6) Console.WriteLine("Iris - setosa"); 
            if (a > 0.6 && a < 1.7) Console.WriteLine("Iris-versicolor"); 
            if (a >= 1.8 && a <= 2.5) Console.WriteLine("Iris-virginica");
            else Console.WriteLine("");
        }

        /////////////////

        public static void r1(double[][] num, int a)
        { 
            //сортировка
            double[] cup;

            for (int i1 = 0; i1 < num.GetLength(0) - 1; i1++)
                for (int i = 0; i < num.GetLength(0) - 1; i++)
                    if (num[i][a] > num[i + 1][a])
                    {
                        cup = num[i];
                        num[i] = num[i + 1];
                        num[i + 1] = cup;
                    }



            for (int i = 0; i < num.GetLength(0); i++)
            {
                for (int j = 0; j < 5; j++)
                    Console.Write(num[i][j] + " ");
                Console.WriteLine();
            }

            //обьединение по числам
            double[,] opr = new double[150,6];
            int k = 0;
            
            for (int i = 0; i < num.GetLength(0) - 1; i++)
            {
                if (num[i][a] == num[i + 1][a])
                {
                    opr[k, 0] = num[i][a];
                    opr[k, Convert.ToInt32(num[i][4])]++;
                }
                else
                {
                    opr[k, 0] = num[i][a];
                    opr[k, Convert.ToInt32(num[i][4])]++;
                    k++;
                }
            }

            if (num[num.GetLength(0) - 1][a] != num[num.GetLength(0) - 2][a])
            {
                opr[k, 0] = num[num.GetLength(0) - 1][a];
                opr[k, Convert.ToInt32(num[num.GetLength(0) - 1][4])]++;
                k++;
            }

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < opr.GetLength(1); j++)
                    Console.Write(opr[i, j] + " ");
                Console.WriteLine();
            }
            


            //наилучшее и похибка


            for (int i = 0; i < k; i++)
            {
                    if (opr[i, 1] >= opr[i, 2] && opr[i, 1] >= opr[i, 3])
                    {
                        opr[i, 4] = 1;
                        opr[i, 5] = opr[i, 2] + opr[i, 3];
                        goto qq;
                    }

                    if (opr[i, 2] >= opr[i, 1] && opr[i, 2] >= opr[i, 3])
                    {
                        opr[i, 4] = 2;
                        opr[i, 5] = opr[i, 1] + opr[i, 3];
                        goto qq;
                    }

                    if (opr[i, 3] >= opr[i, 2] && opr[i, 3] >= opr[i, 2])
                    {
                        opr[i, 4] = 3;
                        opr[i, 5] = opr[i, 2] + opr[i, 1];
                        goto qq;
                    }
                qq: { }
            }

            //for (int i = 0; i < k; i++)
            //{
            //    for (int j = 0; j < opr.GetLength(1); j++)
            //        Console.Write(opr[i, j] + " ");
            //    Console.WriteLine();
            //}


            //обьединение в группы

            int[,] lich = new int[1,3];
            int tr = 0;
            int pohubka = 0;


            for (int i = 0; i < k; i++)
            {
                if (lich[0, 0] == 3 || lich[0, 1] == 3 || lich[0, 2] == 3)
                {
                    if (lich[0, 0] == 3) tr = blabla(i, opr, 0);
                    if (lich[0, 1] == 3) tr = blabla(i, opr, 1);
                    if (lich[0, 2] == 3) tr = blabla(i, opr, 2);

                    for (int j = i - 3; j <= tr; j++)
                        pohubka += Convert.ToInt32(opr[j, 5]);

                        i = tr;
                    lich[0, 0] = 0; lich[0, 1] = 0; lich[0, 2] = 0;
                }
                else
                {
                    lich[0, Convert.ToInt32(opr[i, 4]) - 1]++;
                }

                    
            }

            Console.WriteLine(pohubka);




            //for (int i = 0; i < num.GetLength(0); i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //        Console.Write(num[i][j] + " ");
            //    Console.WriteLine();
            //}

        }

        public static int blabla(int ii, double[,] opr, int ir)
        {
            Console.Write( opr[ii - 3, 0]+"-");

            while (opr[ii, 4] == opr[ii + 1, 4])
            {
                ii++;
            }

            Console.WriteLine(opr[ii, 0]);

            return ii;
        }







        static void Main(string[] args)
        {
            StreamReader fil = new StreamReader(@"D:\3data base\iris.data");
            {
                string line = fil.ReadToEnd();
                string newline = line.Replace("\n", "$");

                string[] dog = newline.Split('$');

                string[][] mass = new string[dog.Length][];
                for (int i = 0; i < dog.Length; i++)
                    mass[i] = dog[i].Split(',');

                for (int i = 0; i < dog.Length; i++)
                    for (int j = 0; j < mass[i].Length; j++)
                        mass[i][j] = mass[i][j].Replace(".", ",");



                for (int i = 0; i < mass.GetLength(0)-2; i++)
                {
                    for (int j = 0; j < 5; j++)
                        Console.Write(mass[i][j] + " ");
                    Console.WriteLine();
                }




                double[][] numbers = new double[dog.Length-2][];
                double[] t = new double[5];

                //Console.WriteLine(numbers.GetLength(0));

                for (int i = 0; i < numbers.GetLength(0); i++)
                {
                    numbers[i] = new double[5];

                    for (int j = 0; j < 5; j++)
                    {
                        
                        if (j < 4)
                        {
                            numbers[i][j] = Convert.ToDouble(mass[i][j]);
                            //Console.WriteLine(mass[i][j]);
                            //Console.WriteLine(numbers[i][j]);
                        }
                        else switch (mass[i][4])
                            {
                                case "Iris-setosa":
                                    numbers[i][4] = 1;
                                    break;
                                case "Iris-versicolor":
                                    numbers[i][4] = 2;
                                    break;
                                case "Iris-virginica":
                                    numbers[i][4] = 3;
                                    break;
                            }


                    }
                    //numbers[i] = t;
                    //for (int r = 0; r < 5; r++)
                    //    numbers[i][r] = t[r];
                }


                //int setosa, versicolor, virginica;
                //setosa = 0; versicolor = 0; virginica = 0;

                //for (int i = 0; i < numbers.GetLength(0); i++)
                //    for (int j = 0; j < numbers.GetLength(1); j++)
                //        if (numbers[i, 3] >= 1.8)
                //        {
                //            switch (mass[i][4])
                //            {
                //                case "Iris-setosa":
                //                    setosa++;
                //                    break;
                //                case "Iris-versicolor":
                //                    versicolor++;
                //                    break;
                //                case "Iris-virginica":
                //                    virginica++;
                //                    break;
                //            }
                //        }


                    //Console.WriteLine(setosa+" "+versicolor+" "+virginica);



                //dz!!!!

                //string lin = Console.ReadLine();
                //string[] m = lin.Split(',');
                //dz(Convert.ToDouble(m[m.Length - 1].Replace(".", ",")));


                for (int i = 0; i < numbers.GetLength(0); i++)
                {
                    for (int j = 0; j < 5; j++)
                        Console.Write(numbers[i][j] + " ");
                    Console.WriteLine();
                }

                r1(numbers, 4);

            }


            Console.ReadKey();

        }
    }
}

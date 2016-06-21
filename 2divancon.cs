using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace newdivandcon
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader fil = new StreamReader(@"D:\3data base\dc.txt");
            {
                //working with data:
                string line = fil.ReadToEnd();

                string[] dog = line.Split('*');

                int count = 0;
                foreach (char a in dog[0])
                    if (a == ',') count++;

                string[][] mass = new string[dog.Length][];
                for (int i = 0; i < dog.Length; i++)
                    mass[i] = dog[i].Split(',');
                
                Tree tree = new Tree();
                Do(mass, tree, count);

                tree.Show();
            }
            Console.ReadKey();
        }




        public static double[] GINI(int st, string[][] mas, int count)
        {
            List<string> names = new List<string>();
            for (int i = 1; i < mas.Length; i++)
            {
                if (!names.Contains(mas[i][st]))
                {
                    names.Add(mas[i][st]);
                }
            }

            double[,] m = new double[names.Count, count];

            foreach (string s in names)
                for (int j = 1; j < mas.Length; j++)
                {
                    if (mas[j][st] == s)
                    {
                        switch (mas[j][count])
                        {
                            case "yes":
                                m[names.IndexOf(s), 0]++;
                                m[names.IndexOf(s), 2]++;
                                break;
                            case "no":
                                m[names.IndexOf(s), 1]++;
                                m[names.IndexOf(s), 2]++;
                                break;
                        }
                    }

                }

            double all = 0;
            for (int i = 0; i < m.GetLength(0); i++)
            {
                m[i, 3] = 1 - (Math.Pow(m[i, 0] / m[i, 2], 2) + Math.Pow(m[i, 1] / m[i, 2], 2));
                all += m[i, 2];
            }



            double gini = 0;

            for (int i = 0; i < m.GetLength(0); i++)
            {
                gini += (m[i, 2] / all) * m[i, 3];
            }

            double[] ginimasiv = new double[m.GetLength(0) + 1];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                ginimasiv[i] = m[i, count - 1];
            }
            ginimasiv[ginimasiv.Length - 1] = gini;

            return ginimasiv;

        }






        public static void Do(string[][] mas, Tree tree, int count)
        {
            //choosing best brunch
            List<double> ginilist = new List<double>();
            double[] ginigini = null;
            for (int i = 0; i < count; i++)
            {
                ginigini = GINI(i, mas, count);
                ginilist.Add(ginigini[ginigini.Length - 1]);
                //Console.WriteLine(ginilist[i]);
            }

            int index = ginilist.IndexOf(ginilist.Min());
            double[] ginimasiv = GINI(index, mas, count);

            //Adding to the tree
            List<string> children = new List<string>();
            for (int i = 1; i < mas.Length; i++)
            {
                if (!children.Contains(mas[i][index]))
                    children.Add(mas[i][index]);
            }

            tree.Add(mas[0][index], children);

            //if one child is perfect add it's result to the tree
            //else make recursion
            for (int i = 0; i < ginimasiv.Length - 1; i++)
            {
                if (ginimasiv[i] == 0)
                {
                    //you may know yes or no value
                    string yesno = null;
                    for (int j = 1; j < mas.Length; j++)
                        if (mas[j][index] == children[i])
                        {
                            yesno = mas[j][count];
                            break;
                        }

                    tree.Add(yesno, null);
                    
                }
                else
                {
                    //need to cut your masiv
                    int lich = 0;
                    for (int j = 1; j < mas.Length; j++)
                        if (mas[j][index] == children[i]) lich++;

                    string[][] newmas = new string[lich+1][];

                    newmas[0] = mas[0];
                    int ii = 1;
                    for (int j = 1; j < mas.Length; j++)
                    {
                        if (mas[j][index] == children[i])
                            newmas[ii++] = mas[j];
                    }

                    //for (int q = 0; q < newmas.Length; q++)
                    //{
                    //    for (int w = 0; w < count; w++)
                    //        Console.WriteLine(newmas[q][w]);
                    //    Console.WriteLine();
                    //}

                    Do(newmas, tree, count);
                }
            }
        }
    }
}

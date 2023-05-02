using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class OfficeOrganization
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// find the minimum costs in MOST EFFICIENT WAY to organize your office to meet your father needs.
        /// </summary>
        /// <param name="N">initial load</param>
        /// <param name="M">target load required by your father</param>
        /// <param name="A">cost of reducing the load by half</param>
        /// <param name="B">cost of reducing the load by 1</param>
        /// <returns>Min total cost to reduce the load from N to M</returns>

        static int Sister(int N)
        {
            return (int)Math.Floor((double)N / 2);
        }
        static int Brother(int N)
        {
            return (N - 1);
        }

        static int FindMin(int N, int M, int A, int B, int MinC)
        {


            if (N == M || N == 0)
                return MinC;

            else
            {

                if ((int)Math.Floor((double)N / 2) >= M && B * (N - (N / 2)) > A)
                {
                    N = Sister(N);
                    MinC += A;
                    //Console.WriteLine("Sister = " + N);

                }

                else
                {
                    N = Brother(N);
                    MinC += B;
                    //Console.WriteLine("Brother = " + N);
                }
                //Console.WriteLine("Cost = " + MinC + "\n");
                MinC = FindMin(N, M, A, B, MinC);

                return MinC;
            }

        }



        public static int OrganizeTheOffice(int N, int M, int A, int B)
        {
            int MinCost = 0;
            //Console.WriteLine("N = " + N + ", M = " + M + ", A = " + A + ", B = " + B);

            return FindMin(N, M, A, B, MinCost);
        }
        #endregion
    }
}


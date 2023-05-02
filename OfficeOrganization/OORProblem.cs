using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "OfficeOrganization"; } }

        public override void TryMyCode()
        {
            int N, M, A, B, output, expected;

            //A Only
            N = 16;
            M = 4;
            A = 3;
            B = 2;
            expected = 6;
            output = OfficeOrganization.OrganizeTheOffice(N, M, A, B);
            Console.WriteLine("Reducing from {0} to {1} [$A = {2}, $B = {3}] costs {4}\nExpected = {5}", N, M, A, B, output, expected);
            Console.WriteLine();

            //B Only
            N = 21;
            M = 11;
            A = 5;
            B = 1;
            expected = 10;
            output = OfficeOrganization.OrganizeTheOffice(N, M, A, B);
            Console.WriteLine("Reducing from {0} to {1} [$A = {2}, $B = {3}] costs {4}\nExpected = {5}", N, M, A, B, output, expected);
            Console.WriteLine();

            //B Only
            N = 20;
            M = 4;
            A = 12;
            B = 1;
            expected = 16;
            output = OfficeOrganization.OrganizeTheOffice(N, M, A, B);
            Console.WriteLine("Reducing from {0} to {1} [$A = {2}, $B = {3}] costs {4}\nExpected = {5}", N, M, A, B, output, expected);
            Console.WriteLine();

            //A & B
            N = 30;
            M = 5;
            A = 5;
            B = 2;
            expected = 14;
            output = OfficeOrganization.OrganizeTheOffice(N, M, A, B);
            Console.WriteLine("Reducing from {0} to {1} [$A = {2}, $B = {3}] costs {4}\nExpected = {5}", N, M, A, B, output, expected);
            Console.WriteLine();

            //A & B
            N = 64;
            M = 16;
            A = 20;
            B = 1;
            expected = 36;
            output = OfficeOrganization.OrganizeTheOffice(N, M, A, B);
            Console.WriteLine("Reducing from {0} to {1} [$A = {2}, $B = {3}] costs {4}\nExpected = {5}", N, M, A, B, output, expected);
            Console.WriteLine();
        }

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int N, M, A, B, output;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);
   
            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
 
            int i = 1;
            while (testCases-- > 0)
            {
                N = br.ReadInt32();
                M = br.ReadInt32();
                A = br.ReadInt32();
                B = br.ReadInt32();

                int actualResult = br.ReadInt32();

                //Console.WriteLine("{0}^{1} mod {2} = {3}", B, P, M, actualResult);
                output = 0;
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            long sum = 0;
                            int numOfRep = 100;
                            Stopwatch sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                            {
                                sum += OfficeOrganization.OrganizeTheOffice(N, M, A, B);
                            }
                            output = (int)(sum / numOfRep);
                            sw.Stop();
                            //Console.WriteLine("N = {0}, M = {1}, time in ms = {2}", N, M, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            output = int.MinValue;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
					tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (output == actualResult)    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0)); 
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases)
        {
            throw new NotImplementedException();

        }

        #endregion

   
    }
}

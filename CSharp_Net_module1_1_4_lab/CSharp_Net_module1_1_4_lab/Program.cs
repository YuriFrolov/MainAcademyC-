using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_1_4_lab
{
    class Program
    {
        // 1) declare enum ComputerType
        enum ComputerType
        {
            Desktop = 0,
            Laptop,
            Server
        }

        // 2) declare struct Computer
        struct Computer
        {
            public ComputerType ComputerType;
            public int CoreCount;
            public int CPUFrequencyMGz;
            public int MemoryGB;
            public int HDDSpaceGb;

            public Computer(ComputerType computerType, int coreCount, int cpuFrequencyMGz, int memoryGB, int hddSpaceGb)
            {
                ComputerType = computerType;
                CoreCount = coreCount;
                CPUFrequencyMGz = cpuFrequencyMGz;
                MemoryGB = memoryGB;
                HDDSpaceGb = hddSpaceGb;
            }
        }

        static void Main(string[] args)
        {
            Computer server = new Computer(ComputerType.Server, 8, 3000, 16, 2000);
            Computer desktop = new Computer(ComputerType.Desktop, 4, 2500, 6, 500);
            Computer laptop = new Computer(ComputerType.Laptop, 2, 1700, 4, 250);
            int[,] departmentComputers = { { 2, 2, 1 }, { 0, 3, 0 }, { 3, 2, 0 }, { 1, 1, 2 } };
            // 3) declare jagged array of computers size 4 (4 departments)
            var departments = new Computer[departmentComputers.GetLength(0)][];
            // 4) set the size of every array in jagged array (number of computers)
            for (var i = 0; i < departmentComputers.GetLength(0); i++)
            {
                var computersCount = 0;
                for (var k = 0; k < departmentComputers.GetLength(1); k++)
                    computersCount += departmentComputers[i, k];
                departments[i] = new Computer[computersCount];
            }

            // 5) initialize array
            // Note: use loops and if-else statements

            for (var i = 0; i < departmentComputers.GetLength(0); i++)
            {
                var computerIndex = 0;
                for (var k = 0; k < departmentComputers.GetLength(1); k++)
                {
                    for (var j = 0; j < departmentComputers[i, k]; j++)
                    {
                        Computer computer;
                        if (k == (int)ComputerType.Desktop)
                            computer = desktop;
                        else if (k == (int)ComputerType.Laptop)
                            computer = laptop;
                        else
                            computer = server;
                        departments[i][computerIndex++] = computer;
                    }
                }
            }

            // 6) count total number of every type of computers
            // 7) count total number of all computers

            // Note: use loops and if-else statements
            // Note: use the same loop for 6) and 7)

            var desktopNumber = 0;
            var laptopNumber = 0;
            var serverNumber = 0;
            var totalNumber = 0;
            for (var i = 0; i < departments.Length; i++)
            {
                for (var k = 0; k < departments[i].Length; k++)
                {
                    var computerType = departments[i][k].ComputerType;
                    if (computerType == ComputerType.Desktop)
                        desktopNumber++;
                    else if (computerType == ComputerType.Laptop)
                        laptopNumber++;
                    else
                        serverNumber++;
                    totalNumber++;
                }
            }
            Console.WriteLine("Number of desktops: " + desktopNumber);
            Console.WriteLine("Number of laptops: " + laptopNumber);
            Console.WriteLine("Number of servers: " + serverNumber);
            Console.WriteLine("Total number of computers: " + totalNumber);


            // 8) find computer with the largest storage (HDD) - 
            // compare HHD of every computer between each other; 
            // find position of this computer in array (indexes)
            // Note: use loops and if-else statements

            var maxHDDSpaceGb = 0;
            for (var i = 0; i < departments.Length; i++)
            {
                for (var k = 0; k < departments[i].Length; k++)
                {
                    var hddSpaceGb = departments[i][k].HDDSpaceGb;
                    if (maxHDDSpaceGb < hddSpaceGb)
                        maxHDDSpaceGb = hddSpaceGb;
                }
            }
            Console.WriteLine("Indexes of computers with max HDD:");
            for (var i = 0; i < departments.Length; i++)
            {
                for (var k = 0; k < departments[i].Length; k++)
                {
                    if (departments[i][k].HDDSpaceGb == maxHDDSpaceGb)
                        Console.Write("("+i+","+k+") ");
                }
            }
            Console.WriteLine();

            // 9) find computer with the lowest productivity (CPU and memory) - 
            // compare CPU and memory of every computer between each other; 
            // find position of this computer in array (indexes)
            // Note: use loops and if-else statements
            // Note: use logical operators in statement conditions

            var minCPUProductivity = int.MaxValue;
            var minMemoryGB = int.MaxValue;
            for (var i = 0; i < departments.Length; i++)
            {
                for (var k = 0; k < departments[i].Length; k++)
                {
                    var computer = departments[i][k];
                    var cpuProductivity = computer.CoreCount * computer.CPUFrequencyMGz;
                    var memoryGB = computer.MemoryGB;
                    if (minCPUProductivity >= cpuProductivity && minMemoryGB >= memoryGB)
                    {
                        minCPUProductivity = cpuProductivity;
                        minMemoryGB = memoryGB;
                    }
                }
            }
            Console.WriteLine("Indexes of computers with minimum productivity:");
            for (var i = 0; i < departments.Length; i++)
            {
                for (var k = 0; k < departments[i].Length; k++)
                {
                    var computer = departments[i][k];
                    var cpuProductivity = computer.CoreCount * computer.CPUFrequencyMGz;
                    if (cpuProductivity == minCPUProductivity && computer.MemoryGB == minMemoryGB)
                        Console.Write("(" + i + "," + k + ") ");
                }
            }
            Console.WriteLine();

            // 10) make desktop upgrade: change memory up to 8
            // change value of memory to 8 for every desktop. Don't do it for other computers
            // Note: use loops and if-else statements
            Console.WriteLine("Type and memory of computers before upgrade:");
            OutputComputerMemory(departments);
            for (var i = 0; i < departments.Length; i++)
            {
                for (var k = 0; k < departments[i].Length; k++)
                {
                    if (departments[i][k].ComputerType == ComputerType.Desktop)
                        departments[i][k].MemoryGB = 8;
                }
            }
            Console.WriteLine("Type and memory of computers after upgrade:");
            OutputComputerMemory(departments);
            Console.ReadKey();
        }

        static void OutputComputerMemory(Computer[][] departments)
        {
            for (var i = 0; i < departments.Length; i++)
            {
                Console.WriteLine("Department " + (i + 1) + ":");
                for (var k = 0; k < departments[i].Length; k++)
                {
                    var computer = departments[i][k];
                    Console.Write("(" + computer.ComputerType + ", " + computer.MemoryGB+") ");
                }
                Console.WriteLine();
            }
        }

    }
}

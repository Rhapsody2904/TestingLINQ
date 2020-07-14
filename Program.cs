using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Milda Willoughby
 * Purpose: practice using LINQ and Lambda expressions
 * Program: Simple console application for working with suppliers, parts, and shipments.
 * Provides a menu and sub-menu for navigating; displays filtered/joined data
 */

namespace Project_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 9; //placeholder value

            //create table arrays
            var suppliers = new[]
            {
                new {SN = 1, SName = "Smith", Status = 20, City = "London"},
                new {SN = 2, SName = "Jones", Status = 10, City = "Paris" },
                new {SN = 3, SName = "Blake", Status = 30, City = "Paris" },
                new {SN = 4, SName = "Clark", Status = 20, City = "London"},
                new {SN = 5, SName = "Adams", Status = 30, City = "Athens"}
            };

            var parts = new[]
            {
                new {PN = 1, PName = "Nut", Color = "Red", Weight = 12, City = "London"},
                new {PN = 2, PName = "Bolt", Color = "Green", Weight = 17, City = "Paris"},
                new {PN = 3, PName = "Screw", Color = "Blue", Weight = 17, City = "Rome"},
                new {PN = 4, PName = "Screw", Color = "Red", Weight = 14, City = "London"},
                new {PN = 5, PName = "Cam", Color = "Blue", Weight = 12, City = "Paris"},
                new {PN = 6, PName = "Cog", Color = "Red", Weight = 19, City = "London"}
            };

            var shipments = new[]
            {
                new {SN = 1, PN = 1, Qty = 300},
                new {SN = 1, PN = 2, Qty = 200},
                new {SN = 1, PN = 3, Qty = 400},
                new {SN = 1, PN = 4, Qty = 200},
                new {SN = 1, PN = 5, Qty = 100},
                new {SN = 1, PN = 6, Qty = 100},
                new {SN = 2, PN = 1, Qty = 300},
                new {SN = 2, PN = 2, Qty = 400},
                new {SN = 3, PN = 2, Qty = 200},
                new {SN = 4, PN = 2, Qty = 200},
                new {SN = 4, PN = 4, Qty = 300},
                new {SN = 4, PN = 5, Qty = 400}
            };

            do //main menu for choosing operations
            {
                Console.WriteLine("Please enter number for the operation you would like to perform: \n" +
                "1 - Display contents of all tables \n" +
                "2 - Search cities by part color \n" +
                "3 - View all suppliers \n" +
                "4 - View orders for a certain supplier \n" +
                "0 - Exit");

                Console.WriteLine();
                Console.Write("Selection: ");

                try //check input
                {
                    input = Int32.Parse(Console.ReadLine());
                    if (input > 4 || input < 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Input error! Please select a valid value!");
                    }
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Input error! Please select a valid value!");
                    input = 9; //reset input
                }
                Console.WriteLine();

                switch (input)
                {
                    case 1: //sub-menu for viewing contents of "tables"
                        int selection = 9;
                        do
                        {
                            Console.WriteLine("Please enter number for the operation you would like to perform: \n" +
                                            "1 - Display contents of supplier table \n" +
                                            "2 - Display contents of parts table \n" +
                                            "3 - Display contents of shipments table \n" +
                                            "0 - Go back to main menu");

                            Console.WriteLine();
                            Console.Write("Selection: ");

                            try
                            {
                                selection = Int32.Parse(Console.ReadLine()); //check input
                                if (selection > 3 || selection < 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Input error! Please select a valid value!");
                                }
                            }
                            catch
                            {
                                Console.WriteLine();
                                Console.WriteLine("Input error! Please select a valid value!");
                                selection = 9;
                            }

                            Console.WriteLine();

                            switch (selection)
                            {
                                case 1: //print out suppliers array
                                    var supDetails =
                                        suppliers.Select(sup => new { num = sup.SN, name = sup.SName, status = sup.Status, city = sup.City });
                                    Console.WriteLine("Contents of Suppliers table: \n");
                                    Console.WriteLine("ID \t" + "Name \t" + "Status \t" + "City \t");
                                    Console.WriteLine("---------------------------------------");
                                    foreach (var sup in supDetails)
                                    {
                                        Console.WriteLine(sup.num + "\t" + sup.name + "\t" + sup.status + "\t" + sup.city);
                                    }
                                    Console.WriteLine();
                                    break;
                                case 2: //print out parts array
                                    var partDetails =
                                        parts.Select(part => new { num = part.PN, name = part.PName, color = part.Color, weight = part.Weight, city = part.City });
                                    Console.WriteLine("Contents of Parts table: \n");
                                    Console.WriteLine("ID \t" + "Name \t" + "Color \t" + "Weight \t" + "City \t");
                                    Console.WriteLine("---------------------------------------");
                                    foreach (var part in partDetails)
                                    {
                                        Console.WriteLine(part.num + "\t" + part.name + "\t" + part.color + "\t" + part.weight + "\t" + part.city);
                                    }
                                    Console.WriteLine();
                                    break;
                                case 3: //print out shipments array
                                    var shipDetails =
                                        shipments.Select(ship => new { supID = ship.SN, part = ship.PN, qty = ship.Qty });
                                    Console.WriteLine("Contents of Shipments table: \n");
                                    Console.WriteLine("SupplierID \t" + "PartID \t" + "Quantity \t");
                                    Console.WriteLine("---------------------------------------");
                                    foreach (var ship in shipDetails)
                                    {
                                        Console.WriteLine(ship.supID + "\t\t" + ship.part + "\t" + ship.qty + "\t");
                                    }
                                    Console.WriteLine();
                                    break;
                                default:
                                    break;
                            }

                        } while (selection != 0);
                        break;
                    case 2:
                        String color;
                        do
                        {
                            Console.WriteLine("Enter color to see what cities carry parts of selected color:");
                            Console.Write("Color to be found: ");
                            color = Console.ReadLine().ToLower();
                        } while (String.IsNullOrEmpty(color));
                        Console.WriteLine();

                        switch (color)
                        {
                            case "green":
                                var greenParts = parts.Where(clr => String.Equals(clr.Color.ToLower(), "green")).Select(x => new { x.Color, x.City }).Distinct().Select(Cit => Cit.City);
                                Console.WriteLine("Cities that have " + color.ToUpper() + " parts:");
                                Console.WriteLine("---------------------------------");
                                foreach (string cty in greenParts)
                                {
                                    Console.WriteLine(cty);

                                }
                                Console.WriteLine();
                                break;
                            case "blue":
                                IEnumerable<string> blueParts = parts.Where(clr => String.Equals(clr.Color.ToLower(), "blue")).Select(x => new { x.Color, x.City }).Distinct().Select(Cit => Cit.City);
                                Console.WriteLine("Cities that have " + color.ToUpper() + " parts:");
                                Console.WriteLine("---------------------------------");
                                foreach (string cty in blueParts)
                                {
                                    Console.WriteLine(cty);
                                }
                                Console.WriteLine();
                                break;
                            case "red":
                                var redParts = parts.Where(clr => String.Equals(clr.Color.ToLower(), "red")).Select(x => new { x.Color, x.City }).Distinct().Select(Cit => Cit.City);
                                Console.WriteLine("Cities that have " + color.ToUpper() + " parts:");
                                Console.WriteLine("---------------------------------");
                                foreach (string cty in redParts)
                                {
                                    Console.WriteLine(cty);
                                }
                                Console.WriteLine();
                                break;
                            default:
                                Console.WriteLine("Sorry, currently, there are no " + color.ToUpper() + " parts available.");
                                Console.WriteLine();
                                break;
                        }
                        break;
                    case 3:
                        IEnumerable<String> supNames = suppliers.OrderBy(sup => sup.SName).Select(sup => sup.SName);
                        Console.WriteLine("Current suppliers: ");
                        Console.WriteLine("-------------------");

                        foreach (string name in supNames)
                        {
                            Console.WriteLine(name);
                        }
                        Console.WriteLine();
                        break;
                    case 4:
                        String temp;

                        do
                        {
                            Console.WriteLine("Please, enter the supplier name or ID number you would like to view the orders for:");
                            Console.Write("Supplier name or ID: ");
                            temp = Console.ReadLine().ToLower();

                        } while (String.IsNullOrEmpty(temp));

                        int num;
                        Console.WriteLine();
                        Boolean noSupplier = false;

                        if (Int32.TryParse(temp, out num))
                        {
                            try
                            {
                                IEnumerable<string> supName = suppliers.Where(s => s.SN == num).Distinct().Select(name => name.SName);
                                temp = supName.First();
                                noSupplier = false;
                            }
                            catch
                            {
                                Console.WriteLine("There are no outstanding orders for supplier with ID " + temp.ToUpper() + " or supplier does not exist.");
                                Console.WriteLine();
                                noSupplier = true;
                            }
                        }
                        else
                        {
                            IEnumerable<int> sup = suppliers.Where(s => String.Equals(s.SName.ToLower(), temp)).Distinct().Select(ID => ID.SN);
                            try
                            {
                                num = sup.First();
                                noSupplier = false;
                            }
                            catch
                            {
                                Console.WriteLine("There are no outstanding orders for " + temp.ToUpper() + " or supplier does not exist.");
                                Console.WriteLine();
                                noSupplier = true;
                            }
                        }

                        if (!noSupplier)
                        {
                            var supAndShip = suppliers.Where(sup => sup.SN == num)
                                .Join(shipments, sup => sup.SN, shp => shp.SN,
                                (sup, shp) => new { sup.SN, sup.SName, shp.PN, shp.Qty })
                                .Join(parts, shp => shp.PN, pts => pts.PN, (shp, pts) => new { shp.SN, shp.SName, pts.PName, shp.Qty })
                                .Select(row => new { row.PName, row.Qty });

                            Console.WriteLine("Current orders for " + temp.ToUpper() + ":");
                            Console.WriteLine();
                            Console.WriteLine("Part \t" + "Quantity \t");
                            Console.WriteLine("------------------------------");

                            foreach (var row in supAndShip)
                            {
                                Console.WriteLine(row.PName + "\t" + row.Qty);
                            }
                        }
                        Console.WriteLine();
                        break;

                }
            } while (input != 0);

        }
    }
}

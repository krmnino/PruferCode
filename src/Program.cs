using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class Program
    {
        static List<Tuple<int, int>> InputParser(string input)
        {
            List<Tuple<int, int>> child_parent_table = new List<Tuple<int, int>>();
            int index_end = input.IndexOf(";");
            int child;
            int parent;
            bool root_set = false;
            if(input.Length == 0 || index_end == -1)
            {
                Console.WriteLine("Tree structure invalid. Usage: [child] [parent];");
                return null;
            }
            while (true)
            {
                string parent_child = input.Substring(0, index_end);
                while (parent_child[0] == ' ')
                {
                    parent_child = parent_child.Substring(1);
                }
                while (parent_child[parent_child.Length - 1] == ' ')
                {
                    parent_child = parent_child.Substring(0, parent_child.Length - 1);
                }
                child = Int32.Parse(parent_child.Substring(0, parent_child.IndexOf(" ")));
                parent = Int32.Parse(parent_child.Substring(parent_child.IndexOf(" ") + 1));
                if (!root_set)
                {
                    if (parent != 0)
                    {
                        Console.WriteLine("Root node is not [0].");
                        return null;
                    }
                    child_parent_table.Add(Tuple.Create(parent, parent));
                    root_set = true;
                }
                for (int i = 0; i < child_parent_table.Count; i++)
                {
                    if (child == child_parent_table[i].Item1)
                    {
                        Console.WriteLine(child + " is duplicated.");
                        return null;
                    }
                }
                child_parent_table.Add(Tuple.Create(child, parent));
                input = input.Substring(index_end + 1);
                index_end = input.IndexOf(";");
                if (index_end == -1)
                {
                    break;
                }
            }
            for (int i = 1; i < child_parent_table.Count; i++)
            {
                for (int j = 1; j < child_parent_table.Count; j++)
                {
                    if (child_parent_table[j].Item1 == i)
                    {
                        break;
                    }
                    if (j == child_parent_table.Count - 1)
                    {
                        Console.WriteLine("Sequence incomplete. Node [" + i + "] is missing.");
                        return null;
                    }
                    if (child_parent_table[j].Item1 > child_parent_table.Count)
                    {
                        Console.WriteLine("Node [" + child_parent_table[j].Item1 + "] is equal or greater than the upper bound for [" + child_parent_table.Count + "].");
                        return null;
                    }
                }
            }
            return child_parent_table;
        }

        static void command_line()
        {
            string[] commands_arr = { "new", "help", "status", "display", "prufer", "delete", "empty", "clear", "exit" };
            TreeContainer container = new TreeContainer();
            while (true)
            {
                Console.Write(">> ");
                string[] parsed_input = Console.ReadLine().Split(' ');
                if (!commands_arr.Contains<string>(parsed_input[0]))
                {
                    Console.WriteLine("Invalid command. Type 'help' for instructions.");
                    continue;
                }
                if(parsed_input[0] == "exit")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                if(parsed_input[0] == "help")
                {
                    Console.WriteLine("     COMMAND        |                              DESCRIPTION");
                    Console.WriteLine("--------------------+------------------------------------------------------------------------------");
                    Console.WriteLine("new -t              | Create a tree by entering a sequence of child-parent pairs.");
                    Console.WriteLine("                    |    usage new -t [child] [parent]; [child] [parent];...");
                    Console.WriteLine("new -p              | Create a tree by entering using a string containing Prufer code.");
                    Console.WriteLine("                    |    usage new -p [parent];[parent];[parent];...");
                    Console.WriteLine("help                | Display menu description for all exiting commands.");
                    Console.WriteLine("status              | Display the status of the tree container.");
                    Console.WriteLine("display [position]  | Display a tree from container at a given index.");
                    Console.WriteLine("prufer [position]   | Generates Prufer code from a tree stored in container at index [position].");
                    Console.WriteLine("empty               | Remove all exiting trees in container.");
                    Console.WriteLine("clear               | Clear the command line.");
                    Console.WriteLine("exit                | Quit program.");
                }
                if (parsed_input[0] == "new")
                {
                    if (parsed_input.Length != 2)
                    {
                        Console.WriteLine("Invalid input. The 'new' command requires a flag.");
                        Console.WriteLine(" -t: Create tree from a collection por pairs child-parent.");
                        Console.WriteLine(" -p: Create tree from Prufer code.");
                    }
                    else if (parsed_input[1] == "-t")
                    {
                        List<Tuple<int, int>> child_parent_table = new List<Tuple<int, int>>();
                        Console.WriteLine("INFO:");
                        Console.WriteLine("Tree structure format: [child] [parent]; [child] [parent]; ...");
                        Console.WriteLine("The first pair entered, parent must be 0 since it is the root.");
                        Console.WriteLine("=== Start here ===");
                        Console.Write(">> ");
                        string parent_child_input = Console.ReadLine();
                        List<Tuple<int, int>> table = InputParser(parent_child_input);
                        if (table != null)
                        {
                            int position = container.AddTree(new Tree(table));
                            Console.WriteLine("Tree added at index " + position);
                        }
                        continue;
                    }
                    else if (parsed_input[1] == "-p")
                    {
                        List<int> prufer_code = new List<int>();
                        Console.WriteLine("INFO:");
                        Console.WriteLine("Prufer format: [parent] [parent] ...");
                        Console.WriteLine("=== Start here ===");
                        Console.Write(">> ");
                        string prufer_input = Console.ReadLine();
                        int index_end = prufer_input.IndexOf(";");
                        if (prufer_input.Length == 0 || index_end == -1)
                        {
                            Console.WriteLine("Prufer code structure invalid. Usage: [parent];[parent]; ...[parent];");
                            continue;
                        }
                        while (true)
                        {
                            while (prufer_input[0] == ' ')
                            {
                                prufer_input = prufer_input.Substring(1);
                            }
                            while (prufer_input[prufer_input.Length - 1] == ' ')
                            {
                                prufer_input = prufer_input.Substring(0, prufer_input.Length - 1);
                            }
                            try
                            {
                                int parsed_prufer_val = Int32.Parse(prufer_input.Substring(0, prufer_input.IndexOf(";")));
                                prufer_code.Add(parsed_prufer_val);
                                prufer_input = prufer_input.Substring(index_end + 1);
                                index_end = prufer_input.IndexOf(";");
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Invalid value for index in container. Use an integer.");
                                break;
                            }
                            if (index_end == -1)
                            {
                                break;
                            }
                        }
                        container.AddTree(new Tree(prufer_code));
                        continue;
                    }
                }
                if (parsed_input[0] == "status")
                {
                    Console.WriteLine(container.ToString());
                }
                if (parsed_input[0] == "prufer")
                {
                    if(parsed_input.Length < 2)
                    {
                        Console.WriteLine("Must specify a tree in container. Use 'status' to check which tree to generate its Prufer code.");
                    }
                    try
                    {
                        int position = Int32.Parse(parsed_input[1]);
                        if (container.Collection[position] == null)
                        {
                            Console.WriteLine("Index " + position + " in container is empty.");
                        }
                        else
                        {
                            List<int> prufer = container.Collection[position].PruferCode();
                            string print_prufer = "Prufer: ";
                            for (int i = 0; i < prufer.Count; i++)
                            {
                                print_prufer += prufer[i] + " ";
                            }
                            Console.WriteLine(print_prufer);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid value for index in container. Use an integer.");
                    }
                    continue;
                }
                if(parsed_input[0] == "display")
                {
                    //TODO
                }
                if(parsed_input[0] == "compare")
                {
                    //TODO
                }
                if (parsed_input[0] == "delete")
                {
                    if(parsed_input.Length < 2)
                    {
                        Console.WriteLine("Must specify a tree in container. Use 'status' to check which tree to delete.");
                    }
                    try
                    {
                        int position = Int32.Parse(parsed_input[1]);
                        if(container.DeleteTree(position) == null)
                        {
                            Console.WriteLine("Index " + position + " in container is empty.");
                        }
                        else
                        {
                            Console.WriteLine("Deleted tree at index: " + position);
                        }
                        continue;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Invalid value for index in container. Use an integer.");
                    }
                    continue;
                }
                if(parsed_input[0] == "empty")
                {
                    container.EmptyCollection();
                    Console.WriteLine("Container emptied.");
                    continue;
                }
                if (parsed_input[0] == "clear")
                {
                    Console.Clear();
                    continue;
                }
            }
           
        }
        static void Main(string[] args)
        {
            command_line();
        }
    }
}
 
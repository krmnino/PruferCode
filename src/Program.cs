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
        static List<Tuple<int, int>> InputParser()
        {
            List<Tuple<int, int>> child_parent_table = new List<Tuple<int, int>>();
            String input = Console.ReadLine();
            int index_end = input.IndexOf(";");
            int child;
            int parent;
            bool root_set = false;
            while (true)
            {
                string parent_child = input.Substring(0, index_end);
                while(parent_child[0] == ' ')
                {
                    parent_child = parent_child.Substring(1);
                }
                while (parent_child[parent_child.Length-1] == ' ')
                {
                    parent_child = parent_child.Substring(0, parent_child.Length - 1);
                }
                child = Int32.Parse(parent_child.Substring(0, parent_child.IndexOf(" ")));
                parent = Int32.Parse(parent_child.Substring(parent_child.IndexOf(" ") + 1));
                if (!root_set)
                {
                    if(parent != 0)
                    {
                        Console.WriteLine("Root node is not [0].");
                        return new List<Tuple<int, int>>();
                    }
                    child_parent_table.Add(Tuple.Create(parent, parent));
                    root_set = true;
                }
                for(int i = 0; i < child_parent_table.Count; i++)
                {
                    if(child == child_parent_table[i].Item1)
                    {
                        Console.WriteLine(child + " is duplicated.");
                        return new List<Tuple<int, int>>();
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
                        return new List<Tuple<int, int>>();
                    }
                    if (child_parent_table[j].Item1 >= child_parent_table.Count)
                    {
                        Console.WriteLine("Node [" + child_parent_table[j].Item1 + "] is equal or greater than the upper bound for [" + child_parent_table.Count + "].");
                        return new List<Tuple<int, int>>();
                    }
                }
            }
            return child_parent_table;
        }

        static void command_line()
        {
            string[] commands_arr = { "new", "help", "status", "structure", "prufer", "erase", "clear", "exit" };
            TreeContainer container = new TreeContainer();
            List<string> prufer_code_container = new List<string>();
            while (true)
            {
                Console.Write(">> ");
                string input_cmd = Console.ReadLine();
                string[] parsed_input = input_cmd.Split(' ');
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
                        Console.WriteLine("Tree structure format: [child] [parent]");
                        Console.WriteLine("Press ENTER, and enter the next node or type '-1' to finish.");
                        Console.WriteLine("In the first pair entered, the parent must be 0 since it's the root.");
                        Console.WriteLine("=== Start here ===");
                        string parent_child_input = "";
                        int child, parent;
                        while (true)
                        {
                            Console.Write(">>> ");
                            parent_child_input = Console.ReadLine();
                            if(parent_child_input == "-1")
                            {
                                Console.WriteLine();
                                break;
                            }
                            while (parent_child_input[0] == ' ')
                            {
                                parent_child_input = parent_child_input.Substring(1);
                            }
                            while (parent_child_input[parent_child_input.Length - 1] == ' ')
                            {
                                parent_child_input = parent_child_input.Substring(0, parent_child_input.Length - 1);
                            }
                            try
                            {
                                child = Int32.Parse(parent_child_input.Substring(0, parent_child_input.IndexOf(" ")));
                                parent = Int32.Parse(parent_child_input.Substring(parent_child_input.IndexOf(" ") + 1));
                                child_parent_table.Add(Tuple.Create(child, parent));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid node value. Must be an integer.");
                                continue;
                            }
                        }
                        if(child_parent_table.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            container.AddTree(new Tree(child_parent_table));
                        }
                    }
                    else if (parsed_input[1] == "-p")
                    {
                        
                    }
                }
                if (parsed_input[0] == "status")
                {
                    
                }
                if (parsed_input[0] == "prufer")
                {

                }
                if (parsed_input[0] == "erase")
                {

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
            /*
            List<Tuple<int, int>> input_table = InputParser();
            Tree t = new Tree(input_table);
            List<int> prufer = t.PruferCode();
            Tree t2 = new Tree(prufer);*/
            command_line();
        }
    }
}
 
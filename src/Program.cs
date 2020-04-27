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
        static bool FindNode(List<Tuple<int, int>> table, int row)
        {
            for(int i = 0; i < table.Count; i++)
            {

            }
            return true;
        }
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
                    child_parent_table.Add(Tuple.Create(parent, parent));
                    root_set = true;
                }
                child_parent_table.Add(Tuple.Create(child, parent));
                Console.WriteLine(child + parent);
                Console.WriteLine(parent_child);
                input = input.Substring(index_end + 1);
                index_end = input.IndexOf(";");
                if(index_end == -1)
                {
                    break;
                }
            }
            return child_parent_table;
        }
        static void Main(string[] args)
        {
            Tree t = new Tree(new Node(0));
            t.Root.AddChild(4);
            t.Root.AddChild(6);
            t.Root.AddChild(3);
            t.Root.Children[0].AddChild(1);
            t.Root.Children[0].AddChild(2);
            t.Root.Children[0].AddChild(7);
            t.Root.Children[0].Children[1].AddChild(5);
            t.Root.Children[0].Children[1].Children[0].DisplayData();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(4, 0);
            dic.Add(6, 0);
            dic.Add(3, 0);
            dic.Add(1, 6);
            dic.Add(2, 6);
            dic.Add(7, 6);
            dic.Add(5, 2);
            Hashtable hs = new Hashtable(dic);
            Console.WriteLine((int)hs[1]);
            //InputParser();
            List<int> data = t.BFS_traversal();
            
        }
    }
}
 
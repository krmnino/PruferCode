using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class TreeContainer
    {
        private List<Tree> collection;
        private int min_empty_spot;
        public List<Tree> Collection { get => collection; }

        public TreeContainer()
        {
            this.min_empty_spot = 0;
            this.collection = new List<Tree>();
            for(int i = 0; i < 10; i++)
            {
                collection.Add(null);
            }
        }

        public TreeContainer(int size)
        {
            this.min_empty_spot = 0;
            this.collection = new List<Tree>();
            for (int i = 0; i < size; i++)
            {
                collection.Add(null);
            }
        }
        public void AddTree(Tree t)
        {
            this.collection[this.min_empty_spot] = t;
            int i = this.min_empty_spot;
            do
            {
                this.min_empty_spot++;
            } while (i < this.collection.Count || this.collection[i] != null);
        }
        public Tree DeleteTree(int position)
        {
            Tree out_tree = null;
            if(position < collection.Count)
            {
                out_tree = collection[position];
                collection[position] = null;
                if(position > this.min_empty_spot)
                {
                    this.min_empty_spot = position;
                }
            }
            return out_tree;
        }
        public override string ToString()
        {
            string out_str = "";
            for(int i = 0; i < collection.Count; i++)
            {
                if(collection[i] == null)
                {
                    out_str += "[NULL]";
                }
                else
                {
                    out_str += "[TREE" + i + "E]";
                }
                if(i != collection.Count - 1)
                {
                    out_str += ",";
                }
            }
            return out_str;
        }
    }
}

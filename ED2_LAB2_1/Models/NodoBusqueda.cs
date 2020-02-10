using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_LAB2_1.Models
{
    public class NodoBusqueda
    {
        //Hijos
        public List<NodoBusqueda> edges { get; private set; }
        //Llaves del arbol
        public List<string> keys { get; private set; }
        public NodoBusqueda parent { get; set; }

        public NodoBusqueda(string key)
        {
            keys = new List<string>();
            keys.Add(key);
            edges = new List<NodoBusqueda>();
        }
        public int Key(string x)
        {
            for (int y = 0; y < keys.Count; y++)
            {
                if (keys[y].CompareTo(x) == 0)
                {
                    return 1;
                }
            }
            return -1;
        }
        public void InsertEdge(NodoBusqueda edge)
        {
            for (int x = 0; x < edges.Count; x++)
            {
                if (edges[x].keys[0].CompareTo(edge.keys[0]) > 0)
                {
                    edges.Insert(x, edge);
                    return;
                }
            }
            edges.Add(edge);
            edge.parent = this;
        }
        public NodoBusqueda GetEdge(int pos)
        {
            if (pos < edges.Count)
            {
                return edges[pos];
            }
            else
            {
                return null;
            }
        }
        public int FindEdgePosition(string k)
        {
            if (keys.Count != 0)
            {
                string left = " ";
                for (int x = 0; x < keys.Count; x++)
                {
                    if (left.CompareTo(k) < 0 && k.CompareTo(keys[x]) < 0)
                    {
                        return x;
                    }
                    else
                    {
                        left = keys[x];
                    }
                }
                if (k.CompareTo(keys[keys.Count - 1]) > 0)
                {
                    return keys.Count;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public void Fuse(NodoBusqueda n1)
        {
            int totalkeys = n1.keys.Count;
            int totaledges = n1.edges.Count;

            totalkeys += this.keys.Count;
            totaledges += this.edges.Count;

            if (totalkeys > 5)
            {
                throw new InvalidOperationException("Total keys of all nodes exceeded 5");
            }

            if (totaledges > 6)
            {
                throw new InvalidOperationException("Total edges of all nodes exceeded 6");
            }

            for (int x = 0; x < n1.keys.Count; x++)
            {
                string k = n1.keys[x];
                this.Push(k);
            }
        }
        public void Fuse(NodoBusqueda n1, NodoBusqueda n2)
        {
            int totalkeys = n1.keys.Count;
            int totaledges = n1.edges.Count;

            totalkeys += n2.keys.Count;
            totaledges += n2.edges.Count;
            totalkeys += this.keys.Count;
            totaledges += this.edges.Count;

            if (totalkeys > 5)
            {
                throw new InvalidOperationException("Total keys of all nodes exceeded 5");
            }

            if (totaledges > 6)
            {
                throw new InvalidOperationException("Total edges of all nodes exceeded 6");
            }

            this.Fuse(n1);
            this.Fuse(n2);
        }
        public NodoBusqueda[] Split()
        {
            if (keys.Count != 4)
            {
                throw new InvalidOperationException(string.Format("This node has {0} keys, can only split a 4 keys node", keys.Count));
            }
            NodoBusqueda new_right = new NodoBusqueda(keys[1]);
            for (int x = 4; x < edges.Count; x++)
            {
                new_right.edges.Add(this.edges[x]);
            }
            for (int x = edges.Count - 1; x >= 4; x--)
            {
                this.edges.RemoveAt(x);
            }
            for (int x = 1; x < edges.Count; x++)
            {
                keys.RemoveAt(x);
            }
            return new NodoBusqueda[] { this, new_right };
        }
        public string Pop(int pos)
        {
            if (keys.Count == 1)
            {
                throw new InvalidOperationException("Cannot pop value from a 1 key node");
            }
            if (pos < keys.Count)
            {
                string k = keys[pos];
                keys.RemoveAt(pos);
                return k;
            }
            return null;
        }
        public void Push(string k)
        {
            if (keys.Count == 5)
            {
                throw new InvalidOperationException("Cannot push value from a 5 key node");
            }
            if (keys.Count == 0)
            {
                keys.Add(k);
            }
            else
            {
                string left = " ";
                for (int x = 0; x < keys.Count; x++)
                {
                    if (left.CompareTo(k) < 0 && k.CompareTo(keys[x]) < 0)
                    {
                        keys.Insert(x, k);
                        return;
                    }
                    else
                    {
                        left = keys[x];
                    }
                }
                keys.Add(k);
            }
        }
        public NodoBusqueda Traverse(string k)
        {
            int pos = FindEdgePosition(k);

            if (pos < edges.Count && pos > -1)
            {
                return edges[pos];
            }
            else
            {
                return null;
            }
        }
    }
}


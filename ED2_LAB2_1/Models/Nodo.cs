﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_LAB2_1.Models
{
    public class Nodo
    {
        //Hijos
        public List<Nodo> edges { get; private set; }
        //Llaves del arbol
        public List<int> keys { get; private set; }
        public Nodo parent { get; set; }

        public Nodo(int key)
        {
            keys = new List<int>();
            keys.Add(key);
            edges = new List<Nodo>();
        }

        public int Key(int x)
        {
            for(int y = 0; y < keys.Count; y++) 
            {
                if(keys[y] == x)
                {
                    return y;
                }
            }
            return -1;
        }
        public void InsertEdge(Nodo edge)
        {
            for(int x = 0; x < edges.Count; x++)
            {
                if(edges[x].keys[0] > edge.keys[0])
                {
                    edges.Insert(x, edge);
                    return;
                }
            }
            edges.Add(edge);
            edge.parent = this;
        }
        public Nodo GetEdge(int pos)
        {
            if(pos < edges.Count)
            {
                return edges[pos];
            }
            else
            {
                return null;
            }
        }
        public int FindEdgePosition(int k)
        {
            if(keys.Count != 0)
            {
                int left = 0;
                for(int x = 0; x <keys.Count; x++)
                {
                    if(left <= k && k < keys[x])
                    {
                        return x;
                    }
                    else
                    {
                        left = keys[x];
                    }
                }
                if(k > keys[keys.Count - 1])
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
        public void Fuse(Nodo n1)
        {
            int totalkeys = n1.keys.Count;
            int totaledges = n1.edges.Count;

            totalkeys += this.keys.Count;
            totaledges += this.edges.Count;

            if(totalkeys > 5)
            {
                throw new InvalidOperationException("Total keys of all nodes exceeded 5");
            }

            if(totaledges > 6)
            {
                throw new InvalidOperationException("Total edges of all nodes exceeded 6");
            }

            for(int x = 0; x < n1.keys.Count; x++)
            {
                int k = n1.keys[x];
                this.Push(k);
            }
        }
        public void Fuse(Nodo n1, Nodo n2)
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
        public Nodo[] Split()
        {
            if(keys.Count != 4)
            {
                throw new InvalidOperationException(string.Format("This node has {0} keys, can only split a 4 keys node", keys.Count));
            }
            Nodo new_right = new Nodo(keys[1]);
            for(int x = 4; x < edges.Count; x++)
            {
                new_right.edges.Add(this.edges[x]);
            }
            for(int x = edges.Count - 1 ; x >= 4; x--)
            {
                this.edges.RemoveAt(x);
            }
            for (int x = 1; x < edges.Count; x++)
            {
                keys.RemoveAt(x);
            }
            return new Nodo[] { this, new_right };
        }
        public int? Pop(int pos)
        {
            if(keys.Count == 1)
            {
                throw new InvalidOperationException("Cannot pop value from a 1 key node");
            }
            if(pos < keys.Count)
            {
                int k = keys[pos];
                keys.RemoveAt(pos);
                return k;
            }
            return null;
        }
        public void Push(int k)
        {
            if(keys.Count == 5)
            {
                throw new InvalidOperationException("Cannot push value from a 5 key node");
            }
            if(keys.Count == 0)
            {
                keys.Add(k);
            }
            else
            {
                int left = 0;
                for(int x = 0; x < keys.Count; x++)
                {
                    if(left <= k && k < keys[x])
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
        public Nodo Traverse(int k)
        {
            int pos = FindEdgePosition(k);

            if(pos < edges.Count && pos > -1)
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

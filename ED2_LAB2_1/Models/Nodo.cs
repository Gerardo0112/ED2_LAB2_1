using System;
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
            for(int y = 0; ; y < keys.Count; y++) 
            {
                if(keys[y] == x)
                {
                    return y;
                }
            }
            return -1;
        }
    }
 }

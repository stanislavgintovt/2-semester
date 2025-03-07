using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Trie
{
    public class Trie
    {
        Vertex head;
        private int _size;
        public int Size
        {
            get => _size;
        }
        public Trie()
        {
            head = new();
        }

        class Vertex
        {
            const int alphabethSize = 2048;
            public Vertex?[] next;
            public bool isTerminal;
            public int size;

           public Vertex()
            {
                next = new Vertex[alphabethSize];
                isTerminal = false;
                size = 0;
            }

            public Vertex? GetNext(char element)
            {
                return next[element];
            }
            public bool Add(char element)
            {
                if (next[element] == null)
                {
                    next[element] = new();
                    size++;
                    return true;
                }
                return false;
            }

            public bool Contain(char element)
            {
                if (next[element] != null)
                {
                    return true;
                }
                return false;
            }

            public bool Remove(char element)
            {
                if (next[element] != null)
                {
                    next[element] = null;
                    size--;
                    return true;
                }
                return false;
            }
        }
        public bool Contain(string element)
        {
            Vertex current = head;
            for (int i = 0; i < element.Length; i++)
            {
                if (!current.Contain(element[i]))
                {
                    return false;
                }
                current = current.next[element[i]];
            }
            return current.isTerminal;
        }

        public bool Add(string element)
        {
            Vertex current = head;
            if (Contain(element))
            {
                return false;
            }

            for (int i = 0; i < element.Length; i++)
            {
                if (!current.Contain(element[i]))
                {
                    current.Add(element[i]);
                }
                else
                {
                    current.size++;
                }
                    current = current.next[element[i]];
            }
            current.isTerminal = true;
            _size++;
            return true;
        }

        public bool Remove(string element)
        {
            Vertex current = head;

            if (!Contain(element))
            {
                return false;
            }

            for (int i = 0; i < element.Length; i++)
            {
                if (current.next[element[i]].size == 1 && current.isTerminal)
                {
                    current.Remove(element[i]);
                    return true;
                }
                current.size--;
                current = current.next[element[i]];
            }
            if (current.isTerminal)
            {
                current.isTerminal = false;
                current.size--;
                return true;
            }

            throw new Exception("proga ne rabotaet");
        }

        public bool Add(string[] element)
        {
            bool flag = false;
            foreach (string i in element)
            {
                if (!Add(i))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            Vertex current = head;
            for (int i = 0; i < prefix.Length; i++)
            {
                if (!current.Contain(prefix[i]))
                {
                    return 0;
                }
                current = current.next[prefix[i]];
            }
            return current.size;
        }
    }
}

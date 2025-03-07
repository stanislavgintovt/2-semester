using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Trie
{                                                        //This structure has field for head of tree, and size, which can be useful. 
    public class Trie
    {
        Vertex head;
        private int _size;                               // Maybe will be useful
        public int Size
        {
            get => _size;
        }
        public Trie()                                    // Empty trie contains only that head.
        {
            head = new();
        }

        class Vertex                                     // Node of tree
        {
            const int alphabethSize = 2048;              // Size of alphabeth, probably this number is big enough
            public Vertex?[] next;                       // In fact this is a hashtable, where hash function returns a number which represented in char
            public bool isTerminal;                      // This variable show, is there any word in trie ended there
            public int size;                             // This will simplify HowManyStartsWithPrefix() function

           public Vertex()                               // new Vertex should be empty
            {
                next = new Vertex[alphabethSize];
                isTerminal = false;                      // This will be true only when we add string
                size = 0;
            }
            /*
            public Vertex? GetNext(char element)
            {
                return next[element];
            }
            I thought it will be useful
            */
            public bool Add(char element)                // Adds a new branch to the vertex
            {                                            // This function will increase counter when used, because used when string adds
                if (next[element] == null)               // Adds only if there no that branch
                {
                    next[element] = new();
                    size++;                              // counter
                    return true;
                }
                return false;
            }

            public bool Contain(char element)
            {
                if (next[element] != null)               // Search for that branch
                {
                    return true;
                }
                return false;
            }

            public bool Remove(char element)             // Remove that branch
            {
                if (next[element] != null)
                {
                    next[element] = null;
                    size--;                              // decrease counter
                    return true; 
                }
                return false; 
            }
        }
        public bool Contain(string element)              // Analog for contain check for string, as functions below
        {
            Vertex current = head;                       // Initializing temporary reference to vertex is only
            for (int i = 0; i < element.Length; i++)     // way to check for all chars in string
            {
                if (!current.Contain(element[i]))        // IntelliSence says it can be null... Well, it can't, because contain function. 13 warnings, don't pay attention
                { 
                    return false;                        // If there no branch for i-th char on i-th branch, trie obviously does not contain string
                }
                current = current.next[element[i]];      // If there is that branch, we go to the next
            }
            return current.isTerminal;                   // There can be situation where our string can be prefix, but is not in trie
        }

        public bool Add(string element)              
        {
            Vertex current = head;
            if (Contain(element))                        // I think this check is neсessary, because if we wouldn't know is this word on tree, we must go twice one path 
            {
                return false;
            }

            for (int i = 0; i < element.Length; i++)
            {
                if (!current.Contain(element[i]))
                {
                    current.Add(element[i]);             // counter increases in that function
                }
                else
                {
                    current.size++;                      // there changing counter is "manual"
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

            if (!Contain(element))                       // there we alse need to check if there element is contained
            {
                return false;
            }

            for (int i = 0; i < element.Length; i++)
            {
                if (current.next[element[i]].size == 1)   // We remove branch only if there is only one way from it
                {
                    current.Remove(element[i]);
                    _size--;
                    return true;
                }
                current.size--;                           // decreasing counter (we deleting)
                current = current.next[element[i]];
            }
            if (current.isTerminal)                       // So, fimal point
            {
                current.isTerminal = false;             
                current.size--;
                _size--;
                return true;
            }

            throw new Exception("Seems to be some data manipulations");     // Luck to catch this exception (it can be catch only if terminal vertex,
        }                                                                   // which guatanteed to be that by Contain method, will become not terminal, which is impossible)

        public bool Add(string[] element)  // for adding multiple strings
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
                    return 0; // We cant pass that path, so, there no string started with that prefix
                }
                current = current.next[prefix[i]];
            }
            return current.size; // The only use of this field
        }
    }
}

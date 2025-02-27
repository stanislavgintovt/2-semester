using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BurrowsWheeler
{
    public class Transformation
    {
        public delegate int StringCompare(string str, int left, int right);
        static int Modulo(int num, int max)
        {
            if (num % max < 0)
            {
                return max + num % max;
            }
            return num % max;
        }
        public static (string, int) BurrowWheelerTransformation(string str)
        {
            StringCompare comparator = PermutationsCompare;
            int[] ordinals = new int[str.Length];
            for(int i = 0; i < str.Length; i++)
            {
                ordinals[i] = i; // Creates an array of "permutations" (number is mean offset)
            }

            MergeSortPermutations(comparator, str, ordinals);

            char[] strTransformated = new char[str.Length];
            int index = 0;
            for (int i = 0; i < str.Length; i++)
            {
                strTransformated[i] = GetCharFromNumberedPermutation(str, ordinals[i], -1);
                if (ordinals[i] == 0)
                {
                    index = i;
                }
            }
            string res = new(strTransformated);
            return (res, index);
        }
        static void MergeSortPermutations(StringCompare comparator, string str, int[] perms, int min = 0, int max = -1)
        {
            if (max == -1)
            {
                max = str.Length;
            }
            if (max == min)
            {
                return;
            }

            if (max - min <= 10)
            {
                InsertionSortPermutations(comparator, str, perms, min, max);
                return;
            }

            int middle = (max + min) / 2;
            MergeSortPermutations(comparator, str, perms, min, middle);
            MergeSortPermutations(comparator, str, perms, middle, max);
            MergePermutations(comparator, str, perms, min, middle, max);
        }
        static void MergePermutations(StringCompare comparator, string str, int[] perms, int min, int middle, int max)
        {
            int leftSize = middle - min;
            int rightSize = max - middle;
            int[] minTemp = new int[leftSize];
            int[] maxTemp = new int[rightSize];
            int i, j;

            for (i = 0; i < leftSize; i++)
                minTemp[i] = perms[min + i];
            for (j = 0; j < rightSize; j++)
                maxTemp[j] = perms[middle + j];

            (i, j) = (0, 0);
            int k = min;

            while (i < leftSize && j < rightSize)
            {
                if (comparator(str, minTemp[i], maxTemp[j]) <= 0)
                {
                    perms[k++] = minTemp[i++];
                }
                else
                {
                    perms[k++] = maxTemp[j++];
                }
            }
            while (i < leftSize)
            {
                perms[k++] = minTemp[i++];
            }
            while (j < rightSize)
            {
                perms[k++] = maxTemp[j++];
            }
        }
        static int PermutationsCompare(string str, int ord1, int ord2) 
        {
            int a, b;
            for (int i = 0; i < str.Length; i++)
            {
                (a, b) = (GetCharFromNumberedPermutation(str, ord1, i), GetCharFromNumberedPermutation(str, ord2, i));
                if (a > b)
                { 
                    return 1;
                }
                if (a < b)
                {
                    return -1;
                }
            }
            return 0; // It's equal comparing two different strings having same length
        }
        static int CharCompare(string str, int ord1, int ord2)
        {
            if (str[ord1] > str[ord2])
            {
                return 1;
            }
            if (str[ord1] < str[ord2])
            {
                return -1;
            }
            return 0; // Comparing two different symbols having their own indexes
        }
        static void InsertionSortPermutations(StringCompare comparator, string str, int[] perms, int min, int max)
        {
            for (int i = min + 1; i < max; i++)
            {
                int key = perms[i];
                bool flag = false;
                for (int j = i - 1; j >= min && !flag;)
                {
                    if (comparator(str, key, perms[j]) == -1)
                    {
                        perms[j + 1] = perms[j];
                        j--;
                        perms[j + 1] = key;
                    }
                    else flag = true;
                }
            }
            return;
        }
        
        static char GetCharFromNumberedPermutation(string str, int ord, int i)
        {
            return str[Modulo(i + ord, str.Length)]; // str[Modulo(i + ord, str1.Length)] gain the i-th symbol of ord-th permutation.
        }                                         // For example, zeroth permutation of "abc" is "abc", second is "cab"
        public static string ReverseBurrowWheelerTransformation(string str, int index)
        {
            StringCompare comparator = CharCompare; //We need to sort string by alphabeth

            int[] ordinals = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ordinals[i] = i; // Creates an array of "permutations" (number is mean offset)
            }

            MergeSortPermutations(comparator, str, ordinals); // Method uses sorted array of source string
            
            char[] res = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                index = ordinals[index];
                res[i] = str[index];
            }

            string source = new(res);
            return source;
        }
    }
}

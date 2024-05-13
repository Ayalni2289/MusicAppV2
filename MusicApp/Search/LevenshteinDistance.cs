using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Search
{
    public static class LevenshteinDistance
    {
        public static int Compute(string s, string t)
        {
            // Dynamic programming table (from 0 to Length)
            int[,] distance = new int[s.Length + 1, t.Length + 1];

            // Base case: edit distance between an empty string and a string of length x is always x
            for (int i = 0; i <= s.Length; i++)
            {
                distance[i, 0] = i;
            }

            // Base case: edit distance between an empty string and a string of length x is always x
            for (int j = 0; j <= t.Length; j++)
            {
                distance[0, j] = j;
            }

            // Go through the table computing the edit distance by taking the minimum of the three
            // possible following operations and adding 1 to that (or 0 if the characters are equal)
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(Math.Min(
                        distance[i - 1, j] + 1,
                        distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            // Return edit distance for the entire problem
            return distance[s.Length, t.Length];
        }

        // Given two strings verify if they are a fuzzy match
        public static bool IsFuzzyMatch(string s, string t, int threshold)
        {
            return Compute(s, t) <= threshold;
        }
    }
}

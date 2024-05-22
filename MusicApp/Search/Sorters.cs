using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Search
{
    public class Sorters
    {
        public List<SearchResultItemControl> AlphabeticalQuickSort(List<SearchResultItemControl> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }

            SearchResultItemControl pivot = list[list.Count / 2];
            List<SearchResultItemControl> less = new List<SearchResultItemControl>();
            List<SearchResultItemControl> greater = new List<SearchResultItemControl>();

            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count / 2)
                {
                    continue;
                }

                if (string.Compare(list[i].title.Text, pivot.title.Text) < 0)
                {
                    less.Add(list[i]);
                }
                else
                {
                    greater.Add(list[i]);
                }
            }

            List<SearchResultItemControl> sortedList = new List<SearchResultItemControl>();
            sortedList.AddRange(AlphabeticalQuickSort(less));
            sortedList.Add(pivot);
            sortedList.AddRange(AlphabeticalQuickSort(greater));

            return sortedList;
        }

        public List<SearchResultItemControl> NumericalQuickSort(List<SearchResultItemControl> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }

            SearchResultItemControl pivot = list[list.Count / 2];
            List<SearchResultItemControl> less = new List<SearchResultItemControl>();
            List<SearchResultItemControl> greater = new List<SearchResultItemControl>();

            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count / 2)
                {
                    continue;
                }

                if (int.Parse(list[i].subTitle2.Text) < int.Parse(pivot.subTitle2.Text))
                {
                    less.Add(list[i]);
                }
                else
                {
                    greater.Add(list[i]);
                }
            }

            List<SearchResultItemControl> sortedList = new List<SearchResultItemControl>();
            sortedList.AddRange(NumericalQuickSort(greater));
            sortedList.Add(pivot);
            sortedList.AddRange(NumericalQuickSort(less));

            return sortedList;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgorithm.Logics
{
    /// <summary>
    /// 配列を半分い割り、左、右それぞれを2つペアまで分割しそれぞれをソート(分割統治)。左側でソートしたペアを2->4->8->16.... と順にマージしつつソートする。右側も同様に、ペアをマージしつつソートする。最後に左と右をマージする。これによりソートの範囲を常に抑えることで常に安定しつつ、安定したソートを行うことができる。
    /// </summary>
    /// <remarks>
    /// stable : yes
    /// inplace : no
    /// Compare : n log2 n
    /// Swap : n log2 n
    /// Order : O(n log n) (Worst case : O(n log n))
    /// ArraySize : 100, IsSorted : True, sortKind : MergeSort, IndexAccessCount : 534, CompareCount : 544, SwapCount : 623
    /// ArraySize : 1000, IsSorted : True, sortKind : MergeSort, IndexAccessCount : 6661, CompareCount : 8724, SwapCount : 9342
    /// ArraySize : 10000, IsSorted : True, sortKind : MergeSort, IndexAccessCount : 86596, CompareCount : 120487, SwapCount : 128076
    /// </remarks>
    /// <typeparam name="T"></typeparam>

    public class MergeSort<T> : SortBase<T> where T : IComparable<T>
    {
        public override SortType SortType => SortType.Merge;

        public override T[] Sort(T[] array)
        {
            base.Statics.Reset(array.Length, SortType, nameof(MergeSort<T>));
            var work = new T[(array.Length) / 2];
            Sort(array, 0, array.Length - 1, work);
            return array;
        }

        private T[] Sort(T[] array, int left, int right, T[] work)
        {
            var mid = (left + right) / 2;
            if (left == right) return array;
            base.Statics.AddIndexAccess();

            // left : merge + sort
            Sort(array, left, mid, work);
            // right : merge + sort
            Sort(array, mid + 1, right, work);
            // left + right: merge
            Merge(array, left, right, mid, work);
            return array;
        }

        // escape left to work, then merge right, and last for left.
        private T[] Merge(T[] array, int left, int right, int mid, T[] work)
        {
            T max = default(T);
            // if array[2] = x,y. set work[0] = x
            for (var i = left; i <= mid; i++)
            {
                base.Statics.AddIndexAccess();
                work[i - left] = array[i];

                // max assign
                if (i - left >= work.Length - 1)
                {
                    max = array.Max();
                    break;
                }
            }

            int l = left;
            int r = mid + 1;

            // merge array-left and work
            while (true)
            {
                var k = l + r - (mid + 1);
                // if left is sorted then merge done.
                if (l > mid) break;

                // if right is sorted, do left(work)
                if (r > right)
                {
                    while (l <= mid)
                    {
                        base.Statics.AddIndexAccess();
                        k = l + r - (mid + 1);
                        Swap(ref array[k], ref work[l - left]);

                        // max assign on edge case
                        if (l - left >= work.Length - 1)
                        {
                            array[right] = max;
                            break;
                        }
                        l++;
                    }
                    break;
                }

                // sort
                base.Statics.AddCompareCount();
                if (work[l - left].CompareTo(array[r]) < 0)
                {
                    Swap(ref array[k], ref work[l - left]);
                    l++;
                }
                else
                {
                    Swap(ref array[k], ref array[r]);
                    r++;
                }
            }

            return array;
        }
    }

    /// <summary>
    /// 非効率なマージソート
    /// </summary>
    /// <remarks>
    /// ArraySize : 100, IsSorted : True, sortKind : MergeSort2, IndexAccessCount : 771, CompareCount : 536, SwapCount : 672
    /// ArraySize : 1000, IsSorted : True, sortKind : MergeSort2, IndexAccessCount : 10975, CompareCount : 8736, SwapCount : 9976
    /// ArraySize : 10000, IsSorted : True, sortKind : MergeSort2, IndexAccessCount : 143615, CompareCount : 120366, SwapCount : 133616
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class MergeSort2<T> : SortBase<T> where T : IComparable<T>
    {
        public override SortType SortType => SortType.Merge;

        public override T[] Sort(T[] array)
        {
            base.Statics.Reset(array.Length, SortType, nameof(MergeSort2<T>));
            return SortImpl(array);
        }

        private T[] SortImpl(T[] array)
        {
            if (array.Length <= 1) return array;

            base.Statics.AddIndexAccess();

            int mid = array.Length / 2;
            var left = array.Take(mid).ToArray();
            var right = array.Skip(mid).ToArray();
            left = SortImpl(left);
            right = SortImpl(right);
            var result = Merge(left, right);
            return result;
        }

        private T[] Merge(T[] left, T[] right)
        {
            var result = new T[left.Length + right.Length];
            var i = 0;
            var j = 0;
            var current = 0;
            while (i < left.Length || j < right.Length)
            {
                base.Statics.AddIndexAccess();
                if (i < left.Length && j < right.Length)
                {
                    base.Statics.AddCompareCount();
                    if (left[i].CompareTo(right[j]) <= 0)
                    {
                        Swap(ref result[current], ref left[i++]);
                    }
                    else
                    {
                        Swap(ref result[current], ref right[j++]);
                    }
                }
                else if (i < left.Length)
                {
                    Swap(ref result[current], ref left[i++]);
                }
                else
                {
                    Swap(ref result[current], ref right[j++]);
                }
                current++;
            }

            return result;
        }
    }
}

﻿using SortAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortTests
{
    public class MockNegativeRandomData : IEnumerable<object[]>
    {
        private List<object[]> testData = new List<object[]>();

        public MockNegativeRandomData()
        {
            testData.Add(new object[] { new InputSample<int>() { InputType = InputType.NegativeRandom, Samples = Enumerable.Range(-100, 100).Sample(100).ToArray() } });
            testData.Add(new object[] { new InputSample<int>() { InputType = InputType.NegativeRandom, Samples = Enumerable.Range(-1000, 1000).Sample(1000).ToArray() } });
            testData.Add(new object[] { new InputSample<int>() { InputType = InputType.NegativeRandom, Samples = Enumerable.Range(-10000, 10000).Sample(10000).ToArray() } });
        }

        public IEnumerator<object[]> GetEnumerator() => testData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

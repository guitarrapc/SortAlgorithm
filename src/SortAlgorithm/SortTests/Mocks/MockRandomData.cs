﻿using SortAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortTests
{
    public class MockRandomData : IEnumerable<object[]>
    {
        private List<object[]> testData = new List<object[]>();

        public MockRandomData()
        {
            testData.Add(new object[] { new InputSample<int>() { InputType = InputType.Random, Samples = Enumerable.Range(0, 100).Sample(100).ToArray() } });
            testData.Add(new object[] { new InputSample<int>() { InputType = InputType.Random, Samples = Enumerable.Range(0, 1000).Sample(1000).ToArray() } });
            testData.Add(new object[] { new InputSample<int>() { InputType = InputType.Random, Samples = Enumerable.Range(0, 10000).Sample(10000).ToArray() } });
        }

        public IEnumerator<object[]> GetEnumerator() => testData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator.DataStructures
{
    public class Point<T>
    {
        public int index { get; set; }
        public T value { get; set; }

        public Point() { }
        public Point(int index, T value)
        {
            this.index = index;
            this.value = value;
        }
    }
}

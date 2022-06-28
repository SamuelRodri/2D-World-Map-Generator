using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public int index { get; }
    public Vector3 point { get; }

    public Node prev { get; set; }
    public Node next { get; set; }

    public Node(int i, Vector3 p)
    {
        this.index = i;
        this.point = p;
        this.next = null;
    }
}

public abstract class Polygon
{
    protected Node first;
    protected Node last;

    public int Count;
    protected Vector3[] vertices;

    public void AddNode(Vector3 point)
    {
        Node newNode = new Node(Count, point);
        newNode.next = null;

        if (Count == 0) // First Node
        {
            first = newNode;
            last = newNode;
        }

        last.next = newNode;
        newNode.next = null;
        last = newNode;
        Count++;
    }

    public void DeleteNode()
    {
        Count--;
    }

    public Vector3[] GetVertices()
    {
        Vector3[] vertices = new Vector3[Count];

        Node node = first;

        while (node != null)
        {
            vertices[node.index] = node.point;
        }
        return vertices;
    }
}

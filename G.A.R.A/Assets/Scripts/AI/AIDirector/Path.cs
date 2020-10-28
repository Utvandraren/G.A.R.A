using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Path
{
    Stack<Node> nodes; //Nodes of the path
    Stack<Edge> edges; //edges connecting the nodes of the path

    public Path()
    {
        nodes = new Stack<Node>();
        edges = new Stack<Edge>();
    }

    public void AddNode(Node node)
    {
        nodes.Push(node);
    }
    public void RemoveNode(Node node)
    {
        nodes.Pop();
    }
    
    public void AddEdge(Edge edge)
    {
        edges.Push(edge);
    }
    public void RemoveEdge(Edge edge)
    {
        edges.Pop();
    }


    public void TraversePath()
    {
        while (nodes.Count > 0)
        {

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Path
{
    public Stack<Node> nodes; //Nodes of the path
    public Stack<Edge> edges; //edges connecting the nodes of the path

    public Path()
    {
        nodes = new Stack<Node>();
        edges = new Stack<Edge>();
    }
    public Path(Path original)
    {
        nodes = new Stack<Node>(original.nodes);
        edges = new Stack<Edge>(original.edges);
        nodes = new Stack<Node>(nodes);
        edges = new Stack<Edge>(edges);
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


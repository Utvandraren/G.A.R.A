using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Path
{
    List<Node> nodes; //Nodes of the path
    List<Edge> edges; //edges connecting the nodes of the path

    public Path()
    {
        nodes = new List<Node>();
        edges = new List<Edge>();
    }

    public void AddNode(Node node)
    {
        nodes.Add(node);
    }
    public void RemoveNode(Node node)
    {
        nodes.Remove(node);
    }
    
    public void AddEdge(Edge edge)
    {
        edges.Add(edge);
    }
    public void RemoveEdge(Edge edge)
    {
        edges.Remove(edge);
    }


    public void TraversePath()
    {

    }
}


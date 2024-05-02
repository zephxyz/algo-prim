using System;
using System.Collections.Generic;
using System.Linq;


class Edge{
    public int from;
    public int to;
    public int weight;
    public Edge(int from, int to, int weight){
        this.from = from;
        this.to = to;
        this.weight = weight;
    }

}
class Graph
{
    public List<int> Vertices { get; private set; } = new();
    public List<Edge> Edges { get; private set; } = new();

    public void AddEdge(Edge edge)
    {
        if (!Vertices.Contains(edge.from))
        {
            Vertices.Add(edge.from);
        }
        if (!Vertices.Contains(to))
        {
            Vertices.Add(edge.to);
        }

        Edges.Add(edge);
    }
}

class TreeNode
{
    public int Vertex { get; private set; }
    public int Weight { get; private set; }
    public List<TreeNode> Children { get; private set; }

    public TreeNode(int vertex, int weight)
    {
        Vertex = vertex;
        Weight = weight;
        Children = new List<TreeNode>();
    }

    public void AddChild(TreeNode child)
    {
        Children.Add(child);
    }
}

class Tree
{
    public TreeNode Root { get; private set; }

    public Tree(int rootVertex)
    {
        Root = new TreeNode(rootVertex, 0); 
    }
    
    private TreeNode? FindNode(TreeNode node, int vertex)
    {
        if (node.Vertex == vertex)
        {
            return node;
        }

        foreach (var child in node.Children)
        {
            TreeNode? found = FindNode(child, vertex);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    private Edge? GetMinEdge(int vertex, Graph graph, List<int> visited)
    {
        
        Edge? minEdge = null;
        List<int> alreadyStored = visited.Where(x => x != vertex).ToList();
        foreach(Edge edge in graph.Edges.Where(e => (e.from == vertex && !alreadyStored.Contains(e.to)) || (e.to == vertex && !alreadyStored.Contains(e.from))))
        {
            if(minEdge == null){
                minEdge = edge;
                
            }
            if(edge.weight < minEdge.weight)
            {
               
                minEdge = edge;
            }
        }
        return minEdge;
    }

    public void Print()
    {
        PrintTree(Root, 0);
    }

    private void AddChild(int parentVertex, int childVertex, int weight)
    {
        TreeNode? parentNode = FindNode(Root, parentVertex);
        if (parentNode != null)
        {
            TreeNode childNode = new TreeNode(childVertex, weight);
            parentNode.AddChild(childNode);
        }
        else
        {
            throw new ArgumentException("Parent vertex not found in the tree.");
        }
    }
    private void PrintTree(TreeNode node, int level)
    {
        Console.WriteLine(level + new string(' ', level*2+1) + $"Vertex: {node.Vertex}, Weight: {node.Weight}");
        foreach (var child in node.Children)
        {
            PrintTree(child, level + 1);
        }
    }

    public void JarnikPrimAlgorithm(Graph graph)
    {
        List<int> visited = new List<int>();
        visited.Add(Root.Vertex);
    
    
        while (visited.Count < graph.Vertices.Count) 
        {
            int from = -1;
            int to = -1;
            
            Edge? minEdge = null;
            foreach (int vertex in visited)
            {
                Edge? minEdgeOfVertex = GetMinEdge(vertex, graph, visited);
                if(minEdgeOfVertex == null)
                {
                    continue;
                }
                if(minEdge == null)
                {
                    minEdge = minEdgeOfVertex;
                    from = vertex;
                    if (minEdge.from == from)
                    {
                        to = minEdge.to;
                    }
                    else
                        to = minEdge.from;
                }
                else if(minEdgeOfVertex.weight < minEdge.weight)
                {
                    minEdge = minEdgeOfVertex;
                    from = vertex;
                    if (minEdge.from == from)
                    {
                        to = minEdge.to;
                    }
                    else
                        to = minEdge.from;
                }
            }

            if (minEdge == null) return;
            AddChild(from, to, minEdge.weight);
            visited.Add(to);
            
        }
    }
}

class Program
{
    static void Main()
    {
        Graph graph = new Graph();

        graph.AddEdge(new Edge(0, 1, 5));
        graph.AddEdge(new Edge(0, 2, 8));
        graph.AddEdge(new Edge(1, 3, 9));
        graph.AddEdge(new Edge(1, 4, 7));
        graph.AddEdge(new Edge(2, 5, 6));
        graph.AddEdge(new Edge(3, 6, 8));
        graph.AddEdge(new Edge(4, 7, 5));
        graph.AddEdge(new Edge(5, 8, 11));
        graph.AddEdge(new Edge(6, 9, 10));
        graph.AddEdge(new Edge(7, 10, 8));
        graph.AddEdge(new Edge(8, 11, 7));
        graph.AddEdge(new Edge(9, 12, 4));
        graph.AddEdge(new Edge(10, 13, 3));
        graph.AddEdge(new Edge(11, 14, 9));
        graph.AddEdge(new Edge(12, 13, 6));
        graph.AddEdge(new Edge(13, 14, 2));
        graph.AddEdge(new Edge(0, 14, 14));
        graph.AddEdge(new Edge(1, 5, 1));
        graph.AddEdge(new Edge(2, 6, 12));
        graph.AddEdge(new Edge(3, 7, 13));
        graph.AddEdge(new Edge(4, 8, 2));
        graph.AddEdge(new Edge(5, 9, 14));
        graph.AddEdge(new Edge(6, 10, 15));
        graph.AddEdge(new Edge(7, 11, 16));
        graph.AddEdge(new Edge(8, 12, 17));
        graph.AddEdge(new Edge(9, 13, 18));
        graph.AddEdge(new Edge(10, 14, 19));

        Tree tree = new Tree(0); // Inicializace stromu s kořenem na vrcholu 0
        tree.JarnikPrimAlgorithm(graph); // Převod grafu na strom pomocí Jarníkova-Primova algoritmu

        // Výpis stromu
        Console.WriteLine("Strom:");
        tree.Print();
    }
    
    
}
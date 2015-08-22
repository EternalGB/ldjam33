using UnityEngine;
using System.Collections.Generic;

public class DFS 
{

    public Stack<NavPoint> stack;

    public DFS(NavPoint startPt)
    {
        this.stack = new Stack<NavPoint>();
        stack.Push(startPt);
    }

    public NavPoint GetNextNavPoint()
    {
        if(stack.Count > 0)
        {
            NavPoint next = stack.Pop();
            next.visited = true;
            foreach(NavPoint neighbour in next.neighbours)
            {
                if(!neighbour.visited)
                    stack.Push(neighbour);
            }
            return next;
        }

        return null;
    }

 
}

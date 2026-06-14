using System.Threading.Tasks;
using UnityEngine;
using Tasks = Cysharp.Threading.Tasks;

namespace Game.Creature
{
    public sealed class AstarPathFinding
    {



        class Node
        {
            Point gridPos;
            Point parent;
            int gCost;
            public int fCost(Point gridPos,Point goal) => gCost + HeuristicCost(gridPos,goal);

        }

        struct Point
        { 
            public readonly int X,Y;
            public Point(int x, int y)
            {
                X = x; 
                Y = y;
            }
        }

        static int HeuristicCost(Point gridPos, Point goal)
            => goal.X - gridPos.X + goal.Y - gridPos.Y;

        Point ToGrid(Vector2 worldPos)
            => new Point(Mathf.FloorToInt(worldPos.x / 32),Mathf.FloorToInt(worldPos.y / 32));

        Vector2 ToWorld(Point gridPos)
            => new Vector2(gridPos.X * 32, gridPos.Y * 32);
    }
}


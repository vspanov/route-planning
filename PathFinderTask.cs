using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestOrder = new int[checkpoints.Length];
            var orders = new int[checkpoints.Length];
            double minCost = 0;
            MakePermutations(checkpoints, orders, bestOrder, 1, ref minCost);
            return bestOrder;
        }
        private static void MakePermutations(Point[] checpoints, int[] orders, int[] bestOrder,
                                             int position, ref double minCost)
        {
            var currCost = PointExtensions.GetPathLength(checpoints, orders);
            if (position == orders.Length)
            {
                if (minCost == 0 || minCost > currCost)
                {
                    minCost = currCost;
                    orders.CopyTo(bestOrder, 0);
                }
                return;
            }
            for (int i = 1; i < orders.Length; i++)
            {
                var index = Array.IndexOf(orders, i, 1, position);
                if (index != -1)
                    continue;
                orders[position] = i;
                var currOrders = new int[position + 1];
                Array.Copy(orders, currOrders, position + 1);
                currCost = PointExtensions.GetPathLength(checpoints, currOrders);
                if (currCost > minCost && minCost != 0) continue;
                MakePermutations(checpoints, orders, bestOrder, position + 1, ref minCost);
            }
        }
    }
}
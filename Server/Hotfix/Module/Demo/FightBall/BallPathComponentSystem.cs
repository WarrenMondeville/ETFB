using System.Threading;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class BallPathComponentHelper
    {
        public static async ETTask MoveAsync(this BallPathComponent self, Vector2 target)
        {
            self.BroadcastPath(target);


            await self.Entity.GetComponent<MoveComponentV2>().MoveToAsync(target, self.CancellationTokenSource.Token);

        }

        public static async ETVoid MoveTo(this BallPathComponent self, Vector2 target)
        {
            if ((self.Target - target).magnitude < 0.1f)
            {
                return;
            }

            self.Target = target;

            Unit unit = self.GetParent<Unit>();


            //PathfindingComponent pathfindingComponent = Game.Scene.GetComponent<PathfindingComponent>();
            //self.ABPath = ComponentFactory.Create<ABPathWrap, Vector3, Vector3>(unit.GetComponent<MoveComponent>().Position,target);// new Vector3(target.x, target.y, target.z));
            //pathfindingComponent.Search(self.ABPath);
            //Log.Debug($"find result: {self.ABPath.Result.ListToString()}");

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();
            await self.MoveAsync(target);
            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        // 从index找接下来3个点，广播
        public static void BroadcastPath(this BallPathComponent self, Vector2 target)
        {
            Unit unit = self.GetParent<Unit>();
            var moveCom = unit.GetComponent<MoveComponentV2>();


            Vector2 unitPos = moveCom.Position;
            M2C_BallPath m2CBallPath = new M2C_BallPath();

            m2CBallPath.Id = unit.Id;
            m2CBallPath.X = unitPos.x;
            m2CBallPath.Y = unitPos.y;
            m2CBallPath.Xt = target.x;
            m2CBallPath.Yt = target.y;

            //for (int i = 0; i < offset; ++i)
            //{
            //    if (index + i >= self.ABPath.Result.Count)
            //    {
            //        break;
            //    }
            //    Vector3 v = self.ABPath.Result[index + i];
            //    m2CPathfindingResult.Xs.Add(v.x);
            //    m2CPathfindingResult.Ys.Add(v.y);
            //    m2CPathfindingResult.Zs.Add(v.z);
            //}
            MessageHelper.Broadcast(m2CBallPath);
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class BallPathComponent : Component
    {
        //public List<Vector2> Path = new List<Vector2>();

        public Vector2 ServerPos, Target;

        public CancellationTokenSource CancellationTokenSource;

        public async ETTask StartMove(CancellationToken cancellationToken)
        {

            Vector2 v = Target;

            float speed = 5;
            // 矫正移动速度
            Vector2 clientPos = this.GetParent<Unit>().Position;
            float serverf = (ServerPos - v).magnitude;
            if (serverf > 0.1f)
            {
                float clientf = (clientPos - v).magnitude;
                speed = clientf / serverf * speed;
            }


            //this.Entity.GetComponent<TurnComponent>().Turn(v);
            await this.Entity.GetComponent<MoveComponentV2>().MoveToAsync(v, speed, cancellationToken);

        }

        public async ETVoid StartMove(M2C_BallPath message)
        {
            // 取消之前的移动协程
            this.CancellationTokenSource?.Cancel();
            this.CancellationTokenSource = new CancellationTokenSource();

            //this.Path.Clear();
            //for (int i = 0; i < message.Xs.Count; ++i)
            //{
            //	this.Path.Add(new Vector2(message.Xs[i], message.Ys[i], message.Zs[i]));
            ServerPos = new Vector2(message.X, message.Y);
            Target = new Vector2(message.Xt, message.Yt);

            await StartMove(this.CancellationTokenSource.Token);
            this.CancellationTokenSource.Dispose();
            this.CancellationTokenSource = null;
        }
    }
}
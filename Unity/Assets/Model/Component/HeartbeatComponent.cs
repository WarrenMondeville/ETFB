using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ETModel
{
    [ObjectSystem]
    public class HeartbeatComponentAwakeSystem : AwakeSystem<HeartbeatComponent>
    {

        public override void Awake(HeartbeatComponent self)
        {
            self.Awake();
        }
    }

    public class HeartbeatComponent : Component
    {
        public static C2G_Heartbeat heartBeat = new C2G_Heartbeat();
        public async void Awake()
        {
            Session session = (this.Entity as Session);
            DetectionNetworkType(session);//检测网络连接状态
            while (!this.IsDisposed)
            {
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(10000);
                session.Send(heartBeat);//每间隔10秒发一条 心跳消息
            }
        }

        public async void DetectionNetworkType(Session session)
        {
            while (!session.IsDisposed)
            {
                await ETModel.Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);
                //如果当前是无网络状态 发送连接失败的消息
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    session.Error = (int)SocketError.NetworkDown;
                    session.Dispose();
                }
            }
        }
    }
}
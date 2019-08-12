using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Frame_PosHandler : AMActorLocationHandler<Unit, Frame_Pos>
    {
        protected override async ETTask Run(Unit unit, Frame_Pos message)
        {
            Vector2 target = new Vector2(message.X, message.Y);
            unit.GetComponent<BallPathComponent>().MoveTo(target).Coroutine();
            await ETTask.CompletedTask;
        }
    }
}
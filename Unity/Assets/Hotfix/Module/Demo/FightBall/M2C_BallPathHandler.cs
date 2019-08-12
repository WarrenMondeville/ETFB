using ETModel;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
namespace ETHotfix
{
    [MessageHandler]
    public class M2C_BallPathHandler : AMHandler<M2C_BallPath>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_BallPath message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            var pos = new Vector2(message.X, message.Y);
            unit.GetComponent<BallPathComponent>().StartMove(message).Coroutine();
            //unit.Position = pos;
            await ETTask.CompletedTask;
        }
    }
}

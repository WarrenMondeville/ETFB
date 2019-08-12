using ETModel;
using Vector3 = UnityEngine.Vector3;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_CreateBallsHandler : AMHandler<M2C_CreateBalls>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_CreateBalls message)
        {
            UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();

            foreach (BallInfo unitInfo in message.Balls)
            {
                if (unitComponent.Get(unitInfo.UnitId) != null)
                {
                    continue;
                }
                Unit unit = BallFactory.Create(unitInfo.UnitId);
                
                unit.Position = new Vector3(unitInfo.X, unitInfo.Y);
               
            }

            await ETTask.CompletedTask;
        }
    }
}

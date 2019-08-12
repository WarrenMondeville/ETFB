using System;
using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class G2M_CreateUnitHandler : AMRpcHandler<G2M_CreateUnit, M2G_CreateUnit>
    {
        protected override async ETTask Run(Session session, G2M_CreateUnit request, M2G_CreateUnit response, Action reply)
        {
            Unit unit = ComponentFactory.CreateWithId<Unit>(IdGenerater.GenerateId());


            await unit.AddComponent<MailBoxComponent>().AddLocation();
            unit.AddComponent<UnitGateComponent, long>(request.GateSessionId);
            response.UnitId = unit.Id;

            var gameType = (GameType)request.GameType;
            var roomCom = Game.Scene.GetComponent<GameRoomComponent>();
            switch (gameType)
            {
                case GameType.Skeleton:
                    roomCom.Add(gameType, 0, unit);
                    var moveCom = unit.AddComponent<MoveComponent>();
                    unit.AddComponent<UnitPathComponent>();
                    moveCom.Position = new Vector3(-10, 0, -10);
                    // 广播创建的unit
                    M2C_CreateUnits createUnits = new M2C_CreateUnits();
                    Unit[] units = roomCom.Get(gameType, 0);
                    foreach (Unit u in units)
                    {
                        UnitInfo unitInfo = new UnitInfo();
                        unitInfo.X = moveCom.Position.x;
                        unitInfo.Y = moveCom.Position.y;
                        unitInfo.Z = moveCom.Position.z;
                        unitInfo.UnitId = u.Id;
                        createUnits.Units.Add(unitInfo);
                    }
                    MessageHelper.Broadcast(createUnits);
                    break;
                case GameType.FightBall:
                    roomCom.Add(gameType, 0, unit);
                    unit.AddComponent<MoveComponentV2>().Position=Vector2.zero;
                    unit.AddComponent<BallPathComponent>();
                    M2C_CreateBalls createBalls = new M2C_CreateBalls();
                    Unit[] balls = roomCom.Get(gameType, 0);
                    foreach (var ball in balls)
                    {
                        BallInfo ballinfo = new BallInfo();
                        ballinfo.X = 0f;
                        ballinfo.Y = 0f;
                        ballinfo.UnitId = ball.Id;
                        createBalls.Balls.Add(ballinfo);
                    }
                    MessageHelper.Broadcast(createBalls);
                    break;
                default:
                    break;
            }


            reply();
        }
    }
}
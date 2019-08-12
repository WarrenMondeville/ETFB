using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class BallSkillHandler : AMActorLocationHandler<Unit, C2M_BallSkill>
    {
        protected override async ETTask Run(Unit unit, C2M_BallSkill message)
        {
            var m = message.Skill;
            await ETTask.CompletedTask;
        }
    }
}

using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIFBCtrComponentSystem : AwakeSystem<UIFBCtrComponent>
    {
        public override void Awake(UIFBCtrComponent self)
        {
            self.Awake();
        }
    }

    public class UIFBCtrComponent : Component
    {


        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            rc.Get<GameObject>("split").GetComponent<Button>().onClick
                .Add(() => {SessionComponent.Instance.Session.Send(new C2M_BallSkill() { Skill = 0, }); });
            rc.Get<GameObject>("feed").GetComponent<Button>().onClick
                .Add(() => {SessionComponent.Instance.Session.Send(new C2M_BallSkill() { Skill = 1, }); });

        }



    }
}

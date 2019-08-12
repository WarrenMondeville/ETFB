using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UiLobbyComponentSystem : AwakeSystem<UILobbyComponent>
    {
        public override void Awake(UILobbyComponent self)
        {
            self.Awake();
        }
    }

    public class UILobbyComponent : Component
    {
        private GameObject enterMap;
        private Text text;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            enterMap = rc.Get<GameObject>("EnterMap");
            enterMap.GetComponent<Button>().onClick.Add(this.EnterMap);
            enterMap = rc.Get<GameObject>("EnterFB");
            enterMap.GetComponent<Button>().onClick.Add(this.EnterFB);

            this.text = rc.Get<GameObject>("Text").GetComponent<Text>();


            text.text = Define.IsILRuntime ? "ILruntime" : "Mono";

        }

        private void EnterFB()
        {
            EnterFBHelper.EnterFBAsync().Coroutine();
        }

        private void EnterMap()
        {
            MapHelper.EnterMapAsync().Coroutine();
        }


    }
}

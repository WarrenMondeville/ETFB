using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class  EnterFBHelper
    {
        public static async ETVoid EnterFBAsync()
        {
            try
            {
                var sceneType = SceneType.FBMap;


                // 加载Unit资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                await resourcesComponent.LoadBundleAsync(ObjType.FightBallUnits.StringToAB());
                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync(sceneType.StringToAB());
                //加载UI
                resourcesComponent.LoadBundle(UIType.UIFBCtr.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UIFBCtr.StringToAB(), UIType.UIFBCtr);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);
                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UIFBCtr, gameObject, false);
                ui.AddComponent<UIFBCtrComponent>();
                Game.Scene.GetComponent<UIComponent>().Add(ui);
                
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(sceneType);
                }


                G2C_EnterMap g2CEnterMap = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap() { GameType = (int)GameType.FightBall, }) as G2C_EnterMap;
                PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;
                Game.Scene.AddComponent<CameraComponent>();
                Game.Scene.AddComponent<InputComponent>();

                Game.EventSystem.Run(EventIdType.EnterMapFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
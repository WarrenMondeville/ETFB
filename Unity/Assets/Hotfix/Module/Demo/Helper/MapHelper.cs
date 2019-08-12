using System;
using ETModel;

namespace ETHotfix
{
    public static class MapHelper
    {
        public static async ETVoid EnterMapAsync()
        {
            try
            {
                var sceneType = SceneType.Map;
                // 加载Unit资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                await resourcesComponent.LoadBundleAsync($"unit.unity3d");

                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync(sceneType.StringToAB());
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(sceneType);
                }

                G2C_EnterMap g2CEnterMap = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap() { GameType = (int)GameType.Skeleton, }) as G2C_EnterMap;
                PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;

                Game.Scene.AddComponent<OperaComponent>();
                //Game.Scene.AddComponent<InputComponent>();
                Game.EventSystem.Run(EventIdType.EnterMapFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
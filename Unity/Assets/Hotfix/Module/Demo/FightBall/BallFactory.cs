using ETModel;
using UnityEngine;

namespace ETModel
{
    public static class BallFactory
    {
        public static Unit Create(long id)
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("FightBallUnits.unity3d", "FightBallUnits");
            GameObject prefab = bundleGameObject.Get<GameObject>("Ball");

            GameObject go = UnityEngine.Object.Instantiate(prefab);

            UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
            go.transform.localScale = Vector2.one*10;
            //go.AddComponent<SpriteRenderer>().sprite =new Sprite();
            Unit unit = ETModel.ComponentFactory.CreateWithId<Unit, GameObject>(id, go);
            unit.AddComponent<BallPathComponent>();
            unit.AddComponent<MoveComponentV2>();
            
            unitComponent.Add(unit);
            return unit;
        }
    }
}
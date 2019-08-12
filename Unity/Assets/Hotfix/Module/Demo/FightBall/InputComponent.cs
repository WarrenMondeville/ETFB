using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class InputComponentAwakeSystem : AwakeSystem<InputComponent>
    {
        public override void Awake(InputComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class InputComponentUpdateSystem : UpdateSystem<InputComponent>
    {
        public override void Update(InputComponent self)
        {
            self.Update();
        }
    }

    public class InputComponent : Component
    {
        public Camera mainCamera;

        public void Awake()
        {
            mainCamera = Game.Scene.GetComponent<CameraComponent>().mainCamera;

        }

        private readonly Frame_Pos framePos = new Frame_Pos();

        public void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                Log.Info(pos.ToString());
                framePos.X = pos.x;
                framePos.Y = pos.y;
                ETModel.SessionComponent.Instance.Session.Send(framePos);

            }
        }

    }
}

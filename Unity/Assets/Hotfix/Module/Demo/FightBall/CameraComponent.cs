using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent>
    {
        public override void Awake(CameraComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class CameraComponentUpdateSystem : UpdateSystem<CameraComponent>
    {
        public override void Update(CameraComponent self)
        {
        }
    }

    public class CameraComponent : Component
    {
        public Camera mainCamera;

        public void Awake()
        {
            if (Camera.main == null)
            {
                GameObject go = new GameObject("Camera");
                mainCamera = go.AddComponent<Camera>();
            }
            else
            {
                mainCamera = Camera.main;
            }
            mainCamera.clearFlags = CameraClearFlags.Color;
            mainCamera.backgroundColor = Color.black;
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 10f;
            mainCamera.nearClipPlane = 0f;
            mainCamera.depth = -1;
        }



    }
}

using System.Collections.Generic;
using System.Threading;
using PF;
using UnityEngine;

namespace ETModel
{
    public class BallPathComponent: Component
    {
        public Vector2 Target;

        
        public List<Vector3> Path;

        public CancellationTokenSource CancellationTokenSource;



    }
}
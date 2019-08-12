using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ETModel
{

    [ObjectSystem]
    public class GameRoomComponentAwakeSystem : AwakeSystem<GameRoomComponent>
    {
        public override void Awake(GameRoomComponent self)
        {
            self.Awake();
        }
    }
    public class GameRoomComponent : Component
    {
        public Dictionary<GameType, Dictionary<int, List<long>>> GameRooms = new Dictionary<GameType, Dictionary<int, List<long>>>();

        private UnitComponent unitComponent;
        internal void Awake()
        {
            unitComponent = Game.Scene.GetComponent<UnitComponent>();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            this.GameRooms.Clear();
        }

        public void Add(GameType gameType, int roomId, Unit unit)
        {
            Dictionary<int, List<long>> game;
            if (!this.GameRooms.TryGetValue(gameType, out game))
            {
                GameRooms[gameType] = new Dictionary<int, List<long>>();
            }
            List<long> room;
            if (!this.GameRooms[gameType].TryGetValue(roomId, out room))
            {
                GameRooms[gameType][roomId] = new List<long>();
            }
            GameRooms[gameType][roomId].Add(unit.Id);
            unitComponent.Add(unit);
        }
        public Unit[] Get(GameType gameType, int roomId)
        {
            Dictionary<int, List<long>> game;
            if (!this.GameRooms.TryGetValue(gameType, out game))
            {
                return null;
            }
            List<long> room;
            if (!game.TryGetValue(roomId, out room))
            {
                return null;
            }
            return unitComponent.Get(room);
        }

    }
}
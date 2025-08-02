using Almoravids.Characters;
using Almoravids.ContentManagement;
using Almoravids.Items;

namespace Almoravids.Level
{
    public class Level_3 : Level
    {
        public Level_3(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
            : base(contentLoader, graphicsDevice)
        {
        }

        public override void Load()
        {
            _mapName = "map/tangier"; // set map name
            _heroSpawn = new Vector2(1180, 390); // hero spawn
            _map = new Map.Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);

            _tetherSpawn = new Vector2(661, 47);
            _enemySpawns = new List<EnemySpawn>
            {
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(460, 620),
                    PathPoints = new List<Vector2> {
                        new Vector2(460, 620),
                        new Vector2(910, 620),
                        new Vector2(910, 260),
                        new Vector2(460, 260),
                    },
                    WaitTimes = new List<float> { 0f, 0f, 0f, 0f },
                    Loop = true
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(910, 620),
                    PathPoints = new List<Vector2> {
                        new Vector2(910, 620),
                        new Vector2(910, 260),
                        new Vector2(460, 260),
                        new Vector2(460, 620)
                    },
                    WaitTimes = new List<float> { 0f, 0f, 0f, 0f },
                    Loop = true
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(910, 260),
                    PathPoints = new List<Vector2> {
                        new Vector2(910, 260),
                        new Vector2(460, 260),
                        new Vector2(460, 620),
                        new Vector2(910, 620),
                    },
                    WaitTimes = new List<float> { 0f, 0f, 0f, 0f },
                    Loop = true
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(460, 260),
                    PathPoints = new List<Vector2> {
                        new Vector2(460, 260),
                        new Vector2(460, 620),
                        new Vector2(910, 620),
                        new Vector2(910, 260),
                    },
                    WaitTimes = new List<float> { 0f, 0f, 0f, 0f },
                    Loop = true
                },
                new EnemySpawn { Type = "archer", Position = new Vector2(937, 700) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(210, 240) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(1300, 530) },

                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(693, 455),
                    PathPoints = new List<Vector2> {
                        new Vector2(693, 455),
                        new Vector2(693, 560),
                    },
                    WaitTimes = new List<float> { 5f, 3f},
                    Loop = false
                },
            };
            _itemSpawns = new List<ItemSpawn>
            {
                new ItemSpawn { Type = "tasbih", Position = new Vector2(676, 130) },
                new ItemSpawn { Type = "litham", Position = new Vector2(1200, 500) },
                new ItemSpawn { Type = "adarga", Position = new Vector2(195, 770) },
                new ItemSpawn { Type = "khuffayn", Position = new Vector2(330, 310) },
                new ItemSpawn { Type = "koumiya", Position = new Vector2(700, 620) },

                new ItemSpawn { Type = "banner", Position = new Vector2(1166+15, 165) },
                new ItemSpawn { Type = "banner", Position = new Vector2(90, 165) },
                new ItemSpawn { Type = "banner", Position = new Vector2(120+15, 800) },

                new ItemSpawn { Type = "bayaah", Position = new Vector2(705, 508) }
            };
        }
    }
}
using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.ContentManagement;

namespace Almoravids.Level
{
    public class Level_2 : Level
    {
        public Level_2(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
            : base(contentLoader, graphicsDevice)
        {
        }

        public override void Load()
        {
            _mapName = "map/marrakech"; // set map name
            _heroSpawn = new Vector2(540, 345); // hero spawn
            _tetherSpawn = new Vector2(500, 345);
            _enemySpawns = new List<EnemySpawn>
            {
                new EnemySpawn { Type = "archer", Position = new Vector2(85, 470) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(500, 500) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(828, 153) },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(1050, 10),
                    PathPoints = new List<Vector2> {
                        new Vector2(1050, 10),
                        new Vector2(1050, 243),
                    },
                    WaitTimes = new List<float> { 0.2f, 0.2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(1381, 10),
                    PathPoints = new List<Vector2> {
                        new Vector2(1381, 243),
                        new Vector2(797, 243),
                    },
                    WaitTimes = new List<float> { 0.2f, 0.2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(640, 50),
                    PathPoints = new List<Vector2> {
                        new Vector2(640, 50),
                        new Vector2(1050, 50),
                    },
                    WaitTimes = new List<float> { 0.2f, 0.2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(797, 220),
                    PathPoints = new List<Vector2> {
                        new Vector2(797, 220),
                        new Vector2(1381, 220),
                    },
                    WaitTimes = new List<float> { 0.2f, 0.2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(808, 194),
                    PathPoints = new List<Vector2> {
                        new Vector2(808, 194),
                        new Vector2(808, 561),
                    },
                    WaitTimes = new List<float> { 0.2f, 0.2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(525, 10),
                    PathPoints = new List<Vector2> {
                        new Vector2(525, 10),
                        new Vector2(525, 120),
                    },
                    WaitTimes = new List<float> { 0.2f, 0.2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(710, 784),
                    PathPoints = new List<Vector2> {
                        new Vector2(710, 784),
                        new Vector2(350, 784),
                    },
                    WaitTimes = new List<float> { 0.1f, 0.1f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "swordsman",
                    Position = new Vector2(175, 345),
                    
                },
            };
            _itemSpawns = new List<ItemSpawn>
            {
                new ItemSpawn { Type = "tasbih", Position = new Vector2(800, 360) },
                new ItemSpawn { Type = "litham", Position = new Vector2(827, 110) },
                new ItemSpawn { Type = "adarga", Position = new Vector2(280, 108) },
                new ItemSpawn { Type = "khuffayn", Position = new Vector2(30, 223) },
                new ItemSpawn { Type = "koumiya", Position = new Vector2(224, 854) },

                new ItemSpawn { Type = "banner", Position = new Vector2(1243+15, 76) },
                new ItemSpawn { Type = "banner", Position = new Vector2(83+15, 62) },
                new ItemSpawn { Type = "banner", Position = new Vector2(493+15, 844) },

                new ItemSpawn { Type = "bayaah", Position = new Vector2(1069+15, 686+32) }
            };
            _map = new Map.Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}
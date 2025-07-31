using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.ContentManagement;

namespace Almoravids.Level
{
    public class Level_1 : Level
    {
        public Level_1(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
            : base(contentLoader, graphicsDevice)
        {
        }

        public override void Load()
        {
            _mapName = "map/sahara"; // set map name
            _heroSpawn = new Vector2(173, 95);
            _tetherSpawn = new Vector2(222, 95);
            _enemySpawns = new List<EnemySpawn>
            {
                new EnemySpawn { Type = "archer", Position = new Vector2(568, 451) },
                new EnemySpawn { Type = "archer", Position = new Vector2(950, 556) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(1212, 457) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(1272, 860) },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(363, 854),
                    PathPoints = new List<Vector2> {
                        new Vector2(325, 854),
                        new Vector2(760, 854),
                    },
                    WaitTimes = new List<float> { 1.5f, 2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(172, 460),
                    PathPoints = new List<Vector2> {
                        new Vector2(172, 460),
                        new Vector2(10, 460),
                    },
                    WaitTimes = new List<float> { 1.5f, 0.5f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(153, 272),
                    PathPoints = new List<Vector2> {
                        new Vector2(173, 272),
                        new Vector2(10, 272),
                    },
                    WaitTimes = new List<float> { 2f, 2f},
                    Loop = false
                },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(286, 632),
                    PathPoints = new List<Vector2> {
                        new Vector2(286, 632),
                        new Vector2(10, 632),
                    },
                    WaitTimes = new List<float> { 0.3f, 0.3f},
                    Loop = false
                },
            };
            _itemSpawns = new List<ItemSpawn>
            {
                new ItemSpawn { Type = "tasbih", Position = new Vector2(611, 130) },
                new ItemSpawn { Type = "litham", Position = new Vector2(1128, 184) },
                new ItemSpawn { Type = "adarga", Position = new Vector2(239, 46) },

                new ItemSpawn { Type = "khuffayn", Position = new Vector2(30, 223) },
                new ItemSpawn { Type = "koumiya", Position = new Vector2(224, 854) },
                //Banners
                new ItemSpawn { Type = "banner", Position = new Vector2(115+15, 785) },
                new ItemSpawn { Type = "banner", Position = new Vector2(1192+15, 721) },
                new ItemSpawn { Type = "banner", Position = new Vector2(1176+15, 117) },

                new ItemSpawn { Type = "bayaah", Position = new Vector2(688+15, 382+32) }
            };
            _map = new Map.Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}
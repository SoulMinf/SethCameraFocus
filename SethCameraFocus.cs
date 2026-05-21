using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace SethCameraFocus
{
    public class SethCameraFocus : Mod
    {

    }

    public class SethCameraSystem : ModSystem
    {
        private const int STATE_HEAT_SNAP = 7;
        private const int STATE_DRAGON = 9;
        private const int STATE_BALLS_IN_YOUR_JAW = 11;
        private const float MaxOffsetPixels = 500f;
        private float _parallax;
        private Vector2 _lastBossCenter;
        private PropertyInfo _currentStateProp;
        private bool _propSearched;

        public override void ModifyScreenPosition()
        {
            if (Main.gameMenu) return;

            Player player = Main.LocalPlayer;
            if (!player.active || player.dead) return;

            Config cfg = ModContent.GetInstance<Config>();

            bool shouldFocus = false;

            NPC seth = FindSeth();

            if (seth != null)
            {
                int state = ReadState(seth);

                bool isGroupA = cfg.Ultimate && (state == STATE_HEAT_SNAP || state == STATE_DRAGON);
                bool isGroupB = cfg.RotatingBlades && state == STATE_BALLS_IN_YOUR_JAW;

                if (isGroupA || isGroupB)
                {
                    float detachPixels = cfg.DetachDistanceTiles * 16f;
                    bool tooFar = player.Distance(seth.Center) > detachPixels;

                    if (!tooFar)
                    {
                        shouldFocus = true;
                        _lastBossCenter = seth.Center;
                    }
                }
            }
            else
            {
                _currentStateProp = null;
                _propSearched = false;
            }

            float targetParallax = shouldFocus ? cfg.MaxParallax : 0f;
            float speed = shouldFocus ? cfg.SpeedIn : cfg.SpeedOut;
            _parallax = MathHelper.Lerp(_parallax, targetParallax, speed);

            if (_parallax > 0.001f)
            {
                Vector2 towardBoss = _lastBossCenter - player.Center;

                float dist = towardBoss.Length();
                float maxRawDist = MaxOffsetPixels / Math.Max(cfg.MaxParallax, 0.001f);
                if (dist > maxRawDist)
                    towardBoss *= maxRawDist / dist;

                Main.screenPosition += towardBoss * _parallax;
            }
        }

        private static NPC FindSeth()
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (!npc.active || npc.ModNPC == null)
                    continue;

                if (npc.ModNPC.GetType().FullName == "Split.Content.Bosses.Seth.Seth")
                    return npc;
            }
            return null;
        }

        private int ReadState(NPC npc)
        {
            if (!_propSearched)
            {
                _propSearched = true;
                _currentStateProp = FindProperty(npc.ModNPC.GetType(), "CurrentState");
            }

            if (_currentStateProp != null)
            {
                try { return (int)_currentStateProp.GetValue(npc.ModNPC); }
                catch { _currentStateProp = null; }
            }

            return (int)npc.ai[0];
        }

        private static PropertyInfo FindProperty(Type type, string name)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            while (type != null && type != typeof(object))
            {
                PropertyInfo prop = type.GetProperty(name, flags);
                if (prop != null) return prop;
                type = type.BaseType;
            }
            return null;
        }
    }
}
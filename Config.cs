using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SethCameraFocus
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.SethCameraFocus.EnableGroups")]

        [LabelKey("$Mods.SethCameraFocus.EnableUltimate")]
        [DefaultValue(true)]
        public bool Ultimate = true;

        [LabelKey("$Mods.SethCameraFocus.EnableRotatingBlades")]
        [DefaultValue(false)]
        public bool RotatingBlades = false;

        [Header("$Mods.SethCameraFocus.Tuning")]

        [LabelKey("$Mods.SethCameraFocus.MaxRange")]
        [Range(10f, 200f)]
        [DefaultValue(100f)]
        public float DetachDistanceTiles = 100f;

        [LabelKey("$Mods.SethCameraFocus.ParalaxStrength")]
        [Range(0.05f, 0.8f)]
        [DefaultValue(0.35f)]
        public float MaxParallax = 0.35f;

        [LabelKey("$Mods.SethCameraFocus.ParalaxIn")]
        [Range(0.01f, 0.2f)]
        [DefaultValue(0.04f)]
        public float SpeedIn = 0.04f;

        [LabelKey("$Mods.SethCameraFocus.ParalaxOut")]
        [Range(0.01f, 0.2f)]
        [DefaultValue(0.025f)]
        public float SpeedOut = 0.025f;
    }
}
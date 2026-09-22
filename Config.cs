using System.ComponentModel;
using System;
using Terraria.ModLoader.Config;

namespace SethCameraFocus
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        private float _DetachDistanceTiles = 100f;

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
        public float DetachDistanceTiles
        {
            get => (float)Math.Round(_DetachDistanceTiles, 0);
            set => _DetachDistanceTiles = value;
        }

        private float _MaxParallax = 0.35f;
        [LabelKey("$Mods.SethCameraFocus.ParalaxStrength")]
        [Range(0.05f, 0.8f)]
        [DefaultValue(0.35f)]
        public float MaxParallax
        {
            get => (float)Math.Round(_MaxParallax, 2);
            set => _MaxParallax = value;
        }

        private float _SpeedIn = 0.04f;
        [LabelKey("$Mods.SethCameraFocus.ParalaxIn")]
        [Range(0.01f, 0.2f)]
        [DefaultValue(0.04f)]
        public float SpeedIn
        {
            get => (float)Math.Round(_SpeedIn, 2);
            set => _SpeedIn = value;
        }

        private float _SpeedOut = 0.02f;
        [LabelKey("$Mods.SethCameraFocus.ParalaxOut")]
        [Range(0.01f, 0.2f)]
        [DefaultValue(0.02f)]
        public float SpeedOut {
            get => (float)Math.Round(_SpeedOut, 2);
            set => _SpeedOut = value;
        }
    }
}
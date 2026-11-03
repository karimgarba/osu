// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Configuration;

namespace osu.Game.Beatmaps.Drawables
{
    /// <summary>
    /// A sprite displaying beatmap-provided artwork (backgrounds or online covers),
    /// which is swapped for <see cref="FallbackTexture"/> when <see cref="OsuSetting.HideBeatmapBackgrounds"/> is enabled.
    /// </summary>
    public partial class HideableBeatmapSprite : Sprite
    {
        private Texture? beatmapTexture;
        private Texture? fallbackTexture;

        private IBindable<bool> hidden = new Bindable<bool>();

        public Texture? BeatmapTexture
        {
            get => beatmapTexture;
            set
            {
                beatmapTexture = value;
                updateTexture();
            }
        }

        public Texture? FallbackTexture
        {
            get => fallbackTexture;
            set
            {
                fallbackTexture = value;
                updateTexture();
            }
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            hidden = config.GetBindable<bool>(OsuSetting.HideBeatmapBackgrounds);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            hidden.BindValueChanged(_ => updateTexture(), true);
        }

        private void updateTexture() => Texture = hidden.Value || beatmapTexture == null ? fallbackTexture : beatmapTexture;
    }
}

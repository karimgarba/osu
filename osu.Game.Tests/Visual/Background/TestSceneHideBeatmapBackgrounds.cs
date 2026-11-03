// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Game.Beatmaps.Drawables;
using osu.Game.Configuration;

namespace osu.Game.Tests.Visual.Background
{
    public partial class TestSceneHideBeatmapBackgrounds : OsuTestScene
    {
        [Resolved]
        private OsuConfigManager config { get; set; } = null!;

        [Resolved]
        private LargeTextureStore textures { get; set; } = null!;

        private HideableBeatmapSprite sprite = null!;
        private Texture beatmapTexture = null!;
        private Texture fallbackTexture = null!;

        [Test]
        public void TestToggle()
        {
            AddStep("show backgrounds", () => config.SetValue(OsuSetting.HideBeatmapBackgrounds, false));
            AddStep("create sprite", () =>
            {
                beatmapTexture = textures.Get(@"Backgrounds/bg1");
                fallbackTexture = textures.Get(@"Backgrounds/bg2");

                Child = sprite = new HideableBeatmapSprite
                {
                    BeatmapTexture = beatmapTexture,
                    FallbackTexture = fallbackTexture,
                };
            });
            AddUntilStep("beatmap texture shown", () => sprite.Texture == beatmapTexture);

            AddStep("hide backgrounds", () => config.SetValue(OsuSetting.HideBeatmapBackgrounds, true));
            AddAssert("fallback texture shown", () => sprite.Texture == fallbackTexture);

            AddStep("show backgrounds", () => config.SetValue(OsuSetting.HideBeatmapBackgrounds, false));
            AddAssert("beatmap texture shown", () => sprite.Texture == beatmapTexture);
        }

        [Test]
        public void TestHiddenOnLoad()
        {
            AddStep("hide backgrounds", () => config.SetValue(OsuSetting.HideBeatmapBackgrounds, true));
            AddStep("create sprite without fallback", () => Child = sprite = new HideableBeatmapSprite
            {
                BeatmapTexture = textures.Get(@"Backgrounds/bg1"),
            });
            AddUntilStep("loaded", () => sprite.IsLoaded);
            AddAssert("no texture shown", () => sprite.Texture == null);
            AddStep("show backgrounds", () => config.SetValue(OsuSetting.HideBeatmapBackgrounds, false));
            AddAssert("texture shown", () => sprite.Texture != null);
        }
    }
}

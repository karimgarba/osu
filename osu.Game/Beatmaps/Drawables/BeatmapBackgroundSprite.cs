// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;

namespace osu.Game.Beatmaps.Drawables
{
    public partial class BeatmapBackgroundSprite : HideableBeatmapSprite
    {
        private readonly IWorkingBeatmap working;

        public BeatmapBackgroundSprite(IWorkingBeatmap working)
        {
            ArgumentNullException.ThrowIfNull(working);

            this.working = working;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            BeatmapTexture = working.GetBackground();
        }
    }
}

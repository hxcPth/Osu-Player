﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Milki.Extensions.MixPlayer;
using Milki.Extensions.MixPlayer.NAudioExtensions;
using Milki.Extensions.MixPlayer.Subchannels;
using Milky.OsuPlayer.Presentation.Annotations;

namespace Milky.OsuPlayer.Media.Audio
{
    public class DirectChannel : MultiElementsChannel
    {
        private readonly string _audioPath;
        private readonly int _delay;
        private readonly SampleControl _control;

        public DirectChannel(string audioPath, int delay, [NotNull] AudioPlaybackEngine engine, SampleControl control = null) : base(engine)
        {
            _audioPath = audioPath;
            _delay = delay;
            _control = control ?? new SampleControl();
        }

        public override async Task<IEnumerable<SoundElement>> GetSoundElements()
        {
            return new[] { SoundElement.Create(_delay, _control.Volume, _control.Balance, _audioPath) };
        }
    }
}
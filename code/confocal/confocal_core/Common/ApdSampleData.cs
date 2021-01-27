using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    public class ApdSampleData
    {
        public int[] NSamples { get; set; }
        public int ChannelIndex { get; set; }
        public long AcquisitionIndex { get; }

        public ApdSampleData(int[] samples, int channelIndex, int acquisitionIndex)
        {
            NSamples = samples;
            ChannelIndex = channelIndex;
            AcquisitionIndex = acquisitionIndex;
        }

    }
}

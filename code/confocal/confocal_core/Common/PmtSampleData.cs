using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    public class PmtSampleData
    {
        public ushort[][] NSamples { get; set; }
        public long AcquisitionCount { get; }

        public PmtSampleData(ushort[][] samples, long acquisitionCount)
        {
            NSamples = samples;
            AcquisitionCount = acquisitionCount;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    public class PmtSampleData
    {
        public short[][] NSamples { get; set; }
        public long[] AcquisitionCount { get; }

        public PmtSampleData(short[][] samples, long[] acquisitionCount)
        {
            NSamples = samples;
            AcquisitionCount = acquisitionCount;
        }
       
    }

}

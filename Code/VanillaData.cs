using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace IndustriesExtendedDLC
{
    public class VanillaData
    {
        public float ExtractorProductionEfficiency { get; }
        public float ExtractorCompanyExportMultiplier { get; }

        public VanillaData()
        {
            ExtractorProductionEfficiency = 8f;
            ExtractorCompanyExportMultiplier = 0.85f;
        }
    }
}

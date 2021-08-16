using System;

namespace KuanMiEditor.Rendering.MKRP
{
    class AdvancedOptionsUIBlock : MaterialUIBlock
    {
        [Flags]
        public enum Features
        {
            None                    = 0,
            Instancing              = 1 << 0,
            SpecularOcclusion       = 1 << 1,
            AddPrecomputedVelocity  = 1 << 2,
            DoubleSidedGI           = 1 << 3,
            EmissionGI              = 1 << 4,
            MotionVector            = 1 << 5,
            StandardLit             = Instancing | SpecularOcclusion | AddPrecomputedVelocity,
            All                     = ~0
        }

        Expandable  m_ExpandableBit;
        Features    m_Features;

        public AdvancedOptionsUIBlock(Expandable expandableBit, Features features = Features.All)
        {
            m_ExpandableBit = expandableBit;
            m_Features = features;
        }
        public override void LoadMaterialProperties()
        {
            //throw new NotImplementedException();
        }

        public override void OnGUI()
        {
            //throw new NotImplementedException();
        }
    }
}
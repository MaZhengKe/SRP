using System;

namespace KuanMiEditor.Rendering.MKRP
{
    class EmissionUIBlock : MaterialUIBlock
    {
        [Flags]
        public enum Features
        {
            None                = 0,
            EnableEmissionForGI = 1 << 0,
            MultiplyWithBase    = 1 << 1,
            All                 = ~0
        }
        
        Expandable  m_ExpandableBit;
        Features    m_Features;

        public EmissionUIBlock(Expandable expandableBit, Features features = Features.All)
        {
            m_ExpandableBit = expandableBit;
            m_Features = features;
        }

        public override void LoadMaterialProperties()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnGUI()
        {
            //throw new System.NotImplementedException();
        }
    }
}
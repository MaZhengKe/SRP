using System;

namespace KuanMiEditor.Rendering.MKRP
{
    class TransparencyUIBlock : MaterialUIBlock
    {
        [Flags]
        public enum Features
        {
            None        = 0,
            Distortion  = 1 << 0,
            Refraction  = 1 << 1,
            All         = ~0
        }
        
        Expandable          m_ExpandableBit;
        Features            m_Features;
        MaterialUIBlockList m_TransparencyBlocks;

        public TransparencyUIBlock(Expandable expandableBit, Features features = Features.All)
        {
            m_ExpandableBit = expandableBit;
            m_Features = features;

            m_TransparencyBlocks = new MaterialUIBlockList(parent);
            if ((features & Features.Refraction) != 0)
                m_TransparencyBlocks.Add(new RefractionUIBlock(1));  // This block will not be used in by a layered shader so we can safely set the layer count to 1
            if ((features & Features.Distortion) != 0)
                m_TransparencyBlocks.Add(new DistortionUIBlock());
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
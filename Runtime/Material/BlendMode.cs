using UnityEngine.Rendering;

namespace KuanMi.Rendering.MKRP
{
    // Enum values are hardcoded for retro-compatibility. Don't change them.
    [GenerateHLSL]
    enum BlendMode
    {
        // Note: value is due to code change, don't change the value
        Alpha = 0,
        Premultiply = 4,
        Additive = 1
    }
}
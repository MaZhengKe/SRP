﻿using System;
using UnityEngine.Rendering;

namespace KuanMi.Rendering.MKRP
{
    internal static class MKRenderQueue
    {
        const int k_TransparentPriorityQueueRange = 100;
        public enum Priority
        {
            Background = UnityEngine.Rendering.RenderQueue.Background,


            Opaque = UnityEngine.Rendering.RenderQueue.Geometry,
            OpaqueDecal = UnityEngine.Rendering.RenderQueue.Geometry + 225, // Opaque Decal mean Opaque that can receive decal
            OpaqueAlphaTest = UnityEngine.Rendering.RenderQueue.AlphaTest,
            OpaqueDecalAlphaTest = UnityEngine.Rendering.RenderQueue.AlphaTest + 25,
            // Warning: we must not change Geometry last value to stay compatible with occlusion
            OpaqueLast = UnityEngine.Rendering.RenderQueue.GeometryLast,

            AfterPostprocessOpaque = UnityEngine.Rendering.RenderQueue.GeometryLast + 1,
            AfterPostprocessOpaqueAlphaTest = UnityEngine.Rendering.RenderQueue.GeometryLast + 10,

            // For transparent pass we define a range of 200 value to define the priority
            // Warning: Be sure no range are overlapping
            PreRefractionFirst = 2750 - k_TransparentPriorityQueueRange,
            PreRefraction = 2750,
            PreRefractionLast = 2750 + k_TransparentPriorityQueueRange,

            TransparentFirst = UnityEngine.Rendering.RenderQueue.Transparent - k_TransparentPriorityQueueRange,
            Transparent = UnityEngine.Rendering.RenderQueue.Transparent,
            TransparentLast = UnityEngine.Rendering.RenderQueue.Transparent + k_TransparentPriorityQueueRange,

            LowTransparentFirst = 3400 - k_TransparentPriorityQueueRange,
            LowTransparent = 3400,
            LowTransparentLast = 3400 + k_TransparentPriorityQueueRange,

            AfterPostprocessTransparentFirst = 3700 - k_TransparentPriorityQueueRange,
            AfterPostprocessTransparent = 3700,
            AfterPostprocessTransparentLast = 3700 + k_TransparentPriorityQueueRange,

            Overlay = UnityEngine.Rendering.RenderQueue.Overlay
        }

        public enum RenderQueueType
        {
            Background,

            // Opaque
            Opaque,
            AfterPostProcessOpaque,

            // Transparent
            PreRefraction,
            Transparent,
            LowTransparent,
            AfterPostprocessTransparent,

            Overlay,

            Unknown
        }
        public static readonly RenderQueueRange k_RenderQueue_OpaqueNoAlphaTest = new RenderQueueRange { lowerBound = (int)Priority.Background, upperBound = (int)Priority.OpaqueAlphaTest - 1 };
        public static readonly RenderQueueRange k_RenderQueue_OpaqueAlphaTest = new RenderQueueRange { lowerBound = (int)Priority.OpaqueAlphaTest, upperBound = (int)Priority.OpaqueLast };
        public static readonly RenderQueueRange k_RenderQueue_OpaqueDecalAndAlphaTest = new RenderQueueRange { lowerBound = (int)Priority.OpaqueDecal, upperBound = (int)Priority.OpaqueLast };
        public static readonly RenderQueueRange k_RenderQueue_AllOpaque = new RenderQueueRange { lowerBound = (int)Priority.Background, upperBound = (int)Priority.OpaqueLast };

        public static readonly RenderQueueRange k_RenderQueue_AfterPostProcessOpaque = new RenderQueueRange { lowerBound = (int)Priority.AfterPostprocessOpaque, upperBound = (int)Priority.AfterPostprocessOpaqueAlphaTest };

        public static readonly RenderQueueRange k_RenderQueue_PreRefraction = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.PreRefractionLast };
        public static readonly RenderQueueRange k_RenderQueue_Transparent = new RenderQueueRange { lowerBound = (int)Priority.TransparentFirst, upperBound = (int)Priority.TransparentLast };
        public static readonly RenderQueueRange k_RenderQueue_TransparentWithLowRes = new RenderQueueRange { lowerBound = (int)Priority.TransparentFirst, upperBound = (int)Priority.LowTransparentLast };
        public static readonly RenderQueueRange k_RenderQueue_LowTransparent = new RenderQueueRange { lowerBound = (int)Priority.LowTransparentFirst, upperBound = (int)Priority.LowTransparentLast };
        public static readonly RenderQueueRange k_RenderQueue_AllTransparent = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.TransparentLast };
        public static readonly RenderQueueRange k_RenderQueue_AllTransparentWithLowRes = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.LowTransparentLast };

        public static readonly RenderQueueRange k_RenderQueue_AfterPostProcessTransparent = new RenderQueueRange { lowerBound = (int)Priority.AfterPostprocessTransparentFirst, upperBound = (int)Priority.AfterPostprocessTransparentLast };

        public static readonly RenderQueueRange k_RenderQueue_All = new RenderQueueRange { lowerBound = 0, upperBound = 5000 };
        public static bool Contains(this RenderQueueRange range, int value) => range.lowerBound <= value && value <= range.upperBound;

        public static RenderQueueType GetTransparentEquivalent(RenderQueueType type)
        {
            switch(type)
            {
                case RenderQueueType.Opaque:
                    return RenderQueueType.Transparent;
                case RenderQueueType.AfterPostProcessOpaque:
                    return RenderQueueType.AfterPostprocessTransparent;
                default:
                    //keep transparent mapped to transparent
                    return type;
                case RenderQueueType.Overlay:
                case RenderQueueType.Background:
                    throw new ArgumentException("Unknow RenderQueueType conversion to transparent equivalent, was " + type);
            }
        }
        
        public static RenderQueueType GetTypeByRenderQueueValue(int renderQueue)
        {
            if (renderQueue == (int)Priority.Background)
                return RenderQueueType.Background;
            if (k_RenderQueue_AllOpaque.Contains(renderQueue))
                return RenderQueueType.Opaque;
            if (k_RenderQueue_AfterPostProcessOpaque.Contains(renderQueue))
                return RenderQueueType.AfterPostProcessOpaque;
            if (k_RenderQueue_PreRefraction.Contains(renderQueue))
                return RenderQueueType.PreRefraction;
            if (k_RenderQueue_Transparent.Contains(renderQueue))
                return RenderQueueType.Transparent;
            if (k_RenderQueue_LowTransparent.Contains(renderQueue))
                return RenderQueueType.LowTransparent;
            if (k_RenderQueue_AfterPostProcessTransparent.Contains(renderQueue))
                return RenderQueueType.AfterPostprocessTransparent;
            if (renderQueue == (int)Priority.Overlay)
                return RenderQueueType.Overlay;
            return RenderQueueType.Unknown;
        }
        
        public static RenderQueueType GetOpaqueEquivalent(RenderQueueType type)
        {
            switch (type)
            {
                case RenderQueueType.PreRefraction:
                case RenderQueueType.Transparent:
                case RenderQueueType.LowTransparent:
                    return RenderQueueType.Opaque;
                case RenderQueueType.AfterPostprocessTransparent:
                    return RenderQueueType.AfterPostProcessOpaque;
                default:
                    //keep opaque mapped to opaque
                    return type;
                case RenderQueueType.Overlay:
                case RenderQueueType.Background:
                    throw new ArgumentException("Unknow RenderQueueType conversion to opaque equivalent, was " + type);
            }
        }
        public static int ChangeType(RenderQueueType targetType, int offset = 0, bool alphaTest = false, bool receiveDecal = false)
        {
            switch (targetType)
            {
                case RenderQueueType.Background:
                    return (int)Priority.Background;
                case RenderQueueType.Opaque:
                    return alphaTest ?
                        (receiveDecal ? (int)Priority.OpaqueDecalAlphaTest : (int)Priority.OpaqueAlphaTest) :
                        (receiveDecal ? (int)Priority.OpaqueDecal : (int)Priority.Opaque);
                case RenderQueueType.AfterPostProcessOpaque:
                    return alphaTest ? (int)Priority.AfterPostprocessOpaqueAlphaTest : (int)Priority.AfterPostprocessOpaque;
                case RenderQueueType.PreRefraction:
                    return (int)Priority.PreRefraction + offset;
                case RenderQueueType.Transparent:
                    return (int)Priority.Transparent + offset;
                case RenderQueueType.LowTransparent:
                    return (int)Priority.LowTransparent + offset;
                case RenderQueueType.AfterPostprocessTransparent:
                    return (int)Priority.AfterPostprocessTransparent + offset;
                case RenderQueueType.Overlay:
                    return (int)Priority.Overlay;
                default:
                    throw new ArgumentException("Unknown RenderQueueType, was " + targetType);
            }
        }

    }
}
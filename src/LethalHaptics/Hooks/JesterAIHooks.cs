using LethalHaptics.Utils;
using UnityEngine;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public class JesterAIHooks
{
    private static readonly int TurningCrank = Animator.StringToHash("turningCrank");
    private static readonly int PoppedOut = Animator.StringToHash("poppedOut");

    [ButtplugPatch]
    public static void Initialize()
    {
        On.JesterAI.Update += JesterAIOnUpdate;
    }

    private static void JesterAIOnUpdate(On.JesterAI.orig_Update orig, JesterAI self)
    {
        orig(self);
        
        // NOTE: I am not sure if there could be two jesters that spawn on a single map.
        if (self.creatureAnimator.GetBool(TurningCrank))
        {
            // TODO: Stroker oscillation 
        }

        if (self.creatureAnimator.GetBool(PoppedOut))
        {
            // TODO: Constant vibration when jester is popped up   
        }
    }
}
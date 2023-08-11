using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Content.Server.Chemistry.AddBonusDamage;
using Content.Shared.Chemistry.AddBonusDamage;

namespace Content.Server.Chemistry.ReagentEffects
{
    /// <summary>
    /// Hallucinations, finally.
    /// <summary>
    public sealed class HallucinationEffect : ReagentEffect
    {
        public override void Effect(ReagentEffectArgs args)
        {
            //Надо будет высирать систему, которая будет срать галюнами
            //Не уверен, что тут вообще нужен эффект, надо просто через Generic насрать
            //ибо он может компонентами срать

            //из госта
                //eyeComponent.VisibilityMask ^= (uint) VisibilityFlags.Ghost;
        }
    }
}

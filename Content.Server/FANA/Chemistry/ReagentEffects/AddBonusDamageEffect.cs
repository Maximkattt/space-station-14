using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Content.Server.Chemistry.AddBonusDamage;
using Content.Shared.Chemistry.AddBonusDamage;

namespace Content.Server.Chemistry.ReagentEffects
{
    /// <summary>
    /// Effect, that increase melee damage of attacks by adding damage to every attack (only for fists :( ).
    /// <summary>
    public sealed class AddBonusDamage : ReagentEffect
    {
        // How much bonus damage will person get
        [DataField("addeddamage")]
        public FixedPoint2 AddedDamage = 5;

        public override void Effect(ReagentEffectArgs args)
        {
            if (!args.EntityManager.HasComponent<AddBonusDamageComponent>(args.SolutionEntity))
            {
                EntitySystem.Get<AddBonusDamageSystem>().AddComponentForBonusDamage(AddedDamage, args);
            }
            else
            {
                EntitySystem.Get<AddBonusDamageSystem>().UpdateTimer(args);
            }

        }
    }
}

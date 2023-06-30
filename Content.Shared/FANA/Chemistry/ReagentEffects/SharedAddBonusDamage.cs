using Content.Shared.Chemistry.Reagent;
using Content.Shared.Damage;
using Content.Shared.FixedPoint;
using Content.Shared.Weapons.Melee.Components;
using Robust.Shared.Serialization;


namespace Content.Shared.Chemistry.ReagentEffects
{
    /// <summary>
    /// Effect, that increase melee damage of attacks by adding damagetype to every attack.
    /// <summary>
    public sealed class AddBonusDamage : ReagentEffect
    {
        // Type of damage, that will be added to every melee atack
        [DataField("damagetype")]
        public string DamageType = "Blunt";
        // How much bonus damage will person get
        [DataField("addeddamage")]
        public FixedPoint2 AddedDamage = 5;

        public override void Effect(ReagentEffectArgs args)
        {
        //I don't know why this working so bad, so i'm going to write my fucking own event handler
            //if (!args.EntityManager.TryGetComponent(args.SolutionEntity, out AddBonusDamageComponent? _))
            //{
                //var damSpec = new DamageSpecifier();
                //damSpec.DamageDict.Add(DamageType, AddedDamage);
                //args.EntityManager.EnsureComponent<BonusMeleeDamageComponent>(args.SolutionEntity).BonusDamage = damSpec;
            //};
        }
    }
}

using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared.Chemistry.AddBonusDamage
{
    [RegisterComponent, NetworkedComponent]
    public sealed class AddBonusDamageComponent : Component
    {
        // How much bonus damage will person get
        [DataField("addeddamage")]
        public FixedPoint2 AddedDamage = 5;

        [DataField("effecttime")]
        public double EffectLifeTime = 2;
        [ViewVariables]
        public TimeSpan ModifierTimer { get; set; } = TimeSpan.Zero;
    }
}

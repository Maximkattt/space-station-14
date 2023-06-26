using Content.Shared.StatusEffect;

namespace Content.Shared.Speech.EntitySystems;

public abstract class SharedBradilaliaSystem : EntitySystem
{
    public const string BradilaliaKey = "Bradilalia";

    [Dependency] private readonly StatusEffectsSystem _statusEffectsSystem = default!;

    // Same as SharedStutteringSystem
    public virtual void DoBradilalia(EntityUid uid, TimeSpan time, bool refresh, StatusEffectsComponent? status = null)
    {
    }

    public virtual void DoRemoveBradilaliaTime(EntityUid uid, double timeRemoved)
    {
        _statusEffectsSystem.TryRemoveTime(uid, BradilaliaKey, TimeSpan.FromSeconds(timeRemoved));
    }

    public void DoRemoveBradilalia(EntityUid uid, double timeRemoved)
    {
       _statusEffectsSystem.TryRemoveStatusEffect(uid, BradilaliaKey);
    }
}

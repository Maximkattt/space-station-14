using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Content.Shared.Weapons.Melee.Events;
using Content.Shared.Chemistry.AddBonusDamage;
using Content.Shared.Damage;
using Robust.Shared.Timing;

namespace Content.Server.Chemistry.AddBonusDamage
{
    /// <summary>
    /// System for handling AddBonusDamageEffect
    /// <summary>
    public sealed class AddBonusDamageSystem : EntitySystem
    {
        [Dependency] private readonly IGameTiming _gameTiming = default!;
        private readonly List<AddBonusDamageComponent> _components = new();
        public override void Initialize()
        {
            base.Initialize();
            UpdatesOutsidePrediction = true;
            SubscribeLocalEvent<AddBonusDamageComponent, GetMeleeDamageEvent>(OnGetAddedMeleeDamage);
        }
        public void AddComponentForBonusDamage(FixedPoint2 addedDamage, ReagentEffectArgs args)
        {
            var damComp = args.EntityManager.EnsureComponent<AddBonusDamageComponent>(args.SolutionEntity);
            damComp.AddedDamage = addedDamage;
            var gameTiming = IoCManager.Resolve<IGameTiming>();
            damComp.ModifierTimer = TimeSpan.FromSeconds(gameTiming.CurTime.TotalSeconds + damComp.EffectLifeTime);
            _components.Add(damComp);
            Dirty(damComp);
        }
        private void OnGetAddedMeleeDamage(EntityUid uid, AddBonusDamageComponent component, ref GetMeleeDamageEvent args)
        {
            //Ну, ёба, в чем проблема
                //Нету просто ивента, который вызывается при атаке энтити,
                //из которого можно получить нужные данные.
                //Все ивенты вызываются либо до получения оружия,
                //либо уже после на самом оружии (за исключением ручек).
                //(а ивенты на оружие не будут обробатываться, ибо AddBonusDamageComponent у оружия нету)
            //Ну, ёба, а че делать
                //1.) Перед получением оружия есть ивент (GetMeleeWeaponEvent),
                    //созданый чтобы подменять стволы в ручках другими системами (наверн).
                    //Можно делать все то, что делает код SharedMeleeWeaponSystem,
                    //а потом немного изебнуться.
                    //(Пока что, самый лучший из возможных исходов, думаю)
                //2.) Пернуть внутрь самой SharedMeleeWeaponSystem.
                    //( :?)  :?)  :?)  :?)  :?)  :?)  :?)  :?) )
                //3.) Обробатывать все ивенты GetMeleeDamageEvent
                    //и чекать наличие у владельца оружия (или самого оружия) AddBonusDamageComponent.
                    //(ПЛОХАЯ ИДЕЯ, МОЖНО ТОЛЬКО ДЛЯ ТЕСТОВ ДАБЫ УБЕДИТЬСЯ В ПРАВИЛЬНОСТИ РАССУЖДЕНИЙ)
                //4.) Найти где я проебался и повесить меня на кресте.
            //Пока пусть будет так - просто увеличенный урон с ручек.
            var damSpec = new DamageSpecifier();
            foreach (var type in args.Damage.DamageDict.Keys)
            {
                args.Damage.DamageDict.TryGetValue(type, out FixedPoint2 damage);
                if (damage != 0)
                {
                    damage += component.AddedDamage;
                    damSpec.DamageDict.Add(type, damage);
                };
            };
            args.Damage = damSpec;
        }
        public void UpdateTimer(ReagentEffectArgs args)
        {
            if (args.EntityManager.TryGetComponent<AddBonusDamageComponent>(args.SolutionEntity, out AddBonusDamageComponent? damComp))
            {
                var gameTiming = IoCManager.Resolve<IGameTiming>();
                damComp.ModifierTimer = TimeSpan.FromSeconds(gameTiming.CurTime.TotalSeconds + damComp.EffectLifeTime);
            }
        }
        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            var currentTime = _gameTiming.CurTime;

            for (var i = _components.Count - 1; i >= 0; i--)
            {
                var component = _components[i];

                if (component.Deleted)
                {
                    _components.RemoveAt(i);
                    continue;
                }

                if (component.ModifierTimer > currentTime) continue;

                _components.RemoveAt(i);
                EntityManager.RemoveComponent<AddBonusDamageComponent>(component.Owner);
            }
        }
    }
}

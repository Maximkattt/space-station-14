using System.Text;
using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Content.Shared.StatusEffect;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems
{
    public sealed class BradilaliaSystem : SharedBradilaliaSystem
    {
        [Dependency] private readonly StatusEffectsSystem _statusEffectsSystem = default!;
        [Dependency] private readonly IRobustRandom _random = default!;

        // Regex of characters to bradilalia.
        private static readonly Regex Bradilalia = new(@"[aeiouyаеёиоуэя]", // Corvax-Localization (what?)
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override void Initialize()
        {
            SubscribeLocalEvent<BradilaliaAccentComponent, AccentGetEvent>(OnAccent);
        }

        public override void DoBradilalia(EntityUid uid, TimeSpan time, bool refresh, StatusEffectsComponent? status = null)
        {
            if (!Resolve(uid, ref status, false))
                return;

            _statusEffectsSystem.TryAddStatusEffect<BradilaliaAccentComponent>(uid, BradilaliaKey, time, refresh, status);
        }

        private void OnAccent(EntityUid uid, BradilaliaAccentComponent component, AccentGetEvent args)
        {
            args.Message = Accentuate(args.Message);
        }

        public string Accentuate(string message)
        {
            var length = message.Length;

            var finalMessage = new StringBuilder();

            string newLetter;

            for (var i = 0; i < length; i++)
            {
                newLetter = message[i].ToString();
                if (Bradilalia.IsMatch(newLetter))
                {
                    if (_random.Prob(0.33f))
                    {
                        newLetter = $"{newLetter}{newLetter}{newLetter}";
                    }
                    else if (_random.Prob(0.33f))
                    {
                        newLetter = $"{newLetter}{newLetter}{newLetter}{newLetter}";
                    }
                    else
                    {
                        newLetter = $"{newLetter}{newLetter}{newLetter}{newLetter}{newLetter}";
                    }
                }

                finalMessage.Append(newLetter);
            }
            return finalMessage.ToString();
        }
    }
}

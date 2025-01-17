using System.Collections.Generic;
using Content.Shared.CharacterAppearance;
using Robust.Shared.Localization;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.Utility;

namespace Content.Shared.Markings
{
    [Prototype("marking")]
    public sealed class MarkingPrototype : IPrototype
    {
        [IdDataField]
        public string ID { get; } = "uwu";

        public string Name { get; private set; } = default!;

        [DataField("bodyPart", required: true)]
        public HumanoidVisualLayers BodyPart { get; } = default!;

        [DataField("markingCategory", required: true)]
        public MarkingCategories MarkingCategory { get; } = default!;

        [DataField("speciesRestriction")]
        public List<string>? SpeciesRestrictions { get; }

        [DataField("followSkinColor")]
        public bool FollowSkinColor { get; } = false;

        [DataField("sprites", required: true)]
        public List<SpriteSpecifier> Sprites { get; private set; } = default!;

        public Marking AsMarking()
        {
            return new Marking(ID, Sprites.Count);
        }
    }
}

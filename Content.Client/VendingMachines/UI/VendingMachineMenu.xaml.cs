﻿using System;
using System.Collections.Generic;
using Robust.Client.AutoGenerated;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.IoC;
using Robust.Shared.Prototypes;
using static Content.Shared.VendingMachines.SharedVendingMachineComponent;

namespace Content.Client.VendingMachines.UI
{
    [GenerateTypedNameReferences]
    public sealed partial class VendingMachineMenu : DefaultWindow
    {
        [Dependency] private readonly IResourceCache _resourceCache = default!;
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

        private VendingMachineBoundUserInterface Owner { get; }

        private List<VendingMachineInventoryEntry> _cachedInventory = new();

        public VendingMachineMenu(VendingMachineBoundUserInterface owner)
        {
            IoCManager.InjectDependencies(this);
            RobustXamlLoader.Load(this);

            Owner = owner;
            VendingContents.OnItemSelected += ItemSelected;
        }

        public void Populate(List<VendingMachineInventoryEntry> inventory)
        {
            VendingContents.Clear();
            _cachedInventory = inventory;
            var longestEntry = "";
            foreach (VendingMachineInventoryEntry entry in inventory)
            {
                var itemName = _prototypeManager.Index<EntityPrototype>(entry.ID).Name;
                if (itemName.Length > longestEntry.Length)
                    longestEntry = itemName;

                Texture? icon = null;
                if(_prototypeManager.TryIndex(entry.ID, out EntityPrototype? prototype))
                    icon = SpriteComponent.GetPrototypeIcon(prototype, _resourceCache).Default;

                VendingContents.AddItem($"{itemName} [{entry.Amount}]", icon);
            }

            SetSize = (Math.Clamp((longestEntry.Length + 2) * 12, 250, 300),
                Math.Clamp(VendingContents.Count * 50, 150, 350));
        }

        public void ItemSelected(ItemList.ItemListSelectedEventArgs args)
        {
            Owner.Eject(_cachedInventory[args.ItemIndex].ID);
        }
    }
}
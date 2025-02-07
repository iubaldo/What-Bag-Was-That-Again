using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.Common;

using System.Linq;


namespace whatbagwasthatagain.src
{
    public class WBWTA_Main : ModSystem
    {
        ICoreClientAPI capi;


        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);

            capi = api;

            capi.Event.RegisterGameTickListener(HighlightSlot, 1000);
        }


        public override void AssetsFinalize(ICoreAPI api)
        {
            base.AssetsFinalize(api);

            // api.Logger.StoryEvent("loading wbwta");
        }


        public void HighlightSlot(float deltaTime)
        {
            if (!capi.World.Player.InventoryManager.OpenedInventories.Any())
                return;

            if (capi.World.Player.InventoryManager.CurrentHoveredSlot == null || capi.World.Player.InventoryManager.CurrentHoveredSlot is not ItemSlotBagContent)
                return;

            ItemSlotBagContent currentSlot = capi.World.Player.InventoryManager.CurrentHoveredSlot as ItemSlotBagContent;
            InventoryPlayerBackPacks backpackInv = capi.World.Player.InventoryManager.GetOwnInventory(GlobalConstants.backpackInvClassName) as InventoryPlayerBackPacks;

            int id = currentSlot.BagIndex;
            backpackInv.PerformNotifySlot(id);
        }
    }
}

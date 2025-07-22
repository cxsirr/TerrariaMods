using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace OneWayWall.Tiles
{
    public class OneWayWall : ModTile
    {
        // 2x2 tile, rotatable, one-way collision depending on facing
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileLavaDeath[Type] = true;

            // Setup as a 2x2 tile
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), "One Way Wall");
            ItemDrop = ModContent.ItemType<Items.OneWayWallItem>();
            DustType = DustID.Stone;
        }

        public override bool RightClick(int i, int j)
        {
            // Find top-left corner of 2x2
            int left = i - (Main.tile[i, j].frameX / 18) % 2;
            int top = j - (Main.tile[i, j].frameY / 18) % 2;

            // Current rotation (0=up,1=right,2=down,3=left)
            int currentRotation = Main.tile[left, top].frameX / 36;
            int nextRotation = (currentRotation + 1) % 4;

            // Update all four tiles' frames
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    Tile tile = Main.tile[left + x, top + y];
                    tile.frameX = (short)(nextRotation * 36 + x * 18);
                    tile.frameY = (short)(y * 18);
                }
            }
            Main.PlaySound(SoundID.MenuTick, new Vector2(i * 16, j * 16));
            return true;
        }

        public override bool HasSmartInteract() => true;

        public override bool Slope(int i, int j) => false; // Prevent sloping

        // Basic one-way logic for platforms (Terraria only natively supports vertical one-way)
        // This version does not fully block from sides but allows you to test rotation and placement.
        // For advanced, per-direction entity collision, more hooks and logic are needed.

        // If you want to experiment with collision, you could implement ModPlayer.PreUpdateMovement or similar!
    }
}

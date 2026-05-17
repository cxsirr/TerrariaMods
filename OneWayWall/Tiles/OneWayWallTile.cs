using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace OneWayWall.Tiles
{
    public class OneWayWallTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(100, 150, 200), CreateMapEntryName());

            DustType = DustID.Stone;
            HitSound = SoundID.Tink;
            MineResist = 1f;
            MinPick = 5;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noSound)
        {
            if (!fail)
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemType<Items.OneWayWallItem>());
            }
        }

        public override bool Hammerable(int i, int j)
        {
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.ShowItemIcon(ItemType<Items.OneWayWallItem>());
        }

        public override void HitSound(int i, int j)
        {
            SoundEngine.PlaySound(SoundID.Tink, new Vector2(i * 16, j * 16));
        }

        public override void HammerEffect(int i, int j)
        {
            // Rotate on hammer hit
            int rotation = (Framing.GetTileSafely(i, j).TileFrameX / 18) % 4;
            rotation = (rotation + 1) % 4;
            Framing.GetTileSafely(i, j).TileFrameX = (short)(rotation * 18);
        }
    }
}

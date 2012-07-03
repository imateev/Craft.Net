using System;
using System.Collections.Generic;
using Craft.Net.Server.Worlds.Generation;
using Craft.Net.Server.Blocks;

namespace Craft.Net.Server.Worlds
{
    public class Region
    {
        // In chunks
        public const int Width = 32, Depth = 32;

        public Dictionary<Vector3, Chunk> Chunks;
        public IWorldGenerator WorldGenerator;
        public Vector3 Position;

        public Region(Vector3 Position, IWorldGenerator WorldGenerator)
        {
            Chunks = new Dictionary<Vector3, Chunk>();
            this.Position = Position;
            this.WorldGenerator = WorldGenerator;
        }

        public Block GetBlock(Vector3 position)
        {
            position = position.Floor();
            Vector3 relativePosition = position;
            position.X = (int)(position.X) / Chunk.Width;
            position.Y = 0;
            position.Z = (int)(position.Z) / Chunk.Height;

            relativePosition.X = (int)(relativePosition.X) % Chunk.Width;
            relativePosition.Y = 0;
            relativePosition.Z = (int)(relativePosition.Z) % Chunk.Height;

            if (!Chunks.ContainsKey(position))
                Chunks.Add(position, WorldGenerator.CreateChunk(position));

            return Chunks[position].GetBlock(relativePosition);
        }
    }
}

using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The GetChestContents (0x1F) packet.
    /// </summary>
    public class GetChestContents : TerrariaPacket
    {

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetChestContents"/> class.
        /// </summary>
        public GetChestContents()
            : base((byte)PacketTypes.GetChestContents)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetChestContents"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public GetChestContents(BinaryReader br)
            : base(br)
        {
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[GetChestContents: TileX = {TileX} TileY = {TileY}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(4);
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            /*
             * Length and ID headers get written in the base packet class.
             */
            if (includeHeader) {
                base.ToStream(stream, includeHeader);
            }

            /*
             * Always make sure to not close the stream when serializing.
             * 
             * It is up to the caller to decide if the underlying stream
             * gets closed.  If this is a network stream we do not want
             * the regressions of unconditionally closing the TCP socket
             * once the payload of data has been sent to the client.
             */
            using (BinaryWriter br = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true)) {
                br.Write(TileX);
                br.Write(TileY);
            }
        }

        #endregion

    }
}

﻿using System.Buffers.Binary;
using System.ComponentModel;

namespace WindivertDotnet
{
    public struct TcpHeader
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        private ushort srcPort;
        public ushort SrcPort
        {
            get => BinaryPrimitives.ReverseEndianness(srcPort);
            set => srcPort = BinaryPrimitives.ReverseEndianness(value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ushort dstPort;
        public ushort DstPort
        {
            get => BinaryPrimitives.ReverseEndianness(dstPort);
            set => dstPort = BinaryPrimitives.ReverseEndianness(value);
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        private uint seqNum;
        public uint SeqNum
        {
            get => BinaryPrimitives.ReverseEndianness(seqNum);
            set => seqNum = BinaryPrimitives.ReverseEndianness(value);
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        private uint ackNum;
        public uint AckNum
        {
            get => BinaryPrimitives.ReverseEndianness(ackNum);
            set => ackNum = BinaryPrimitives.ReverseEndianness(value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ushort _bitfield;


        public ushort Reserved1
        {
            get
            {
                return (ushort)(_bitfield & 0xFu);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~0xFu) | (value & 0xFu));
            }
        }


        public ushort HdrLength
        {
            get
            {
                return (ushort)((_bitfield >> 4) & 0xFu);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0xFu << 4)) | ((value & 0xFu) << 4));
            }
        }


        public ushort Fin
        {
            get
            {
                return (ushort)((_bitfield >> 8) & 0x1u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x1u << 8)) | ((value & 0x1u) << 8));
            }
        }



        public ushort Syn
        {
            get
            {
                return (ushort)((_bitfield >> 9) & 0x1u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x1u << 9)) | ((value & 0x1u) << 9));
            }
        }


        public ushort Rst
        {
            get
            {
                return (ushort)((_bitfield >> 10) & 0x1u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x1u << 10)) | ((value & 0x1u) << 10));
            }
        }


        public ushort Psh
        {
            get
            {
                return (ushort)((_bitfield >> 11) & 0x1u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x1u << 11)) | ((value & 0x1u) << 11));
            }
        }


        public ushort Ack
        {
            get
            {
                return (ushort)((_bitfield >> 12) & 0x1u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x1u << 12)) | ((value & 0x1u) << 12));
            }
        }

        public ushort Urg
        {
            get
            {
                return (ushort)((_bitfield >> 13) & 0x1u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x1u << 13)) | ((value & 0x1u) << 13));
            }
        }

        public ushort Reserved2
        {
            get
            {
                return (ushort)((_bitfield >> 14) & 0x3u);
            }

            set
            {
                _bitfield = (ushort)((_bitfield & ~(0x3u << 14)) | ((value & 0x3u) << 14));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ushort window;

        public ushort Window
        {
            get => BinaryPrimitives.ReverseEndianness(window);
            set => window = BinaryPrimitives.ReverseEndianness(value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ushort checksum;

        public ushort Checksum
        {
            get => BinaryPrimitives.ReverseEndianness(checksum);
            set => checksum = BinaryPrimitives.ReverseEndianness(value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ushort urgPtr;

        public ushort UrgPtr
        {
            get => BinaryPrimitives.ReverseEndianness(urgPtr);
            set => urgPtr = BinaryPrimitives.ReverseEndianness(value);
        }
    }
}

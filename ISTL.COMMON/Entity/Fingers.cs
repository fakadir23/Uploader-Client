using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.COMMON.Entity
{
    public class Fingers
    {
        private sbyte id;
        private byte[] fingerMinex;
        private byte[] fingerWSQ;
        private byte[] fingerBMP;

        public Fingers(sbyte id, byte[] fingerWSQ, byte[] fingerBMP)
        {
            Id = id;
            FingerWSQ = fingerWSQ;
            FingerBMP = fingerBMP;
        }

        public Fingers(sbyte id, byte[] fingerMinex, byte[] fingerWSQ, byte[] fingerBMP)
        {
            Id = id;
            FingerMinex = fingerMinex;
            FingerWSQ = fingerWSQ;
            FingerBMP = fingerBMP;
        }

        public sbyte Id
        {
            get { return id; }
            set { id = value; }
        }
        public byte[] FingerMinex
        {
            get { return fingerMinex; }
            set { fingerMinex = value; }
        }
        public byte[] FingerWSQ
        {
            get { return fingerWSQ; }
            set { fingerWSQ = value; }
        }

        public byte[] FingerBMP
        {
            get { return fingerBMP; }
            set { fingerBMP = value; }
        }
    }
}

using System;

namespace DragonDataSniffer
{
    public class OnHandShakeArgs : EventArgs
    {
        public OnHandShakeArgs(short pXorPos)
        {
            XorPos = pXorPos;
        }

        public short XorPos { get; private set; }
    }
}
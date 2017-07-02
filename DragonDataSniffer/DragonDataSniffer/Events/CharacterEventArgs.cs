using DragonDataSniffer.Object;
using System;

namespace DragonDataSniffer.Events
{
    public class CharacterEventArgs : EventArgs
    {
        public CharacterEventArgs(Character pChar)
        {
            pCharacter = pChar;
        }

        public Character pCharacter { get; private set; }
    }
}
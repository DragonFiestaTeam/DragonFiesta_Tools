using System;
using DragonDataSniffer.Object;

namespace DragonDataSniffer.Events
{
    public class CharacterEventArgs : EventArgs
    {
        public CharacterEventArgs(Character pChar)
        {
            PCharacter = pChar;
        }
        public Character PCharacter { get; private set; }
    }
}
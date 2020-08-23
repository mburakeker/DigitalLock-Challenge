using System;
using System.Collections.Generic;
using System.Text;
using DigitalLock.Interfaces;

namespace DigitalLock
{
    public class PickLock : IPickLock
    {
        public IDigitalLock _digitalLock;
        public PickLock(IDigitalLock digitalLock)
        {
            _digitalLock = digitalLock;
        }
        public string Unlock(IDigitalLock digitalLock)
        {
            string currentChars = digitalLock.ReadAll();
            int lockSize = currentChars.Length;
            int cipherLength = digitalLock.GetCipherLength();
            digitalLock.Reset();
            TryAllCombinations(digitalLock,"", 0, lockSize);

            return digitalLock.ReadAll();
        }
        public void TryAllCombinations(IDigitalLock digitalLock, string pwd,int position, int size)
        {
            if (position < size)
                for (int i = 0; i < digitalLock.GetCipherLength(); i++)
                {
                    digitalLock.Turn(TurnDirection.Forward, position, 1);
                    if (!digitalLock.IsLocked())
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(digitalLock.ReadAll());
                        TryAllCombinations(digitalLock,pwd + i, position + 1, size);
                    }

                }
        }
    }
}

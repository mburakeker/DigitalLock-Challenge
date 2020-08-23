using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLock.Interfaces
{
    public interface IPickLock
    {
        public string Unlock(IDigitalLock digitalLock);
        public void TryAllCombinations(IDigitalLock digitalLock, string pwd, int position, int size);
    }
}

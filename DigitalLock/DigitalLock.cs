using System;
using System.Collections.Generic;
using System.Text;
using DigitalLock.Interfaces;
using DigitalLock.Models;
using DigitalLock.Infrastructure.Exceptions;
using Microsoft.Extensions.Options;

namespace DigitalLock
{
   
    class DigitalLock : IDigitalLock
    {
        public DigitalLockModel _digitalLock;
        public string lockResetPos;
        public DigitalLock(IOptions<DigitalLockModel> digitalLock)
        {
            _digitalLock = digitalLock.Value;
        }
        public int GetCipherLength()
        {
            return _digitalLock.Charset.Length;
        }

        public bool IsLocked()
        {
            return _digitalLock.IsLocked;
        }

        public bool Lock(bool garbleAfterLock)
        {
            if (_digitalLock.IsLocked)
            {
                throw Exceptions.AlreadyLocked;
            }
            else
            {
                _digitalLock.Password = _digitalLock.Chars;
                _digitalLock.IsLocked = true;
                return true;
            }
        }

        public char Read(int circleIndex)
        {
            ValidateCircleIndex(circleIndex);
            return _digitalLock.Chars[circleIndex];
        }

        public string ReadAll()
        {
            return _digitalLock.Chars;
        }

        public bool Reset()
        {
            if (lockResetPos == null) lockResetPos = GetResetPosition();

            _digitalLock.Chars = lockResetPos;
            return true;
        }

        public bool Turn(TurnDirection direction, int circleIndex, int step)
        {
            ValidateCircleIndex(circleIndex);
            ValidateCharset(_digitalLock.Charset, _digitalLock.Chars);
            int posOfCharInCharset = _digitalLock.Charset.IndexOf(_digitalLock.Chars[circleIndex]);
            char[] tempChars = _digitalLock.Chars.ToCharArray();
            switch (direction)
            {
                case TurnDirection.Forward:
                    tempChars[circleIndex] = _digitalLock.Charset[(posOfCharInCharset + step) % _digitalLock.Charset.Length];
                    break;
                case TurnDirection.Backward:
                    if(posOfCharInCharset>step)
                        tempChars[circleIndex] = _digitalLock.Charset[((posOfCharInCharset - step) % _digitalLock.Charset.Length)];
                    else
                        tempChars[circleIndex] = _digitalLock.Charset[_digitalLock.Charset.Length - ((posOfCharInCharset - step) % _digitalLock.Charset.Length)];
                    break;
                default:
                    throw Exceptions.InvalidTurnDirection;
            }
            _digitalLock.Chars = new string(tempChars);
            if (_digitalLock.Chars == _digitalLock.Password)
            {

                Console.WriteLine("Found Password: " + ReadAll());
                _digitalLock.IsLocked = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        void ValidateCharset(string charset, string chars)
        {
            foreach (var charItem in chars)
            {
                if (!charset.ToLower().Contains(charItem))
                {
                    throw Exceptions.CharsNotMatchingCharset;
                }
            }
        }
        string GetResetPosition()
        {
            char[] resetPosition = new char[_digitalLock.Size];
            for (int i = 0; i < _digitalLock.Size; i++)
            {
                resetPosition[i] = _digitalLock.Charset[0];
            }
            return new string(resetPosition);
        }
        void ValidateCircleIndex(int circleIndex)
        {
            if (circleIndex > _digitalLock.Size | circleIndex < 0)
                throw Exceptions.InvalidCircleIndex;
        }
    }
}

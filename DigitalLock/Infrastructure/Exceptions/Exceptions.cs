using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLock.Infrastructure.Exceptions
{
    public class Exceptions : SystemException
    {

        public static readonly ExceptionModel AlreadyLocked = new ExceptionModel
        {
            StatusCode = 409,
            Title = "The lock is already locked.",
            Detail = "The lock you are trying to lock is already locked.",
        };
        public static readonly ExceptionModel AlreadyUnlocked = new ExceptionModel
        {
            StatusCode = 400,
            Title = "The lock is already unlocked.",
            Detail = "The lock you are trying to unlock is already unlocked.",
        };
        public static readonly ExceptionModel CipherNotStrongEnough = new ExceptionModel
        {
            StatusCode = 400,
            Title = "The Cipher Charset is not strong enough.",
            Detail = "The Cipher Charset must be at least 10 characters.",
        };
        public static readonly ExceptionModel CharsNotMatchingCharset = new ExceptionModel
        {
            StatusCode = 400,
            Title = "Chars not matching with Charset.",
            Detail = "Selected chars do not match the cipher charset.",
        };
        public static readonly ExceptionModel InvalidCircleIndex = new ExceptionModel
        {
            StatusCode = 400,
            Title = "Invalid Circle Index.",
            Detail = "The Cipher doesn't have this index.",
        };
        public static readonly ExceptionModel InvalidTurnDirection = new ExceptionModel
        {
            StatusCode = 400,
            Title = "Invalid Turn Direction.",
            Detail = "The Turn Direction you have selected is invalid.",
        };
    }
}

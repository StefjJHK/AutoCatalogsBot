using System;

namespace Presentation.Aspects.Abstracts
{
    public interface IRequestCircutBraker
    {
        public T Execute<T>(Func<T> request);
    }
}
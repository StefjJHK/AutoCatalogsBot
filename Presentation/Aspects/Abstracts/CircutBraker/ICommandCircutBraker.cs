using System;

namespace Presentation.Aspects.Abstracts
{
    public interface ICommandCircutBraker
    {
        public void Execute(Action action);
    }
}

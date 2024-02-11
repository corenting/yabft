namespace Yabft.Common;
using Yabft.InputOuput;

internal readonly struct RunnerOptions
{
    internal RunnerOptions(bool wrap, IInputOutput inputOutputSystem)
    {
        this.Wrap = wrap;
        this.InputOutputSystem = inputOutputSystem;
    }

    internal bool Wrap { get; }
    internal IInputOutput InputOutputSystem { get; }
}

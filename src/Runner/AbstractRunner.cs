namespace Yabft.Runner;

using Yabft.Common;
using Yabft.InputOuput;

internal abstract class AbstractRunner(RunnerOptions options, string program)
{
    protected Instruction[] Program { get; private set; } = Parser.Parse(program);

    protected int CurrentProgramPosition { get; set; }

    protected int CurrentTapePosition { get; set; }

    protected byte[] Tape { get; set; } = new byte[Constants.TapeLength];

    protected IInputOutput InputOutputSystem { get; set; } = options.InputOutputSystem;

    protected bool Wrap { get; private set; } = options.Wrap;

    public abstract void Run();
}

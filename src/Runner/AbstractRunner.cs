namespace Yabft.Runner;

using Yabft.Common;
using Yabft.InputOuput;

internal abstract class AbstractRunner
{
    public AbstractRunner(RunnerOptions options, string program)
    {
        this.Program = Parser.Parse(program);
        this.CurrentProgramPosition = 0;
        this.CurrentTapePosition = 0;
        this.Tape = new byte[Constants.TapeLength];

        this.InputOutputSystem = options.InputOutputSystem;
        this.Wrap = options.Wrap;
    }

    protected Instruction[] Program { get; private set; }

    protected int CurrentProgramPosition { get; set; }

    protected int CurrentTapePosition { get; set; }

    protected byte[] Tape { get; set; }

    protected IInputOutput InputOutputSystem { get; set; }

    protected bool Wrap { get; private set; }

    public abstract void Run();
}

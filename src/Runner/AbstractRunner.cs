namespace Yabft.Runner
{
    using Yabft.InputOuput;

    public abstract class AbstractRunner
    {
        public AbstractRunner(IInputOutput inputOutputSystem, string program)
        {
            this.Program = program;
            this.CurrentProgramPosition = 0;
            this.CurrentTapePosition = 0;
            this.Tape = new byte[Shared.Constants.TapeLength];

            this.InputOutputSystem = inputOutputSystem;
        }

        protected string Program { get; private set; }

        protected int CurrentProgramPosition { get; set; }

        protected int CurrentTapePosition { get; set; }

        protected byte[] Tape { get; set; }

        protected IInputOutput InputOutputSystem { get; set; }

        public abstract void Run();
    }
}

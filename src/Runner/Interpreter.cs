namespace Yabft.Runner
{
    using System;
    using System.Collections.Generic;
    using Yabft.Shared;
    using static Yabft.Shared.Constants;

    public class Interpreter : AbstractRunner
    {
        private Dictionary<int, int> loopsJumps;

        public Interpreter(RunnerOptions options, string program)
            : base(options, program)
        {
            this.loopsJumps = new Dictionary<int, int>();
            this.ComputeJumps();
        }

        public override void Run()
        {
            do
            {
                Instruction instruction = this.Program[this.CurrentProgramPosition++];
                switch (instruction.Type)
                {
                    case InstructionType.Add:
                    case InstructionType.Substract:
                        this.InstructionAddSubstract(instruction);
                        break;
                    case InstructionType.LoopBegin:
                        this.InstructionLoopBegin();
                        break;
                    case InstructionType.LoopEnd:
                        this.InstructionLoopEnd();
                        break;
                    case InstructionType.MoveLeft:
                    case InstructionType.MoveRight:
                        this.InstructionMove(instruction);
                        break;
                    case InstructionType.Read:
                        this.InstructionRead();
                        break;
                    case InstructionType.Write:
                        this.InstructionWrite();
                        break;
                    default:
                        continue;
                }
            }
            while (this.CurrentProgramPosition < this.Program.Length);
        }

        private void ComputeJumps()
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < this.Program.Length; i++)
            {
                Instruction instruction = this.Program[i];
                if (instruction.Type == InstructionType.LoopBegin)
                {
                    stack.Push(i);
                }
                else if (instruction.Type == InstructionType.LoopEnd)
                {
                    int startPos = stack.Pop();
                    this.loopsJumps.Add(startPos, i);
                    this.loopsJumps.Add(i, startPos);
                }
            }
        }

        private void InstructionWrite()
        {
            byte currentByte = this.Tape[this.CurrentTapePosition];
            this.InputOutputSystem.WriteByte(currentByte);
        }

        private void InstructionRead()
        {
            this.Tape[this.CurrentTapePosition] = this.InputOutputSystem.ReadByte();
        }

        private void InstructionMove(Instruction instruction)
        {
            if (instruction.Type == InstructionType.MoveRight)
            {
                this.CurrentTapePosition += Convert.ToByte(instruction.Amount);
            }
            else
            {
                this.CurrentTapePosition -= Convert.ToByte(instruction.Amount);
            }

            if (this.Wrap)
            {
                this.CurrentTapePosition = this.CurrentTapePosition.Wrap(0, Constants.TapeLength);
            }
        }

        private void InstructionLoopBegin()
        {
            if (this.Tape[this.CurrentTapePosition] == 0)
            {
                this.CurrentProgramPosition = this.loopsJumps[this.CurrentProgramPosition - 1];
            }
        }

        private void InstructionLoopEnd()
        {
            if (this.Tape[this.CurrentTapePosition] != 0)
            {
                this.CurrentProgramPosition = this.loopsJumps[this.CurrentProgramPosition - 1];
            }
        }

        private void InstructionAddSubstract(Instruction instruction)
        {
            if (instruction.Type == InstructionType.Add)
            {
                this.Tape[this.CurrentTapePosition] += Convert.ToByte(instruction.Amount);
            }
            else
            {
                this.Tape[this.CurrentTapePosition] -= Convert.ToByte(instruction.Amount);
            }
        }
    }
}

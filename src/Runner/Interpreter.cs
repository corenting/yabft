namespace Yabft.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Yabft.InputOuput;
    using Yabft.Shared;
    using static Yabft.Shared.Constants;

    public class Interpreter : AbstractRunner
    {
        private List<(int, int)> loopsJumps;

        public Interpreter(IInputOutput inputOutputSystem, string program)
            : base(inputOutputSystem, program)
        {
            this.loopsJumps = new List<(int, int)>();
            this.ComputeJumps();
        }

        public override void Run()
        {
            do
            {
                Instruction instruction = this.Program.ElementAt(this.CurrentProgramPosition++);

                switch (instruction.Type)
                {
                    case InstructionType.Add:
                    case InstructionType.Substract:
                        this.InstructionAddSubstract(instruction.Amount);
                        break;
                    case InstructionType.LoopBegin:
                        this.InstructionLoopBegin();
                        break;
                    case InstructionType.LoopEnd:
                        this.InstructionLoopEnd();
                        break;
                    case InstructionType.MoveLeft:
                    case InstructionType.MoveRight:
                        this.InstructionMove(instruction.Type);
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
            while (this.CurrentProgramPosition < this.Program.Count);
        }

        private void ComputeJumps()
        {
            for (int index = 0; index < this.Program.Count; index++)
            {
                Instruction currentInstruction = this.Program.ElementAt(index);
                if (currentInstruction.Type != InstructionType.LoopBegin)
                {
                    continue;
                }

                int? endIndex = this.ComputeLoopEnd(index);
                if (endIndex != null)
                {
                    this.loopsJumps.Add((index, endIndex.Value));
                }
            }
        }

        private int? ComputeLoopEnd(int startPos)
        {
            int index = startPos + 1;
            int count = 1;

            while (index < this.Program.Count)
            {
                Instruction currentInstruction = this.Program.ElementAt(index);

                if (currentInstruction.Type == InstructionType.LoopBegin)
                {
                    count++;
                }

                if (currentInstruction.Type == InstructionType.LoopEnd)
                {
                    count--;
                }

                // Corresponding end found
                if (count == 0)
                {
                    return index + 1;
                }

                index++;
            }

            return null;
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

        private void InstructionMove(InstructionType type)
        {
            this.CurrentTapePosition += type == InstructionType.MoveLeft ? -1 : 1;
            this.CurrentTapePosition = this.CurrentTapePosition.Wrap(0, Constants.TapeLength);
        }

        private void InstructionLoopBegin()
        {
            if (this.Tape[this.CurrentTapePosition] == 0)
            {
                this.CurrentProgramPosition = this.loopsJumps.Where(elt => elt.Item1 == this.CurrentProgramPosition - 1).First().Item2;
            }
        }

        private void InstructionLoopEnd()
        {
            if (this.Tape[this.CurrentTapePosition] != 0)
            {
                this.CurrentProgramPosition = this.loopsJumps.Where(elt => elt.Item2 == this.CurrentProgramPosition).First().Item1;
            }
        }

        private void InstructionAddSubstract(int amount)
        {
            if (amount >= 0)
            {
                this.Tape[this.CurrentTapePosition] += Convert.ToByte(amount);
            }
            else
            {
                this.Tape[this.CurrentTapePosition] -= Convert.ToByte(Math.Abs(amount));
            }
        }
    }
}

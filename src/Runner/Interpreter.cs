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
        private Dictionary<int, int> loopsJumps;
        public Interpreter(IInputOutput inputOutputSystem, string program)
            : base(inputOutputSystem, program)
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
            for (int index = 0; index < this.Program.Length; index++)
            {
                Instruction currentInstruction = this.Program[index];
                if (currentInstruction.Type != InstructionType.LoopBegin)
                {
                    continue;
                }

                int? endIndex = this.ComputeLoopEnd(index);
                if (endIndex != null)
                {
                    this.loopsJumps.Add(index, endIndex.Value);
                }
            }
        }

        private int? ComputeLoopEnd(int startPos)
        {
            int index = startPos + 1;
            int count = 1;

            while (index < this.Program.Length)
            {
                Instruction currentInstruction = this.Program[index];

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

            this.CurrentTapePosition = this.CurrentTapePosition.Wrap(0, Constants.TapeLength);
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
                this.CurrentProgramPosition = this.loopsJumps[this.CurrentProgramPosition];
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

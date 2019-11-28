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
                (Instruction instruction, InstructionParameter? instructionParameter) =
                    this.Program[this.CurrentProgramPosition++].DecodeInstruction();

                switch (instruction)
                {
                    case Instruction.Add:
                        this.InstructionAdd(instructionParameter.Value);
                        break;
                    case Instruction.LoopBegin:
                        this.InstructionLoopBegin();
                        break;
                    case Instruction.LoopEnd:
                        this.InstructionLoopEnd();
                        break;
                    case Instruction.Move:
                        this.InstructionMove(instructionParameter.Value);
                        break;
                    case Instruction.Read:
                        this.InstructionRead();
                        break;
                    case Instruction.Write:
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
                char currentChar = this.Program[index];
                if (currentChar != '[')
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

            while (index < this.Program.Length)
            {
                Instruction currentInstruction = this.Program[index].DecodeInstruction().Item1;

                if (currentInstruction == Instruction.LoopBegin)
                {
                    count++;
                }

                if (currentInstruction == Instruction.LoopEnd)
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

        private void InstructionMove(InstructionParameter instructionParameter)
        {
            this.CurrentTapePosition += instructionParameter == InstructionParameter.Up ? 1 : -1;
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

        private void InstructionAdd(InstructionParameter instructionParameter)
        {
            if (instructionParameter == InstructionParameter.Up)
            {
                this.Tape[this.CurrentTapePosition] += 1;
            }
            else
            {
                this.Tape[this.CurrentTapePosition] -= 1;
            }
        }
    }
}

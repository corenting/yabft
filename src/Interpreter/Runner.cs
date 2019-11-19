namespace Yabft.Interpreter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Yabft.InputOuput;
    using Yabft.Shared;
    using static Yabft.Shared.Constants;

    public class Runner
    {
        private readonly string program;
        private int currentProgramPosition;
        private int currentTapePosition;
        private byte[] tape;
        private List<(int, int)> loopsJumps;

        private IInputOutput inputOutputSystem;

        public Runner(IInputOutput inputOutputSystem, string program)
        {
            this.program = program;
            this.currentProgramPosition = 0;
            this.currentTapePosition = 0;
            this.tape = new byte[Shared.Constants.TapeLength];

            this.loopsJumps = new List<(int, int)>();
            this.ComputeJumps();

            this.inputOutputSystem = inputOutputSystem;

            return;
        }

        public void Run()
        {
            do
            {
                (Instruction instruction, InstructionParameter? instructionParameter) =
                    this.program[this.currentProgramPosition++].DecodeInstruction();

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
            while (this.currentProgramPosition < this.program.Length);
        }

        private void ComputeJumps()
        {
            for (int index = 0; index < this.program.Length; index++)
            {
                char currentChar = this.program[index];
                if (currentChar != '[' && currentChar != ']')
                {
                    continue;
                }

                if (currentChar == '[' && this.ComputeStartLoop(index) is int endIndex)
                {
                    this.loopsJumps.Add((index, endIndex));
                }
                else if (currentChar == ']' && this.ComputeEndLoop(index) is int startIndex)
                {
                    this.loopsJumps.Add((index, startIndex));
                }
                else
                {
                    throw new Exception("Invalid loop");
                }
            }
        }

        private int? ComputeStartLoop(int startPos)
        {
            int index = startPos + 1;
            int count = 1;

            while (index < this.program.Length)
            {
                Instruction currentInstruction = this.program[index].DecodeInstruction().Item1;

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

        private int? ComputeEndLoop(int startPos)
        {
            int index = startPos + -1;
            int count = 1;

            while (index >= 0)
            {
                Instruction currentInstruction = this.program[index].DecodeInstruction().Item1;

                if (currentInstruction == Instruction.LoopBegin)
                {
                    count--;
                }

                if (currentInstruction == Instruction.LoopEnd)
                {
                    count++;
                }

                // Corresponding start found
                if (count == 0)
                {
                    return index + 1;
                }

                index--;
            }

            return null;
        }

        private void InstructionWrite()
        {
            byte currentByte = this.tape[this.currentTapePosition];
            this.inputOutputSystem.WriteByte(currentByte);
        }

        private void InstructionRead()
        {
            this.tape[this.currentTapePosition] = this.inputOutputSystem.ReadByte();
        }

        private void InstructionMove(InstructionParameter instructionParameter)
        {
            this.currentTapePosition += instructionParameter == InstructionParameter.Up ? 1 : -1;
            this.currentTapePosition = this.currentTapePosition.Wrap(0, Constants.TapeLength);
        }

        private void InstructionLoopBegin()
        {
            if (this.tape[this.currentTapePosition] == 0)
            {
                this.currentProgramPosition = this.loopsJumps.Where(elt => elt.Item1 == this.currentProgramPosition - 1).First().Item2;
            }
        }

        private void InstructionLoopEnd()
        {
            if (this.tape[this.currentTapePosition] != 0)
            {
                this.currentProgramPosition = this.loopsJumps.Where(elt => elt.Item1 == this.currentProgramPosition - 1).First().Item2;
            }
        }

        private void InstructionAdd(InstructionParameter instructionParameter)
        {
            if (instructionParameter == InstructionParameter.Up)
            {
                this.tape[this.currentTapePosition] += 1;
            }
            else
            {
                this.tape[this.currentTapePosition] -= 1;
            }
        }
    }
}

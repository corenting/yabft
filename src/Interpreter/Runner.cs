using System;
using System.Collections.Generic;
using System.Linq;
using yabft.Shared;
using static yabft.Shared.Constants;

namespace yabft.Interpreter
{
    class Runner
    {
        private readonly string _program;
        private int _currentProgramPosition;
        private int _currentTapePosition;
        private byte[] _tape;
        private List<(int, int)> _loopsJumps;

        public Runner(String program)
        {
            _program = program;
            _currentProgramPosition = 0;
            _currentTapePosition = 0;
            _tape = new byte[Shared.Constants.TapeLength];

            _loopsJumps = new List<(int, int)>();
            ComputeJumps();
            return;
        }

        private void ComputeJumps()
        {
            for (int index = 0; index < _program.Length; index++)
            {
                char currentChar = _program[index];
                if (currentChar != '[' && currentChar != ']')
                {
                    continue;
                }

                if (currentChar == '[' && ComputeStartLoop(index) is int endIndex)
                {
                    _loopsJumps.Add((index, endIndex));
                }
                else if (currentChar == ']' && ComputeEndLoop(index) is int startIndex)
                {
                    _loopsJumps.Add((index, startIndex));
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

            while (index < _program.Length)
            {
                Instruction currentInstruction = _program[index].DecodeInstruction().Item1;

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
                Instruction currentInstruction = _program[index].DecodeInstruction().Item1;

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

        public void Run()
        {
            do
            {
                (Instruction instruction, InstructionParameter? instructionParameter) =
                    _program[_currentProgramPosition++].DecodeInstruction();

                //Console.WriteLine("Pos: " + (_currentProgramPosition - 1).ToString());

                switch (instruction)
                {
                    case Instruction.Add:
                        InstructionAdd(instructionParameter.Value);
                        break;
                    case Instruction.LoopBegin:
                        InstructionLoopBegin();
                        break;
                    case Instruction.LoopEnd:
                        InstructionLoopEnd();
                        break;
                    case Instruction.Move:
                        InstructionMove(instructionParameter.Value);
                        break;
                    case Instruction.Read:
                        InstructionRead();
                        break;
                    case Instruction.Write:
                        InstructionWrite();
                        break;
                    default:
                        continue;
                }

            } while (_currentProgramPosition < _program.Length);
        }

        private void InstructionWrite()
        {
            Console.Write(Convert.ToChar(_tape[_currentTapePosition]));
        }

        private void InstructionRead()
        {
            char inputChar = Console.ReadKey().KeyChar;
            _tape[_currentTapePosition] = Convert.ToByte(inputChar);
        }

        private void InstructionMove(InstructionParameter instructionParameter)
        {
            _currentTapePosition += (instructionParameter == InstructionParameter.Up ? 1 : -1);
            _currentTapePosition = _currentTapePosition.Wrap(0, Constants.TapeLength);
        }

        private void InstructionLoopBegin()
        {
            if (_tape[_currentTapePosition] == 0)
            {
                _currentProgramPosition = _loopsJumps.Where(elt => elt.Item1 == _currentProgramPosition - 1).First().Item2;
            }
        }

        private void InstructionLoopEnd()
        {
            if (_tape[_currentTapePosition] != 0)
            {
                _currentProgramPosition = _loopsJumps.Where(elt => elt.Item1 == _currentProgramPosition - 1).First().Item2;
            }
        }

        private void InstructionAdd(InstructionParameter instructionParameter)
        {
            if (instructionParameter == InstructionParameter.Up)
            {
                _tape[_currentTapePosition] += 1;
            }
            else
            {
                _tape[_currentTapePosition] -= 1;
            }
        }
    }
}

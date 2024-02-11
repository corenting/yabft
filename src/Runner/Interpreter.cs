namespace Yabft.Runner;

using System;
using System.Collections.Generic;
using Yabft.Common;
using static Yabft.Common.Constants;

internal class Interpreter : AbstractRunner
{
    private readonly Dictionary<int, int> loopsJumps;

    public Interpreter(RunnerOptions options, string program)
        : base(options, program)
    {
        this.loopsJumps = [];
        this.ComputeJumps();
    }

    public override void Run()
    {
        do
        {
            var instruction = this.Program[this.CurrentProgramPosition++];
            switch (instruction.Type)
            {
                case InstructionType.Add:
                    this.InstructionAdd(instruction);
                    break;
                case InstructionType.Substract:
                    this.InstructionSubstract(instruction);
                    break;
                case InstructionType.LoopBegin:
                    this.InstructionLoopBegin();
                    break;
                case InstructionType.LoopEnd:
                    this.InstructionLoopEnd();
                    break;
                case InstructionType.MoveLeft:
                    this.InstructionMoveLeft(instruction);
                    break;
                case InstructionType.MoveRight:
                    this.InstructionMoveRight(instruction);
                    break;
                case InstructionType.Read:
                    this.InstructionRead();
                    break;
                case InstructionType.Write:
                    this.InstructionWrite();
                    break;
                case InstructionType.Nop:
                    break;
                default:
                    continue;
            }
        }
        while (this.CurrentProgramPosition < this.Program.Length);
        this.InputOutputSystem.OnStop();
    }

    private void ComputeJumps()
    {
        var stack = new Stack<int>();
        for (var i = 0; i < this.Program.Length; i++)
        {
            var instruction = this.Program[i];
            if (instruction.Type == InstructionType.LoopBegin)
            {
                stack.Push(i);
            }
            else if (instruction.Type == InstructionType.LoopEnd)
            {
                var startPos = stack.Pop();
                this.loopsJumps.Add(startPos, i);
                this.loopsJumps.Add(i, startPos);
            }
        }
    }

    private void InstructionWrite()
    {
        var currentByte = this.Tape[this.CurrentTapePosition];
        this.InputOutputSystem.WriteByte(currentByte);
    }

    private void InstructionRead() => this.Tape[this.CurrentTapePosition] = this.InputOutputSystem.ReadByte();

    private void InstructionMoveLeft(Instruction instruction)
    {
        this.CurrentTapePosition -= Convert.ToByte(instruction.Amount);
        this.WrapCurrentPosition();
    }

    private void InstructionMoveRight(Instruction instruction)
    {
        this.CurrentTapePosition += Convert.ToByte(instruction.Amount);
        this.WrapCurrentPosition();
    }

    private void WrapCurrentPosition()
    {
        if (this.Wrap)
        {
            this.CurrentTapePosition = this.CurrentTapePosition.Wrap(0, TapeLength);
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

    private void InstructionAdd(Instruction instruction) => this.Tape[this.CurrentTapePosition] += Convert.ToByte(instruction.Amount);

    private void InstructionSubstract(Instruction instruction) => this.Tape[this.CurrentTapePosition] -= Convert.ToByte(instruction.Amount);
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using Liquip.WASM.VM;

namespace Liquip.WASM.Instruction;

public class Instruction
{
    public static bool Optimizer = false; // The optimizer rewrites instructions to a more optimal form
    public int index;
    public Instruction Next;
    public uint offset;
    public byte opCode;
    public uint Pointer;
    public int Pos;
    public byte type;

    /// <summary>
    /// Base Instruction
    /// </summary>
    /// <param name="parser"></param>
    /// <param name="implemented"></param>
    /// <exception cref="Exception"></exception>
    public Instruction(Parser parser, bool implemented = false)
    {
        if (!implemented)
        {
            throw new Exception("Instruction not implemmented: " + this);
        }
    }

    public virtual void End(Instruction end)
    {
        // throw new Exception("End not implementedin " + this);
    }

    public static Inst[] Consume(Parser parser, bool debug)
    {
        var controlFlowStack = new Stack<Instruction>();
        Instruction start = null;
        Instruction last = null;

        var done = false;

        var pos = 0;
        while (!done)
        {
            Instruction current = null;
            var pointer = parser.GetPointer();
            var code = parser.GetByte();

            switch (code)
            {
                /* Control Instructions */

                case 0x00: // unreachable
                    current = new Unreachable(parser);
                    break;
                case 0x01: // nop
                    current = new Nop(parser);
                    break;
                case 0x02: // block
                    current = new Block(parser);
                    controlFlowStack.Push(current);
                    break;
                case 0x03: // loop
                    current = new Loop(parser);
                    controlFlowStack.Push(current);
                    break;
                case 0x04: // if
                    current = new If(parser);
                    controlFlowStack.Push(current);
                    break;
                case 0x05: // else
                {
                    if (controlFlowStack.Count == 0)
                    {
                        throw new Exception("Else with no matching if.");
                    }

                    current = new Else(parser);
                    var match = controlFlowStack.Pop();
                    match.End(current); // notify of else
                    controlFlowStack.Push(current); // add back to find end
                    break;
                }
                case 0x0B: // end
                    if (controlFlowStack.Count == 0)
                    {
                        current = new End(parser);
                        done = true;
                        break;
                    }

                {
                    current = new End(parser);
                    var match = controlFlowStack.Pop();

                    match.End(current); // notify of end
                    break;
                }
                case 0x0C: // br
                    current = new Br(parser);
                    break;
                case 0x0D: // br_if
                    current = new BrIf(parser);
                    break;
                case 0x0E: // br_table
                    current = new BrTable(parser);
                    break;
                case 0x0F: // return
                    current = new Return(parser);
                    break;
                case 0x10: // call
                    current = new Call(parser);
                    break;
                case 0x11: // call_indirect
                    current = new CallIndirect(parser);
                    break;

                /* Parametric Instructions */

                case 0x1A: // drop
                    current = new Drop(parser);
                    break;
                case 0x1B: // select
                    current = new Select(parser);
                    break;

                /* Variable Instructions */

                case 0x20: // local.get
                    current = new LocalGet(parser);
                    break;
                case 0x21: // local.set
                    current = new LocalSet(parser);
                    break;
                case 0x22: // local.tee
                    current = new LocalTee(parser);
                    break;
                case 0x23: // global.get
                    current = new GlobalGet(parser);
                    break;
                case 0x24: // global.set
                    current = new GlobalSet(parser);
                    break;

                /* Memory Instructions */

                case 0x28: // i32.load
                    current = new I32load(parser);
                    break;
                case 0x29: // i64.load
                    current = new I64load(parser);
                    break;
                case 0x2A: // f32.load
                    current = new F32load(parser);
                    break;
                case 0x2B: // f64.load
                    current = new F64load(parser);
                    break;
                case 0x2C: // i32.load8_s
                    current = new I32load8s(parser);
                    break;
                case 0x2D: // i32.load8_u
                    current = new I32load8u(parser);
                    break;
                case 0x2E: // i32.load16_s
                    current = new I32load16s(parser);
                    break;
                case 0x2F: // i32.load16_u
                    current = new I32load16u(parser);
                    break;
                case 0x30: // i64.load8_s
                    current = new I64load8s(parser);
                    break;
                case 0x31: // i64.load8_u
                    current = new I64load8u(parser);
                    break;
                case 0x32: // i64.load16_s
                    current = new I64load16s(parser);
                    break;
                case 0x33: // i64.load16_u
                    current = new I64load16u(parser);
                    break;
                case 0x34: // i64.load32_s
                    current = new I64load32s(parser);
                    break;
                case 0x35: // i64.load32_u
                    current = new I64load32u(parser);
                    break;
                case 0x36: // i32.store
                    current = new I32store(parser);
                    break;
                case 0x37: // i64.store
                    current = new I64store(parser);
                    break;
                case 0x38: // f32.store
                    current = new F32store(parser);
                    break;
                case 0x39: // f64.store
                    current = new F64store(parser);
                    break;
                case 0x3A: // i32.store8
                    current = new I32store8(parser);
                    break;
                case 0x3B: // i32.store16
                    current = new I32store16(parser);
                    break;
                case 0x3C: // i64.store8
                    current = new I64store8(parser);
                    break;
                case 0x3D: // i64.store16
                    current = new I64store16(parser);
                    break;
                case 0x3E: // i64.store32
                    current = new I64store32(parser);
                    break;
                case 0x3F: // memory.size
                    current = new MemorySize(parser);
                    break;
                case 0x40: // memory.grow
                    current = new MemoryGrow(parser);
                    break;

                /* Numeric Instructions */

                case 0x41: // i32.const
                    current = new I32const(parser);
                    break;
                case 0x42: // i64.const
                    current = new I64const(parser);
                    break;
                case 0x43: // f32.const
                    current = new F32const(parser);
                    break;
                case 0x44: // f64.const
                    current = new F64const(parser);
                    break;

                case 0x45: // i32.eqz
                    current = new I32eqz(parser);
                    break;
                case 0x46: // i32.eq
                    current = new I32eq(parser);
                    break;
                case 0x47: // i32.ne
                    current = new I32ne(parser);
                    break;
                case 0x48: // i32.lt_s
                    current = new I32lts(parser);
                    break;
                case 0x49: // i32.lt_u
                    current = new I32ltu(parser);
                    break;
                case 0x4A: // i32.gt_s
                    current = new I32gts(parser);
                    break;
                case 0x4B: // i32.gt_u
                    current = new I32gtu(parser);
                    break;
                case 0x4C: // i32.le_s
                    current = new I32les(parser);
                    break;
                case 0x4D: // i32.le_u
                    current = new I32leu(parser);
                    break;
                case 0x4E: // i32.ge_s
                    current = new I32ges(parser);
                    break;
                case 0x4F: // i32.ge_u
                    current = new I32geu(parser);
                    break;

                case 0x50: // i64.eqz
                    current = new I64eqz(parser);
                    break;
                case 0x51: // i64.eq
                    current = new I64eq(parser);
                    break;
                case 0x52: // i64.ne
                    current = new I64ne(parser);
                    break;
                case 0x53: // i64.lt_s
                    current = new I64lts(parser);
                    break;
                case 0x54: // i64.lt_u
                    current = new I64ltu(parser);
                    break;
                case 0x55: // i64.gt_s
                    current = new I64gts(parser);
                    break;
                case 0x56: // i64.gt_u
                    current = new I64gtu(parser);
                    break;
                case 0x57: // i64.le_s
                    current = new I64les(parser);
                    break;
                case 0x58: // i64.le_u
                    current = new I64leu(parser);
                    break;
                case 0x59: // i64.ge_s
                    current = new I64ges(parser);
                    break;
                case 0x5A: // i64.ge_u
                    current = new I64geu(parser);
                    break;

                case 0x5B: // f32.eq
                    current = new F32eq(parser);
                    break;
                case 0x5C: // f32.ne
                    current = new F32ne(parser);
                    break;
                case 0x5D: // f32.lt
                    current = new F32lt(parser);
                    break;
                case 0x5E: // f32.gt
                    current = new F32gt(parser);
                    break;
                case 0x5F: // f32.le
                    current = new F32le(parser);
                    break;
                case 0x60: // f32.ge
                    current = new F32ge(parser);
                    break;

                case 0x61: // f64.eq
                    current = new F64eq(parser);
                    break;
                case 0x62: // f64.ne
                    current = new F64ne(parser);
                    break;
                case 0x63: // f64.lt
                    current = new F64lt(parser);
                    break;
                case 0x64: // f64.gt
                    current = new F64gt(parser);
                    break;
                case 0x65: // f64.le
                    current = new F64le(parser);
                    break;
                case 0x66: // f64.ge
                    current = new F64ge(parser);
                    break;

                case 0x67: // i32.clz
                    current = new I32clz(parser);
                    break;
                case 0x68: // i32.ctz
                    current = new I32ctz(parser);
                    break;
                case 0x69: // i32.popcnt
                    current = new I32popcnt(parser);
                    break;
                case 0x6A: // i32.add
                    current = new I32add(parser);
                    break;
                case 0x6B: // i32.sub
                    current = new I32sub(parser);
                    break;
                case 0x6C: // i32.mul
                    current = new I32mul(parser);
                    break;
                case 0x6D: // i32.div_s
                    current = new I32divs(parser);
                    break;
                case 0x6E: // i32.div_u
                    current = new I32divu(parser);
                    break;
                case 0x6F: // i32.rem_s
                    current = new I32rems(parser);
                    break;
                case 0x70: // i32.rem_u
                    current = new I32remu(parser);
                    break;
                case 0x71: // i32.and
                    current = new I32and(parser);
                    break;
                case 0x72: // i32.or
                    current = new I32or(parser);
                    break;
                case 0x73: // i32.xor
                    current = new I32xor(parser);
                    break;
                case 0x74: // i32.shl
                    current = new I32shl(parser);
                    break;
                case 0x75: // i32.shr_s
                    current = new I32shrs(parser);
                    break;
                case 0x76: // i32.shr_u
                    current = new I32shru(parser);
                    break;
                case 0x77: // i32.rotl
                    current = new I32rotl(parser);
                    break;
                case 0x78: // i32.rotr
                    current = new I32rotr(parser);
                    break;

                case 0x79: // i64.clz
                    current = new I64clz(parser);
                    break;
                case 0x7A: // i64.ctz
                    current = new I64ctz(parser);
                    break;
                case 0x7B: // i64.popcnt
                    current = new I64popcnt(parser);
                    break;
                case 0x7C: // i64.add
                    current = new I64add(parser);
                    break;
                case 0x7D: // i64.sub
                    current = new I64sub(parser);
                    break;
                case 0x7E: // i64.mul
                    current = new I64mul(parser);
                    break;
                case 0x7F: // i64.div_s
                    current = new I64divs(parser);
                    break;
                case 0x80: // i64.div_u
                    current = new I64divu(parser);
                    break;
                case 0x81: // i64.rem_s
                    current = new I64rems(parser);
                    break;
                case 0x82: // i64.rem_u
                    current = new I64remu(parser);
                    break;
                case 0x83: // i64.and
                    current = new I64and(parser);
                    break;
                case 0x84: // i64.or
                    current = new I64or(parser);
                    break;
                case 0x85: // i64.xor
                    current = new I64xor(parser);
                    break;
                case 0x86: // i64.shl
                    current = new I64shl(parser);
                    break;
                case 0x87: // i64.shr_s
                    current = new I64shrs(parser);
                    break;
                case 0x88: // i64.shr_u
                    current = new I64shru(parser);
                    break;
                case 0x89: // i64.rotl
                    current = new I64rotl(parser);
                    break;
                case 0x8A: // i64.rotr
                    current = new I64rotr(parser);
                    break;

                case 0x8B: // f32.abs
                    current = new F32abs(parser);
                    break;
                case 0x8C: // f32.neg
                    current = new F32neg(parser);
                    break;
                case 0x8D: // f32.ceil
                    current = new F32ceil(parser);
                    break;
                case 0x8E: // f32.floor
                    current = new F32floor(parser);
                    break;
                case 0x8F: // f32.trunc
                    current = new F32trunc(parser);
                    break;
                case 0x90: // f32.nearest
                    current = new F32nearest(parser);
                    break;
                case 0x91: // f32.sqrt
                    current = new F32sqrt(parser);
                    break;
                case 0x92: // f32.add
                    current = new F32add(parser);
                    break;
                case 0x93: // f32.sub
                    current = new F32sub(parser);
                    break;
                case 0x94: // f32.mul
                    current = new F32mul(parser);
                    break;
                case 0x95: // f32.div
                    current = new F32div(parser);
                    break;
                case 0x96: // f32.min
                    current = new F32min(parser);
                    break;
                case 0x97: // f32.max
                    current = new F32max(parser);
                    break;
                case 0x98: // f32.copysign
                    current = new F32copysign(parser);
                    break;

                case 0x99: // f64.abs
                    current = new F64abs(parser);
                    break;
                case 0x9A: // f64.neg
                    current = new F64neg(parser);
                    break;
                case 0x9B: // f64.ceil
                    current = new F64ceil(parser);
                    break;
                case 0x9C: // f64.floor
                    current = new F64floor(parser);
                    break;
                case 0x9D: // f64.trunc
                    current = new F64trunc(parser);
                    break;
                case 0x9E: // f64.nearest
                    current = new F64nearest(parser);
                    break;
                case 0x9F: // f64.sqrt
                    current = new F64sqrt(parser);
                    break;
                case 0xA0: // f64.add
                    current = new F64add(parser);
                    break;
                case 0xA1: // f64.sub
                    current = new F64sub(parser);
                    break;
                case 0xA2: // f64.mul
                    current = new F64mul(parser);
                    break;
                case 0xA3: // f64.div
                    current = new F64div(parser);
                    break;
                case 0xA4: // f64.min
                    current = new F64min(parser);
                    break;
                case 0xA5: // f64.max
                    current = new F64max(parser);
                    break;
                case 0xA6: // f64.copysign
                    current = new F64copysign(parser);
                    break;

                case 0xA7: // i32.wrap_i64
                    current = new I32wrapI64(parser);
                    break;
                case 0xA8: // i32.trunc_f32_s
                    current = new I32truncF32s(parser);
                    break;
                case 0xA9: // i32.trunc_f32_u
                    current = new I32truncF32u(parser);
                    break;
                case 0xAA: // i32.trunc_f64_s
                    current = new I32truncF64s(parser);
                    break;
                case 0xAB: // i32.trunc_f64_u
                    current = new I32truncF64u(parser);
                    break;
                case 0xAC: // i64.extend_i32_s
                    current = new I64extendI32s(parser);
                    break;
                case 0xAD: // i64.extend_i32_u
                    current = new I64extendI32u(parser);
                    break;
                case 0xAE: // i64.trunc_f32_s
                    current = new I64truncF32s(parser);
                    break;
                case 0xAF: // i64.trunc_f32_u
                    current = new I64truncF32u(parser);
                    break;
                case 0xB0: // i64.trunc_f64_s
                    current = new I64truncF64s(parser);
                    break;
                case 0xB1: // i64.trunc_f64_u
                    current = new I64truncF64u(parser);
                    break;
                case 0xB2: // f32.convert_i32_s
                    current = new F32convertI32s(parser);
                    break;
                case 0xB3: // f32.convert_i32_u
                    current = new F32convertI32u(parser);
                    break;
                case 0xB4: // f32.convert_i64_s
                    current = new F32convertI64s(parser);
                    break;
                case 0xB5: // f32.convert_i64_u
                    current = new F32convertI64u(parser);
                    break;
                case 0xB6: // f32.demote_f64
                    current = new F32demoteF64(parser);
                    break;
                case 0xB7: // f64.convert_i32_s
                    current = new F64convertI32s(parser);
                    break;
                case 0xB8: // f64.convert_i32_u
                    current = new F64convertI32u(parser);
                    break;
                case 0xB9: // f64.convert_i64_s
                    current = new F64convertI64s(parser);
                    break;
                case 0xBA: // f64.convert_i64.u
                    current = new F64convertI64u(parser);
                    break;
                case 0xBB: // f64.promote_f32
                    current = new F64promoteF32(parser);
                    break;
                case 0xBC: // i32.reinterpret_f32
                    current = new I32reinterpretF32(parser);
                    break;
                case 0xBD: // i64.reinterpret_i32
                    current = new I64reinterpretI32(parser);
                    break;
                case 0xBE: // f32.reinterpret_i32
                    current = new F32reinterpretI32(parser);
                    break;
                case 0xBF: // f64.reinterpret_i64
                    current = new F64reinterpretI64(parser);
                    break;
                default:
                    throw new Exception("Opcode not implemented: 0x" + code.ToString("X"));
            }

            current.opCode = code;
            current.Pos = pos;
            pos++;
            if (start == null)
            {
                start = current;
            }

            if (current != null)
            {
                current.Pointer = pointer;
                if (debug)
                {
                    Console.WriteLine(current);
                    Console.ReadKey();
                }

                if (last != null && !done && last.Next == null)
                {
                    last.Next = current;
                }

                last = current;
            }
        }

        /*** INITIALIZE NEW PROGRAM ***/
        var program = new List<Inst>();
        var i = new Inst();
        var unreachable = new Inst();
        unreachable.opCode = 0x00;
        for (var inst = start; inst != null; inst = inst.Next)
        {
            i.opCode = inst.opCode;
            i.pointer = inst.Pointer;
            i.i = inst;
            var two = (uint)((inst.opCode << 8) + (inst.Next == null ? 0x00 : inst.Next.opCode));
            var three =
                (uint)((two << 8) + (inst.Next == null || inst.Next.Next == null ? 0x00 : inst.Next.Next.opCode));
            var four = (uint)((three << 8) + (inst.Next == null || inst.Next.Next == null || inst.Next.Next.Next == null
                ? 0x00
                : inst.Next.Next.Next.opCode));

            if (Optimizer)
            {
                switch (four)
                {
                    case 0x20204621: // local.local.i32.eq.local
                    case 0x20204721: // local.local.i32.ne.local
                    case 0x20204821: // local.local.i32.lt_s.local
                    case 0x20204921: // local.local.i32.lt_u.local
                    case 0x20204A21: // local.local.i32.gt_s.local
                    case 0x20204B21: // local.local.i32.gt_u.local
                    case 0x20206A21: // local.local.i32.add.local
                    case 0x20206B21: // local.local.i32.sub.local
                    case 0x20206C21: // local.local.i32.mul.local
                    case 0x20206D21: // local.local.i32.div_s.local
                    case 0x20206E21: // local.local.i32.div_u.local
                    case 0x20206F21: // local.local.i32.rem_s.local
                    case 0x20207021: // local.local.i32.rem_u.local
                    case 0x20207121: // local.local.i32.and.local
                    case 0x20207221: // local.local.i32.or.local
                    case 0x20207321: // local.local.i32.xor.local
                    case 0x20207421: // local.local.i32.shl.local
                    case 0x20207521: // local.local.i32.shr_s.local
                    case 0x20207621: // local.local.i32.shr_u.local
                    case 0x20207721: // local.local.i32.rotl.local
                    case 0x20207821: // local.local.i32.rotr.local

                    case 0x20207C21: // local.local.i64.add.local
                    case 0x20207D21: // local.local.i64.sub.local
                    case 0x20207E21: // local.local.i64.mul.local
                    case 0x20207F21: // local.local.i64.div_s.local
                    case 0x20208021: // local.local.i64.div_u.local
                    case 0x20208121: // local.local.i64.rem_s.local
                    case 0x20208221: // local.local.i64.rem_u.local
                    case 0x20208321: // local.local.i64.and.local
                    case 0x20208421: // local.local.i64.or.local
                    case 0x20208521: // local.local.i64.xor.local
                    case 0x20208621: // local.local.i64.shl.local
                    case 0x20208721: // local.local.i64.shr_s.local
                    case 0x20208821: // local.local.i64.shr_u.local
                    case 0x20208921: // local.local.i64.rotl.local
                    case 0x20208A21: // local.local.i64.rotr.local
                        i.opCode = four;
                        i.a = (inst as LocalGet).index;
                        i.b = (inst.Next as LocalGet).index;
                        i.c = (inst.Next.Next.Next as LocalSet).index;
                        program.Add(i);
                        program.Add(unreachable);
                        program.Add(unreachable);
                        program.Add(unreachable);
                        inst = inst.Next.Next.Next;
                        continue;
                }

                switch (three)
                {
                    case 0x202036: // local.local.i32.store
                    case 0x202037: // local.local.i64.store
                    case 0x202038: // local.local.f32.store
                    case 0x202039: // local.local.f64.store

                    case 0x20203A: // local.local.i32.store8
                    case 0x20203B: // local.local.i32.store16
                        i.opCode = three;
                        i.a = inst.index;
                        i.b = inst.Next.index;
                        i.pos64 = inst.Next.Next.offset;
                        program.Add(i);
                        program.Add(unreachable);
                        program.Add(unreachable);
                        inst = inst.Next.Next;
                        continue;
                    case 0x202046: // local.local.i32.eq
                    case 0x202047: // local.local.i32.ne
                    case 0x202048: // local.local.i32.lt_s
                    case 0x202049: // local.local.i32.lt_u
                    case 0x20204A: // local.local.i32.gt_s
                    case 0x20204B: // local.local.i32.gt_u

                    case 0x20206A: // local.local.i32.add
                    case 0x20206B: // local.local.i32.sub
                    case 0x20206C: // local.local.i32.mul
                    case 0x20206D: // local.local.i32.div_s
                    case 0x20206E: // local.local.i32.div_u
                    case 0x20206F: // local.local.i32.rem_s
                    case 0x202070: // local.local.i32.rem_u
                    case 0x202071: // local.local.i32.and
                    case 0x202072: // local.local.i32.or
                    case 0x202073: // local.local.i32.xor
                    case 0x202074: // local.local.i32.shl
                    case 0x202075: // local.local.i32.shr_s
                    case 0x202076: // local.local.i32.shr_u

                    case 0x2020A0: // local.local.f64.add
                    case 0x2020A1: // local.local.f64.sub
                    case 0x2020A2: // local.local.f64.mul
                    case 0x2020A3: // local.local.f64.div
                    case 0x2020A4: // local.local.f64.min
                    case 0x2020A5: // local.local.f64.max

                        i.opCode = three;
                        i.a = (inst as LocalGet).index;
                        i.b = (inst.Next as LocalGet).index;
                        program.Add(i);
                        program.Add(unreachable);
                        program.Add(unreachable);
                        inst = inst.Next.Next;
                        continue;
                    case 0x202821: // local.i32.load.local
                    case 0x202921: // local.i32.load.local

                    case 0x202B21: // local.f64.load.local

                    case 0x202C21: // local.i32.load8_s.local
                    case 0x202D21: // local.i32.load8_u.local
                    case 0x202E21: // local.i32.load16_s.local
                    case 0x202F21: // local.i32.load16_u.local
                        i.opCode = three;
                        i.a = inst.index;
                        i.pos64 = inst.Next.offset;
                        i.b = inst.Next.Next.index;
                        inst = inst.Next.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        program.Add(unreachable);
                        continue;

                    case 0x209921: // local.f64.abs
                    case 0x209A21: // local.f64.neg
                    case 0x209B21: // local.f64.ceil
                    case 0x209C21: // local.f64.floor
                    case 0x209D21: // local.f64.trunc
                    case 0x209E21: // local.f64.nearest
                    case 0x209F21: // local.f64.sqrt
                    case 0x20A021: // local.f64.add
                    case 0x20A121: // local.f64.sub
                    case 0x20A221: // local.f64.mul
                    case 0x20A321: // local.f64.div
                    case 0x20A421: // local.f64.min
                    case 0x20A521: // local.f64.max
                    case 0x20A621: // local.f64.copysign

                    case 0x20B721: // local.f64.convert_i32_s.local
                    case 0x20B821: // local.f64.convert_i32_u.local
                        i.opCode = three;
                        i.a = inst.index;
                        i.b = inst.Next.Next.index;
                        inst = inst.Next.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        program.Add(unreachable);
                        continue;
                }

                switch (two)
                {
                    case 0x200D: // local.br_if
                        i.opCode = two;
                        i.a = (inst as LocalGet).index;
                        i.pos = (int)(inst.Next as BrIf).labelidx + 1;
                        inst = inst.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        continue;
                    case 0x2021: // local.copy
                        i.opCode = two;
                        i.a = (inst as LocalGet).index;
                        i.b = (inst.Next as LocalSet).index;
                        inst = inst.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        continue;

                    case 0x2028: // local.i32.load
                    case 0x2029: // local.i64.load
                    case 0x202A: // local.f32.load
                    case 0x202B: // local.f64.load

                    case 0x202C: // local.i32.load8_s
                    case 0x202D: // local.i32.load8_u
                    case 0x202E: // local.i32.load16_s
                    case 0x202F: // local.i32.load16_u

                    case 0x2036: // local.i32.store
                    case 0x2037: // local.i64.store
                    case 0x2038: // local.f32.store
                    case 0x2039: // local.f64.store

                    case 0x203A: // local.i32.store8
                    case 0x203B: // local.i32.store16
                        i.opCode = two;
                        i.a = inst.index;
                        i.pos64 = inst.Next.offset;
                        program.Add(i);
                        program.Add(unreachable);
                        inst = inst.Next;
                        continue;

                    // local.get *

                    case 0x2045: // local.i32.eqz
                    case 0x2046: // local.i32.eq
                    case 0x2047: // local.i32.ne
                    case 0x2048: // local.i32.lt_s
                    case 0x2049: // local.i32.lt_u
                    case 0x204A: // local.i32.gt_s
                    case 0x204B: // local.i32.gt_u

                    case 0x206A: // local.i32.add
                    case 0x206B: // local.i32.sub
                    case 0x206C: // local.i32.mul
                    case 0x206D: // local.i32.div_s
                    case 0x206E: // local.i32.div_u
                    case 0x206F: // local.i32.rem_s
                    case 0x2070: // local.i32.rem_u
                    case 0x2071: // local.i32.and

                    case 0x2074: //local.i32.shl
                    case 0x2075: //local.i32.shr_s
                    case 0x2076: //local.i32.shr_u
                        i.opCode = two;
                        i.a = (inst as LocalGet).index;
                        inst = inst.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        continue;

                    case 0x2099: // local.f64.abs
                    case 0x209A: // local.f64.neg
                    case 0x209B: // local.f64.ceil
                    case 0x209C: // local.f64.floor
                    case 0x209D: // local.f64.trunc
                    case 0x209E: // local.f64.nearest
                    case 0x209F: // local.f64.sqrt
                    case 0x20A0: // local.f64.add
                    case 0x20A1: // local.f64.sub
                    case 0x20A2: // local.f64.mul
                    case 0x20A3: // local.f64.div
                    case 0x20A4: // local.f64.min
                    case 0x20A5: // local.f64.max
                    case 0x20A6: // local.f64.copysign

                    case 0x20B7: // local.f64.convert_i32_s
                    case 0x20B8: // local.f64.convert_i32_u
                        i.opCode = two;
                        i.a = (inst as LocalGet).index;
                        inst = inst.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        continue;

                    case 0xB721: // f64.convert_i32_s.local
                    case 0xB821: // f64.convert_i32_u.local
                        i.opCode = two;
                        i.a = (inst.Next as LocalSet).index;
                        inst = inst.Next;
                        program.Add(i);
                        program.Add(unreachable);
                        continue;

                    // const *

                    case 0x4121: // i32.const.local
                        i.opCode = 0x4121;
                        i.i32 = (inst as I32const).value;
                        i.a = (inst.Next as LocalSet).index;
                        program.Add(i);
                        program.Add(unreachable);
                        inst = inst.Next;
                        continue;
                    case 0x4221: // i64.const.local
                        i.opCode = 0x4221;
                        i.i64 = (inst as I64const).value;
                        i.a = (inst.Next as LocalSet).index;
                        program.Add(i);
                        program.Add(unreachable);
                        inst = inst.Next;
                        continue;
                }
            }

            switch (inst.opCode)
            {
                case 0x00: // unreachable
                case 0x01: // nop
                    program.Add(i);
                    break;
                case 0x02: // block
                    i.pos = (inst as Block).label.Pos;
                    program.Add(i);
                    break;
                case 0x03: // loop
                    program.Add(i);
                    break;
                case 0x04: // if
                    i.pos = (inst as If).label.Pos;
                    program.Add(i);
                    break;
                case 0x05: // else
                    i.pos = (inst as Else).label.Pos - 1;
                    program.Add(i);
                    break;
                case 0x0B: // end
                    program.Add(i);
                    break;
                case 0x0C: // br
                    i.pos = (int)(inst as Br).labelidx + 1;
                    program.Add(i);
                    break;
                case 0x0D: // br_if
                    i.pos = (int)(inst as BrIf).labelidx + 1;
                    program.Add(i);
                    break;
                case 0x0E: // br_table
                    i.pos = (inst as BrTable).defaultLabelidx;
                    i.table = (inst as BrTable).table;
                    program.Add(i);
                    break;
                case 0x0F: // return
                    program.Add(i);
                    break;
                case 0x10: // call
                    i.pos = (inst as Call).funcidx;
                    program.Add(i);
                    break;
                case 0x11: // call_indirect
                    i.pos = (inst as CallIndirect).tableidx;
                    program.Add(i);
                    break;

                /* Parametric Instructions */

                case 0x1A: // drop
                case 0x1B: // select
                    program.Add(i);
                    break;

                /* Variable Instructions */

                case 0x20: // local.get
                case 0x21: // local.set
                case 0x22: // local.tee
                case 0x23: // global.get
                case 0x24: // global.set
                    i.a = inst.index;
                    program.Add(i);
                    break;

                /* Memory Instructions */

                case 0x28: // i32.load
                case 0x29: // i64.load
                case 0x2A: // f32.load
                case 0x2B: // f64.load
                case 0x2C: // i32.load8_s
                case 0x2D: // i32.load8_u
                case 0x2E: // i32.load16_s
                case 0x2F: // i32.load16_u
                case 0x30: // i64.load8_s
                case 0x31: // i64.load8_u
                case 0x32: // i64.load16_s
                case 0x33: // i64.load16_u
                case 0x34: // i64.load32_s
                case 0x35: // i64.load32_u
                case 0x36: // i32.store
                case 0x37: // i64.store
                case 0x38: // f32.store
                case 0x39: // f64.store
                case 0x3A: // i32.store8
                case 0x3B: // i32.store16
                case 0x3C: // i64.store8
                case 0x3D: // i64.store16
                case 0x3E: // i64.store32
                    i.pos64 = inst.offset;
                    program.Add(i);
                    break;

                case 0x3F: // memory.size
                case 0x40: // memory.grow
                    program.Add(i);
                    break;

                /* Numeric Instructions */

                case 0x41: // i32.const
                    i.value.type = Type.i32;
                    i.value.i32 = (inst as I32const).value;
                    program.Add(i);
                    break;
                case 0x42: // i64.const
                    i.value.type = Type.i64;
                    i.value.i64 = (inst as I64const).value;
                    program.Add(i);
                    break;
                case 0x43: // f32.const
                    i.value.type = Type.f32;
                    i.value.f32 = (inst as F32const).value;
                    program.Add(i);
                    break;
                case 0x44: // f64.const
                    i.value.type = Type.f64;
                    i.value.f64 = (inst as F64const).value;
                    program.Add(i);
                    break;

                case 0x45: // i32.eqz
                case 0x46: // i32.eq
                case 0x47: // i32.ne
                case 0x48: // i32.lt_s
                case 0x49: // i32.lt_u
                case 0x4A: // i32.gt_s
                case 0x4B: // i32.gt_u
                case 0x4C: // i32.le_s
                case 0x4D: // i32.le_u
                case 0x4E: // i32.ge_s
                case 0x4F: // i32.ge_u
                case 0x50: // i64.eqz
                case 0x51: // i64.eq
                case 0x52: // i64.ne
                case 0x53: // i64.lt_s
                case 0x54: // i64.lt_u
                case 0x55: // i64.gt_s
                case 0x56: // i64.gt_u
                case 0x57: // i64.le_s
                case 0x58: // i64.le_u
                case 0x59: // i64.ge_s
                case 0x5A: // i64.ge_u
                case 0x5B: // f32.eq
                case 0x5C: // f32.ne
                case 0x5D: // f32.lt
                case 0x5E: // f32.gt
                case 0x5F: // f32.le
                case 0x60: // f32.ge
                case 0x61: // f64.eq
                case 0x62: // f64.ne
                case 0x63: // f64.lt
                case 0x64: // f64.gt
                case 0x65: // f64.le
                case 0x66: // f64.ge
                case 0x67: // i32.clz
                case 0x68: // i32.ctz
                case 0x69: // i32.popcnt
                case 0x6A: // i32.add
                case 0x6B: // i32.sub
                case 0x6C: // i32.mul
                case 0x6D: // i32.div_s
                case 0x6E: // i32.div_u
                case 0x6F: // i32.rem_s
                case 0x70: // i32.rem_u
                case 0x71: // i32.and
                case 0x72: // i32.or
                case 0x73: // i32.xor
                case 0x74: // i32.shl
                case 0x75: // i32.shr_s
                case 0x76: // i32.shr_u
                case 0x77: // i32.rotl
                case 0x78: // i32.rotr
                case 0x79: // i64.clz
                case 0x7A: // i64.ctz
                case 0x7B: // i64.popcnt
                case 0x7C: // i64.add
                case 0x7D: // i64.sub
                case 0x7E: // i64.mul
                case 0x7F: // i64.div_s
                case 0x80: // i64.div_u
                case 0x81: // i64.rem_s
                case 0x82: // i64.rem_u
                case 0x83: // i64.and
                case 0x84: // i64.or
                case 0x85: // i64.xor
                case 0x86: // i64.shl
                case 0x87: // i64.shr_s
                case 0x88: // i64.shr_u
                case 0x89: // i64.rotl
                case 0x8A: // i64.rotr
                case 0x8B: // f32.abs
                case 0x8C: // f32.neg
                case 0x8D: // f32.ceil
                case 0x8E: // f32.floor
                case 0x8F: // f32.trunc
                case 0x90: // f32.nearest
                case 0x91: // f32.sqrt
                case 0x92: // f32.add
                case 0x93: // f32.sub
                case 0x94: // f32.mul
                case 0x95: // f32.div
                case 0x96: // f32.min
                case 0x97: // f32.max
                case 0x98: // f32.copysign
                case 0x99: // f64.abs
                case 0x9A: // f64.neg
                case 0x9B: // f64.ceil
                case 0x9C: // f64.floor
                case 0x9D: // f64.trunc
                case 0x9E: // f64.nearest
                case 0x9F: // f64.sqrt
                case 0xA0: // f64.add
                case 0xA1: // f64.sub
                case 0xA2: // f64.mul
                case 0xA3: // f64.div
                case 0xA4: // f64.min
                case 0xA5: // f64.max
                case 0xA6: // f64.copysign
                case 0xA7: // i32.wrap_i64
                case 0xA8: // i32.trunc_f32_s
                case 0xA9: // i32.trunc_f32_u
                case 0xAA: // i32.trunc_f64_s
                case 0xAB: // i32.trunc_f64_u
                case 0xAC: // i64.extend_i32_s
                case 0xAD: // i64.extend_i32_u
                case 0xAE: // i64.trunc_f32_s
                case 0xAF: // i64.trunc_f32_u
                case 0xB0: // i64.trunc_f64_s
                case 0xB1: // i64.trunc_f64_u
                case 0xB2: // f32.convert_i32_s
                case 0xB3: // f32.convert_i32_u
                case 0xB4: // f32.convert_i64_s
                case 0xB5: // f32.convert_i64_u
                case 0xB6: // f32.demote_f64
                case 0xB7: // f64.convert_i32_s
                case 0xB8: // f64.convert_i32_u
                case 0xB9: // f64.convert_i64_s
                case 0xBA: // f64.convert_i64.u
                case 0xBB: // f64.promote_f32
                case 0xBC: // i32.reinterpret_f32
                case 0xBD: // i64.reinterpret_i32
                case 0xBE: // f32.reinterpret_i32
                case 0xBF: // f64.reinterpret_i64
                    program.Add(i);
                    break;
                default:
                    throw new Exception("Missing implementation of opCode: " + i.opCode.ToString("X"));
            }
        }

        i.opCode = 0x0F;
        i.pointer = (uint)program.Count;
        i.i = new Return(null);

        program.Add(i);

        /******************************/
        if (Optimizer)
        {
            for (var o = 0; o < program.Count; o++)
            {
                if (program[o].opCode == 0x202821)
                {
                    var count = 0;
                    while (o + count < program.Count && program[o + count].opCode == 0x202821)
                    {
                        count += 3;
                    }

                    count /= 3;

                    if (count > 1)
                    {

                        var n = program[o];
                        n.opCode = 0xFF000000;
                        n.optimalProgram = new Inst[count];
                        for (var m = 0; m < count; m++)
                        {
                            n.optimalProgram[m] = program[m * 3 + o];
                            if (m > 0)
                            {
                                var u = new Inst();
                                u.opCode = 0x00;
                                program[m * 3 + o] = u;
                            }
                        }

                        program[o] = n;
                    }
                }

                if (program[o].opCode == 0x4121)
                {
                    var count = 0;
                    while (o + count < program.Count && program[o + count].opCode == 0x4121)
                    {
                        count += 2;
                    }

                    count /= 2;

                    if (count > 1)
                    {
                        var n = program[o];
                        n.opCode = 0xFE000000;
                        n.optimalProgram = new Inst[count];
                        for (var m = 0; m < count; m++)
                        {
                            n.optimalProgram[m] = program[m * 2 + o];
                            if (m > 0)
                            {
                                var u = new Inst();
                                u.opCode = 0x00;
                                program[m * 2 + o] = u;
                            }
                        }

                        program[o] = n;
                    }
                }
            }
        }

        return program.ToArray();
    }

    /// <summary>
    /// gets the sting os the opcode
    /// </summary>
    /// <param name="opCode"></param>
    /// <returns></returns>
    public static string Translate(uint opCode) =>
        opCode switch
        {
            0x00 => "unreachable",
            0x01 => "nop",
            0x02 => "block",
            0x03 => "loop",
            0x04 => "if",
            0x05 => "else",
            0x0B => "end",
            0x0C => "br",
            0x0D => "br_if",
            0x0E => "br_table",
            0x0F => "return",
            0x10 => "call",
            0x11 => "call_indirect",
            0x1A => "drop",
            0x1B => "select",
            0x20 => "local.get",
            0x21 => "local.set",
            0x22 => "local.tee",
            0x23 => "global.get",
            0x24 => "global.set",
            0x28 => "i32.load",
            0x29 => "i64.load",
            0x2A => "f32.load",
            0x2B => "f64.load",
            0x2C => "i32.load8_s",
            0x2D => "i32.load8_u",
            0x2E => "i32.load16_s",
            0x2F => "i32.load16_u",
            0x30 => "i64.load8_s",
            0x31 => "i64.load8_u",
            0x32 => "i64.load16_s",
            0x33 => "i64.load16_u",
            0x34 => "i64.load32_s",
            0x35 => "i64.load32_u",
            0x36 => "i32.store",
            0x37 => "i64.store",
            0x38 => "f32.store",
            0x39 => "f64.store",
            0x3A => "i32.store8",
            0x3B => "i32.store16",
            0x3C => "i64.store8",
            0x3D => "i64.store16",
            0x3E => "i64.store32",
            0x3F => "memory.size",
            0x40 => "memory.grow",
            0x41 => "i32.const",
            0x42 => "i64.const",
            0x43 => "f32.const",
            0x44 => "f64.const",
            0x45 => "i32.eqz",
            0x46 => "i32.eq",
            0x47 => "i32.ne",
            0x48 => "i32.lt_s",
            0x49 => "i32.lt_u",
            0x4A => "i32.gt_s",
            0x4B => "i32.gt_u",
            0x4C => "i32.le_s",
            0x4D => "i32.le_u",
            0x4E => "i32.ge_s",
            0x4F => "i32.ge_u",
            0x50 => "i64.eqz",
            0x51 => "i64.eq",
            0x52 => "i64.ne",
            0x53 => "i64.lt_s",
            0x54 => "i64.lt_u",
            0x55 => "i64.gt_s",
            0x56 => "i64.gt_u",
            0x57 => "i64.le_s",
            0x58 => "i64.le_u",
            0x59 => "i64.ge_s",
            0x5A => "i64.ge_u",
            0x5B => "f32.eq",
            0x5C => "f32.ne",
            0x5D => "f32.lt",
            0x5E => "f32.gt",
            0x5F => "f32.le",
            0x60 => "f32.ge",
            0x61 => "f64.eq",
            0x62 => "f64.ne",
            0x63 => "f64.lt",
            0x64 => "f64.gt",
            0x65 => "f64.le",
            0x66 => "f64.ge",
            0x67 => "i32.clz",
            0x68 => "i32.ctz",
            0x69 => "i32.popcnt",
            0x6A => "i32.add",
            0x6B => "i32.sub",
            0x6C => "i32.mul",
            0x6D => "i32.div_s",
            0x6E => "i32.div_u",
            0x6F => "i32.rem_s",
            0x70 => "i32.rem_u",
            0x71 => "i32.and",
            0x72 => "i32.or",
            0x73 => "i32.xor",
            0x74 => "i32.shl",
            0x75 => "i32.shr_s",
            0x76 => "i32.shr_u",
            0x77 => "i32.rotl",
            0x78 => "i32.rotr",
            0x79 => "i64.clz",
            0x7A => "i64.ctz",
            0x7B => "i64.popcnt",
            0x7C => "i64.add",
            0x7D => "i64.sub",
            0x7E => "i64.mul",
            0x7F => "i64.div_s",
            0x80 => "i64.div_u",
            0x81 => "i64.rem_s",
            0x82 => "i64.rem_u",
            0x83 => "i64.and",
            0x84 => "i64.or",
            0x85 => "i64.xor",
            0x86 => "i64.shl",
            0x87 => "i64.shr_s",
            0x88 => "i64.shr_u",
            0x89 => "i64.rotl",
            0x8A => "i64.rotr",
            0x8B => "f32.abs",
            0x8C => "f32.neg",
            0x8D => "f32.ceil",
            0x8E => "f32.floor",
            0x8F => "f32.trunc",
            0x90 => "f32.nearest",
            0x91 => "f32.sqrt",
            0x92 => "f32.add",
            0x93 => "f32.sub",
            0x94 => "f32.mul",
            0x95 => "f32.div",
            0x96 => "f32.min",
            0x97 => "f32.max",
            0x98 => "f32.copysign",
            0x99 => "f64.abs",
            0x9A => "f64.neg",
            0x9B => "f64.ceil",
            0x9C => "f64.floor",
            0x9D => "f64.trunc",
            0x9E => "f64.nearest",
            0x9F => "f64.sqrt",
            0xA0 => "f64.add",
            0xA1 => "f64.sub",
            0xA2 => "f64.mul",
            0xA3 => "f64.div",
            0xA4 => "f64.min",
            0xA5 => "f64.max",
            0xA6 => "f64.copysign",
            0xA7 => "i32.wrap_i64",
            0xA8 => "i32.trunc_f32_s",
            0xA9 => "i32.trunc_f32_u",
            0xAA => "i32.trunc_f64_s",
            0xAB => "i32.trunc_f64_u",
            0xAC => "i64.extend_i32_s",
            0xAD => "i64.extend_i32_u",
            0xAE => "i64.trunc_f32_s",
            0xAF => "i64.trunc_f32_u",
            0xB0 => "i64.trunc_f64_s",
            0xB1 => "i64.trunc_f64_u",
            0xB2 => "f32.convert_i32_s",
            0xB3 => "f32.convert_i32_u",
            0xB4 => "f32.convert_i64_s",
            0xB5 => "f32.convert_i64_u",
            0xB6 => "f32.demote_f64",
            0xB7 => "f64.convert_i32_s",
            0xB8 => "f64.convert_i32_u",
            0xB9 => "f64.convert_i64_s",
            0xBA => "f64.convert_i64.u",
            0xBB => "f64.promote_f32",
            0xBC => "i32.reinterpret_f32",
            0xBD => "i64.reinterpret_i32",
            0xBE => "f32.reinterpret_i32",
            0xBF => "f64.reinterpret_i64",
            0x200D => "local.br_if",
            0x2021 => "local.copy",
            0x2028 => "local.i32.load",
            0x2029 => "local.i64.load",
            0x202A => "local.f32.load",
            0x202B => "local.f64.load",
            0x202C => "local.i32.load8_s",
            0x202D => "local.i32.load8_u",
            0x202E => "local.i32.load16_s",
            0x202F => "local.i32.load16_u",
            0x2036 => "local.i32.store",
            0x2037 => "local.i64.store",
            0x2038 => "local.f32.store",
            0x2039 => "local.f64.store",
            0x203A => "local.i32.store8",
            0x203B => "local.i32.store16",
            0x2045 => "local.132.eqz",
            0x2046 => "local.i32.eq",
            0x2047 => "local.i32.ne",
            0x2048 => "local.i32.lt_s",
            0x2049 => "local.i32.lt_u",
            0x204A => "local.i32.gt_s",
            0x204B => "local.i32.gt_u",
            0x206A => "local.i32.add",
            0x206B => "local.i32.sub",
            0x206C => "local.i32.mul",
            0x206D => "local.i32.div_s",
            0x206E => "local.i32.div_u",
            0x206F => "local.i32.rem_s",
            0x2070 => "local.i32.rem_u",
            0x2071 => "local.i32.and",
            0x2072 => "local.i32.or",
            0x2073 => "local.i32.xor",
            0x2074 => "local.i32.shl",
            0x2075 => "local.i32.shr_s",
            0x2076 => "local.i32.shr_u",
            0x2099 => "local.f64.abs",
            0x209A => "local.f64.neg",
            0x209B => "local.f64.ceil",
            0x209C => "local.f64.floor",
            0x209D => "local.f64.trunc",
            0x209E => "local.f64.nearest",
            0x209F => "local.f64.sqrt",
            0x20A0 => "local.f64.add",
            0x20A1 => "local.f64.sub",
            0x20A2 => "local.f64.mul",
            0x20A3 => "local.f64.div",
            0x20A4 => "local.f64.min",
            0x20A5 => "local.f64.max",
            0x20A6 => "local.f64.copysign",
            0x20B7 => "local.f64.convert_i32_s",
            0x20B8 => "local.f64.convert_i32_u",
            0xB721 => "f64.convert_i32_s.local",
            0xB821 => "f64.convert_i32_u.local",
            0x4121 => "i32.const.local",
            0x4221 => "i64.const.local",
            0x202036 => "local.local.i32.store",
            0x202037 => "local.local.i64.store",
            0x202038 => "local.local.f32.store",
            0x202039 => "local.local.f64.store",
            0x20203A => "local.local.i32.store8",
            0x20203B => "local.local.i32.store16",
            0x202046 => "local.local.i32.eq",
            0x202047 => "local.local.i32.ne",
            0x202048 => "local.local.i32.lt_s",
            0x202049 => "local.local.i32.lt_u",
            0x20204A => "local.local.i32.gt_s",
            0x20204B => "local.local.i32.gt_u",
            0x20206A => "local.local.i32.add",
            0x20206B => "local.local.i32.sub",
            0x20206C => "local.local.i32.mul",
            0x20206D => "local.local.i32.div_s",
            0x20206E => "local.local.i32.div_u",
            0x20206F => "local.local.i32.rem_s",
            0x202070 => "local.local.i32.rem_u",
            0x202071 => "local.local.i32.and",
            0x202072 => "local.local.i32.or",
            0x202073 => "local.local.i32.xor",
            0x202074 => "local.local.i32.shl",
            0x202075 => "local.local.i32.shr_s",
            0x202076 => "local.local.i32.shr_u",
            0x202099 => "local.local.f64.abs",
            0x20209A => "local.local.f64.neg",
            0x20209B => "local.local.f64.ceil",
            0x20209C => "local.local.f64.floor",
            0x20209D => "local.local.f64.trunc",
            0x20209E => "local.local.f64.nearest",
            0x20209F => "local.local.f64.sqrt",
            0x2020A0 => "local.local.f64.add",
            0x2020A1 => "local.local.f64.sub",
            0x2020A2 => "local.local.f64.mul",
            0x2020A3 => "local.local.f64.div",
            0x2020A4 => "local.local.f64.min",
            0x2020A5 => "local.local.f64.max",
            0x2020A6 => "local.local.f64.copysign",
            0x202821 => "local.i32.load.local",
            0x202921 => "local.i64.load.local",
            0x202B21 => "local.f64.load.local",
            0x202C21 => "local.i32.load8_s.local",
            0x202D21 => "local.i32.load8_u.local",
            0x202E21 => "local.i32.load16_s.local",
            0x202F21 => "local.i32.load16_u.local",
            0x20A021 => "local.f64.add",
            0x20A121 => "local.f64.sub",
            0x20A221 => "local.f64.mul",
            0x20A321 => "local.f64.div",
            0x20A421 => "local.f64.min",
            0x20A521 => "local.f64.max",
            0x20B721 => "local.f64.convert_i32_s.local",
            0x20B821 => "local.f64.convert_i32_u.local",
            0x20204621 => "local.local.i32.eq.local",
            0x20204721 => "local.local.i32.ne.local",
            0x20204821 => "local.local.i32.lt_s.local",
            0x20204921 => "local.local.i32.lt_u.local",
            0x20204A21 => "local.local.i32.gt_s.local",
            0x20204B21 => "local.local.i32.gt_u.local",
            0x20206A21 => "local.local.i32.add.local",
            0x20206B21 => "local.local.i32.sub.local",
            0x20206C21 => "local.local.i32.mul.local",
            0x20206D21 => "local.local.i32.div_s.local",
            0x20206E21 => "local.local.i32.div_u.local",
            0x20206F21 => "local.local.i32.rem_s.local",
            0x20207021 => "local.local.i32.rem_u.local",
            0x20207121 => "local.local.i32.and.local",
            0x20207221 => "local.local.i32.or.local",
            0x20207321 => "local.local.i32.xor.local",
            0x20207421 => "local.local.i32.shl.local",
            0x20207521 => "local.local.i32.shr_s.local",
            0x20207621 => "local.local.i32.shr_u.local",
            0x20207721 => "local.local.i32.rotl.local",
            0x20207821 => "local.local.i32.rotr.local",
            0x20207C21 => "local.local.i64.add.local",
            0x20207D21 => "local.local.i64.sub.local",
            0x20207E21 => "local.local.i64.mul.local",
            0x20207F21 => "local.local.i64.div_s.local",
            0x20208021 => "local.local.i64.div_u.local",
            0x20208121 => "local.local.i64.rem_s.local",
            0x20208221 => "local.local.i64.rem_u.local",
            0x20208321 => "local.local.i64.and.local",
            0x20208421 => "local.local.i64.or.local",
            0x20208521 => "local.local.i64.xor.local",
            0x20208621 => "local.local.i64.shl.local",
            0x20208721 => "local.local.i64.shr_s.local",
            0x20208821 => "local.local.i64.shr_u.local",
            0x20208921 => "local.local.i64.rotl.local",
            0x20208A21 => "local.local.i64.rotr.local",
            0xFE000000 => "loop of i32.const.local",
            0xFF000000 => "loop of local.i32.load.local",
            0xFF => "Loop Overhead:",
            _ => "unknown opcode: " + opCode.ToString("X")
        };
}

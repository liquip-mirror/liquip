namespace Zarlo.AML;

public class OpCodes
{
    public const byte NULL_NAME = 0x00;
    public const byte DUAL_NAME_PREFIX = 0x2E;
    public const byte MULTI_NAME_PREFIX = 0x2F;
    public const byte ROOT_CHAR = (byte)'\\';
    public const byte PREFIX_CHAR = (byte)'^';

    public const byte RESERVED_FIELD = 0x00;
    public const byte ACCESS_FIELD = 0x01;
    public const byte CONNECT_FIELD = 0x02;
    public const byte EXTENDED_ACCESS_FIELD = 0x03;

    public const byte ZERO_OP = 0x00;
    public const byte ONE_OP = 0x01;
    public const byte ONES_OP = 0xff;
    public const byte BYTE_CONST = 0x0a;
    public const byte WORD_CONST = 0x0b;
    public const byte DWORD_CONST = 0x0c;
    public const byte STRING_PREFIX = 0x0d;
    public const byte QWORD_CONST = 0x0e;

    public const byte DEF_ALIAS_OP = 0x06;
    public const byte DEF_NAME_OP = 0x08;
    public const byte DEF_SCOPE_OP = 0x10;
    public const byte DEF_BUFFER_OP = 0x11;
    public const byte DEF_PACKAGE_OP = 0x12;
    public const byte DEF_METHOD_OP = 0x14;
    public const byte DEF_EXTERNAL_OP = 0x15;
    public const byte DEF_CREATE_DWORD_FIELD_OP = 0x8a;
    public const byte DEF_CREATE_WORD_FIELD_OP = 0x8b;
    public const byte DEF_CREATE_BYTE_FIELD_OP = 0x8c;
    public const byte DEF_CREATE_BIT_FIELD_OP = 0x8d;
    public const byte DEF_CREATE_QWORD_FIELD_OP = 0x8f;
    public const byte EXT_DEF_MUTEX_OP = 0x01;
    public const byte EXT_DEF_COND_REF_OF_OP = 0x12;
    public const byte EXT_DEF_CREATE_FIELD_OP = 0x13;
    public const byte EXT_REVISION_OP = 0x30;
    public const byte EXT_DEF_FATAL_OP = 0x32;
    public const byte EXT_DEF_OP_REGION_OP = 0x80;
    public const byte EXT_DEF_FIELD_OP = 0x81;
    public const byte EXT_DEF_DEVICE_OP = 0x82;
    public const byte EXT_DEF_PROCESSOR_OP = 0x83;
    public const byte EXT_DEF_POWER_RES_OP = 0x84;
    public const byte EXT_DEF_THERMAL_ZONE_OP = 0x85;

/*
 * Type 1 opcodes
 */
    public const byte DEF_CONTINUE_OP = 0x9f;
    public const byte DEF_IF_ELSE_OP = 0xa0;
    public const byte DEF_ELSE_OP = 0xa1;
    public const byte DEF_WHILE_OP = 0xa2;
    public const byte DEF_NOOP_OP = 0xa3;
    public const byte DEF_RETURN_OP = 0xa4;
    public const byte DEF_BREAK_OP = 0xa5;
    public const byte DEF_BREAKPOINT_OP = 0xcc;

/*
 * Type 2 opcodes
 */
    public const byte DEF_STORE_OP = 0x70;
    public const byte DEF_ADD_OP = 0x72;
    public const byte DEF_CONCAT_OP = 0x73;
    public const byte DEF_INCREMENT_OP = 0x75;
    public const byte DEF_DECREMENT_OP = 0x76;
    public const byte DEF_SHIFT_LEFT = 0x79;
    public const byte DEF_SHIFT_RIGHT = 0x7a;
    public const byte DEF_AND_OP = 0x7b;
    public const byte DEF_CONCAT_RES_OP = 0x84;
    public const byte DEF_OBJECT_TYPE_OP = 0x8e;
    public const byte DEF_L_AND_OP = 0x90;
    public const byte DEF_L_OR_OP = 0x91;
    public const byte DEF_L_NOT_OP = 0x92;
    public const byte DEF_L_EQUAL_OP = 0x93;
    public const byte DEF_L_GREATER_OP = 0x94;
    public const byte DEF_L_LESS_OP = 0x95;
    public const byte DEF_TO_INTEGER_OP = 0x99;
    public const byte DEF_MID_OP = 0x9e;

/*
 * Miscellaneous objects
 */
    public const byte EXT_DEBUG_OP = 0x31;
    public const byte LOCAL0_OP = 0x60;
    public const byte LOCAL1_OP = 0x61;
    public const byte LOCAL2_OP = 0x62;
    public const byte LOCAL3_OP = 0x63;
    public const byte LOCAL4_OP = 0x64;
    public const byte LOCAL5_OP = 0x65;
    public const byte LOCAL6_OP = 0x66;
    public const byte LOCAL7_OP = 0x67;
    public const byte ARG0_OP = 0x68;
    public const byte ARG1_OP = 0x69;
    public const byte ARG2_OP = 0x6a;
    public const byte ARG3_OP = 0x6b;
    public const byte ARG4_OP = 0x6c;
    public const byte ARG5_OP = 0x6d;
    public const byte ARG6_OP = 0x6e;

    public const byte EXT_OPCODE_PREFIX = 0x5b;
}

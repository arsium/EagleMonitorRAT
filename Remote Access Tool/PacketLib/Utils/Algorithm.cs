
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    public enum Algorithm : byte
    {
        NONE =          0,//TODO : GENERATE RANDOM
        Blowfish =      1,
        Cast5 =         2,
        Cast6 =         3,
        DesEde =        4,
        DesEngine =     5,
        Dstu7624 =      6,
        Gost28147 =     7,
        Idea =          8,
        Noekeon =       9,
        RC2 =           10,
        RC532 =         11,
        RC6 =           12,
        Rijndael =      13,
        Seed =          14,
        Serpent =       15,
        Skipjack =      16,
        SM4 =           17,
        Tea =           18,
        Threefish =     19,
        Tnepres =       20,
        Twofish =       21,
        Xtea =          22,
        Chacha =        23,
        Chacha7539 =    24,
        Salsa20 =       25,
        XSalsa20 =      26,
        Vmpc =          27,
        RC4 =           28,
        VmpcKsa3 =      29,
        HC256 =         30,
        Isaac =         31
    }
}

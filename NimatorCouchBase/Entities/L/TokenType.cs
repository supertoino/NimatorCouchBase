using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NimatorCouchBase.Entities.L
{
    public enum TokenType
    {
        LeftParam,
        RigthParam,
        Comma,
        Equal,
        Not,
        Plus,
        Minus,
        Asterisk,
        Bigger,
        Smaller,        
        Variable,
        Eof,
        Scalar
    }
}

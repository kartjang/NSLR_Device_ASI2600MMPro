using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cam_ASI2600MMPro_Test
{
    internal class Global
    {
    }

    // Log Level을 지정 할 Enum
    public enum LOG
    {
        I, //INFO
        W, //WARN
        D, //DEBUG
        E, //ERROR
        F, //FATAL
    }

    // UserControl에서 Main으로 Log를 전달 하기 위한 Delegate
    public delegate void delegate_Logger(LOG level, object sender, string strLog);
}

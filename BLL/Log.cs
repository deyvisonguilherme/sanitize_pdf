using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Log
    {
        public static void LogarError(String _mensagem, String _tracer)
        {
            using (StreamWriter swt = new StreamWriter(".", true, Encoding.UTF8))
            {
                swt.WriteLine(DateTime.Now.ToString());
                swt.WriteLine(_tracer);
                swt.WriteLine(_mensagem);
            }
        }
    }
}

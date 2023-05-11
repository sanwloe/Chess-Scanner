using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppLib_Parser;
using Mach4;

namespace AppLib_InputGCode
{
    public delegate void InsertGCode(Move move);
    public static class Input
    {
        static IMach4 mach4;
        static IMyScriptObject myScript;
        static bool blockMach = true; 

        static Input()
        {
            mach4 = (IMach4)Marshal.GetActiveObject("Mach4.Document");

            myScript = (IMyScriptObject)mach4.GetScriptDispatch();

            if(mach4 != null)
            {
                blockMach = false;
            }
            else
            {
                MessageBox.Show("Mach4.exe не знайдено в списку запущених програм!");
            }
        }

        public static void ExecuteCode(string cmd)
        {
            if(blockMach == false)
            {
                myScript.Code(cmd); 
            }               
        }
        public static void Reset()
        {
            if(blockMach == false)
            {
                myScript.DoOEMButton(1021);
            }     
        }
        public static void CycleStart()
        {
            if(blockMach == false)
            {
                myScript.DoOEMButton(1000);
            }
            
        }
        public static void Zero()
        {
            if(blockMach == false)
            {
                myScript.DoOEMButton(1008);

                myScript.DoOEMButton(1009);

                myScript.DoOEMButton(1010);
            }
        }
        public static void Stop()
        {
            if(blockMach == false)
            {
                myScript.DoOEMButton(1003);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLib_Parser
{
    public class Move
    {
        public Move(string movefrom,string moveto)
        {
            MoveTo = moveto;
            MoveFrom = movefrom;
        }
        string _MoveTo;
        public string _MoveToInt;
        public string MoveTo 
        {
            get { return _MoveTo; }
            set
            {
                if(value != null)
                {
                    var result = value.Replace("highlight square-", "");
                    _MoveToInt = result;
                    switch (result[0])
                    {
                        case '1':
                            _MoveTo = result.Insert(1, "A").Remove(0, 1);
                            break;
                        case '2':
                            _MoveTo = result.Insert(1, "B").Remove(0, 1);
                            break;
                        case '3':
                            _MoveTo = result.Insert(1, "C").Remove(0, 1);
                            break;
                        case '4':
                            _MoveTo = result.Insert(1, "D").Remove(0, 1);
                            break;
                        case '5':
                            _MoveTo = result.Insert(1, "E").Remove(0, 1);
                            break;
                        case '6':
                            _MoveTo = result.Insert(1, "F").Remove(0, 1);
                            break;
                        case '7':
                            _MoveTo = result.Insert(1, "G").Remove(0, 1);
                            break;
                        case '8':
                            _MoveTo = result.Insert(1, "H").Remove(0, 1);
                            break;
                        default:
                            _MoveTo = value.Replace("highlight square-", "").ToUpper();
                            break;
                    }
                }             
            }
        }
        string _MoveFrom;
        public string _MoveFromInt;
        public string MoveFrom
        {
            get { return _MoveFrom; }
            set
            {
                if (value!=null)
                {
                    var result = value.Replace("highlight square-", "");
                    _MoveFromInt = result;
                    switch (result[0])
                    {
                        case '1':
                                _MoveFrom = result.Insert(1, "A").Remove(0, 1);
                                break;
                        case '2':
                                _MoveFrom = result.Insert(1, "B").Remove(0, 1);
                                break;
                        case '3':
                                _MoveFrom = result.Insert(1, "C").Remove(0, 1);
                                break;
                        case '4':
                                _MoveFrom = result.Insert(1,"D").Remove(0, 1);
                                break;
                        case '5':
                                _MoveFrom = result.Insert(1,"E").Remove(0, 1);
                                break;
                        case '6':
                                _MoveFrom = result.Insert(1,"F").Remove(0, 1);
                                break;
                        case '7':
                                _MoveFrom = result.Insert(1, "G").Remove(0, 1);
                                break;
                        case '8':
                                _MoveFrom = result.Insert(1,"H").Remove(0, 1);
                                break;
                        default:
                            _MoveFrom = value.Replace("highlight square-", "").ToUpper();
                            break;
                    }
                }
            }
        }
    }
}

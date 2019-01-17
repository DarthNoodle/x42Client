using Altcoined.Shared.CoinDaemon.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altcoined.Shared.Utils.Web
{
    public class JSONBuilder
    {
        private const String JSON_RPC_VERSION = "2.0";

        //[{"jsonrpc":"2.0","id":2,"method":"getinfo"}]
        //[{"jsonrpc":"2.0","id":2,"method":"getaccountaddress","params":["Mooo"]}]
        //"[{"jsonrpc":"2.0","id":2,"method":"sendfrom","params":["fromaddress","toaddress",123.0,1,"",""]}
        //[{"jsonrpc":"2.0","id":2,"method":"getinfo"},{"jsonrpc":"2.0","id":3,"method":"getbalance"}]        
        public static String BuildRequest(List<RPCRequest> methods)
        {
            StringBuilder _JsonString = new StringBuilder();

            _JsonString.Append("[");

            for (int i = 0; i < methods.Count; i++)
            {
                _JsonString.Append(BuildSingleMethodString(methods[i]));

                //are there more in the array
                if (i < methods.Count - 1)
                {
                    _JsonString.Append(",");
                }
            }//end of for

            _JsonString.Append("]");

            return _JsonString.ToString();
        }


        private static String BuildSingleMethodString(RPCRequest method)
        {
            String _JSONParamiter = String.Format("\"jsonrpc\":\"{0}\"", JSON_RPC_VERSION);

            StringBuilder _JsonString = new StringBuilder();

            _JsonString.Append("{" + _JSONParamiter + ",");
            _JsonString.Append(String.Format("\"id\":\"{0}\",", method.ID));
            _JsonString.Append(String.Format("\"method\":\"{0}\"", method.Method));

            //"[{"jsonrpc":"2.0","id":2,"method":"sendfrom","params":["fromaddress","toaddress",123.0,1,"",""]}
            if (method.Parameters.Count > 0)
            {
                _JsonString.Append(",\"params\":[");

                for (int i = 0; i < method.Parameters.Count; i++)
                {
                    if ((method.Parameters[i] is String))
                    {

                        _JsonString.Append("\"" + method.Parameters[i] + "\"");
                    }
                    else
                    {
                        if (method.Parameters[i] is Boolean)
                        {
                            _JsonString.Append(method.Parameters[i].ToString().ToLower());
                        }
                        else
                        {
                            _JsonString.Append(method.Parameters[i]);
                        }

                    }


                    //are there more in the array
                    if (i < method.Parameters.Count - 1)
                    {
                        _JsonString.Append(",");
                    }

                }//end of for

                _JsonString.Append("]");
            }//end of if

            _JsonString.Append("}");

            return _JsonString.ToString();
        }
    }
}

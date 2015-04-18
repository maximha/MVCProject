using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Helpers
{
    public class ResultHelper
    {
        public static ResultModel boolResult(bool result, string message)
        {
            return new ResultModel()
            {
                result = result,
                message = message
            };
        }
        public static KeysModel arrayResult(int userID, string userKey)
        {
            List<string> keyIds = getUserKeys(userID).Select(x => encodeKey(x.Key, userKey)).ToList<string>();
            KeysModel keysModel = new KeysModel
            {
                result = true,
                keys = keyIds
            };
            return keysModel;
        }
    }
}
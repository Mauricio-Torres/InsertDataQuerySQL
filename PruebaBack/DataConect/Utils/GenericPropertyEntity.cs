using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataConect.Utils
{
    public static class GenericPropertyEntity<TModel> where TModel : class
    {
        public static List<Tuple<string,object>> PrintTModelProperty(TModel tmodelObj)
        {
            Type tModelType = tmodelObj.GetType();
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            List<Tuple<string, object>> nameProerty = new List<Tuple<string, object>>();

            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                nameProerty.Add (new Tuple<string, object>(property.Name, property.GetValue(tmodelObj)));
            }

            return nameProerty;
        }
    }
}

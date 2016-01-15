using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace PokeBattle_Client
{
    class JSTuple2Converter<T1, T2> : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(Tuple<T1, T2>); }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            T1 item1 = (T1)dictionary["Item1"];
            T2 item2 = (T2)dictionary["Item2"];
            return new Tuple<T1, T2>(item1, item2);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

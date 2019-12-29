using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Lib.Models.V3
{
    [Serializable]
    public class Expando : DynamicObject
    {
        [NonSerialized]
        private PropertyInfo[] _propertiesInfos;

        [NonSerialized]
        private readonly Dictionary<string, object> _properties;

        public Expando() 
        {
            _propertiesInfos = GetPropertiesInfos();
            _properties = new Dictionary<string, object>();
        }

        public object this[string propertyName]
        {
            get
            {
                if (GetProperty(propertyName, out var result))
                {
                    return result;
                }

                throw new KeyNotFoundException(propertyName);
            }
            set => SetProperty(propertyName, value);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _propertiesInfos.Select(x => x.Name).Union(_properties.Keys).Distinct();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_properties.TryGetValue(binder.Name, out result))
            {
                return true;
            }

            try
            {
                return GetProperty(binder.Name, out result);
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex);
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            try
            {
                if (!SetProperty(binder.Name, value))
                {
                    _properties[binder.Name] = value;
                }

                return true;
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                if (InvokeMethod(binder.Name, args, out result))
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex);
            }

            result = null;
            return false;
        }

        protected bool GetProperty(string name, out object result)
        {
            if (_properties.TryGetValue(name, out result))
            {
                return true;
            }

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;
            var memberInfos = GetType().GetMember(name, bindingFlags);
            var memberInfo = memberInfos.FirstOrDefault();

            if (memberInfo?.MemberType == MemberTypes.Property)
            {
                result = ((PropertyInfo) memberInfo).GetValue(this, null);
                return true;
            }

            result = null;
            return false;                
        }

        protected bool SetProperty(string name, object value)
        {
            if (_properties.ContainsKey(name))
            {
                _properties[name] = value;
            }

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance;
            var memberInfos = GetType().GetMember(name, bindingFlags);
            var memberInfo = memberInfos.FirstOrDefault();

            if (memberInfo?.MemberType == MemberTypes.Property)
            {
                ((PropertyInfo) memberInfo).SetValue(this, value, null);
                return true;
            }

            return false;                
        }

        protected bool InvokeMethod(string name, object[] args, out object result)
        {
            const BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance;
            var memberInfos = GetType().GetMember(name, bindingFlags);
            var memberInfo = memberInfos.FirstOrDefault() as MethodInfo;

            if (memberInfo?.MemberType == MemberTypes.Property)
            {
                result = memberInfo.Invoke(this, args);
                return true;
            }

            result = null;
            return false;
        }

        private PropertyInfo[] GetPropertiesInfos()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            return GetType().GetProperties(bindingFlags);
        }
    }
}

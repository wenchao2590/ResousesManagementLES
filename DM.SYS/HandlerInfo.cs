using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DM.SYS
{
    public partial class HandlerInfo
    {
        private object classObject;
        private MethodInfo method;

        public object ClassObject
        {
            get
            {
                return classObject;
            }

            set
            {
                classObject = value;
            }
        }

        public MethodInfo Method
        {
            get
            {
                return method;
            }

            set
            {
                method = value;
            }
        }
    }
}

using LL.MDE.Components.Qvt.Common;
using System;
using System.Diagnostics;
using System.Reflection;

namespace MDD4All.QVT.EaAccess
{
    public class EaMetaModelInterface : IMetaModelInterface
    {
        public object CreateNewObjectInField(object element, string fieldName)
        {
            object result = null;

            switch (fieldName)
            {
                case "Packages":
                    EA.Package pHybrid = (EA.Package)element;
                    result = (EA.Package) pHybrid.Packages.AddNew("NewPackage", "Package");
                    pHybrid.Packages.Refresh();
                    break;
                case "Elements":
                    EA.Package elementPackage = (EA.Package)element;                   
                    result = (EA.Element) elementPackage.Elements.AddNew("NewElement", "Class");
                    elementPackage.Elements.Refresh();
                    break;
                case "Connectors":
                    EA.Element connectorElement = (EA.Element)element;
                    result = (EA.Connector) connectorElement.Connectors.AddNew("NewConnector", "Connector");
                    connectorElement.Connectors.Refresh();
                    break;
            }

            return result;
        }

        public void AddOrSetInField(object element, string fieldName, object value)
        {
            try
            {
                Type type = element.GetType();


                type.InvokeMember(fieldName, BindingFlags.SetProperty, null, element, new object[] { value });
                //FieldInfo memberPropertyInfo = element.GetType().GetField(fieldName);
                //memberPropertyInfo.SetValue(element, value);
                object[] args = new object[0];
                element.GetType().InvokeMember("Update", BindingFlags.Default | BindingFlags.InvokeMethod, null, element, args);
            }
            catch
            {
                
            }
        }
    }
}

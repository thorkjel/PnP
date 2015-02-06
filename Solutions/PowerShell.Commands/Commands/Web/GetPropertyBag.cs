﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace OfficeDevPnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SPOPropertyBag")]
    public class GetPropertyBag : SPOWebCmdlet
    {
        [Parameter(Mandatory = false, Position=0, ValueFromPipeline=true)]
        public string Key = string.Empty;
        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Key))
            {
                WriteObject(SelectedWeb.GetPropertyBagValueString(Key, string.Empty));
            }
            else
            {
                if (SelectedWeb.IsPropertyAvailable("AllProperties"))
                {
                    WriteObject(SelectedWeb.AllProperties.FieldValues);
                }
                else
                {
                    var values = SelectedWeb.AllProperties;
                    ClientContext.Load(values);
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(values.FieldValues);
                }
            }
        }
    }
}

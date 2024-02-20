#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.NetLogic;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion

public class InstanceMultiplierTool : BaseNetLogic
{
    [ExportMethod]
    public void CreateInstances()
    {
        Log.Info("Reading Parameters...");
   
        int var_qty = (int)LogicObject.GetVariable("Quantity").Value;
        if (var_qty < 1) {
            Log.Info("Quantity must be positive");
            return;}

        var var_typeAlias = LogicObject.GetAlias("Type_Alias");
       // var var_objtypeAlias = LogicObject.GetByType<var_typeAlias>("Type_Alias");
        if (var_typeAlias == null) {
            Log.Info("Alias must be defined"); 
            return;}
        string str_typeAlias = (string)var_typeAlias.BrowseName;
        
        //  Create Folder
        Log.Info("Creating "+ str_typeAlias +"s Folder...");
        var InstancesFolder = InformationModel.Make<Folder>(str_typeAlias + "s");
            Project.Current.Get("Model").Add(InstancesFolder);

        //  Create instance of type
        Log.Info("Creating "+ var_qty +" "+ str_typeAlias+ " Instances...");
        for (int i = 1; i < var_qty+1; i++)
        {
                //Make Instance
            var VarInstance = InformationModel.MakeObject(str_typeAlias + i, var_typeAlias.NodeId);
                //Add Instance to folder
            InstancesFolder.Add(VarInstance);

        }
    }
}

#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.MicroController;
using FTOptix.NativeUI;
using FTOptix.WebUI;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Core;
#endregion

public class LineA : BaseNetLogic
{
    [ExportMethod]
    public void GenerateRandomFloat()
    {
        Random rnd = new Random();

        // 1. Generate a double between 0.0 and 1.0, then scale it to 10.0
        double randomDouble = rnd.NextDouble() * 10.0;

        // 2. Optional: Round to 2 decimal places so the HMI looks cleaner
        // (Industrial sensors usually don't provide 15 decimal places of precision!)
        double roundedValue = Math.Round(randomDouble, 2);

        // 3. Find the variable in your project Model
        // Ensure the Tag in Optix is set to Data Type: Float or Double
        var targetVariable = Project.Current.GetVariable("Model/LineA_Speed");

        if (targetVariable != null)
        {
            targetVariable.Value = (float)roundedValue; // Casting to float for Optix compatibility
            Log.Info("Random Generator", $"New Real Value: {roundedValue}");
        }
        else
        {
            Log.Error("Random Generator", "Float Variable not found at the specified path.");
        }
    }
}

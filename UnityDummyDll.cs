using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CG.SDK.Dotnet.Attributes;
using CG.SDK.Dotnet.Engine;
using CG.SDK.Dotnet.Engine.Unity;
using CG.SDK.Dotnet.Helper.IO;
using CG.SDK.Dotnet.Plugin.Output;

namespace CG.Output.UnityDummyDll;

[PluginInfo(
    Name = nameof(UnityDummyDll),
    Version = "5.0.0",
    Author = "CorrM",
    Description = "Generate dummy dll",
    WebsiteLink = "https://github.com/CheatGear",
    SourceCodeLink = "https://github.com/CheatGear/Output.UnityDummyDll"
)]
public sealed class UnityDummyDll : OutputPlugin<UnitySdkFile>
{
    public override string OutputName => "DummyDll";

    public override GameEngine SupportedEngines => GameEngine.Unity;

    public override OutputPurpose SupportedPurpose => OutputPurpose.Internal | OutputPurpose.External;

    public override IReadOnlyDictionary<Enum, OutputOption> Options { get; } = new Dictionary<Enum, OutputOption>();

    public override async ValueTask SaveAsync(string saveDirPath, OutputPurpose processPurpose)
    {
        // Write assemblies
        foreach ((string fileName, byte[] fileContent) in SdkFile.Assemblies)
        {
            using var ms = new MemoryStream(fileContent);
            await FileManager.WriteAsync(saveDirPath, fileName, ms).ConfigureAwait(false);
        }
    }
}

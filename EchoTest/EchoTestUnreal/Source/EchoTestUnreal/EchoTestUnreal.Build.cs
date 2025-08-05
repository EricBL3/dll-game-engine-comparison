// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class EchoTestUnreal : ModuleRules
{
	public EchoTestUnreal(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] {
			"Core",
			"CoreUObject",
			"Engine",
			"InputCore",
			"EnhancedInput",
			"AIModule",
			"StateTreeModule",
			"GameplayStateTreeModule",
			"UMG"
		});

		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.AddRange(new string[] {
			"EchoTestUnreal",
			"EchoTestUnreal/Variant_Platforming",
			"EchoTestUnreal/Variant_Combat",
			"EchoTestUnreal/Variant_Combat/AI",
			"EchoTestUnreal/Variant_SideScrolling",
			"EchoTestUnreal/Variant_SideScrolling/Gameplay",
			"EchoTestUnreal/Variant_SideScrolling/AI"
		});

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using UnityEditor.iOS.Xcode;
using System.IO;

public class AddCrystalIDPlist {

	[PostProcessBuild]
	public static void OnPostprocessBuild (BuildTarget BuildTarget, string path) {
		Debug.Log (path);
		if (BuildTarget == BuildTarget.iOS) {

			// Go get pbxproj file
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

			// PBXProject class represents a project build settings file,
			// here is how to read that in.
			PBXProject proj = new PBXProject ();
			proj.ReadFromFile (projPath);

			// This is the Xcode target in the generated project
			string target = proj.TargetGuidByName ("Unity-iPhone");

			// Copy plist from the project folder to the build folder
			FileUtil.CopyFileOrDirectory ("Assets/Plugins/iOS/CrystalExpressSDK/CrystalExpress.plist", path + "/CrystalExpress.plist");
			proj.AddFileToBuild (target, proj.AddFile("CrystalExpress.plist", "CrystalExpress.plist"));

			AddFramework (proj, target);

			// Write PBXProject object back to the file
			proj.WriteToFile (projPath);
		}
	}

	//Add required libraries into project
	private static void AddFramework (PBXProject proj, string target) {

		proj.AddFrameworkToProject (target, "libz.tbd", weak:false);
		proj.AddFrameworkToProject (target, "libsqlite3.tbd", weak:false);
		proj.AddFrameworkToProject (target, "libicucore.tbd", weak:false);

	}
}

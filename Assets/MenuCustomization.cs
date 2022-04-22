using UnityEditor;
using UnityEngine;
using System.IO;

public class MenuCustomization
{
	[MenuItem("Custom/Fix compile errors!")]
	static void VerwijderBeeFolder() {
		string folder = Directory.GetCurrentDirectory().Replace("\\", "/") + @"/Library/Bee";
		if (FileUtil.DeleteFileOrDirectory(folder))
		{
			Debug.Log("Het zou nu opgelost moeten zijn. Wacht eventjes totdat Unity alles opnieuw heeft gecompiled.");
			UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
		}
		else
			Debug.LogWarning("Kon de kutfolder niet verwijderen, probeer het opnieuw, dat werkt hiet wel vaak:\n" + folder);
	}
}

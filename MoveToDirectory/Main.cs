using System;
using System.IO;
using System.Reflection;

namespace MoveToDirectory
{
	class MainClass
	{
		private static string basePath;
		
		public static void Main (string[] args)
		{

			basePath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(MainClass)).CodeBase);
			basePath = basePath.Replace("file:","");
			
			System.Console.WriteLine(basePath);
			MoveFilesRecursively(new DirectoryInfo(basePath));
			
			foreach (DirectoryInfo dir in new DirectoryInfo(basePath).GetDirectories())
		    {
				System.Console.WriteLine("Deleting Directory " + dir.FullName);
		        System.IO.Directory.Delete(dir.FullName, true);
		    }
			
		}
		
		public static void MoveFilesRecursively(DirectoryInfo source) 
		{
		    foreach (DirectoryInfo dir in source.GetDirectories())
		    {
		        MoveFilesRecursively(dir);
		    }
		    foreach (FileInfo file in source.GetFiles())
		    {
				try
				{
					if (source.FullName == basePath)
					{
						continue;
					}
					
					//System.Console.WriteLine(source.FullName);
					
					System.Console.WriteLine("Moving file" + file.FullName);
					System.IO.File.Move(file.FullName, System.IO.Path.Combine(basePath, file.Name));
		    
				}
				catch(Exception ex)
				{
				}

			}
		}
		
	}
}

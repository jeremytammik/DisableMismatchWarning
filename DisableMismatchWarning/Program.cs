using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DisableMismatchWarning
{
  class Program
  {
    const string _arch_mismatch_tag
      = "<ResolveAssemblyWarnOrErrorOnTargetArchitectureMismatch>";

    const string _property_group
      = "  <PropertyGroup>\r\n"
      + "    <ResolveAssemblyWarnOrErrorOnTargetArchitectureMismatch>\r\n"
      + "      None\r\n"
      + "    </ResolveAssemblyWarnOrErrorOnTargetArchitectureMismatch>\r\n"
      + "  </PropertyGroup>\r\n";

    /// <summary>
    /// Examine the given file.
    /// Check that it is not read-only.
    /// Check that it appears to be a valid 
    /// Visual Studio project file.
    /// Check that it does not already contain a 
    /// ResolveAssemblyWarnOrErrorOnTargetArchitectureMismatch 
    /// tag.
    /// If so, create a backup file and 
    /// add the property group.
    /// </summary>
    static bool AddPropertyGroup( string filename )
    {
      bool rc = false;

      Console.WriteLine( filename + " -- examine..." );

      FileAttributes atts = File.GetAttributes(
        filename );

      bool isReadOnly = ( FileAttributes.ReadOnly
        == ( atts & FileAttributes.ReadOnly ) );

      if( isReadOnly )
      {
        Console.WriteLine( filename + " -- read-only." );
        return rc;
      }

      List<string> lines = new List<string>(
        File.ReadLines( filename ) );

      bool isProjectFile
        = lines[0].StartsWith( "<?xml version=" )
        && lines[1].StartsWith( "<Project " );

      if( !isProjectFile )
      {
        Console.WriteLine( filename + " -- not a Visual Studio project." );
        return rc;
      }

      int n = 2;

      while( lines[n].StartsWith( "  <Import " ) )
      {
        ++n;
      }

      isProjectFile = lines[n].Equals( "  <PropertyGroup>" );

      if( !isProjectFile )
      {
        isProjectFile
          = lines[0].StartsWith( "<Project " )
          && lines[1].Equals( "  <PropertyGroup>" );

        n = 1;
      }

      bool hasArchMismatchTag = 0 < lines.Where(
        a => a.Contains( _arch_mismatch_tag ) ).Count();

      if( hasArchMismatchTag )
      {
        Console.WriteLine( filename + " -- already has an architecture mismatch tag." );
        return rc;
      }

      Console.WriteLine( filename + " -- process..." );

      File.Copy( filename, filename + ".bak", true );

      lines.Insert( n, _property_group );

      File.WriteAllLines( filename, lines );

      rc = true;

      return rc;
    }

    /// <summary>
    /// Return full path of all files mathing pattern
    /// found recursively below the given root folder.
    /// </summary>
    /// <param name="root">Root starting folder</param>
    /// <param name="searchPattern">Filename pattern</param>
    /// <returns>Enumeration of full file paths</returns>
    static IEnumerable<string> Search(
      string root,
      string searchPattern )
    {
      Queue<string> dirs = new Queue<string>();

      dirs.Enqueue( root );

      while( 0 < dirs.Count )
      {
        string dir = dirs.Dequeue();

        // files

        string[] paths = null;

        try
        {
          paths = Directory.GetFiles(
            dir, searchPattern );
        }
        catch
        {
          // swallow
        }

        if( paths != null && 0 < paths.Length )
        {
          foreach( string file in paths )
          {
            yield return file;
          }
        }

        // sub-directories

        paths = null;
        try
        {
          paths = Directory.GetDirectories( dir );
        }
        catch
        {
          // swallow
        }

        if( paths != null && paths.Length > 0 )
        {
          foreach( string subDir in paths )
          {
            dirs.Enqueue( subDir );
          }
        }
      }
    }

    /// <summary>
    /// Add a new property group to suppress the 
    /// processor architecture mismatch warning MSB3270
    /// to all Visual Studio C# and VB .NET project 
    /// files with the filename extension CSPROJ or 
    /// VBPROJ found recursively in the current working 
    /// directory or any of its subfolders.
    /// </summary>
    static int Main( string[] args )
    {
      string root = Directory.GetCurrentDirectory();

      foreach( string match in Search( root, "*proj" ) )
      {
        string path = match.ToLower();
        if( path.EndsWith( ".csproj" )
          || path.EndsWith( ".vbproj" ) )
        {
          AddPropertyGroup( match );
        }
      }
      return 0;
    }
  }
}

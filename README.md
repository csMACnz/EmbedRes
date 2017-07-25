csMACnz.EmbedRes
================

Some helpful stuff to use embedded resources with dotnet.

To use
------

0. Add the csMACnz.EmbedRes NuGet package to your project.
1. Create a folder in your project to hold your resource text files (e.g. `MyResources`).
2. Add text files to your resource folder (e.g. `Resource1.txt`, `Resource2.sql`).
3. Create a `.cs` file to match the name of the resource folder.
4. Add the following code to your cs file:

``` csharp
public static class MyResources
{
    public static string Resource1 => GetFileContents("Resource1.txt");

    public static string Resource2 => GetFileContents("Resource2.sql");

    public static string GetFileContents(string resourceName)
    {
        return ResourceLoader.GetContentFromFolderMatchingTypeName<MyResources>(resourceName);
    }
}
```

5. Make sure you embed your resources by the following to the `project.json` file.

``` json
{
  "version": "1.0.0-*",
  "buildOptions": {
    "debugType": "portable",
    "embed": [ "MyResources/*.*" ]
  }
}
```

or in your csproj:

``` xml
<Project Sdk="Microsoft.NET.Sdk">
...
  <ItemGroup>
    <EmbeddedResource Include="MyResources\Resource1.txt" />
    <EmbeddedResource Include="MyResources\Resource2.sql" />
  </ItemGroup>
...
</Project>
```

Advanced Usage
--------------

If you are comfortable with the above, try using these helpers for more 'magic' (a.k.a convention):

``` csharp
public static class MyResources
{
    public static string Resource1 => GetTextContents();

    public static string Resource2 => GetSqlContents();

    public static string GetTextContents([CallerMemberName] string resourceName = null)
    {
        return GetFileContents($"{resourceName}.txt");
    }

    public static string GetSqlContents([CallerMemberName] string resourceName = null)
    {
        return GetFileContents($"{resourceName}.sql");
    }

    // This can be much simpler if you only have one filetype/extension
    public static string GetFileContents(string resourceName)
    {
        return ResourceLoader.GetContentFromFolderMatchingTypeName<MyResources>(resourceName);
    }
}
```

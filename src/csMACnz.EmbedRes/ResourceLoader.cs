using System.IO;
using System.Reflection;

namespace csMACnz.EmbedRes
{
    public static class ResourceLoader
    {
        public static Stream GetStreamFromFolderMatchingTypeName<T>(string resourceFilename)
        {
            return GetStreamFromFolderMatchingTypeName(typeof(T).GetTypeInfo(), resourceFilename);
        }

        public static Stream GetStreamFromFolderMatchingTypeName(TypeInfo targetTypeInfo, string resourceFilename)
        {
            var assembly = targetTypeInfo.Assembly;

            var path = GetPathFromFolderMatchingTypeName(targetTypeInfo, resourceFilename);

            return GetResourceStream(assembly, path);
        }

        public static string GetContentFromFolderMatchingTypeName<T>(string resourceFilename)
        {
            return GetContentFromFolderMatchingTypeName(typeof(T).GetTypeInfo(), resourceFilename);
        }

        public static string GetContentFromFolderMatchingTypeName(TypeInfo targetTypeInfo, string resourceFilename)
        {
            var assembly = targetTypeInfo.Assembly;

            var path = GetPathFromFolderMatchingTypeName(targetTypeInfo, resourceFilename);

            return ResourceLoader.GetResourceContent(assembly, path);
        }

        public static string GetPathFromFolderMatchingTypeName(TypeInfo targetTypeInfo, string resourceFilename)
        {
            var resourceNamespace = targetTypeInfo.FullName;

            return $"{resourceNamespace}.{resourceFilename}";
        }

        public static Stream GetResourceStream(Assembly assembly, string path)
        {
            return assembly.GetManifestResourceStream(path);
        }

        public static string GetResourceContent(Assembly assembly, string path)
        {
            var resourceStream = GetResourceStream(assembly, path);

            using (var reader = new StreamReader(resourceStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

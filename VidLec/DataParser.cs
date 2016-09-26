using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    class DataParser
    {
        /// <summary>
        /// Deserializes the folders into Folder classes and sort them
        /// in a tree-like manner
        /// </summary>
        /// <param name="catalogDetails"></param>
        /// <returns>The root of the tree</returns>
        public static Folder ParseCatalogDetails(string catalogDetails)
        {
            List<Folder> unlinkedFolders = new List<Folder>();

            JObject root = JObject.Parse(catalogDetails);
            JArray navFolders = (JArray)root[AppConfig.SiteData.JsonNavigationFoldersName];
            JsonSerializer serializer = new JsonSerializer();

            foreach (JObject item in navFolders)
            {
                Folder f = (Folder)serializer.Deserialize(new JTokenReader(item), typeof(Folder));
                unlinkedFolders.Add(f);
            }

            return SortFolders(unlinkedFolders);
        }

        /// <summary>
        /// Sorts the folders, making a tree-like structure
        /// </summary>
        /// <param name="folders">Unsorted list of Folder class instances</param>
        /// <returns>The root of the tree</returns>
        private static Folder SortFolders(List<Folder> folders)
        {
            // Find root folder
            Folder root = null;
            foreach (Folder f in folders)
                if (f.ParentCatalogFolderId == null)
                    root = f;
            if (root == null)
                return root;

            // Recursively find folder childs
            return FindChilds(root, folders);
        }

        /// <summary>
        /// Recursively finds childs for parent Folder
        /// 
        /// Worst case running time:
        ///     T(n) = T(n-1) + O(n)
        /// </summary>
        /// <param name="parent">Parent to find childs for</param>
        /// <param name="folders">All unlinked folders</param>
        /// <returns>The parent</returns>
        private static Folder FindChilds(Folder parent, List<Folder> folders)
        {
            foreach (Folder f in folders)
            {
                if (f.ParentCatalogFolderId == parent.DynamicFolderId) // if f is a child
                {
                    if (HasChild(f, folders)) // if f has children
                        parent.ChildFolders.Add(FindChilds(f, folders)); // add f as parent
                    else // if f has no children
                        parent.ChildFolders.Add(f); // add f as an child
                }
            }
            return parent;
        }

        /// <summary>
        /// Check if this parent contains childs
        /// </summary>
        /// <param name="parent">Parent to check for</param>
        /// <param name="folders">Folders with possible childs</param>
        /// <returns>Whether this parent has children</returns>
        private static bool HasChild(Folder parent, List<Folder> folders)
        {
            foreach (Folder f in folders)
                if (f.ParentCatalogFolderId == parent.DynamicFolderId)
                    return true;
            return false;
        }
    }
}

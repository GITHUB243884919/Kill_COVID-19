/*******************************************************************
* FileName:     FindResourceDepend.cs
* Author:       Fan Zheng Yong
* Date:         2020-4-14
* Description:  
* other:    
********************************************************************/



using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UFrame.MiniGame
{
    public class FindResourceDepend : EditorWindow
    {
        static string rootDir = "ResourcesDepend";

        [MenuItem("Assets/UFrame/ResourceTool/CopyDependToOneDir")]
        static void CopyToOneDir()
        {
            CreateDir(string.Format("./{0}", rootDir), false);
            string[] strs = Selection.assetGUIDs;
            string path = AssetDatabase.GUIDToAssetPath(strs[0]);
            //Debug.LogError(path);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            string fileName = System.IO.Path.GetFileName(path);
            string destDir = System.IO.Path.Combine(string.Format("./{0}", rootDir), fileNameWithoutExtension);
            CreateDir(destDir, true);
            var deps = AssetDatabase.GetDependencies(path, true);
            for (int i = 0; i < deps.Length; i++)
            {
                string sourcePath = deps[i].Replace("Assets", "");
                sourcePath = Application.dataPath + sourcePath;
                sourcePath = sourcePath.Replace("\\", "/");
                //Debug.LogError(sourcePath);
                string targetPath = Application.dataPath;
                targetPath = targetPath.Replace("Assets", "");
                targetPath = targetPath.Replace("\\", "/");
                targetPath += rootDir + "/" + fileNameWithoutExtension
                    + "/" + System.IO.Path.GetFileName(deps[i]);
                System.IO.File.Copy(sourcePath, targetPath, true);
                System.IO.File.Copy(sourcePath + ".meta", targetPath + ".meta", true);
            }
        }

        [MenuItem("Assets/UFrame/ResourceTool/CopyDependToTowDir")]
        static void CopyToTowDir()
        {
            CreateDir(string.Format("./{0}", rootDir), false);
            string[] strs = Selection.assetGUIDs;
            string path = AssetDatabase.GUIDToAssetPath(strs[0]);
            //Debug.LogError(path);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            string fileName = System.IO.Path.GetFileName(path);
            string destDir = System.IO.Path.Combine(string.Format("./{0}", rootDir), fileNameWithoutExtension);
            CreateDir(destDir, true);
            var deps = AssetDatabase.GetDependencies(path, true);
            for (int i = 0; i < deps.Length; i++)
            {
                string sourcePath = deps[i].Replace("Assets", "");
                sourcePath = Application.dataPath + sourcePath;
                sourcePath = sourcePath.Replace("\\", "/");
                Debug.LogError("[sour] " + sourcePath);
                string targetPath = Application.dataPath;
                targetPath = targetPath.Replace("Assets", "");
                targetPath = targetPath.Replace("\\", "/");
                //资源本身copy到自身命名目录,依赖的copy到根目录
                if (sourcePath.EndsWith(path))
                {
                    targetPath += rootDir + "/" + fileNameWithoutExtension + "/" + System.IO.Path.GetFileName(deps[i]);
                }
                else
                {
                    targetPath += rootDir + "/" + System.IO.Path.GetFileName(deps[i]);
                }
                Debug.LogError("[dest] " + targetPath);
                System.IO.File.Copy(sourcePath, targetPath, true);
                System.IO.File.Copy(sourcePath + ".meta", targetPath + ".meta", true);

            }
        }

        [MenuItem("Assets/UFrame/ResourceTool/Analyse")]
        static void Analyse()
        {

        }

        [MenuItem("Assets/UFrame/ResourceTool/ProjectPrefabsRemoveMissingScript")]
        public static void ProjectPrefabsRemoveMissingScript()
        {
            string[] filePaths = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories);
            int sum = 0;
            for (int i = 0; i < filePaths.Length; i++)
            {
                string path = filePaths[i].Replace(Application.dataPath, "Assets");
                GameObject objPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject obj = PrefabUtility.InstantiatePrefab(objPrefab) as GameObject;
                //判断是否存在于Hierarchy面板上
                if (obj.hideFlags == HideFlags.None)
                {
                    var components = obj.GetComponents<Component>();
                    SerializedObject so = new SerializedObject(obj);
                    var soProperties = so.FindProperty("m_Component");
                    int r = 0;
                    for (int j = 0; j < components.Length; j++)
                    {
                        if (components[j] == null)
                        {
                            soProperties.DeleteArrayElementAtIndex(j - r);
                            Debug.LogError("清除了物体：" + obj.name + " 的一个missing脚本");
                            r++;
                        }
                    }
                    if (r > 0)
                    {
                        so.ApplyModifiedProperties();
                        PrefabUtility.SaveAsPrefabAssetAndConnect(obj, path, InteractionMode.AutomatedAction);
                        AssetDatabase.Refresh();
                    }
                    sum += r;
                    UnityEngine.Object.DestroyImmediate(obj);
                }

            }
            Debug.LogError("清除完成,清理个数：" + sum);
        }

        [MenuItem("Assets/UFrame/ResourceTool/SelectedPrefabRemoveMissingScript")]
        public static void SelectedPrefabRemoveMissingScript()
        {
            string[] selected = Selection.assetGUIDs;
            string[] filePaths = new string[selected.Length];
            for(int i = 0; i < selected.Length; i++)
            {
                filePaths[i] = AssetDatabase.GUIDToAssetPath(selected[0]);
            }
            int sum = 0;
            for (int i = 0; i < filePaths.Length; i++)
            {
                string path = filePaths[i].Replace(Application.dataPath, "Assets");
                GameObject objPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject obj = PrefabUtility.InstantiatePrefab(objPrefab) as GameObject;
                //判断是否存在于Hierarchy面板上
                if (obj.hideFlags == HideFlags.None)
                {
                    var components = obj.GetComponents<Component>();
                    SerializedObject so = new SerializedObject(obj);
                    var soProperties = so.FindProperty("m_Component");
                    int r = 0;
                    for (int j = 0; j < components.Length; j++)
                    {
                        if (components[j] == null)
                        {
                            soProperties.DeleteArrayElementAtIndex(j - r);
                            Debug.LogError("清除了物体：" + obj.name + " 的一个missing脚本");
                            r++;
                        }
                    }
                    if (r > 0)
                    {
                        so.ApplyModifiedProperties();
                        PrefabUtility.SaveAsPrefabAssetAndConnect(obj, path, InteractionMode.AutomatedAction);
                        AssetDatabase.Refresh();
                    }
                    sum += r;
                    UnityEngine.Object.DestroyImmediate(obj);
                }

            }
            Debug.LogError("清除完成,清理个数：" + sum);
        }

        /// <summary>
        /// 递归创建目录
        /// </summary>
        /// <param name="str">String.</param>
        public static void CreateDir(string str, bool clean = false)
        {
            if (clean)
            {
                DeleteDir(str);
            }

            DirectoryInfo dir = new DirectoryInfo(str);
            if (!dir.Parent.Exists)
            {
                CreateDir(dir.Parent.FullName);
            }
            if (!dir.Exists)
                dir.Create();
        }

        /// <summary>
        /// 递归删除目录
        /// </summary>
        /// <param name="str">String.</param>
        public static void DeleteDir(string str)
        {
            DirectoryInfo dir = new DirectoryInfo(str);
            if (!dir.Exists)
            {
                return;
            }

            foreach (var file in dir.GetFiles())
            {
                if (file.FullName.EndsWith(".meta", StringComparison.Ordinal))
                {
                    continue;
                }
                file.Delete();
            }

            foreach (var d in dir.GetDirectories())
            {
                DeleteDir(d.FullName);
            }

            dir.Delete(true);
        }

    }

}

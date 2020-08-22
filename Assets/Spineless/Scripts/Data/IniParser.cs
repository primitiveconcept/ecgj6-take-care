/*	
	.Ini filePath Parser
	Author: Tristan 'Kennyist' Cunningham - www.tristanjc.com
	Date: 13/04/2014
	License: Creative commons ShareAlike 3.0 - https://creativecommons.org/licenses/by-sa/3.0/
    Minor modifications by Zachary A. White (http://www.primitiveconcept.com)
*/

namespace JsonFlow
{
    using UnityEngine;
    using System.Collections;
    using System.IO;


    /// <summary>
    /// An .ini filePath parser that Creates and edits .ini files, With functions to fetch and delete values.
    /// </summary>
    internal class IniParser
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IniParser"/> class without loading a filePath.
        /// </summary>
        public IniParser()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="IniParser"/> class with loading a filePath.
        /// </summary>
        /// <param name="file">Name of the file you want to Load.</param>
        public IniParser(string filePath)
        {
            Load(filePath);
        }
        #endregion


        /// <summary>
        /// How many keys are stored.
        /// </summary>
        public int Count()
        {
            return this.keys.Count;
        }


        /// <summary>
        /// Returns true if the file exists, or false if it doesnt.
        /// </summary>
        /// <param name="file">The selected file.</param>
        public bool DoesExist(string filePath)
        {
            return File.Exists(filePath) ? true : false;
        }


        /// <summary>
        /// Returns the value for the input variable.
        /// </summary>
        /// <param name="key">The variable name.</param>
        public string Get(string key)
        {
            for (int i = 0; i < this.keys.Count; i++)
            {
                if (this.keys[i].Equals(key))
                {
                    return this.vals[i].ToString();
                }
            }

            return null;
        }


        /// <summary>
        /// Returns the Key, Value and comment of the choosen variable.
        /// </summary>
        /// <returns>String array containing the 3 values</returns>
        /// <param name="key">The variable name.</param>
        public string[] GetLine(string key)
        {
            string[] list = new string[2];

            for (int i = 0; i < this.keys.Count; i++)
            {
                if (this.keys[i].Equals(key))
                {
                    list[0] = this.keys[i].ToString();
                    list[1] = this.vals[i].ToString();
                    list[2] = this.comments[i].ToString();
                    return list;
                }
            }

            return list;
        }


        /// <summary>
        /// Load the specified filePath.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Load(string filePath)
        {
            this.keys = new ArrayList();
            this.vals = new ArrayList();
            this.comments = new ArrayList();

            string line = "", dir = filePath;
            int offset = 0, comment = 0;

            try
            {
                using (StreamReader sr = new StreamReader(dir))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        offset = line.IndexOf("=");
                        comment = line.IndexOf("//");
                        if (offset > 0)
                        {
                            if (comment != -1)
                            {
                                Set(
                                    line.Substring(0, offset),
                                    line.Substring(offset + 1, (comment - (offset + 1))),
                                    line.Substring(comment + 1));
                            }
                            else
                            {
                                Set(line.Substring(0, offset), line.Substring(offset + 1));
                            }
                        }
                    }

                    sr.Close();
                }
            }
            catch (IOException e)
            {
                Debug.Log("Error opening " + filePath);
                Debug.LogWarning(e);
            }
        }


        /// <summary>
        /// Removes the selected Variable including its value and comment.
        /// </summary>
        /// <param name="key">The variable name.</param>
        public void Remove(string key)
        {
            for (int i = 0; i < this.keys.Count; i++)
            {
                if (this.keys[i].Equals(key))
                {
                    this.keys.RemoveAt(i);
                    this.vals.RemoveAt(i);
                    this.comments.RemoveAt(i);
                    return;
                }
            }

            Debug.LogWarning("Key not found");
        }


        /// <summary>
        /// Save the specified filePath.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Save(string filePath)
        {
            //File.Delete(filePath);
            StreamWriter wr = new StreamWriter(filePath);

            for (int i = 0; i < this.keys.Count; i++)
            {
                if (this.comments[i].Equals(""))
                {
                    wr.WriteLine(this.keys[i] + "=" + this.vals[i]);
                }
                else
                {
                    wr.WriteLine(this.keys[i] + "=" + this.vals[i] + " //" + this.comments[i]);
                }
            }

            wr.Close();
        }


        /// <summary>
        /// Set the variable and value if they dont exist. Updates the variables value if does exist.
        /// </summary>
        /// <param name="key">The variable name</param>
        /// <param name="val">The value of the variable</param>
        public void Set(string key, string val)
        {
            for (int i = 0; i < this.keys.Count; i++)
            {
                if (this.keys[i].Equals(key))
                {
                    this.vals[i] = val;
                    return;
                }
            }

            this.keys.Add(key);
            this.vals.Add(val);
            this.comments.Add("");
        }


        /// <summary>
        /// Set the variable and value if they dont exist including a comment. Updates the variables value and comment if does exist.
        /// </summary>
        /// <param name="key">The variable name</param>
        /// <param name="val">The value of the variable</param>
        /// <param name="comment">The comment of the variable</param>
        public void Set(string key, string val, string comment)
        {
            for (int i = 0; i < this.keys.Count; i++)
            {
                if (this.keys[i].Equals(key))
                {
                    this.vals[i] = val;
                    this.comments[i] = comment;
                    return;
                }
            }

            this.keys.Add(key);
            this.vals.Add(val);
            this.comments.Add(comment);
        }


        #region Fields
        private ArrayList keys = new ArrayList();
        private ArrayList vals = new ArrayList();
        private ArrayList comments = new ArrayList();
        #endregion
    }
}